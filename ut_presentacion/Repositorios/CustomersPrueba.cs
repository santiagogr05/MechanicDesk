using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class CustomersPrueba
    {
        private readonly IConexion? iConexion;
        private List<Customers>? lista;
        private Customers? entidad;

        
        public CustomersPrueba()
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
            this.lista = this.iConexion!.Customers!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Customers()!;
            this.iConexion!.Customers!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.CustomerName = "Josué Hoyos";
            this.entidad!.Identification = "32012694";
            this.entidad!.PhoneNumber = "3043551027";
            this.entidad!.Email = "josu@mail.com";

            var entry = this.iConexion!.Entry<Customers>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Customers!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
