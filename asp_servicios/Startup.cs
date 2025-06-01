using asp_servicios.Controllers;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using lib_dominio.Nucleo;
namespace asp_servicios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });

            //JWT Autenticación
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DatosGenerales.clave)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // Reduce el ti4empo de tolerancia para la expiración del token
                };
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            // Repositorios
            services.AddScoped<IConexion, Conexion>();
            // Aplicaciones
            services.AddScoped<IBrandsAplicacion, BrandsAplicacion>();
            services.AddScoped<ICustomersAplicacion, CustomersAplicacion>();
            services.AddScoped<ICategoriesAplicacion, CategoriesAplicacion>();
            services.AddScoped<IEmployeesAplicacion, EmployeesAplicacion>();
            services.AddScoped<IOrdersAplicacion, OrdersAplicacion>();
            services.AddScoped<IOrderServicesAplicacion, OrderServicesAplicacion>();
            services.AddScoped<IPaymentMethodsAplicacion, PaymentMethodsAplicacion>();
            services.AddScoped<IProductsAplicacion, ProductsAplicacion>();
            services.AddScoped<ISalesAplicacion, SalesAplicacion>();
            services.AddScoped<IServicesAplicacion, ServicesAplicacion>();
            services.AddScoped<IServicesProductsAplicacion, ServicesProductsAplicacion>();
            services.AddScoped<IVehiclesAplicacion, VehiclesAplicacion>();
            services.AddScoped<IUsersAplicacion, UsersAplicacion>();
            services.AddScoped<IRolesAplicacion, RolesAplicacion>();
            services.AddScoped<IAuditoriaAplicacion, AuditoriaAplicacion>();
            // Controladores
            services.AddScoped<TokenController, TokenController>();

            services.AddCors(o => o.AddDefaultPolicy(b => b.AllowAnyOrigin()));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors();
        }
    }
}