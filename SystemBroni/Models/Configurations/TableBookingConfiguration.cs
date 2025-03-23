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
                .HasOne(vb => vb.User)
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            // Один столик может быть забронирован только один раз
            builder
                .HasOne(vb => vb.Table)
                .WithMany()
                .HasForeignKey("TableId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
