using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Vistas
{
    public class TakeOrderModel : PageModel
    {
        [BindProperty]
        public string SelectedCustomerName { get; set; } = string.Empty;
        //public string FoundCustomerName { get; set; } = string.Empty;
        public string FeedbackMessage { get; set; } = string.Empty;
        private ICustomersPresentacion _customersPresentacion;

        public TakeOrderModel(ICustomersPresentacion customersPresentacion)
        {
            this._customersPresentacion = customersPresentacion;
        }
        public void OnGet()
        {
        }

        //public async Task<IActionResult> OnPostSearchAsync()
        //{
        //    FoundCustomerName = string.Empty;
        //    FeedbackMessage = string.Empty;

        //    if (string.IsNullOrWhiteSpace(SearchTerm))
        //    {
        //        FeedbackMessage = "Por favor, ingrese un nombre para buscar.";
        //        return Page();
        //    }

        //    var searchCustomerEntity = new Customers { CustomerName = SearchTerm };

        //    var customersFound = await _customersPresentacion.PorNombre(searchCustomerEntity);



        //}
        public async Task<IActionResult> OnGetCustomersSuggestions(string searchTerm)
        {
            // Si el término de búsqueda está vacío o es muy corto, devolvemos una lista vacía
            // Puedes ajustar el largo mínimo (ej. 2 caracteres) para evitar búsquedas muy amplias
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
            {
                return new JsonResult(new List<Customers>());
            }

            try
            {
                // Creamos una entidad Customers con el término de búsqueda
                var searchCustomerEntity = new Customers { CustomerName = searchTerm };

                // Llamamos a tu capa de presentación para buscar clientes por nombre
                var customersFound = await _customersPresentacion.PorNombre(searchCustomerEntity);

                // Devolvemos la lista de clientes encontrados como un JSON
                return new JsonResult(customersFound ?? new List<Customers>());
            }
            catch (Exception ex)
            {
                // En caso de error, devolvemos un estado 500 y un objeto JSON con el error.
                // Es importante que el JavaScript en el frontend pueda manejar esto.
                return StatusCode(500, new { Error = ex.Message, Message = "Error al obtener sugerencias de clientes." });
            }
        }

        // --- Método para procesar la selección final del cliente (POST) ---
        // Este handler se ejecutará cuando el usuario haga clic en el botón "Seleccionar Cliente"
        public async Task<IActionResult> OnPostSelectCustomer()
        {
            FeedbackMessage = string.Empty; // Limpiamos mensajes anteriores

            if (string.IsNullOrWhiteSpace(SelectedCustomerName))
            {
                FeedbackMessage = "Por favor, seleccione o ingrese un nombre de cliente.";
                return Page();
            }

            try
            {
                var searchCustomerEntity = new Customers { CustomerName = SelectedCustomerName };
                var customersFound = await _customersPresentacion.PorNombre(searchCustomerEntity);

                // Intentamos encontrar una coincidencia exacta con lo que el usuario seleccionó o escribió
                var customer = customersFound?.FirstOrDefault(c => c.CustomerName.Equals(SelectedCustomerName, StringComparison.OrdinalIgnoreCase));

                if (customer != null)
                {
                    FeedbackMessage = $"Cliente '{customer.CustomerName}' seleccionado. ID: {customer.Id}.";
                    // Aquí puedes realizar acciones adicionales, como guardar el ID del cliente en la sesión
                    // o redirigir a otra página con el cliente seleccionado.
                    // Ejemplo: TempData["SelectedCustomerId"] = customer.Id;
                }
                else
                {
                    // Esto ocurre si el usuario escribió algo que no está en la lista exacta,
                    // o si la lista de sugerencias no incluyó el resultado exacto (si PorNombre busca "contiene").
                    FeedbackMessage = $"No se encontró un cliente exacto con el nombre '{SelectedCustomerName}'. Por favor, elija de la lista o refine su búsqueda.";
                }
            }
            catch (Exception ex)
            {
                FeedbackMessage = $"Ocurrió un error al procesar la selección: {ex.Message}";
            }

            return Page(); // Volvemos a la misma página con el mensaje actualizado
        }
    }
}

