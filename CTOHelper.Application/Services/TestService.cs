using CTOHelper.Domain.Entities;
using CTOHelper.Application.Interfaces;

namespace CTOHelper.Application.Services;

public class TestService : ITaskService
{
    public Task<List<DevTask>> GetAllTasksAsync() =>
        Task.FromResult(new List<DevTask>
        {
            new DevTask { Id = Guid.NewGuid(), Title = "Set up DB", Description = "Create EF context", DueDate = DateTime.Now.AddDays(2), AssignedTo = "Alice" },
            new DevTask { Id = Guid.NewGuid(), Title = "Design onboarding", Description = "Sketch wireframes", DueDate = DateTime.Now.AddDays(3), AssignedTo = "Bob" }
        });
}