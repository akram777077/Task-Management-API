using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskMangment.Data.Entities.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Name);
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleName)
            .OnDelete(DeleteBehavior.Cascade);
    }
}