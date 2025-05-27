using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class OrderServicesPrueba
    {
        private readonly IConexion? iConexion;
        private List<OrderServices>? lista;
        private OrderServices? entidad;

        public OrderServicesPrueba()
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
            this.lista = this.iConexion!.OrderServices!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            var order = this.iConexion!.Orders!.FirstOrDefault(x => x.Id == 2);
            var service = this.iConexion!.Services!.FirstOrDefault(x => x.Id == 3);
            this.entidad = EntidadesNucleo.OrderServices(order!,service!)!;
            this.iConexion!.OrderServices!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.ServiceId = 1;
            this.entidad!.OrderId = 1;

            var entry = this.iConexion!.Entry<OrderServices>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.OrderServices!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}