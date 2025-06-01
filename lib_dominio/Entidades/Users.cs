
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Users
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")] public Roles? _Roles { get; set; }
    }
}