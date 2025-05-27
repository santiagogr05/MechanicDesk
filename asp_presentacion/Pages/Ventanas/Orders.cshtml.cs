using lib_dominio.Entidades;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class OrdersPageModel : PageModel
{
    private readonly IOrdersPresentacion _ordersPresentacion;

    public OrdersPageModel(IOrdersPresentacion ordersPresentacion)
    {
        _ordersPresentacion = ordersPresentacion;
    }

    [BindProperty(SupportsGet = true)]
    public string? FiltroReferencia { get; set; }

    public List<Orders>? OrdersList { get; set; }

    public async Task OnGetAsync()
    {
        var todasLasOrdenes = await _ordersPresentacion.Listar();

        if (!string.IsNullOrWhiteSpace(FiltroReferencia))
        {
            OrdersList = todasLasOrdenes
                .Where(o => o.OrderRef != null && o.OrderRef.Contains(FiltroReferencia, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        else
        {
            OrdersList = todasLasOrdenes;
        }
    }
}
