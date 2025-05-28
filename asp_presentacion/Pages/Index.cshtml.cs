using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        public bool EstaLogueado = false;
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contrasena { get; set; }

        

        public void OnGet()
        {
            var variable_session = HttpContext.Session.GetString("Usuario");
            if (!String.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true;
                return;
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                Email = string.Empty;
                Contrasena = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IActionResult OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) &&
                    string.IsNullOrEmpty(Contrasena))
                {
                    OnPostBtClean();
                    return Page();
                }

                if ("admin.admin" != Email + "." + Contrasena)
                {
                    OnPostBtClean();
                    return Page();
                }
                ViewData["Logged"] = true;
                HttpContext.Session.SetString("Usuario", Email!);
                //EstaLogueado = true;

                OnPostBtClean();
                return RedirectToPage("Vistas/Home");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }

        public IActionResult OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Index");

            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }
    }
}