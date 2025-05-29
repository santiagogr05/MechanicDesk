
namespace lib_dominio.Entidades
{
    public class RolesPermissions
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Roles? _Roles { get; set; }
        public int PermissionId { get; set; }
        public Permissions? _Permissions { get; set; }
    }
}
