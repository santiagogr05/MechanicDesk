using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class CustomersAplicacion : ICustomersAplicacion
    {
        private IConexion? IConexion = null;

        public CustomersAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Customers? Borrar(Customers? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Customers!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Customers? Guardar(Customers? entidad)
        {
           if (entidad == null)
                throw new Exception("lbFaltaInformacion");
          if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");
            
            this.IConexion!.Customers!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Customers> Listar()
        {
            return this.IConexion!.Customers!.Take(20).ToList();
        }

        public Customers? Modificar(Customers? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            
            var entry = this.IConexion!.Entry<Customers>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Customers> PorIdentificacion(Customers? entidad)
        {
            return this.IConexion!.Customers!
                .Where(x => x.Identification!.Contains(entidad!.Identification!))
                .ToList();

        }
    }
}
