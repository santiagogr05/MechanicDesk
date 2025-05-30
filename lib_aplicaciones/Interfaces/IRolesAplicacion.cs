using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    interface IRolesAplicacion
    {
        void Configurar(string StringConexion);
        Roles? CrearRol(Roles? entidad);
        Roles? Modificar(Roles? entidad);
        Roles? Borrar(Roles? entidad);
        bool AsignarPermisosRol(int roleId, int permissionId);
        bool RemoverPermisosRol(int roleId, int permissionId);
    }
}
