using lib_dominio.Entidades;
using lib_presentaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_presentacion.Pages.Vistas
{
    public class TakeOrderModel : PageModel
    {
        // Las propiedades [BindProperty] ahora solo se usarán si el formulario se postea tradicionalmente,
        // pero para AJAX, los valores los leeremos directamente del cuerpo de la petición.
        // Las mantenemos por si hay algún otro formulario en la página.
        [BindProperty]
        public string SelectedCustomerName { get; set; } = string.Empty;
        [BindProperty]
        public string SelectedEmployeeName { get; set; } = string.Empty;
        [BindProperty]
        public string SelectedVehiclePlate { get; set; } = string.Empty;

        //public string FeedbackMessage { get; set; } = string.Empty;

        public List<string> CustomerNames { get; set; } = new List<string>();
        public List<string> EmployeesNames { get; set; } = new List<string>();
        public List<string> VehiclePlates { get; set; } = new List<string>();

        // Las propiedades FoundCustomer/FoundVehicle ya no son estrictamente necesarias para el flujo de UI
        // si la UI se actualiza con DisplayX. Las podemos mantener si las usas para lógica interna.
        public Customers? FoundCustomer { get; set; }
        public Vehicles? FoundVehicle { get; set; }

        // Propiedades para mostrar el cliente seleccionado en la UI (se llenan en el backend y se envían a la UI)
        //public string DisplayCustomerName { get; set; } = string.Empty;
        //public string DisplayCustomerId { get; set; } = string.Empty;

        //public string DisplayVehiclePlate { get; set; } = string.Empty;
        //public string DisplayVehicleChassis { get; set; } = string.Empty;
        //public string DisplayVehicleOwner { get; set; } = string.Empty;

        private readonly ICustomersPresentacion _customersPresentacion;
        private readonly IEmployeesPresentacion _employeesPresentacion;
        private readonly Comunicaciones _comunicaciones;
        private readonly IVehiclesPresentacion _vehiclesPresentacion;
        

        public TakeOrderModel(ICustomersPresentacion customersPresentacion, IEmployeesPresentacion employeesPresentacion, IVehiclesPresentacion vehiclesPresentacion, Comunicaciones comunicaciones)
        {
            _customersPresentacion = customersPresentacion;
            _employeesPresentacion = employeesPresentacion;
            _vehiclesPresentacion = vehiclesPresentacion;
            _comunicaciones = comunicaciones;
        }

        public async Task OnGet()
        {
            // Llenar ambos datalists al cargar la página
            try
            {
                var allCustomers = await _customersPresentacion.Listar();
                CustomerNames = allCustomers.Where(c => c.CustomerName != null).Select(c => c.CustomerName!).ToList();

                var allVehicles = await _vehiclesPresentacion.Listar(); // Asumo que IVehiclesPresentacion tiene un método Listar()
                VehiclePlates = allVehicles.Where(v => v.Plate != null).Select(v => v.Plate!).ToList();
            }
            catch (Exception ex)
            {
                //FeedbackMessage = $"Error al cargar datos iniciales: {ex.Message}"; // Si quieres mostrar este mensaje
                Console.Error.WriteLine($"Error en OnGet al listar clientes/vehículos: {ex.Message}");
                CustomerNames = new List<string>();
                VehiclePlates = new List<string>();
            }
        }

        // NO usaremos OnPostSelectCustomer para la lógica de selección de UI.
        // En su lugar, usaremos un nuevo handler para AJAX.
        // Mantén este si tienes otros formularios POST en la página que lo usen.
        // public async Task<IActionResult> OnPostSelectCustomer() { /* ... */ }

        // Nuevo handler para la selección de cliente vía AJAX
        public async Task<JsonResult> OnPostSelectCustomerAjax([FromBody] string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                return new JsonResult(new { success = false, message = "Por favor, ingresa un nombre de cliente." });
            }

            try
            {
                var customerToSearch = new Customers { CustomerName = customerName };
                var foundCustomers = await _customersPresentacion.PorNombre(customerToSearch);

                var customer = foundCustomers.FirstOrDefault(c => c.CustomerName != null && c.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase));

                if (customer != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        customerName = customer.CustomerName,
                        customerId = customer.Identification?.ToString(), // Asegúrate de que Identification pueda ser string
                        //message = $"Cliente '{customer.CustomerName}' seleccionado correctamente."
                    });
                }
                else
                {
                    return new JsonResult(new { success = false, message = $"No se encontró ningún cliente con el nombre '{customerName}'." });
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en OnPostSelectCustomerAjax: {ex.Message}");
                return new JsonResult(new { success = false, message = $"Error al seleccionar cliente: {ex.Message}" });
            }
        }

        public async Task<JsonResult> OnPostSelectEmployeeAjax([FromBody] string employeeName)
        {
            if (string.IsNullOrWhiteSpace(employeeName))
            {
                return new JsonResult(new { success = false, message = "Por favor, ingresa un nombre de cliente." });
            }

            try
            {
                var employeesToSearch = new Employees { EmployeeName = employeeName };
                var foundEmployees = await _employeesPresentacion.PorNombre(employeesToSearch);

                var employee = foundEmployees.FirstOrDefault(c => c.EmployeeName != null && c.EmployeeName.Equals(employeeName, StringComparison.OrdinalIgnoreCase));

                if (employee != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        employeeName = employee.EmployeeName,
                        employeeId = employee.Identification?.ToString(), // Asegúrate de que Identification pueda ser string
                        //message = $"Cliente '{customer.CustomerName}' seleccionado correctamente."
                    });
                }
                else
                {
                    return new JsonResult(new { success = false, message = $"No se encontró ningún cliente con el nombre '{employeeName}'." });
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en OnPostSelectCustomerAjax: {ex.Message}");
                return new JsonResult(new { success = false, message = $"Error al seleccionar cliente: {ex.Message}" });
            }
        }


        // NO usaremos OnPostSelectVehicle para la lógica de selección de UI.
        // En su lugar, usaremos un nuevo handler para AJAX.
        // public async Task<IActionResult> OnPostSelectVehicle() { /* ... */ }

        // Nuevo handler para la selección de vehículo vía AJAX
        public async Task<JsonResult> OnPostSelectVehicleAjax([FromBody] string vehiclePlate)
        {
            if (string.IsNullOrWhiteSpace(vehiclePlate))
            {
                return new JsonResult(new { success = false, message = "Por favor, ingresa una placa de vehículo." });
            }

            try
            {
                var vehicleToSearch = new Vehicles { Plate = vehiclePlate };
                var foundVehicles = await _vehiclesPresentacion.PorPlaca(vehicleToSearch);

                var vehicle = foundVehicles.FirstOrDefault(v => v.Plate != null && v.Plate.Equals(vehiclePlate, StringComparison.OrdinalIgnoreCase));

                if (vehicle != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        vehiclePlate = vehicle.Plate,
                        vehicleChassis = vehicle.Chassis?.ToString(),
                        vehicleOwner = vehicle._Customer!.CustomerName,
                        vehiclePhoneNumber = vehicle._Customer!.PhoneNumber!.ToString(),
                    });
                }
                else
                {
                    return new JsonResult(new { success = false, message = $"No se encontró ningún vehículo con la placa '{vehiclePlate}'." });
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en OnPostSelectVehicleAjax: {ex.Message}");
                return new JsonResult(new { success = false, message = $"Error al seleccionar vehículo: {ex.Message}" });
            }
        }

        // Handlers de búsqueda dinámica con AJAX (se mantienen como están, solo asegurando los tipos)
        public async Task<JsonResult> OnGetSearchCustomers(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return new JsonResult(new List<string>());
            }
            try
            {
                var customerToSearch = new Customers { CustomerName = term };
                var customers = await _customersPresentacion.PorNombre(customerToSearch);
                var customerNames = customers
                    .Where(c => c.CustomerName != null && c.CustomerName.Contains(term, StringComparison.OrdinalIgnoreCase))
                    .Select(c => c.CustomerName)
                    .ToList();
                return new JsonResult(customerNames);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en OnGetSearchCustomers: {ex.Message}");
                return new JsonResult(new List<string>());
            }
        }

        public async Task<JsonResult> OnGetSearchVehicles(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return new JsonResult(new List<string>());
            }
            try
            {
                var vehicleToSearch = new Vehicles { Plate = term };
                var vehicles = await _vehiclesPresentacion.PorPlaca(vehicleToSearch);
                var vehiclePlates = vehicles
                    .Where(c => c.Plate != null && c.Plate.Contains(term, StringComparison.OrdinalIgnoreCase))
                    .Select(c => c.Plate)
                    .ToList();
                return new JsonResult(vehiclePlates);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en OnGetSearchVehicles: {ex.Message}");
                return new JsonResult(new List<string>());
            }
        }
    }
}