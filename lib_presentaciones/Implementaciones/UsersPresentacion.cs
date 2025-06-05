
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using System.Collections.Specialized;
using System.ComponentModel;

namespace lib_presentaciones.Implementaciones
{
    public class UsersPresentacion : IUsersPresentacion
    {
        private Comunicaciones? _comunicaciones;
        public UsersPresentacion(Comunicaciones? comunicaciones)
        {
            _comunicaciones = comunicaciones;
        }

        public async Task<List<Users>> Listar()
        {
            var lista = new List<Users>();
            var datos = new Dictionary<string, object>();


            datos = _comunicaciones!.ConstruirUrl(datos, "Users/Listar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Users>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }

        public async Task<Users> CrearUsuario(Users? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            datos = _comunicaciones!.ConstruirUrl(datos, "Users/CrearUsuario");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Users>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}
