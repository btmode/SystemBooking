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

        // Уникальность Email
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Один пользователь может иметь много бронирований столиков
        modelBuilder.Entity<TableBooking>()
            .HasOne(b => b.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        // Один столик может быть забронирован только один раз
        modelBuilder.Entity<TableBooking>()
            .HasOne(b => b.Table)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        // Один пользователь может иметь много бронирований VIP-комнат
        modelBuilder.Entity<VipRoomBooking>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Одна VIP-комната может быть забронирована только один раз
        modelBuilder.Entity<VipRoomBooking>()
            .HasOne(b => b.VipRoom)
            .WithMany()
            .HasForeignKey(b => b.VipRoomId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
