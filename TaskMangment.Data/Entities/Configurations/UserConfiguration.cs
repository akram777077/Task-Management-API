using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskMangment.Data.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(t => t.UserName);
        
        builder.Property(t => t.UserName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Password)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.Username)
            .OnDelete(DeleteBehavior.Cascade);
    }
}