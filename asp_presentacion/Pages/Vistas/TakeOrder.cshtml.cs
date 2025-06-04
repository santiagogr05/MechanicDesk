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
            // Si el t�rmino de b�squeda est� vac�o o es muy corto, devolvemos una lista vac�a
            // Puedes ajustar el largo m�nimo (ej. 2 caracteres) para evitar b�squedas muy amplias
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
            {
                return new JsonResult(new List<Customers>());
            }

            try
            {
                // Creamos una entidad Customers con el t�rmino de b�squeda
                var searchCustomerEntity = new Customers { CustomerName = searchTerm };

                // Llamamos a tu capa de presentaci�n para buscar clientes por nombre
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

        // --- M�todo para procesar la selecci�n final del cliente (POST) ---
        // Este handler se ejecutar� cuando el usuario haga clic en el bot�n "Seleccionar Cliente"
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

                // Intentamos encontrar una coincidencia exacta con lo que el usuario seleccion� o escribi�
                var customer = customersFound?.FirstOrDefault(c => c.CustomerName.Equals(SelectedCustomerName, StringComparison.OrdinalIgnoreCase));

                if (customer != null)
                {
                    FeedbackMessage = $"Cliente '{customer.CustomerName}' seleccionado. ID: {customer.Id}.";
                    // Aqu� puedes realizar acciones adicionales, como guardar el ID del cliente en la sesi�n
                    // o redirigir a otra p�gina con el cliente seleccionado.
                    // Ejemplo: TempData["SelectedCustomerId"] = customer.Id;
                }
                else
                {
                    // Esto ocurre si el usuario escribi� algo que no est� en la lista exacta,
                    // o si la lista de sugerencias no incluy� el resultado exacto (si PorNombre busca "contiene").
                    FeedbackMessage = $"No se encontr� un cliente exacto con el nombre '{SelectedCustomerName}'. Por favor, elija de la lista o refine su b�squeda.";
                }
            }
            catch (Exception ex)
            {
                FeedbackMessage = $"Ocurri� un error al procesar la selecci�n: {ex.Message}";
            }

            return Page(); // Volvemos a la misma p�gina con el mensaje actualizado
        }
    }
}

