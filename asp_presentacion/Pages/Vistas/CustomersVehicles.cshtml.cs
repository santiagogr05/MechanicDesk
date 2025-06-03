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

        public CustomersVehiclesModel(Comunicaciones comunicaciones)
        {
            _comunicaciones = comunicaciones;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Preparar los datos necesarios
            var datos = new Dictionary<string, object>();

            // Construir la URL para el endpoint (asumiendo que es "Clientes/Listar")
            datos = _comunicaciones.ConstruirUrl(datos, "Customers/Listar");

            // Ejecutar la llamada a la API
            var respuesta = await _comunicaciones.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                // Maneja el error según tu lógica (podrías redirigir, mostrar un mensaje, etc.)
                ModelState.AddModelError(string.Empty, respuesta["Error"].ToString()!);
                return Page();
            }


            if (respuesta.ContainsKey("Entidades"))
            {
                var lista = respuesta["Entidades"];
                ListaClientes = JsonConversor.ConvertirAObjeto<List<Customers>>(lista?.ToString() ?? "[]");
            }


            return Page();
        }
    }
}
