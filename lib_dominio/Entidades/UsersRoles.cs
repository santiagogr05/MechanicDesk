namespace lib_dominio.Entidades
{
    public class UsersRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Users? _Users { get; set; }
        public int RoleId { get; set; }
        public Roles? _Roles { get; set; }
    }
}
