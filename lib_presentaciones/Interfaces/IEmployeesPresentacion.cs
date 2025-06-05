using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IEmployeesPresentacion
    {        
        Task<List<Employees>> PorIdentificacion(Employees? entidad);
        Task<List<Employees>> PorNombre(Employees? entidad);
        Task<List<Employees>> Listar();
        Task<Employees?> Guardar(Employees? entidad);
        Task<Employees?> Modificar(Employees? entidad);
        Task<Employees?> Borrar(Employees? entidad);


    }
}