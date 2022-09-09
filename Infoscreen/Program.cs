using DbInfoscreenLibrary;
using Infoscreen.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using Radzen;
using Serilog;
using Serilog.Extensions.Logging;

namespace Infoscreen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddDbContextFactory<DbInfoscreenContext>();

            builder.Services.AddMudServices();
            builder.Services.AddScoped<Radzen.DialogService>();
            builder.Services.AddScoped<Radzen.NotificationService>();

            builder.Services.AddSingleton<ScreenData>();

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Program", "Infoscreen-Web")
                .Enrich.WithProperty("Maching", System.Environment.MachineName)
                .WriteTo.Seq("http://localhost:5341", bufferBaseFilename: Path.Join("Logs", "log"))
                .CreateLogger();

            builder.Logging.AddProvider(new SerilogLoggerProvider());

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

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}