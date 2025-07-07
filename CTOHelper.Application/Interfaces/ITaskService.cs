using CTOHelper.Domain.Entities;

namespace CTOHelper.Application.Interfaces;

public interface ITaskService
{
    Task<List<DevTask>> GetAllTasksAsync();
}