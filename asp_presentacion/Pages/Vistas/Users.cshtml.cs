using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_presentacion.Pages.Vistas
{
    public class UsersModel : PageModel
    {
        public List<Users> ListaUsuarios { get; set; } = new();
        [BindProperty]
        public Users NewUser { get; set; } = new();
        public SelectList? RolesList { get; set; } 
        private readonly IUsersPresentacion _usersPresentacion;
        

        private readonly Comunicaciones _comunicaciones;
        public UsersModel(Comunicaciones? comunicaciones, IUsersPresentacion usersPresentacion)
        {
            _comunicaciones = comunicaciones!;
            _usersPresentacion = usersPresentacion;

        }
        public async Task OnGetAsync()
        {
            var datos = _comunicaciones.ConstruirUrl(new(), "Users/Listar");
            var respuesta = await _comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                ModelState.AddModelError(string.Empty, respuesta["Error"].ToString()!);
            }

            if (respuesta.ContainsKey("Entidades"))
            {
                ListaUsuarios = JsonConversor.ConvertirAObjeto<List<Users>>(respuesta["Entidades"].ToString() ?? "[]");

            }
            var availableRoles = await GetAvailableRolesAsync();
            RolesList = new SelectList(availableRoles, "Id", "RoleName");
        }

        private async Task<List<Roles>> GetAvailableRolesAsync()
        {
          
            await Task.Delay(10);

            return new List<Roles>
            {
                new Roles { Id = 1, RoleName = "Admin" },    
                new Roles { Id = 2, RoleName = "Mecanico" }  
            };
        }
        public async Task<IActionResult> OnPostAddUserAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Recargar datos necesarios para la vista (roles, lista de usuarios)
                return Page();
            }

            try
            {
                // Aquí la propiedad NewUser.Password tiene el valor en texto plano.
                // Tu método iAplicacion.CrearUsuario (llamado a través de _usersPresentacion.CrearUsuario)
                // debería encargarse de hashear la contraseña antes de guardarla.
                var usuarioCreado = await _usersPresentacion.CrearUsuario(NewUser);

                if (usuarioCreado != null)
                {
                    TempData["SuccessMessage"] = "Usuario creado exitosamente!";
                    return RedirectToPage(); // Redirige a OnGetAsync, que recargará la lista
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo crear el usuario.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al crear usuario: {ex.Message}");
            }

            await OnGetAsync(); // Recargar datos en caso de error para mostrar el formulario nuevamente
            return Page();
        }
    }
}
