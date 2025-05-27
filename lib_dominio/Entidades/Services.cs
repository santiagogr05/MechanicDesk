

namespace lib_dominio.Entidades
{
    public class Services
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public string? Reference { get; set; }
        public decimal Price { get; set; }
        public string? StimatedTime { get; set; }
        public List<OrderServices>? OrderServicesList { get; set; }
        public List<ServicesProducts>? ServicesProductsList { get; set; }
    }
}
