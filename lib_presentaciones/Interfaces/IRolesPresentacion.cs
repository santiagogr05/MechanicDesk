using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IRolesPresentacion
    {
        Task<List<Roles>> Listar();
    }
}
