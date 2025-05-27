using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IEmployeesAplicacion
    {
        void Configurar(string StringConexion);
        List<Employees> PorIdentificacion(Employees? entidad);
        List<Employees> Listar();
        Employees? Guardar(Employees? entidad);
        Employees? Modificar(Employees? entidad);
        Employees? Borrar(Employees? entidad);


    }
}