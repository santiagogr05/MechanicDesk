using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IBrandsAplicacion
    {
        void Configurar(string StringConexion);
        List<Brands> PorPais(Brands? entidad);
        List<Brands> Listar();
        Brands? Guardar(Brands? entidad);
        Brands? Modificar(Brands? entidad);
        Brands? Borrar(Brands? entidad);


    }
}
