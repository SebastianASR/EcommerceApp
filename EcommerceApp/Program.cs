using EcommerceApp.Data;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. MVC
builder.Services.AddControllersWithViews();

// 2. SESIONES
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

    // Seguridad extra para cookies
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// 3. CONEXIÓN A POSTGRESQL EN NEON
var connectionString = builder.Configuration.GetConnectionString("NeonConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// 4. CONFIGURACIÓN DE IDENTITY
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Reglas de contraseña más seguras
    options.Password.RequireDigit = true;                 // Requiere número
    options.Password.RequireLowercase = true;             // Requiere minúscula
    options.Password.RequireUppercase = true;             // Requiere mayúscula
    options.Password.RequireNonAlphanumeric = true;       // Requiere símbolo: !, @, #, etc.
    options.Password.RequiredLength = 8;                  // Mínimo 8 caracteres
    options.Password.RequiredUniqueChars = 1;

    // Bloqueo por intentos fallidos
    options.Lockout.AllowedForNewUsers = true;            // Aplica bloqueo a usuarios nuevos
    options.Lockout.MaxFailedAccessAttempts = 5;          // 5 intentos fallidos
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // Bloqueo 15 m

    // Configuración de usuario
    options.User.RequireUniqueEmail = true;               // No permite correos duplicados

    
    options.SignIn.RequireConfirmedEmail = false;
})
.AddErrorDescriber<SpanishIdentityErrorDescriber>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 5. CONFIGURACIÓN DE COOKIES DE AUTENTICACIÓN
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";

    // Seguridad de cookie
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    // Duración de sesión autenticada
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// 6. SEED DE ROLES Y USUARIOS
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedRolesAndAdmin(services);
}

// 7. PIPELINE HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Sesiones antes de mapear rutas
app.UseSession();

app.UseAuthentication(); // Verifica QUIÉN eres
app.UseAuthorization();  // Verifica QUÉ puedes hacer

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();