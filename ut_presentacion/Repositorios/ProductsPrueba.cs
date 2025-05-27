using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ProductsPrueba
    {
        private readonly IConexion? iConexion;
        private List<Products>? lista;
        private Products? entidad;

        public ProductsPrueba()
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
            this.lista = this.iConexion!.Products!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            var category = this.iConexion!.Categories!.FirstOrDefault(x => x.Id == 2);
            this.entidad = EntidadesNucleo.Products(category!)!;
            this.iConexion!.Products!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.ProductName = "Frenos de disco";
            this.entidad!.PurchasePrice = 200.00m;
            this.entidad!.SalePrice = this.entidad.PurchasePrice * 1.5m;
            this.entidad!.Reference = "P997";

            var entry = this.iConexion!.Entry<Products>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Products!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}