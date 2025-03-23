using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SystemBroni.Models.Configurations;

namespace SystemBroni.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<VipRoom> VipRooms { get; set; }
    public DbSet<TableBooking> TableBookings { get; set; }
    public DbSet<VipRoomBooking> VipRoomBookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TableConfiguration()); // Добавляем
        modelBuilder.ApplyConfiguration(new VipRoomConfiguration()); // Добавляем
        modelBuilder.ApplyConfiguration(new TableBookingConfiguration());
        modelBuilder.ApplyConfiguration(new VipRoomBookingConfiguration()); // Добавляем
    }
}
