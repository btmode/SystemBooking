using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SystemBroni.Models.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd(); // Автоинкремент

            builder.Property(t => t.Status)
                .HasConversion<int>();


            // Связь с бронированием
            builder
                .HasMany<TableBooking>()
                .WithOne(tb => tb.Table)
                .HasForeignKey("TableId")
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
