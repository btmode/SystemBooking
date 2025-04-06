using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace SystemBroni.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // делает Email уникальным
            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            // делает Phone уникальным
            builder
                .HasIndex(p => p.Phone)
                .IsUnique();
        }
    }
}
