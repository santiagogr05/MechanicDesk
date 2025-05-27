using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ServicesAplicacion : IServicesAplicacion
    {
        private IConexion? IConexion = null;

        public ServicesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Services? Borrar(Services? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Services!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Services? Guardar(Services? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Services!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Services> Listar()
        {
            return this.IConexion!.Services!.Take(20).ToList();
        }

        public Services? Modificar(Services? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Services>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Services> PorReferencia(Services? entidad)
        {
            return this.IConexion!.Services!
                .Where(x => x.Reference!.Contains(entidad!.Reference!))
                .ToList();

        }
    }
}
