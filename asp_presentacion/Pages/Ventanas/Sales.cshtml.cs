using lib_dominio.Entidades;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace tu_proyecto.Pages
{
    public class SalesModel : PageModel
    {
        private readonly ISalesPresentacion _presentacion;

        public SalesModel(ISalesPresentacion presentacion)
        {
            _presentacion = presentacion;
        }

        [BindProperty]
        public Sales? Filtro { get; set; }

        [BindProperty]
        public List<Sales>? Lista { get; set; }

        public async Task OnGetAsync()
        {
            Lista = await _presentacion.Listar();
        }

        public async Task<IActionResult> OnPostBuscarAsync()
        {
            if (string.IsNullOrWhiteSpace(Filtro?.SaleRef))
            {
                Lista = await _presentacion.Listar();
            }
            else
            {
                Lista = await _presentacion.PorReferencia(Filtro);
            }

            return Page();
        }
    }
}
