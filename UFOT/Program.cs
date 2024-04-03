using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using UFOT.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar el pipeline de solicitudes HTTP.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BancoWebContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "adm",
    pattern: "adm/{action=Index}/{id?}",
    defaults: new { controller = "ADM" });

app.Run();
