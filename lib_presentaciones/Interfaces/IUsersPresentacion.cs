
using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IUsersPresentacion
    {
        Task<Users?> CrearUsuario(Users? entidad);
        Task<List<Users>> Listar();
    }
}
