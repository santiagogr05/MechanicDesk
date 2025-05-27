

using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class ServicesProducts
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ServiceId")] public Services? _Service { get; set; }
        [ForeignKey("ProductId")] public Products? _Prodcut { get; set; }
    }
}
