using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class EmployeesAplicacion : IEmployeesAplicacion
    {
        private IConexion? IConexion = null;

        public EmployeesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Employees? Borrar(Employees? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Employees!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Employees? Guardar(Employees? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Employees!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Employees> Listar()
        {
            return this.IConexion!.Employees!.Take(20).ToList();
        }

        public Employees? Modificar(Employees? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Employees>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Employees> PorIdentificacion(Employees? entidad)
        {
            return this.IConexion!.Employees!
                .Where(x => x.Identification!.Contains(entidad!.Identification!))
                .ToList();

        }

        public List<Employees> PorNombre(Employees? entidad)
        {
            return this.IConexion!.Employees!
                .Where(x => x.EmployeeName!.Contains(entidad!.EmployeeName!))
                .ToList();
        }
    }
}
