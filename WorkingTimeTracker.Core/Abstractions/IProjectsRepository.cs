using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.DataAccess.Repositories
{
    public interface IProjectsRepository
    {
        Task<Guid> Create(Project project);
        Task<bool> Delete(Guid id);
        Task<List<Project>> Get();
        Task<Project?> GetById(Guid id);
        Task<bool> Update(Guid id, string title, string code, bool isActive);
    }
}