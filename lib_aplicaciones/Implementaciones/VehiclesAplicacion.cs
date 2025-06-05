using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class VehiclesAplicacion : IVehiclesAplicacion
    {
        private IConexion? IConexion = null;

        public VehiclesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Vehicles? Borrar(Vehicles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Vehicles!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Vehicles? Guardar(Vehicles? entidad)
        {
           if (entidad == null)
                throw new Exception("lbFaltaInformacion");
          if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");
            
            this.IConexion!.Vehicles!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Vehicles> Listar()
        {
            return this.IConexion!.Vehicles!.Take(20)
                .Include(x => x._Customer)
                .Include(x => x._Brand)
                .ToList();
        }

        public Vehicles? Modificar(Vehicles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            
            var entry = this.IConexion!.Entry<Vehicles>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Vehicles> PorPlaca(Vehicles? entidad)
        {
            return this.IConexion!.Vehicles!
                .Where(x => x.Plate!.Contains(entidad!.Plate!))
                .Include(x => x._Customer)
                .ToList();

        }
    }
}
