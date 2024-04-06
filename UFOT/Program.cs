using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UFOT.Data;
using NLog;
using NLog.Extensions;
using NLog.Web;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using UFOT;
using UFOT.Controllers;

public class Program
{
    static void Main(string[] args)
    {
        // Aquí va el cuerpo de tu método Main

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
       
    }

}
