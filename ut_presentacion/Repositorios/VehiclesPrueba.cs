using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class VehiclesPrueba
    {
        private readonly IConexion? iConexion;
        private List<Vehicles>? lista;
        private Vehicles? entidad;


        public VehiclesPrueba()
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
            this.lista = this.iConexion!.Vehicles!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            var brand = iConexion!.Brands!.FirstOrDefault(x => x.Id == 6);
            var customer = iConexion!.Customers!.FirstOrDefault(x => x.Id == 5);
            this.entidad = EntidadesNucleo.Vehicles(brand,customer)!;
            this.iConexion!.Vehicles!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Plate = "ZXC612";
            this.entidad!.Chassis = "FE7-416541";
            this.entidad!.Color = "Azul";
            this.entidad!.Engine = "V12 Gasolina";


            var entry = this.iConexion!.Entry<Vehicles>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Vehicles!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}
