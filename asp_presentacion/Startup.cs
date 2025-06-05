using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using lib_presentaciones; // Asegúrate de tener este using para Comunicaciones

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
            // --- Ajuste 1: Orden de inyección de builder.Services ---
            // Los registros con 'builder.Services' generalmente deben ir antes de 'services.Add...'
            // O, si es posible, usa 'services.Add...' para consistencia si no hay una razón específica para 'builder.Services'.

            // Configuración de Sesión
            services.AddSession(options => // Usa services.AddSession directamente
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddHttpContextAccessor(); // Necesario para acceder a HttpContext en Razor Pages
            // Presentaciones (tus clases de lib_presentaciones.Implementaciones)
            // Asegúrate de que lib_presentaciones.Comunicaciones esté registrado ANTES que las clases que la usan (como BrandsPresentacion)
            services.AddScoped<Comunicaciones>(); // ¡Esta es la línea clave que faltaba o estaba en el lugar equivocado!

            services.AddScoped<IBrandsPresentacion, BrandsPresentacion>();
            services.AddScoped<ICustomersPresentacion, CustomersPresentacion>();
            services.AddScoped<ICategoriesPresentacion, CategoriesPresentacion>();
            services.AddScoped<IEmployeesPresentacion, EmployeesPresentacion>();
            services.AddScoped<IOrderServicesPresentacion, OrderServicesPresentacion>();
            services.AddScoped<IPaymentMethodsPresentacion, PaymentMethodsPresentacion>();
            services.AddScoped<IProductsPresentacion, ProductsPresentacion>();
            services.AddScoped<ISalesPresentacion, SalesPresentacion>();
            services.AddScoped<IServicesPresentacion, ServicesPresentacion>();
            services.AddScoped<IServicesProductsPresentacion, ServicesProductsPresentacion>();
            services.AddScoped<IVehiclesPresentacion, VehiclesPresentacion>();
            // Es preferible usar services.AddScoped si no hay una razón para builder.Services
            services.AddScoped<IOrdersPresentacion, OrdersPresentacion>();
            services.AddScoped<IAuditoriaPresentacion, AuditoriaPresentacion>();// Cambiado de builder.Services.AddScoped

            // --- No necesitas AddControllers ni AddEndpointsApiExplorer aquí ---
            // Estos son para aplicaciones API. Tu aplicación de presentación usa Razor Pages.
            // services.AddControllers();
            // services.AddEndpointsApiExplorer();

            services.AddRazorPages();

        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // --- Ajuste 2: Orden del middleware de sesión ---
            // UseSession() debe ir ANTES de UseAuthorization() y UseEndpoints (que MapRazorPages usa internamente).
            app.UseSession(); // Mover aquí

            // Aquí no necesitas UseAuthentication() ni UseAuthorization() para JWT,
            // ya que esta es la aplicación CLIENTE que CONSUME la API protegida.
            // La autenticación/autorización JWT la maneja tu API (asp_servicios).
            // Si tu aplicación de presentación tuviera su propio sistema de autenticación (ej. Cookies),
            // entonces sí irían aquí. Pero para JWT consumiendo una API, no.

            app.UseAuthorization(); // Si tienes alguna autorización basada en cookies o políticas para Razor Pages, déjalo. Si no, puedes quitarlo.

            app.MapRazorPages();
            // --- Quitar app.Run() de Configure (generalmente va en Program.cs en .NET 6+) ---
            // app.Run(); // Si tienes un Program.cs, esta línea ya la tendrá.
        }
    }
}