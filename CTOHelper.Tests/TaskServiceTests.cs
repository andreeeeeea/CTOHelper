using CTOHelper.Domain.Entities;
using CTOHelper.Infrastructure.Database;
using CTOHelper.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CTOHelper.Tests;

public class TaskServiceTests : IDisposable
{
    private readonly AppDbContext _db;
    private readonly TaskService _ts;

    public TaskServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _db = new AppDbContext(options);
        _db.Database.OpenConnection();
        _db.Database.EnsureCreated();
        
        _ts = new TaskService(_db);
    }

    [Fact]
    public async Task CreateTask_ShouldCreateTask()
    {
        var task = new DevTask
        {
            Title = "Test Task",
            Description = "This is a test task",
            DueDate = DateTime.Now.AddDays(7)
        };

        var result = await _ts.CreateTaskAsync(task);

        // Assertions to check if the task was created successfully
        Assert.NotNull(result);
        Assert.Equal("Test Task", result.Title);
        var savedTask = await _db.DevTasks.FindAsync(result.Id);
        Assert.NotNull(savedTask);
    }

    [Fact]
    public async Task GetTaskById_ShouldReturnTask()
    {
        var task = new DevTask
        {
            Title = "Test Task",
            Description = "This is a test task",
            DueDate = DateTime.Now.AddDays(7)
        };
        _db.DevTasks.Add(task);
        await _db.SaveChangesAsync();

        var result = await _ts.GetTaskByIdAsync(task.Id);

        // Assertions to check if the task was retrieved successfully
        Assert.NotNull(result);
        Assert.Equal(task.Id, result.Id);
        Assert.Equal("Test Task", result.Title);
    }

    [Fact]
    public async Task UpdateTask_ShouldUpdateTask()
    {
        var task = new DevTask
        {
            Title = "Test Task",
            Description = "This is a test task",
            DueDate = DateTime.Now.AddDays(7)
        };
        _db.DevTasks.Add(task);
        await _db.SaveChangesAsync();

        task.Title = "Updated Task";
        var result = await _ts.UpdateTaskAsync(task);

        // Assertions to check if the task was updated successfully
        Assert.True(result);
        var updatedTask = await _db.DevTasks.FindAsync(task.Id);
        Assert.NotNull(updatedTask);
        Assert.Equal("Updated Task", updatedTask.Title);
    }

    [Fact]
    public async Task DeleteTask_ShouldDeleteTask()
    {
        var task = new DevTask
        {
            Title = "Test Task",
            Description = "This is a test task",
            DueDate = DateTime.Now.AddDays(7)
        };
        _db.DevTasks.Add(task);
        await _db.SaveChangesAsync();

        var result = await _ts.DeleteTaskAsync(task.Id);

        // Assertions to check if the task was deleted successfully
        Assert.True(result);
        var deletedTask = await _db.DevTasks.FindAsync(task.Id);
        Assert.Null(deletedTask);
    }

    [Fact]
    public async Task AssignTaskToEmployee_ShouldAssingTask()
    {
        var task = new DevTask
        {
            Title = "Test Task",
            Description = "This is a test task",
            DueDate = DateTime.Now.AddDays(7)
        };
        _db.DevTasks.Add(task);
        await _db.SaveChangesAsync();
        var employeeId = Guid.NewGuid(); // Simulate an employee ID

        var result = await _ts.AssignTaskToEmployeeAsync(task.Id, employeeId);

        // Assertions to check if the task was assigned successfully
        Assert.True(result);
        var assignedTask = await _db.DevTasks.FindAsync(task.Id);
        Assert.NotNull(assignedTask);
    }
    
    [Fact]
    public async Task UnassignTask_ShouldUnassignTask()
    {
        var task = new DevTask
        {
            Title = "Test Task",
            Description = "This is a test task",
            DueDate = DateTime.Now.AddDays(7),
            AssignedToEmployeeId = Guid.NewGuid()
        };
        _db.DevTasks.Add(task);
        await _db.SaveChangesAsync();

        var result = await _ts.UnassignTaskAsync(task.Id);

        // Assertions to check if the task was unassigned successfully
        Assert.True(result);
        var unassignedTask = await _db.DevTasks.FindAsync(task.Id);
        Assert.NotNull(unassignedTask);
        Assert.Null(unassignedTask.AssignedToEmployeeId);
    }

    [Fact]
    public async Task GetAllTasks_ShouldReturnTasks()
    {
        var task1 = new DevTask
        {
            Title = "Task 1",
            Description = "Description 1",
            DueDate = DateTime.Now.AddDays(7)
        };
        var task2 = new DevTask
        {
            Title = "Task 2",
            Description = "Description 2",
            DueDate = DateTime.Now.AddDays(14)
        };
        _db.DevTasks.AddRange(task1, task2);
        await _db.SaveChangesAsync();

        var tasks = await _ts.GetTasksAsync();

        // Assertions to check if the tasks were retrieved successfully
        Assert.NotNull(tasks);
        Assert.Equal(2, tasks.Count());
    }
    public void Dispose()
    {
        _db.Database.CloseConnection();
        _db.Dispose();
    }
}