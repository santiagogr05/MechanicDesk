using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ServicesPrueba
    {
        private readonly IConexion? iConexion;
        private List<Services>? lista;
        private Services? entidad;

        public ServicesPrueba()
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
            this.lista = this.iConexion!.Services!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Services()!;
            this.iConexion!.Services!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Reference = "Se666";
            this.entidad!.Price = 500.22m;
            this.entidad!.ServiceName = "Revisión tecnicomecanica";
            this.entidad!.StimatedTime = "2 dias";

            var entry = this.iConexion!.Entry<Services>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Services!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}