using lib_dominio.Nucleo;
using lib_presentaciones; // Necesario para la clase Comunicaciones
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Necesario para HttpContext.Session

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        public bool EstaLogueado { get; set; } = false;

        // Se inyecta la clase Comunicaciones a través del constructor
        private readonly Comunicaciones _comunicaciones;

        [BindProperty] public string? UserName { get; set; }
        [BindProperty] public string? Password { get; set; }

        // Para mostrar mensajes de error o éxito en la vista
        [TempData] // TempData es útil para mensajes que persisten tras una redirección
        public string? Message { get; set; }

        // Constructor para la inyección de dependencias
        public IndexModel(Comunicaciones comunicaciones)
        {
            _comunicaciones = comunicaciones; // Asigna la instancia inyectada
        }

        public IActionResult OnGet()
        {
            // Verifica si el usuario ya está logueado en la sesión
            var userNameEnSesion = HttpContext.Session.GetString("UserName");
            // También podrías verificar el token directamente si lo guardas con una clave conocida
            // var tokenEnSesion = HttpContext.Session.GetString("_AuthToken");

            if (!String.IsNullOrEmpty(userNameEnSesion))
            {
                EstaLogueado = true;
                // Si ya está logueado, podrías redirigirlo directamente a la página principal
                // return RedirectToPage("/Vistas/Home");
            }
            // Si no está logueado, simplemente muestra la página de login (comportamiento por defecto)
            return Page();
        }

        // Este handler no parece usarse en tu .cshtml, pero si lo necesitas, aquí está.
        public void OnPostBtClean()
        {
            UserName = string.Empty;
            Password = string.Empty;
            // Message = string.Empty; // Limpiar mensajes si es necesario
        }

        public async Task<IActionResult> OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    Message = "Por favor, ingresa tu usuario y contraseña.";
                    return Page();
                }

                var datosInput = new Dictionary<string, object>
                {
                    { "UserName", UserName! },
                    { "Password", Password! }
                };

                // Usa la instancia _comunicaciones inyectada.
                // Esta instancia ahora puede usar la sesión HTTP gracias a IHttpContextAccessor.
                Dictionary<string, object> respuestaAutenticacion = await _comunicaciones.Autenticar(datosInput);

                if (respuestaAutenticacion.ContainsKey("Error"))
                {
                    Message = respuestaAutenticacion["Error"].ToString();
                    return Page();
                }
                else if (respuestaAutenticacion.ContainsKey("Token"))
                {
                    // Autenticación exitosa.
                    // La clase Comunicaciones (versión corregida) ya guarda el token en la sesión.
                    // Guardamos el UserName en sesión para la lógica de EstaLogueado y para mostrarlo.
                    HttpContext.Session.SetString("UserName", UserName!);

                    EstaLogueado = true;
                    Message = "¡Inicio de sesión exitoso!"; // Mensaje de éxito opcional
                    return RedirectToPage("/Vistas/Home"); // Redirige a la página principal
                }
                else
                {
                    Message = "Respuesta inesperada del servidor de autenticación.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Considera un logging más robusto aquí en un entorno de producción
                Console.WriteLine($"EXCEPCION en OnPostBtEnter: {ex}");
                Message = "Ha ocurrido un error inesperado durante el inicio de sesión. Por favor, inténtalo de nuevo.";
                return Page();
            }
        }

        public IActionResult OnPostBtClose() // Para cerrar sesión
        {
            // La clase Comunicaciones podría tener un método para invalidar/limpiar el token si es necesario
            // _comunicaciones.CerrarSesion(); 

            HttpContext.Session.Clear(); // Limpia todas las claves de la sesión
            EstaLogueado = false;
            Message = "Has cerrado sesión.";
            return RedirectToPage("/Index"); // Redirige a la página de login
        }
    }
}
