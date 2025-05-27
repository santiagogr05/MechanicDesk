using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class SalesPrueba
    {
        private readonly IConexion? iConexion;
        private List<Sales>? lista;
        private Sales? entidad;

        public SalesPrueba()
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
            this.lista = this.iConexion!.Sales!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            var order = this.iConexion!.Orders!.FirstOrDefault(x => x.Id == 3);
            var paymentMethod = this.iConexion!.PaymentMethods!.FirstOrDefault(x => x.Id == 2);
            this.entidad = EntidadesNucleo.Sales(order!,paymentMethod!)!;
            this.iConexion!.Sales!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.SaleRef = "S999";
            this.entidad!.Total = 1000m;

            var entry = this.iConexion!.Entry<Sales>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Sales!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}