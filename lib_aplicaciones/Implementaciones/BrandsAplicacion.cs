using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class BrandsAplicacion : IBrandsAplicacion
    {
        private IConexion? IConexion = null;


        public BrandsAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Brands? Borrar(Brands? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Brands!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Brands? Guardar(Brands? entidad)
        {
           if (entidad == null)
                throw new Exception("lbFaltaInformacion");
          if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");
            
            this.IConexion!.Brands!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Brands> Listar()
        {
            return this.IConexion!.Brands!.Take(20).ToList();
        }

        public Brands? Modificar(Brands? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            
            var entry = this.IConexion!.Entry<Brands>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Brands> PorPais(Brands? entidad)
        {
            return this.IConexion!.Brands!
                .Where(x => x.OriginCountry!.Contains(entidad!.OriginCountry!))
                .ToList();

        }
    }
}
