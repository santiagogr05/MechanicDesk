using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IAuditoriaAplicacion
    {
        void Configurar(string StringConexion);
        List<Auditoria> Listar();
    }
}
