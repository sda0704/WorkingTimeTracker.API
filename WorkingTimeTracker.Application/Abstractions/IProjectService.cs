using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.Application.Abstractions
{
    public interface IProjectService
    {
        Task<Guid> CreateProject(Project project);
        Task<bool> DeleteProject(Guid id);
        Task<Project> GetById(Guid id);
        Task<List<Project>> GetAllProjects();
        Task<bool> UpdateProject(Guid id, string title, string code, bool isActive);
    }
}