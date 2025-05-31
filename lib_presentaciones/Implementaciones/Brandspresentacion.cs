using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class BrandsPresentacion : IBrandsPresentacion
    {
        private Comunicaciones? _comunicaciones;

        public BrandsPresentacion(Comunicaciones? comunicaciones)
        {
            _comunicaciones = comunicaciones;
        }

        public async Task<List<Brands>> Listar()
        {
            var lista = new List<Brands>();
            var datos = new Dictionary<string, object>();

            
            datos = _comunicaciones!.ConstruirUrl(datos, "Brands/Listar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Brands>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Brands>> PorPais(Brands? entidad)
        {
            var lista = new List<Brands>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;

            datos = _comunicaciones!.ConstruirUrl(datos, "Brands/PorPais");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Brands>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Brands?> Guardar(Brands? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;
            
            datos = _comunicaciones!.ConstruirUrl(datos, "Brands/Guardar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Brands>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Brands?> Modificar(Brands? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Brands/Modificar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Brands>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Brands?> Borrar(Brands? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Brands/Borrar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Brands>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}