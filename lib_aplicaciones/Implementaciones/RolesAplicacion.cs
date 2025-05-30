using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private IConexion? IConexion = null;

        public RolesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Roles? CrearRol(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Verificar si el nombre del rol ya existe
            var existingRole = this.IConexion!.Roles!
                .FirstOrDefault(r => r.RoleName == entidad.RoleName);

            if (existingRole != null)
                throw new Exception("lbRolYaExiste");

            this.IConexion!.Roles!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Roles? Modificar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Verificar si el nombre del rol ya existe (excluyendo este mismo rol)
            var existingRole = this.IConexion!.Roles!
                .FirstOrDefault(r => r.RoleName == entidad.RoleName && r.Id != entidad.Id);

            if (existingRole != null)
                throw new Exception("lbRolYaExiste");

            var entry = this.IConexion!.Entry<Roles>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Roles? Borrar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Verificar si el rol está asignado a algún usuario
            var hasUsers = this.IConexion!.UsersRoles!
                .Any(ur => ur.RoleId == entidad.Id);

            if (hasUsers)
                throw new Exception("lbRolAsignadoUsuarios");

            // Eliminar primero los permisos asociados al rol
            var rolePermissions = this.IConexion!.RolesPermissions!
                .Where(rp => rp.RoleId == entidad.Id)
                .ToList();

            foreach (var rp in rolePermissions)
            {
                this.IConexion.RolesPermissions!.Remove(rp);
            }

            this.IConexion!.Roles!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public bool AsignarPermisosRol(int roleId, int permissionId)
        {
            try
            {
                var role = this.IConexion!.Roles!.Find(roleId);
                var permission = this.IConexion!.Permissions!.Find(permissionId);

                if (role == null || permission == null)
                    return false;

                // Verificar si la asignación ya existe
                var existingAssignment = this.IConexion!.RolesPermissions!
                    .FirstOrDefault(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

                if (existingAssignment != null)
                    return true; // La asignación ya existe, consideramos que se realizó correctamente

                // Crear nueva asignación
                var rolePermission = new RolesPermissions
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                };

                this.IConexion!.RolesPermissions!.Add(rolePermission);
                this.IConexion.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoverPermisosRol(int roleId, int permissionId)
        {
            try
            {
                // Buscar la asignación de permiso existente
                var rolePermission = this.IConexion!.RolesPermissions!
                    .FirstOrDefault(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

                if (rolePermission == null)
                    return true; // No existe la asignación, consideramos que ya fue removida correctamente

                // Eliminar la asignación
                this.IConexion!.RolesPermissions!.Remove(rolePermission);
                this.IConexion.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
