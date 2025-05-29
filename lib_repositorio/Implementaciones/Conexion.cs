using lib_dominio.Entidades;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorio.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Sales>? Sales { get; set; }
        public DbSet<Customers>? Customers { get; set; }
        public DbSet<Vehicles>? Vehicles { get; set; }
        public DbSet<Employees>? Employees { get; set; }
        public DbSet<Orders>? Orders { get; set; }
        public DbSet<OrderServices>? OrderServices { get; set; }
        public DbSet<ServicesProducts>? ServicesProducts { get; set; }
        public DbSet<Services>? Services { get; set; }
        public DbSet<Products>? Products { get; set; }
        public DbSet<Brands>? Brands { get; set; }
        public DbSet<PaymentMethods>? PaymentMethods { get; set; }
        public DbSet<Categories>? Categories { get; set; }

        public DbSet<Users>? Users { get; set; }
        public DbSet<UsersRoles>? UserRoles { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<RolesPermissions>? RolesPermissions { get; set; }
        public DbSet<Permissions>? Permissions { get; set; }

    }
}
