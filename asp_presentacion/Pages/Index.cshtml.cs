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

        // Se inyecta la clase Comunicaciones a trav�s del constructor
        private readonly Comunicaciones _comunicaciones;

        [BindProperty] public string? UserName { get; set; }
        [BindProperty] public string? Password { get; set; }

        // Para mostrar mensajes de error o �xito en la vista
        [TempData] // TempData es �til para mensajes que persisten tras una redirecci�n
        public string? Message { get; set; }

        // Constructor para la inyecci�n de dependencias
        public IndexModel(Comunicaciones comunicaciones)
        {
            _comunicaciones = comunicaciones; // Asigna la instancia inyectada
        }

        public IActionResult OnGet()
        {
            // Verifica si el usuario ya est� logueado en la sesi�n
            var userNameEnSesion = HttpContext.Session.GetString("UserName");
            // Tambi�n podr�as verificar el token directamente si lo guardas con una clave conocida
            // var tokenEnSesion = HttpContext.Session.GetString("_AuthToken");

            if (!String.IsNullOrEmpty(userNameEnSesion))
            {
                EstaLogueado = true;
                // Si ya est� logueado, podr�as redirigirlo directamente a la p�gina principal
                // return RedirectToPage("/Vistas/Home");
            }
            // Si no est� logueado, simplemente muestra la p�gina de login (comportamiento por defecto)
            return Page();
        }

        // Este handler no parece usarse en tu .cshtml, pero si lo necesitas, aqu� est�.
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
                    Message = "Por favor, ingresa tu usuario y contrase�a.";
                    return Page();
                }

                var datosInput = new Dictionary<string, object>
                {
                    { "UserName", UserName! },
                    { "Password", Password! }
                };

                // Usa la instancia _comunicaciones inyectada.
                // Esta instancia ahora puede usar la sesi�n HTTP gracias a IHttpContextAccessor.
                Dictionary<string, object> respuestaAutenticacion = await _comunicaciones.Autenticar(datosInput);

                if (respuestaAutenticacion.ContainsKey("Error"))
                {
                    Message = respuestaAutenticacion["Error"].ToString();
                    return Page();
                }
                else if (respuestaAutenticacion.ContainsKey("Token"))
                {
                    // Autenticaci�n exitosa.
                    // La clase Comunicaciones (versi�n corregida) ya guarda el token en la sesi�n.
                    // Guardamos el UserName en sesi�n para la l�gica de EstaLogueado y para mostrarlo.
                    HttpContext.Session.SetString("UserName", UserName!);

                    EstaLogueado = true;
                    Message = "�Inicio de sesi�n exitoso!"; // Mensaje de �xito opcional
                    return RedirectToPage("/Vistas/Home"); // Redirige a la p�gina principal
                }
                else
                {
                    Message = "Respuesta inesperada del servidor de autenticaci�n.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Considera un logging m�s robusto aqu� en un entorno de producci�n
                Console.WriteLine($"EXCEPCION en OnPostBtEnter: {ex}");
                Message = "Ha ocurrido un error inesperado durante el inicio de sesi�n. Por favor, int�ntalo de nuevo.";
                return Page();
            }
        }

        public IActionResult OnPostBtClose() // Para cerrar sesi�n
        {
            // La clase Comunicaciones podr�a tener un m�todo para invalidar/limpiar el token si es necesario
            // _comunicaciones.CerrarSesion(); 

            HttpContext.Session.Clear(); // Limpia todas las claves de la sesi�n
            EstaLogueado = false;
            Message = "Has cerrado sesi�n.";
            return RedirectToPage("/Index"); // Redirige a la p�gina de login
        }
    }
}
