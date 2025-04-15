using Microsoft.EntityFrameworkCore;
using Serilog;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });
            
            
            // Конфигурация Serilog
            var path = "Logger.txt";
            builder.Host.UseSerilog((context, services, configuration) => configuration
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(path, rollingInterval: RollingInterval.Day)
            );
           
            
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IVipRoomService, VipRoomService>();
            builder.Services.AddScoped<IVipRoomBookingService, VipRoomBookingService>();
            builder.Services.AddScoped<ITableBookingService, TableBookingService>();

            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            // app.MapGet("/", (ILogger<Program> logger) =>
            // {
            //     logger.LogInformation("Привет! Это лог в файл и в консоль :)");
            //     return "Готово!";
            // });
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
