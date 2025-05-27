using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IOrderServicesAplicacion
    {
        void Configurar(string StringConexion);
        List<OrderServices> PorOrden(OrderServices? entidad);
        List<OrderServices> Listar();
        OrderServices? Guardar(OrderServices? entidad);
        OrderServices? Modificar(OrderServices? entidad);
        OrderServices? Borrar(OrderServices? entidad);


    }
}