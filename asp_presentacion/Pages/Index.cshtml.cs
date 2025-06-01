using lib_dominio.Nucleo;
using lib_presentaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        // Esta es una propiedad pública ahora, para un mejor uso en ASP.NET Core
        public bool EstaLogueado { get; set; } = false; // Inicializa en false

        // La instancia de Comunicaciones se mantiene
        Comunicaciones com = new Comunicaciones();

        [BindProperty] public string? UserName { get; set; }
        [BindProperty] public string? Password { get; set; }

        // Nueva propiedad para mostrar mensajes de error al usuario
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            // Verifica si el usuario ya está logueado en la sesión
            var variable_session = HttpContext.Session.GetString("UserName");
            if (!String.IsNullOrEmpty(variable_session))
            {
                // Si hay un UserName en sesión, se considera logueado
                EstaLogueado = true;
                // Opcional: podrías redirigir directamente si ya está logueado y no quieres mostrar el formulario
                // return RedirectToPage("Vistas/Home");
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                // Limpia los campos de usuario y contraseña
                UserName = string.Empty;
                Password = string.Empty;
                // No limpies ErrorMessage aquí si quieres que persista después de un intento fallido
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public async Task<IActionResult> OnPostBtEnter() // Hazlo async para poder usar await
        {
            try
            {
                // 1. Validaciones básicas en el servidor
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    ErrorMessage = "Por favor, ingresa tu usuario y contraseña.";
                    OnPostBtClean(); // Limpia los campos
                    return Page(); // Vuelve a la página actual con el mensaje de error
                }

                // 2. Prepara los datos para enviar a la capa de comunicaciones
                var datosInput = new Dictionary<string, object>
                {
                    ["UserName"] = UserName!, // Usa el UserName enlazado
                    ["Password"] = Password!  // Usa el Password enlazado
                };

                // 3. Llama al método Autenticar de la clase Comunicaciones
                // No construyas la URL aquí, asume que Comunicaciones ya sabe su URL de autenticación
                // datosInput["UrlToken"] = "Token/Autenticar"; // Esto ya lo maneja Comunicaciones internamente

                // Espera la respuesta de autenticación
                // Asegúrate de que Autenticar de Comunicaciones devuelva un Dictionary<string, object> con 'Error' o 'Token'
                Dictionary<string, object> respuestaAutenticacion = await com.Autenticar(datosInput);

                // 4. Evalúa la respuesta de la autenticación
                if (respuestaAutenticacion.ContainsKey("Error"))
                {
                    // Si la API devuelve un error (ej. credenciales inválidas)
                    ErrorMessage = respuestaAutenticacion["Error"].ToString();
                    OnPostBtClean(); // Limpia los campos
                    return Page(); // Vuelve a la página con el error
                }
                else if (respuestaAutenticacion.ContainsKey("Token"))
                {
                    // Autenticación exitosa
                    // Guarda el nombre de usuario en la sesión
                    HttpContext.Session.SetString("UserName", UserName!);
                    // Puedes guardar el token también si lo necesitas para futuras llamadas desde el cliente o si tu PageModel necesita hacer llamadas autenticadas
                    // HttpContext.Session.SetString("AuthToken", respuestaAutenticacion["Token"].ToString()!);

                    EstaLogueado = true; // Actualiza el estado de login en el modelo
                    OnPostBtClean(); // Limpia los campos del formulario
                    return RedirectToPage("/Vistas/Home"); // Redirige al home
                }
                else
                {
                    // Caso de respuesta inesperada de la API (no hay Token ni Error)
                    ErrorMessage = "La respuesta del servidor no fue la esperada. Contacta a soporte.";
                    OnPostBtClean();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Captura cualquier error inesperado en la lógica del PageModel o la comunicación
                LogConversor.Log(ex, ViewData!);
                ErrorMessage = "Ha ocurrido un error inesperado. Por favor, inténtalo de nuevo.";
                OnPostBtClean();
                return Page();
            }
        }

        public IActionResult OnPostBtClose()
        {
            try
            {
                // Limpia toda la sesión
                HttpContext.Session.Clear();
                EstaLogueado = false; // Actualiza el estado de login
                return RedirectToPage("/Index"); // Redirige a la página de login
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }
    }
}