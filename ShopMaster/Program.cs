using AccesoDatos.Data;
using AccesoDatos.Data.Inicializador;
using AccesoDatos.Data.Repository;
using AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Servicio;
using Servicio.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true; // Evita el acceso a la cookie desde JavaScript
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Solo se envía en conexiones HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict; // Evita que la cookie se envíe en solicitudes de origen cruzado
        options.LoginPath = "/Acceso/Auth/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
        options.AccessDeniedPath = "/Acceso/Auth/AccessDenied";
    });

builder.Services.AddHttpContextAccessor();

// Configuración de la cadena de conexión a SQL Server
var connectionString = builder.Configuration.GetConnectionString("ConexionSQL");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


// Repositorio
builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();

// Servicios
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IMarcaServicio, MarcaServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();
builder.Services.AddScoped<ISliderServicio, SliderServicio>();
builder.Services.AddScoped<IHomeServicio, HomeServicio>();
builder.Services.AddScoped<IAuthServicio, AuthServicio>();

// Inicialziador
builder.Services.AddScoped<IInicializadorDb, InicializadorDb>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//Metodo que ejecuta la siembra de datos
await SiembraDatos();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Tienda}/{controller=Home}/{action=Index}/{id?}");

app.Run();
async Task SiembraDatos()
{
    using (var scope = app.Services.CreateScope())
    {
        var inicializadorBD = scope.ServiceProvider.GetRequiredService<IInicializadorDb>();
        await inicializadorBD.Inicializacion();
    }
}