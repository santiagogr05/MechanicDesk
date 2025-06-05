using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{

    public class CustomersPresentacion : ICustomersPresentacion
    {
        private Comunicaciones? _comunicaciones;

        public CustomersPresentacion(Comunicaciones? comunicaciones)
        {
            this._comunicaciones = comunicaciones;
            _comunicaciones = comunicaciones;
        }

        public async Task<List<Customers>> Listar()
        {
            var lista = new List<Customers>();
            var datos = new Dictionary<string, object>();

            datos = _comunicaciones!.ConstruirUrl(datos, "Customers/Listar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Customers>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Customers>> PorIdentificacion(Customers? entidad)
        {
            var lista = new List<Customers>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;
            
            datos = _comunicaciones!.ConstruirUrl(datos, "Customers/PorIdentificacion");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Customers>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<Customers>> PorNombre(Customers? entidad)
        {
            var lista = new List<Customers>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;
            
            datos = _comunicaciones!.ConstruirUrl(datos, "Customers/PorNombre");            

            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Customers>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Customers?> Guardar(Customers? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Customers/Guardar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Customers>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Customers?> Modificar(Customers? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Customers/Modificar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Customers>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Customers?> Borrar(Customers? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Customers/Borrar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Customers>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }     
        
    }
}