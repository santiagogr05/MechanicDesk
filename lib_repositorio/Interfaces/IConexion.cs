using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorio.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Sales>? Sales { get; set; }
        DbSet<Customers>? Customers { get; set; }
        DbSet<Vehicles>? Vehicles { get; set; }
        DbSet<Employees>? Employees { get; set; }
        DbSet<Orders>? Orders { get; set; }
        DbSet<OrderServices>? OrderServices { get; set; }
        DbSet<ServicesProducts>? ServicesProducts { get; set; }
        DbSet<Services>? Services { get; set; }
        DbSet<Products>? Products { get; set; }
        DbSet<Brands>? Brands { get; set; }
        DbSet<PaymentMethods>? PaymentMethods { get; set; }
        DbSet<Categories>? Categories { get; set; }

        DbSet<Users>? Users { get; set; }
        DbSet<UsersRoles>? UsersRoles { get; set; }
        DbSet<Roles>? Roles { get; set; }
        DbSet<RolesPermissions>? RolesPermissions { get; set; }
        DbSet<Permissions>? Permissions { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
