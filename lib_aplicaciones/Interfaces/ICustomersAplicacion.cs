

using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ICustomersAplicacion
    {
        void Configurar(string StringConexion);
        List<Customers> PorIdentificacion(Customers? entidad);
        List<Customers> PorNombre(Customers? entidad);
        List<Customers> Listar();
        Customers? Guardar(Customers? entidad);
        Customers? Modificar(Customers? entidad);
        Customers? Borrar(Customers? entidad);


    }
}
