using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace lib_aplicaciones.Implementaciones
{
    public class UsersAplicacion : IUsersAplicacion
    {
        private IConexion? IConexion = null;

        public UsersAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Users? CrearUsuario(Users? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Verificar si el nombre de usuario ya existe
            var existingUser = ObtenerPorUserName(entidad);
            if (existingUser != null)
                throw new Exception("lbUsuarioYaExiste");

            // Hashear la contraseña antes de guardar
            entidad.PasswordHash = HashPassword(entidad.PasswordHash);

            this.IConexion!.Users!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Users? ObtenerPorUserName(Users? entidad)
        {
            if (entidad == null || string.IsNullOrEmpty(entidad.UserName))
                throw new Exception("lbFaltaInformacion");

            return this.IConexion!.Users!
                .Include(u => u.UsersRoles)
                .FirstOrDefault(x => x.UserName == entidad.UserName);
        }

        public Users? ObtenerPorId(Users? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            return this.IConexion!.Users!
                .Include(u => u.UsersRoles)
                .FirstOrDefault(x => x.Id == entidad.Id);
        }

        public Users? ValidarCredenciales(string nombreUsuario, string password)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(password))
                throw new Exception("lbFaltaInformacion");

            var user = this.IConexion!.Users!
                .Include(u => u.UsersRoles)
                .FirstOrDefault(x => x.UserName == nombreUsuario);

            if (user == null)
                return null;

            // Verificar contraseña
            bool isValid = VerifyPassword(password, user.PasswordHash);

            if (!isValid)
                return null;

            return user;
        }

        public bool AsignarRol(int userId, int roleId)
        {
            try
            {
                var user = this.IConexion!.Users!.Find(userId);
                var role = this.IConexion!.Roles!.Find(roleId);

                if (user == null || role == null)
                    return false;

                // Verificar si la asignación ya existe
                var existingAssignment = this.IConexion!.UsersRoles!
                    .FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);

                if (existingAssignment != null)
                    return true; // La asignación ya existe, consideramos que se realizó correctamente

                // Crear nueva asignación
                var userRole = new UsersRoles
                {
                    UserId = userId,
                    RoleId = roleId
                };

                this.IConexion!.UsersRoles!.Add(userRole);
                this.IConexion.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Permissions> obtenerPermisos(int userId)
        {
            // Obtener todos los permisos asociados a los roles del usuario
            var permisos = this.IConexion!.UsersRoles!
                .Where(ur => ur.UserId == userId)
                .Join(this.IConexion.RolesPermissions!,
                    ur => ur.RoleId,
                    rp => rp.RoleId,
                    (ur, rp) => rp.PermissionId)
                .Join(this.IConexion.Permissions!,
                    permissionId => permissionId,
                    permission => permission.Id,
                    (permissionId, permission) => permission)
                .Distinct()
                .ToList();

            return permisos;
        }

        // Métodos de ayuda para el hash de contraseñas
        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            string hashedInput = HashPassword(password);
            return string.Equals(hashedInput, hashedPassword);
        }
    }
}
