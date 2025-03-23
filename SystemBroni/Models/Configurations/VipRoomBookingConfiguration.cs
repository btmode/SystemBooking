using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace SystemBroni.Models.Configurations
{
    public class VipRoomBookingConfiguration : IEntityTypeConfiguration<VipRoomBooking>
    {
        public void Configure(EntityTypeBuilder<VipRoomBooking> builder)
        {
            // Один пользователь может бронировать много комнат
            builder
                .HasOne(vb => vb.User)
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            // Одна VIP-комната может быть забронирована только раз
            builder
                .HasOne(vb => vb.VipRoom)
                .WithMany()
                .HasForeignKey("VipRoomId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
