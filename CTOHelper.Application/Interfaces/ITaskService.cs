using CTOHelper.Domain.Entities;

namespace CTOHelper.Application.Interfaces;

public interface ITaskService
{
    Task<TaskItem> CreateTaskAsync(DevTask task);
    Task<TaskItem?> GetTaskByIdAsync(Guid id);
    Task<bool> UpdateTaskAsync(DevTask task);
    Task<bool> DeleteTaskAsync(Guid taskId);
    Task<bool> AssignTaskToEmployeeAsync(Guid taskId, Guid employeeId);
    Task<bool> UnassignTaskAsync(Guid taskId);
    Task<bool> UpdateTaskStatusAsync(Guid taskId, string status);
    Task<IEnumerable<TaskItem>> GetTasksAsync(Guid? employeeId = null, string? status = null);
}