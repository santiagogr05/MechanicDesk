using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IBrandsPresentacion
    {       
        Task<List<Brands>> PorPais(Brands? entidad);
        Task<List<Brands>> Listar();
        Task<Brands?> Guardar(Brands? entidad);
        Task<Brands?> Modificar(Brands? entidad);
        Task<Brands?> Borrar(Brands? entidad);


    }
}
