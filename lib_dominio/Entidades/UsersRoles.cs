using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class UsersRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")] public Users? _Users { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")] public Roles? _Roles { get; set; }
    }
}
