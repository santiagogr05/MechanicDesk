

using Newtonsoft.Json;

namespace lib_dominio.Entidades
{
    public class Customers
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Identification { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public List<Vehicles>? VehiclesList { get; set; }
    }
}
