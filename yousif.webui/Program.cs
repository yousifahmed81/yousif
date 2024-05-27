using ClassLibrary2;
using Library.Services;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using yousif.webui.Components;

namespace yousif.webui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContextFactory<BarberDbContext>(Options =>
                Options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DBconnection")));

            // Add services to the container.

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddScoped<IBarberService, BarberService>();
            builder.Services.AddScoped<IServiceService, ServicesService>();

            var app = builder.Build();

           

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
