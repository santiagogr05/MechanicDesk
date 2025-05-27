using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class OrderServicesAplicacion : IOrderServicesAplicacion
    {
        private IConexion? IConexion = null;

        public OrderServicesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public OrderServices? Borrar(OrderServices? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.OrderServices!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public OrderServices? Guardar(OrderServices? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.OrderServices!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<OrderServices> Listar()
        {
            return this.IConexion!.OrderServices!.Take(20)
                .Include(x => x._Order)
                .Include(x => x._Service)
                .ToList();
        }

        public OrderServices? Modificar(OrderServices? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<OrderServices>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<OrderServices> PorOrden(OrderServices? entidad)
        {
            return this.IConexion!.OrderServices!
                .Where(x => x.OrderId! == entidad!.OrderId!)
                .ToList();

        }
    }
}
