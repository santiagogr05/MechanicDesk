using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class PaymentMethodsPresentacion : IPaymentMethodsPresentacion
    {
        private Comunicaciones? _comunicaciones;

        public PaymentMethodsPresentacion(Comunicaciones? comunicaciones)
        {
            _comunicaciones = comunicaciones;
        }

        public async Task<List<PaymentMethods>> Listar()
        {
            var lista = new List<PaymentMethods>();
            var datos = new Dictionary<string, object>();

            datos = _comunicaciones!.ConstruirUrl(datos, "PaymentMethods/Listar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<PaymentMethods>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<List<PaymentMethods>> PorActivo(PaymentMethods? entidad)
        {
            var lista = new List<PaymentMethods>();
            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad!;

            datos = _comunicaciones!.ConstruirUrl(datos, "PaymentMethods/PorActivo");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<PaymentMethods>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<PaymentMethods?> Guardar(PaymentMethods? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "PaymentMethods/Guardar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<PaymentMethods>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<PaymentMethods?> Modificar(PaymentMethods? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "PaymentMethods/Modificar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<PaymentMethods>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<PaymentMethods?> Borrar(PaymentMethods? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "PaymentMethods/Borrar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<PaymentMethods>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}