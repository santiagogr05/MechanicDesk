using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ICategoriesPresentacion
    {        
        Task<List<Categories>> PorNombre(Categories? entidad);
        Task<List<Categories>> Listar();
        Task<Categories?> Guardar(Categories? entidad);
        Task<Categories?> Modificar(Categories? entidad);
        Task<Categories?> Borrar(Categories? entidad);


    }
}