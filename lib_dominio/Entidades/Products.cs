using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace lib_dominio.Entidades
{
    public class Products
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Reference { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")] public Categories? _Category { get; set; }
        public List<ServicesProducts>? ServicesProductsList { get; set; }
    }
}
