using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class SalesAplicacion : ISalesAplicacion
    {
        private IConexion? IConexion = null;

        public SalesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Sales? Borrar(Sales? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Sales!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Sales? Guardar(Sales? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Sales!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Sales> Listar()
        {
            return this.IConexion!.Sales!.Take(20)
                .Include(x => x._PaymentMethod)
                .Include(x => x._Order)
                .ToList();
        }

        public Sales? Modificar(Sales? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Sales>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Sales> PorReferencia(Sales? entidad)
        {
            return this.IConexion!.Sales!
                .Where(x => x.SaleRef!.Contains(entidad!.SaleRef!))
                .ToList();

        }
    }
}
