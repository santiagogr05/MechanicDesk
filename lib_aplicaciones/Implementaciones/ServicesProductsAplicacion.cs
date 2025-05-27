using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ServicesProductsAplicacion : IServicesProductsAplicacion
    {
        private IConexion? IConexion = null;

        public ServicesProductsAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public ServicesProducts? Borrar(ServicesProducts? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.ServicesProducts!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ServicesProducts? Guardar(ServicesProducts? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.ServicesProducts!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<ServicesProducts> Listar()
        {
            return this.IConexion!.ServicesProducts!.Take(20)
                .Include(x => x._Service)
                .Include(x => x._Prodcut)                
                .ToList();
        }

        public ServicesProducts? Modificar(ServicesProducts? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<ServicesProducts>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<ServicesProducts> PorServicio(ServicesProducts? entidad)
        {
            return this.IConexion!.ServicesProducts!
                .Where(x => x.ServiceId! == entidad!.ServiceId!)
                .ToList();

        }
    }
}

