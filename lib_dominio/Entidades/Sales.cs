

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Sales
    {
        public int Id { get; set; }
        public string? SaleRef { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Total { get; set; }
        public int OrderId { get; set; }
        //public List<Orders>? OrdersList { get; set; } 
        
        [ForeignKey("PaymentMethodId")] public PaymentMethods? _PaymentMethod { get; set; }
        
        [ForeignKey("OrderId")] public Orders? _Order { get; set; }
    }
}
