

using Newtonsoft.Json;

namespace lib_dominio.Entidades
{
    public class PaymentMethods
    {
        public int Id { get; set; }
        public string? PaymentMethod { get; set; }
        public bool Active { get; set; }
        [JsonIgnore]
        public List<Sales>? SalesList { get; set; }
    }
}
