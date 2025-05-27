using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PaymentMethodsAplicacion : IPaymentMethodsAplicacion
    {
        private IConexion? IConexion = null;

        public PaymentMethodsAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public PaymentMethods? Borrar(PaymentMethods? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.PaymentMethods!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public PaymentMethods? Guardar(PaymentMethods? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.PaymentMethods!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<PaymentMethods> Listar()
        {
            return this.IConexion!.PaymentMethods!.Take(20).ToList();
        }

        public PaymentMethods? Modificar(PaymentMethods? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<PaymentMethods>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<PaymentMethods> PorActivo(PaymentMethods? entidad)
        {
            return this.IConexion!.PaymentMethods!
                .Where(x => x.Active! == entidad!.Active!)
                .ToList();

        }
    }
}
