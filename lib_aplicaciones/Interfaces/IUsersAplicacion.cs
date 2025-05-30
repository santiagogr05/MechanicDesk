using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IUsersAplicacion
    {
        void Configurar(string StringConexion);
        Users? CrearUsuario(Users? entidad);
        Users? ObtenerPorUserName(Users? entidad);
        Users? ObtenerPorId(Users? entidad);
        Users? ValidarCredenciales(string nombreUsuario, string password);
        bool AsignarRol(int userId, int roleId);
        List<Permissions> obtenerPermisos(int userId);
    }
}
