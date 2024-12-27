using System;
using Microsoft.EntityFrameworkCore;
using TaskMangment.Api.Entities;
using TaskMangment.Api.Entities.Configurations;

namespace TaskMangment.Api.Data;

public class ToDoListDbContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());
    }
}
