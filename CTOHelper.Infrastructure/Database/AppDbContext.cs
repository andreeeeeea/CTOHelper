using CTOHelper.Domain.Entities;
using CTOHelper.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CTOHelper.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<DevTask> DevTasks { get; set; } = null!;
    public DbSet<TaskItem> TaskItems { get; set; } = null!;
}