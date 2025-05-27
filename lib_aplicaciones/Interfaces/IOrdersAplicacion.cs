using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IOrdersAplicacion
    {
        void Configurar(string StringConexion);
        List<Orders> PorReferencia(Orders? entidad);
        List<Orders> Listar();
        Orders? Guardar(Orders? entidad);
        Orders? Modificar(Orders? entidad);
        Orders? Borrar(Orders? entidad);


    }
}