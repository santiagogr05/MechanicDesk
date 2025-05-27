using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class OrdersAplicacion : IOrdersAplicacion
    {
        private IConexion? IConexion = null;

        public OrdersAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Orders? Borrar(Orders? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Orders!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Orders? Guardar(Orders? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Orders!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Orders> Listar()
        {
            return this.IConexion!.Orders!.Take(20)
                .Include(x => x._Employees)
                .Include(x => x._Vehicle)
                .ToList();
        }

        public Orders? Modificar(Orders? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Orders>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Orders> PorReferencia(Orders? entidad)
        {
            return this.IConexion!.Orders!
                .Where(x => x.OrderRef!.Contains(entidad!.OrderRef!))
                .ToList();

        }
    }
}
