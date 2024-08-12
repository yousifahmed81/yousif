using Barber.UI.Components;
using ClassLibrary2;
using Library.Services;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Net;

namespace Barber.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
           .AddInteractiveServerComponents();
            builder.Services.AddFluentUIComponents();

            builder.Services.AddDbContextFactory<BarberDbContext>(Options =>
            Options.UseSqlServer(builder.Configuration.GetConnectionString("DBconnection"), opt =>
           opt.EnableRetryOnFailure(
               maxRetryCount: 5,
               maxRetryDelay: System.TimeSpan.FromSeconds(30),
               errorNumbersToAdd: null)));



            builder.Services.AddScoped<IBarberService, BarberService>();
            builder.Services.AddScoped<IServiceService, ServicesService>();
            builder.WebHost.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
            builder.WebHost.ConfigureKestrel((context, serverOptions) =>
            {
                serverOptions.Listen(IPAddress.Loopback, 5001);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();

        }
    }
}