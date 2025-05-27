

using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IVehiclesPresentacion
    {
        Task<List<Vehicles>> PorPlaca(Vehicles? entidad);
        Task<List<Vehicles>> Listar();
        Task<Vehicles?> Guardar(Vehicles? entidad);
        Task<Vehicles?> Modificar(Vehicles? entidad);
        Task<Vehicles?> Borrar(Vehicles? entidad);


    }
}
