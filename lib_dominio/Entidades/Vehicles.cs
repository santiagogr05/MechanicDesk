
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Vehicles
    {
        public int Id { get; set; }
        public string? Plate { get; set; }
        public string? Chassis { get; set; }
        public string? Color { get; set; }
        public string? Engine { get; set; }
        public int? BrandId { get; set; }
        public int? CustomerId { get; set; }
        [ForeignKey("BrandId")] public Brands? _Brand { get; set; }
        [ForeignKey("CustomerId")] public Customers? _Customer { get; set; }
    }
}
