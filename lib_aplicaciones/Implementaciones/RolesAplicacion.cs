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
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

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
            var hasUsers = this.IConexion!.Users!
                .Any(u => u.RoleId == entidad.Id);

            if (hasUsers)
                throw new Exception("lbRolAsignadoUsuarios");

            this.IConexion!.Roles!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Roles> Listar()
        {
        
            return this.IConexion!.Roles!.ToList();
        }

        /*public List<Users>? ObtenerUsuarios(Roles? entidad)
        {
            if (entidad == null || entidad.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var usuarios = this.IConexion!.Users!
                .Where(u => u.RoleId == entidad.Id)
                .ToList();

            return usuarios.Count > 0 ? usuarios : null;
        }*/


        public Roles? BuscarPorNombre(Roles? entidad)
        {
            if (entidad == null || string.IsNullOrEmpty(entidad.RoleName))
                throw new Exception("lbFaltaInformacion");

            return this.IConexion!.Roles!
                .FirstOrDefault(r => r.RoleName!.Contains(entidad.RoleName!));
        }
    }
}
