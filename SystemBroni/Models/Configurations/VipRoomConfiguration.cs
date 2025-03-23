using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SystemBroni.Models.Configurations
{
    public class VipRoomConfiguration : IEntityTypeConfiguration<VipRoom>
    {
        public void Configure(EntityTypeBuilder<VipRoom> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedOnAdd(); // Автоинкремент

            builder.Property(v => v.Status)
                .HasConversion<int>();
            
            // Связь с бронированием
            builder
                .HasMany<VipRoomBooking>()
                .WithOne(vb => vb.VipRoom)
                .HasForeignKey("VipRoomId")
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
