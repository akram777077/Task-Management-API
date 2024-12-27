using System;

namespace TaskMangment.Api.Entities.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
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

