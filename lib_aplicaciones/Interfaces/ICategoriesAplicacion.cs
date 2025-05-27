using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ICategoriesAplicacion
    {
        void Configurar(string StringConexion);
        List<Categories> PorNombre(Categories? entidad);
        List<Categories> Listar();
        Categories? Guardar(Categories? entidad);
        Categories? Modificar(Categories? entidad);
        Categories? Borrar(Categories? entidad);


    }
}