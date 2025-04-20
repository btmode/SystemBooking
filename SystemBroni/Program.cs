using Microsoft.EntityFrameworkCore;
using Serilog;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni
{
    // Добавить возможность ограничения количества логов -
    // 1) По размеру (максимум можно хранить 5МБ)
    // 2) По кол-ву файлов (максимум можно хранить 10 файлов)
    // 3) По дате логов (максимум можно за последние 7 дней)
    // BackgroundService (не Hangfre!)
    
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
            builder.Host.UseSerilog((context, services, configuration) => configuration
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/app.txt", rollingInterval: RollingInterval.Day)
            );
            
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IVipRoomService, VipRoomService>();
            builder.Services.AddScoped<IVipRoomBookingService, VipRoomBookingService>();
            builder.Services.AddScoped<ITableBookingService, TableBookingService>();
            builder.Services.AddScoped<BookingNumberGenerator>();


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
            
            // Показывать номер для пользоватлеля
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Test}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
