

using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ICustomersPresentacion
    {        
        Task<List<Customers>> PorIdentificacion(Customers? entidad);
        Task<List<Customers>> PorNombre(Customers? entidad);
        Task<List<Customers>> Listar();
        Task<Customers?> Guardar(Customers? entidad);
        Task<Customers?> Modificar(Customers? entidad);
        Task<Customers?> Borrar(Customers? entidad);

    }
}
