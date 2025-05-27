using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class OrdersPrueba
    {
        private readonly IConexion? iConexion;
        private List<Orders>? lista;
        private Orders? entidad;

        public OrdersPrueba()
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
            this.lista = this.iConexion!.Orders!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            var vehicle = this.iConexion!.Vehicles!.FirstOrDefault(x => x.Id == 4);
            var employee = this.iConexion!.Employees!.FirstOrDefault(x => x.Id == 1);
            this.entidad = EntidadesNucleo.Orders(vehicle!,employee!)!;
            this.iConexion!.Orders!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.OrderRef = "D999";
            this.entidad!.CustomerRemark = "Cambio de volante";
            this.entidad!.ServiceCenterRemark = "Pintura nueva";

            var entry = this.iConexion!.Entry<Orders>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Orders!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}