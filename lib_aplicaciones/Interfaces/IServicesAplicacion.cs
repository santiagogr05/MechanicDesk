using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IServicesAplicacion
    {
        void Configurar(string StringConexion);
        List<Services> PorReferencia(Services? entidad);
        List<Services> Listar();
        Services? Guardar(Services? entidad);
        Services? Modificar(Services? entidad);
        Services? Borrar(Services? entidad);


    }
}