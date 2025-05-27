using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ProductsAplicacion : IProductsAplicacion
    {
        private IConexion? IConexion = null;

        public ProductsAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Products? Borrar(Products? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Products!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Products? Guardar(Products? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Products!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Products> Listar()
        {
            return this.IConexion!.Products!.Take(20)
                .Include(x => x._Category)
                .ToList();
        }

        public Products? Modificar(Products? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Products>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Products> PorReferencia(Products? entidad)
        {
            return this.IConexion!.Products!
                .Where(x => x.Reference!.Contains(entidad!.Reference!))
                .ToList();

        }
    }
}
