using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ISalesPresentacion
    {
        Task<List<Sales>> PorReferencia(Sales? entidad);
        Task<List<Sales>> Listar();
        Task<Sales?> Guardar(Sales? entidad);
        Task<Sales?> Modificar(Sales? entidad);
        Task<Sales?> Borrar(Sales? entidad);


    }
}