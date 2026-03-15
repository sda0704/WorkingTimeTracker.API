
using WorkingTimeTracker.DataAccess.Repositories;
using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.Application.Abstractions;

namespace WorkingTimeTracker.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectsRepository _projectsRepository;

    public ProjectService(IProjectsRepository projectRepository)
    {
        _projectsRepository = projectRepository;
    }
    public async Task<List<Project>> GetAllProjects()
    {
        return await _projectsRepository.Get();
    }
    public async Task<Project?> GetById(Guid id)
    {
        return await _projectsRepository.GetById(id);
    }
    public async Task<Guid> CreateProject(Project project)
    {
        return await _projectsRepository.Create(project);
    }
    public async Task<bool> UpdateProject(Guid id, string title, string code, bool isActive)
    {
        return await _projectsRepository.Update(id, title, code, isActive);
    }
    public async Task<bool> DeleteProject(Guid id)
    {
        return await _projectsRepository.Delete(id);
    }
}
