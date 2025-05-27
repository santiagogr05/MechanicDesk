using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class PaymentMethodsPrueba
    {
        private readonly IConexion? iConexion;
        private List<PaymentMethods>? lista;
        private PaymentMethods? entidad;

        public PaymentMethodsPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.PaymentMethods!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.PaymentMethods()!;
            this.iConexion!.PaymentMethods!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.PaymentMethod = "Convenio";
            this.entidad!.Active = false;

            var entry = this.iConexion!.Entry<PaymentMethods>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.PaymentMethods!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}