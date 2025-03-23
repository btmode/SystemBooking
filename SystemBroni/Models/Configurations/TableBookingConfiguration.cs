using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace SystemBroni.Models.Configurations
{
    public class TableBookingConfiguration : IEntityTypeConfiguration<TableBooking>
    {
        public void Configure(EntityTypeBuilder<TableBooking> builder)
        {
            // Один пользователь может иметь много бронирований столиков
            builder
                .HasOne(x => x.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            // Один столик может быть забронирован только один раз
            builder
                .HasOne(x => x.Table)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
