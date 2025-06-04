using Microsoft.AspNetCore.Mvc.RazorPages;
using lib_dominio.Entidades;
using lib_presentaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using lib_dominio.Nucleo;

namespace asp_presentacion.Pages.Vistas
{
    public class CustomersVehiclesModel : PageModel
    {
        private readonly Comunicaciones _comunicaciones;

        public List<Customers> ListaClientes { get; set; } = new();
        public List<Vehicles> ListaVehiculos { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string Filtro { get; set; } = "";
        [BindProperty(SupportsGet = true)]
        public string Tipo { get; set; } = "clientes";

        public CustomersVehiclesModel(Comunicaciones comunicaciones)
        {
            _comunicaciones = comunicaciones;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Tipo == "vehiculos")
            {
                var datos = _comunicaciones.ConstruirUrl(new(), "Vehicles/Listar");
                var respuesta = await _comunicaciones.Ejecutar(datos);

                if (respuesta.ContainsKey("Error"))
                {
                    ModelState.AddModelError(string.Empty, respuesta["Error"].ToString()!);
                    return Page();
                }

                if (respuesta.ContainsKey("Entidades"))
                {
                    ListaVehiculos = JsonConversor.ConvertirAObjeto<List<Vehicles>>(respuesta["Entidades"].ToString() ?? "[]");
                    if (!string.IsNullOrWhiteSpace(Filtro))
                    {
                        ListaVehiculos = ListaVehiculos
                             .Where(v => v.Plate?.ToLower().Contains(Filtro.ToLower()) == true)
                             .ToList();
                    }
                }
                return Page();
            }
            else
            {
                var datos = _comunicaciones.ConstruirUrl(new(), "Customers/Listar");
                var respuesta = await _comunicaciones.Ejecutar(datos);
                if (respuesta.ContainsKey("Error"))
                {
                    ModelState.AddModelError(string.Empty, respuesta["Error"].ToString()!);
                    return Page();
                }
                if (respuesta.ContainsKey("Entidades"))
                {
                    ListaClientes = JsonConversor.ConvertirAObjeto<List<Customers>>(respuesta["Entidades"].ToString() ?? "[]");
                    if (!string.IsNullOrWhiteSpace(Filtro))
                    {
                        ListaClientes = ListaClientes
                             .Where(c => c.CustomerName?.ToLower().Contains(Filtro.ToLower()) == true)
                             .ToList();
                    }
                }

                return Page();
            }
        }
    }
}
