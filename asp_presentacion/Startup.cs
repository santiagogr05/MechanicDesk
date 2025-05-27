using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;

namespace asp_presentacion
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
            // Presentaciones
            services.AddScoped<IBrandsPresentacion, BrandsPresentacion>();
            services.AddScoped<ICustomersPresentacion, CustomersPresentacion>();
            services.AddScoped<ICategoriesPresentacion, CategoriesPresentacion>();
            services.AddScoped<IEmployeesPresentacion, EmployeesPresentacion>();
            //services.AddScoped<IOrdersPresentacion, OrdersPresentacion>();
            services.AddScoped<IOrderServicesPresentacion, OrderServicesPresentacion>();
            services.AddScoped<IPaymentMethodsPresentacion, PaymentMethodsPresentacion>();
            services.AddScoped<IProductsPresentacion, ProductsPresentacion>();
            services.AddScoped<ISalesPresentacion, SalesPresentacion>();
            services.AddScoped<IServicesPresentacion, ServicesPresentacion>();
            services.AddScoped<IServicesProductsPresentacion, ServicesProductsPresentacion>();
            services.AddScoped<IVehiclesPresentacion, VehiclesPresentacion>();
            builder.Services.AddScoped<IOrdersPresentacion, OrdersPresentacion>();


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseSession();
            app.Run();
        }
    }
}