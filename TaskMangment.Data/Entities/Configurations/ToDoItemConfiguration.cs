namespace TaskMangment.Data.Entities.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ToDoItemConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {

        builder.HasKey(t => t.Id);


        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);


        builder.Property(t => t.Description)
            .HasMaxLength(500);


        builder.Property(t => t.DueDate)
            .IsRequired();


        builder.Property(t => t.IsCompleted)
            .HasDefaultValue(false);
    }
}

