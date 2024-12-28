using System;
using Microsoft.EntityFrameworkCore;
using TaskMangment.Api.Data;

namespace TaskMangment.Test.Data;

public class InMemoryDbContext : ToDoListDbContext
{
    public InMemoryDbContext(DbContextOptions<ToDoListDbContext> options) 
        : base(options)
    {
    }

    public static InMemoryDbContext CreateInMemoryDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ToDoListDbContext>();
        optionsBuilder.UseSqlite("DataSource=:memory:");

        var dbContext = new InMemoryDbContext(optionsBuilder.Options);
        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();

        return dbContext;
    }

    public override void Dispose()
    {
        Database.EnsureDeleted();
        base.Dispose();
    }
}

