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
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            // Verificar si el nombre de usuario ya existe
            var existingUser = this.IConexion!.Users!
                .FirstOrDefault(u => u.UserName == entidad.UserName);

            if (existingUser != null)
                throw new Exception("lbUsuarioYaExiste");

            // Hashear la contraseña antes de guardar
            entidad.PasswordHash = HashPassword(entidad.PasswordHash);

            this.IConexion!.Users!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Users? Modificar(Users? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            // Verificar si el nombre de usuario ya existe (excluyendo este mismo usuario)
            var existingUser = this.IConexion!.Users!
                .FirstOrDefault(u => u.UserName == entidad.UserName && u.Id != entidad.Id);

            if (existingUser != null)
                throw new Exception("lbUsuarioYaExiste");

            // Si se proporciona una nueva contraseña, hashearla
            if (!string.IsNullOrEmpty(entidad.PasswordHash))
            {
                // Verificar si la contraseña ya está hasheada comparando con la almacenada
                var storedUser = this.IConexion!.Users!.Find(entidad.Id);
                if (storedUser != null && entidad.PasswordHash != storedUser.PasswordHash)
                {
                    entidad.PasswordHash = HashPassword(entidad.PasswordHash);
                }
            }

            var entry = this.IConexion!.Entry<Users>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Users? Borrar(Users? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Users!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Users>? Listar()
        {
            return this.IConexion!.Users!
                .Include(u => u._Roles)
                .Take(20)
                .ToList();
        }

        public Users? ObtenerPorUserName(Users? entidad)
        {
            if (entidad == null || string.IsNullOrEmpty(entidad.UserName))
                throw new Exception("lbFaltaInformacion");

            return this.IConexion!.Users!
                .Include(u => u._Roles)
                .FirstOrDefault(x => x.UserName == entidad.UserName);
        }

        public Users? ObtenerPorId(Users? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            return this.IConexion!.Users!
                .Include(u => u._Roles)
                .FirstOrDefault(x => x.Id == entidad.Id);
        }

        public bool ValidarCredenciales(string nombreUsuario, string password)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(password))
                throw new Exception("lbFaltaInformacion");

            var user = this.IConexion!.Users!
                .Include(u => u._Roles)
                .FirstOrDefault(x => x.UserName == nombreUsuario);

            if (user == null)
                return false;

            // Verificar contraseña
            return VerifyPassword(password, user.PasswordHash);

        }

        public bool AsignarRol(int userId, int roleId)
        {
            try
            {
                var user = this.IConexion!.Users!.Find(userId);
                var role = this.IConexion!.Roles!.Find(roleId);

                if (user == null || role == null)
                    return false;

                // Asignar rol al usuario
                user.RoleId = roleId;
                this.IConexion.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Métodos de ayuda para el hash de contraseñas
        private string HashPassword(string? password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string? hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            string hashedInput = HashPassword(password);
            return string.Equals(hashedInput, hashedPassword);
        }
    }
}
