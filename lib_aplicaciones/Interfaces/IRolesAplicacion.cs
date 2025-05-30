using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IRolesAplicacion
    {
        void Configurar(string StringConexion);
        Roles? CrearRol(Roles? entidad);
        Roles? Modificar(Roles? entidad);
        Roles? Borrar(Roles? entidad);
        List<Roles> Listar();
        //List<Users>? ObtenerUsuarios(Roles? entidad);
        Roles? BuscarPorNombre(Roles? entidad);

    }
}
