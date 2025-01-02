using Microsoft.EntityFrameworkCore;
using TaskMangment.Data.Entities;
using TaskMangment.Data.Entities.Configurations;

namespace TaskMangment.Data;

public class ToDoListDbContext : DbContext
{
    public DbSet<TaskEntity> ToDoItems { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options) {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=TaskMangment;User Id=postgres;Password=123;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoListDbContext).Assembly);
    }
}
