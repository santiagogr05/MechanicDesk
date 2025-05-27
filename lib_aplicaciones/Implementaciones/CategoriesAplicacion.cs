using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Extensions.Logging;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class CategoriesAplicacion : ICategoriesAplicacion
    {
        private IConexion? IConexion = null;

        public CategoriesAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }
        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public Categories? Borrar(Categories? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            this.IConexion!.Categories!.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Categories? Guardar(Categories? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Categories!.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Categories> Listar()
        {
            return this.IConexion!.Categories!.Take(20).ToList();
        }

        public Categories? Modificar(Categories? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.IConexion!.Entry<Categories>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion.SaveChanges();
            return entidad;

        }

        public List<Categories> PorNombre(Categories? entidad)
        {
            return this.IConexion!.Categories!
                .Where(x => x.CategoryName!.Contains(entidad!.CategoryName!))
                .ToList();

        }
    }
}
