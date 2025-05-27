

using Newtonsoft.Json;

namespace lib_dominio.Entidades
{
    public class Brands
    {
        public int Id { get; set; }
        public string? BrandName { get; set; }
        public string? OriginCountry { get; set; }
        [JsonIgnore]
        public List<Vehicles>? VehiclesList { get; set; }
    }
}
