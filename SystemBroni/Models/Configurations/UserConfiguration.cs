using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace SystemBroni.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .HasIndex(p => p.Phone)
                .IsUnique();
        }
    }
}
