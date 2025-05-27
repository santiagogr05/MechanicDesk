using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IServicesProductsAplicacion
    {
        void Configurar(string StringConexion);
        List<ServicesProducts> PorServicio(ServicesProducts? entidad);
        List<ServicesProducts> Listar();
        ServicesProducts? Guardar(ServicesProducts? entidad);
        ServicesProducts? Modificar(ServicesProducts? entidad);
        ServicesProducts? Borrar(ServicesProducts? entidad);


    }
}