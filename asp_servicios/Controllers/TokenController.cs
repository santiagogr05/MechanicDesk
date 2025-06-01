using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using lib_dominio.Nucleo;
using lib_aplicaciones.Interfaces;
using asp_servicios.Nucleo;

namespace asp_servicios.Controllers
{
    public class TokenController : ControllerBase
    {
        private readonly IUsersAplicacion iUsersAplicacion;
        public TokenController(IUsersAplicacion iusersAplicacion)
        {
            this.iUsersAplicacion = iusersAplicacion;
        }
        private Dictionary<string, object> ObtenerDatos()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = new StreamReader(Request.Body).ReadToEnd().ToString();
                if (string.IsNullOrEmpty(datos))
                    datos = "{}";
                return JsonConversor.ConvertirAObjeto(datos);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return respuesta;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Token/Autenticar")]
        public string Autenticar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!datos.ContainsKey("UserName") ||
                    !datos.ContainsKey("Password"))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                string userName = datos["UserName"].ToString()!;
                string password = datos["Password"].ToString()!;

                this.iUsersAplicacion.Configurar(Configuracion.ObtenerValor("StringConexion")!);

                if (!this.iUsersAplicacion.ValidarCredenciales(userName, password))
                {
                    respuesta["Error"] = "lbCredencialesInvalidas";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var user = this.iUsersAplicacion.ObtenerPorUserName(userName);
                if (user == null)
                {
                    respuesta["Error"] = "lbUsuarioNoEncontrado";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                string roleName = user._Roles?.RoleName ?? "Mecanico";


                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,roleName)
                }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DatosGenerales.clave)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                respuesta["Token"] = tokenHandler.WriteToken(token);
                respuesta["User"] = new { user.Id, user.UserName, Role = roleName };
                respuesta["Respuesta"] = "OK";
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        public bool Validate(Dictionary<string, object> data)
        {
            try
            {
                var authorizationHeader = data["Bearer"].ToString();
                authorizationHeader = authorizationHeader!.Replace("Bearer ", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadToken(authorizationHeader) as JwtSecurityToken;
               
                if (DateTime.UtcNow > token.ValidTo)
                    return false;
                return true;

                
            }
            catch
            {
                return false;
            }
        }
        public bool ValidateWithRole(Dictionary<string, object> data, string requiredRole)
        {
            try
            {
                if (!data.ContainsKey("Bearer"))
                    return false;

                var authorizationHeader = data["Bearer"].ToString();
                authorizationHeader = authorizationHeader!.Replace("Bearer ", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadToken(authorizationHeader) as JwtSecurityToken;

                if (token == null || DateTime.UtcNow > token.ValidTo)
                    return false;

                // Check if the user has the required role
                var roleClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                if (roleClaim == null || roleClaim.Value != requiredRole)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}