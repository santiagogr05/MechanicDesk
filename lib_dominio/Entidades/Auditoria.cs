
namespace lib_dominio.Entidades
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string? Tabla { get; set; }
        public string? Operacion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Usuario { get; set; }
    }
}
