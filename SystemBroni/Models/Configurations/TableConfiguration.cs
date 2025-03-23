using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SystemBroni.Models.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder
                .HasMany<TableBooking>()
                .WithOne(tb => tb.Table)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
