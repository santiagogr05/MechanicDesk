using Microsoft.AspNetCore.Mvc.RazorPages;
using lib_dominio.Entidades;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class VehiclesPageModel : PageModel
{
    private readonly IVehiclesPresentacion _vehiclesPresentacion;

    public VehiclesPageModel(IVehiclesPresentacion vehiclesPresentacion)
    {
        _vehiclesPresentacion = vehiclesPresentacion;
    }

    [BindProperty(SupportsGet = true)]
    public string? FiltroPlaca { get; set; }

    public List<Vehicles>? VehiclesList { get; set; }

    public async Task OnGetAsync()
    {
        if (!string.IsNullOrWhiteSpace(FiltroPlaca))
        {
            VehiclesList = await _vehiclesPresentacion.PorPlaca(new Vehicles { Plate = FiltroPlaca });
        }
        else
        {
            VehiclesList = await _vehiclesPresentacion.Listar();
        }
    }
}
