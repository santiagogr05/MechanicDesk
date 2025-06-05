using lib_dominio.Entidades;
using lib_presentaciones.Interfaces;
using lib_presentaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc.Rendering;
using lib_presentaciones.Implementaciones;

namespace asp_presentacion.Pages.Vistas
{
    public class ServicesModel : PageModel
    {
        public List<Services> ListaServicios { get; set; } = new();
        [BindProperty]
        public Services NewService { get; set; } = new();
        private readonly IServicesPresentacion _servicesPresentacion;


        private readonly Comunicaciones _comunicaciones;
        public ServicesModel(Comunicaciones? comunicaciones, IServicesPresentacion servicesPresentacion)
        {
            _comunicaciones = comunicaciones!;
            _servicesPresentacion = servicesPresentacion;
        }
        public async Task OnGetAsync()
        {
            var datos = _comunicaciones.ConstruirUrl(new(), "Services/Listar");
            var respuesta = await _comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                ModelState.AddModelError(string.Empty, respuesta["Error"].ToString()!);
            }

            if (respuesta.ContainsKey("Entidades"))
            {
                ListaServicios = JsonConversor.ConvertirAObjeto<List<Services>>(respuesta["Entidades"].ToString() ?? "[]");

            }
            
        }

        public async Task<IActionResult> OnPostAddUserAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); 
                return Page();
            }

            try
            {
                var servicioCreado = await _servicesPresentacion.Guardar(NewService);

                if (servicioCreado != null)
                {
                    TempData["SuccessMessage"] = "Servicio creado exitosamente!";
                    return RedirectToPage(); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo crear el servicio.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al crear Servicio: {ex.Message}");
            }

            await OnGetAsync(); 
            return Page();
        }
    }
}
