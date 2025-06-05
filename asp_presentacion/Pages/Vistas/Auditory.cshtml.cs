using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Vistas
{
    public class AuditoryModel : PageModel
    {
        public List<Auditoria> ListaAuditoria { get; set; } = new();
        private readonly Comunicaciones _comunicaciones;
        public AuditoryModel(Comunicaciones? comunicaciones)
        {
            _comunicaciones = comunicaciones!;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var datos = _comunicaciones.ConstruirUrl(new(), "Auditoria/Listar");
            var respuesta = await _comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                ModelState.AddModelError(string.Empty, respuesta["Error"].ToString()!);
                return Page();
            }

            if (respuesta.ContainsKey("Entidades"))
            {
                ListaAuditoria = JsonConversor.ConvertirAObjeto<List<Auditoria>>(respuesta["Entidades"].ToString() ?? "[]");
                    
            }
            return Page();
        }
    }
}
