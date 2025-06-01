using asp_servicios.Nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriaController : ControllerBase
    {
        private IAuditoriaAplicacion? iAplicacion = null; 

        public AuditoriaController(IAuditoriaAplicacion? iAplicacion,
            TokenController tokenController)
        {
            this.iAplicacion = iAplicacion; 
        }

        private Dictionary<string, object> ObtenerDatos()
        {
            var datos = new StreamReader(Request.Body).ReadToEnd().ToString();
            if (string.IsNullOrEmpty(datos))
                datos = "{}";
            return JsonConversor.ConvertirAObjeto(datos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public string Listar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion")!);
                respuesta["Entidades"] = this.iAplicacion!.Listar();

                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }
    }
}