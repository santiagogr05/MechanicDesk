using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IUsersAplicacion
    {
        void Configurar(string StringConexion);
        Users? CrearUsuario(Users? entidad);
        Users? ObtenerPorUserName(Users? entidad);
        Users? ObtenerPorId(Users? entidad);
        Users? Modificar(Users? entidad);
        Users? Borrar(Users? entidad);
        List<Users>? Listar();
        bool ValidarCredenciales(string nombreUsuario, string password);
        bool AsignarRol(int userId, int roleId);
    }
}
