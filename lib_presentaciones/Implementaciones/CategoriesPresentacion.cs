using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class CategoriesPresentacion : ICategoriesPresentacion
    {
        private Comunicaciones? _comunicaciones;

        public CategoriesPresentacion(Comunicaciones? comunicaciones)
        {
            _comunicaciones = comunicaciones;
        }
        public async Task<List<Categories>> Listar()
        {
            var lista = new List<Categories>();
            var datos = new Dictionary<string, object>();

            
            datos = _comunicaciones!.ConstruirUrl(datos, "Categories/Listar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Categories>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Categories>> PorNombre(Categories? entidad)
        {
            var lista = new List<Categories>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;

            
            datos = _comunicaciones!.ConstruirUrl(datos, "Categories/PorNombre");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Categories>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Categories?> Guardar(Categories? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Categories/Guardar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Categories>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Categories?> Modificar(Categories? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Categories/Modificar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Categories>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Categories?> Borrar(Categories? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Categories/Borrar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Categories>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}