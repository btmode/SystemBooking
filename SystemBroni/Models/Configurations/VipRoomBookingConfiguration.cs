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
                .HasOne(x => x.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            // Одна VIP-комната может быть забронирована только раз
            builder
                .HasOne(x => x.VipRoom)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
