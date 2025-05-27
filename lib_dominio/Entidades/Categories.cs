

namespace lib_dominio.Entidades
{
    public class Categories
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public bool Active { get; set; }
        List<Products>? ProductsList { get; set; }
    }
}
