using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAuditoriaPresentacion
    {
        Task<List<Auditoria>> Listar();
    }
}
