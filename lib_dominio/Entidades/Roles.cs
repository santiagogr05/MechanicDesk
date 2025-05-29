
namespace lib_dominio.Entidades
{
    public class Roles
    {
        public int Id { get; set; }
        public string? RoleName{ get; set; }
        public List<UsersRoles>? _UsersRoles { get; set; }
        public List<RolesPermissions>? _RolesPermissions { get; set; }
    }
}
