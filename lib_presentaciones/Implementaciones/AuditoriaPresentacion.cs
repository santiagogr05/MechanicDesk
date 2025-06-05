
using lib_presentaciones.Interfaces;
using lib_dominio.Entidades;

using lib_dominio.Nucleo;
namespace lib_presentaciones.Implementaciones
{
    public class AuditoriaPresentacion : IAuditoriaPresentacion
    {
        private Comunicaciones? _comunicaciones;
        public AuditoriaPresentacion(Comunicaciones? comunicaciones)
        {
            _comunicaciones = comunicaciones;
        }
        public async Task<List<Auditoria>> Listar()
        {
            var lista = new List<Auditoria>();
            var datos = new Dictionary<string, object>();


            datos = _comunicaciones!.ConstruirUrl(datos, "Auditoria/Listar");
            var respuesta = await _comunicaciones!.Ejecutar(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Auditoria>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
    }
}
