using CTOHelper.Domain.Entities;
using CTOHelper.Application.Interfaces;
using CTOHelper.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CTOHelper.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;

    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<DevTask> CreateTaskAsync(DevTask task)
    {
        _db.DevTasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<DevTask?> GetTaskByIdAsync(Guid id)
    {
        return await _db.DevTasks.FindAsync(id);
    }

    public async Task<bool> UpdateTaskAsync(DevTask task)
    {
        var existingTask = await _db.DevTasks.FindAsync(task.Id);
        if (existingTask == null) return false;

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;
        existingTask.AssignedToUserId = task.AssignedToUserId;
        existingTask.Status = task.Status;
        existingTask.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTaskAsync(Guid taskId)
    {
        var existingTask = await _db.DevTasks.FindAsync(taskId);
        if (existingTask == null) return false;

        _db.DevTasks.Remove(existingTask);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignTaskToEmployeeAsync(Guid taskId, Guid employeeId)
    {
        var existingTask = await _db.DevTasks.FindAsync(taskId);
        if (existingTask == null) return false;

        existingTask.AssignedToEmployeeId = employeeId;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnassignTaskAsync(Guid taskId)
    {
        var existingTask = await _db.DevTasks.FindAsync(taskId);
        if (existingTask == null) return false;

        existingTask.AssignedToEmployeeId = null;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateTaskStatusAsync(Guid taskId, string status)
    {
        var existingTask = await _db.DevTasks.FindAsync(taskId);
        if (existingTask == null) return false;

        existingTask.Status = status;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<DevTask>> GetTasksAsync(Guid? employeeId = null, string? status = null)
    {
        var query = _db.DevTasks.AsQueryable();

        if (employeeId.HasValue)
        {
            query = query.Where(t => t.AssignedToEmployeeId == employeeId.Value);
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(t => t.Status == status);
        }

        return await query.ToListAsync();
    }


}