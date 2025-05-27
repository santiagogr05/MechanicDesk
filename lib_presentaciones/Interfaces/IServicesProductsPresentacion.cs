using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IServicesProductsPresentacion
    {
        Task<List<ServicesProducts>> PorServicio(ServicesProducts? entidad);
        Task<List<ServicesProducts>> Listar();
        Task<ServicesProducts?> Guardar(ServicesProducts? entidad);
        Task<ServicesProducts?> Modificar(ServicesProducts? entidad);
        Task<ServicesProducts?> Borrar(ServicesProducts? entidad);


    }
}