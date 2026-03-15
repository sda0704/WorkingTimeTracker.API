using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.Application.Abstractions
{
    public interface ITasksService
    {
        Task<Guid> CreateTask(Tasks task);
        Task<bool> DeleteTask(Guid taskId);
        Task<List<Tasks>> GetAllTasks();
        Task<Tasks?> GetTaskById(Guid taskId);
        Task<bool> UpdateTask(Tasks task);
    }
}