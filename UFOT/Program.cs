using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using UFOT.Controllers;
using UFOT.Data;
using UFOT;

var builder = WebApplication.CreateBuilder(args);

// Configura el pipeline de solicitudes HTTP.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BancoWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 10;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopCenter;
});
builder.Services.AddTransient<LogService>();
builder.Services.AddTransient<BaseController>();

// Agrega la configuraci�n de la sesi�n antes de crear la aplicaci�n
builder.Services.AddSession(options =>
{
    // Configura el tiempo de expiraci�n de la sesi�n si lo deseas
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); // Mueve esto fuera del bloque condicional

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "adm",
    pattern: "adm/{action=Index}/{id?}",
    defaults: new { controller = "ADM" });

app.Run();
