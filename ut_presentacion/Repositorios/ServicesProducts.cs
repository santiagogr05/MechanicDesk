using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ServicesProductsPrueba
    {
        private readonly IConexion? iConexion;
        private List<ServicesProducts>? lista;
        private ServicesProducts? entidad;

        public ServicesProductsPrueba()
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
            this.lista = this.iConexion!.ServicesProducts!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            var product = this.iConexion!.Products!.FirstOrDefault(x => x.Id == 1);
            var service = this.iConexion!.Services!.FirstOrDefault(x => x.Id == 1);
            this.entidad = EntidadesNucleo.ServicesProducts(product!,service!)!;
            this.iConexion!.ServicesProducts!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.ProductId = 2;
            this.entidad!.ServiceId = 2;
            var entry = this.iConexion!.Entry<ServicesProducts>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.ServicesProducts!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}