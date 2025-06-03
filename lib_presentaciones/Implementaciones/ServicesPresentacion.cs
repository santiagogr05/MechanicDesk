using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class ServicesPresentacion : IServicesPresentacion
    {
        private Comunicaciones? _comunicaciones;

        public ServicesPresentacion(Comunicaciones? comunicaciones)
        {
            _comunicaciones = comunicaciones;
        }

        public async Task<List<Services>> Listar()
        {
            var lista = new List<Services>();
            var datos = new Dictionary<string, object>();

            datos = _comunicaciones!.ConstruirUrl(datos, "Services/Listar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Services>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Services>> PorReferencia(Services? entidad)
        {
            var lista = new List<Services>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;

            datos = _comunicaciones!.ConstruirUrl(datos, "Services/PorReferencia");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Services>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Services?> Guardar(Services? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Services/Guardar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Services>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Services?> Modificar(Services? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Services/Modificar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Services>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Services?> Borrar(Services? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Services/Borrar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Services>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}