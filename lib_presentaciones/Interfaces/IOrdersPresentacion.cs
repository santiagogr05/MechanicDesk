using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IOrdersPresentacion
    {      
        Task<List<Orders>> PorReferencia(Orders? entidad);
        Task<List<Orders>> Listar();
        Task<Orders?> Guardar(Orders? entidad);
        Task<Orders?> Modificar(Orders? entidad);
        Task<Orders?> Borrar(Orders? entidad);


    }
}