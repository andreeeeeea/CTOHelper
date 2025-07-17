using CTOHelper.Domain.Entities;
using CTOHelper.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace CTOHelper.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<DevTask> DevTasks { get; set; } = null!;
    public DbSet<TaskItem> TaskItems { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=ctohelper.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}