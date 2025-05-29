
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class RolesPermissions
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")] public Roles? _Roles { get; set; }
        public int PermissionId { get; set; }
        [ForeignKey("PermissionId")]public Permissions? _Permissions { get; set; }
    }
}
