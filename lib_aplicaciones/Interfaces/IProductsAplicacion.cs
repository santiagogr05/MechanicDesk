using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IProductsAplicacion
    {
        void Configurar(string StringConexion);
        List<Products> PorReferencia(Products? entidad);
        List<Products> Listar();
        Products? Guardar(Products? entidad);
        Products? Modificar(Products? entidad);
        Products? Borrar(Products? entidad);


    }
}