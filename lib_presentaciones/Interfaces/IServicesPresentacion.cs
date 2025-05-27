using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IServicesPresentacion
    {
        Task<List<Services>> PorReferencia(Services? entidad);
        Task<List<Services>> Listar();
        Task<Services?> Guardar(Services? entidad);
        Task<Services?> Modificar(Services? entidad);
        Task<Services?> Borrar(Services? entidad);


    }
}