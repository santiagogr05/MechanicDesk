
namespace lib_dominio.Entidades
{
    public class Permissions
    {
        public int Id { get; set; }
        public string? PermissionName { get; set; }
        public string? Description { get; set; }
        public List<RolesPermissions>? _RolesPermissions { get; set; }
    }
}
