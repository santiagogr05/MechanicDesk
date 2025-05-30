
using Newtonsoft.Json;

namespace lib_dominio.Entidades
{
    public class Roles
    {
        public int Id { get; set; }
        public string? RoleName{ get; set; }
        [JsonIgnore]
        public List<Users>? UsersList { get; set; }
        [JsonIgnore]
        public List<RolesPermissions>? RolesPermissionsList { get; set; }
    }
}
