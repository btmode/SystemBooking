using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SystemBroni.Models.Configurations
{
    public class VipRoomConfiguration : IEntityTypeConfiguration<VipRoom>
    {
        public void Configure(EntityTypeBuilder<VipRoom> builder)
        {
            builder
                .HasMany<VipRoomBooking>()
                .WithOne(x => x.VipRoom)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
