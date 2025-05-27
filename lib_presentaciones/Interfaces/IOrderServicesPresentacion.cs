using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IOrderServicesPresentacion
    {        
        Task<List<OrderServices>> PorOrden(OrderServices? entidad);
        Task<List<OrderServices>> Listar();
        Task<OrderServices?> Guardar(OrderServices? entidad);
        Task<OrderServices?> Modificar(OrderServices? entidad);
        Task<OrderServices?> Borrar(OrderServices? entidad);


    }
}