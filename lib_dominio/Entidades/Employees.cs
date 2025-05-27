
using Newtonsoft.Json;

namespace lib_dominio.Entidades
{
    public class Employees
    {
        public int Id { get; set; }
        public string? Identification { get; set; }
        public string? EmployeeName { get; set; }
        public bool Active { get; set; }
        [JsonIgnore]
        public List<Orders>? OrdersList { get; set; }
    }
}
