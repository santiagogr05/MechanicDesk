using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IProductsPresentacion
    {
        Task<List<Products>> PorReferencia(Products? entidad);
        Task<List<Products>> Listar();
        Task<Products?> Guardar(Products? entidad);
        Task<Products?> Modificar(Products? entidad);
        Task<Products?> Borrar(Products? entidad);


    }
}