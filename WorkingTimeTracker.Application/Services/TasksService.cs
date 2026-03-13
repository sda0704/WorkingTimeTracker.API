using WorkingTimeTracker.Core.Models;


using WorkingTimeTracker.DataAccess.Repositories;

namespace WorkingTimeTracker.Application.Services;

public class TasksService
{
    private readonly ITasksRepository _taskRepository;
    private readonly IProjectsRepository _projectsrepository;

   
    

    public TasksService(ITasksRepository tasksRepository, IProjectsRepository projectsRepository)
    {
        _taskRepository = tasksRepository;   
        _projectsrepository = projectsRepository;
    }

    public async Task<List<Tasks>> GetAllTasks()
    {
         return await _taskRepository.Get();
    }
    public async Task<Tasks?> GetTaskById(Guid taskId)
    {
        return await _taskRepository.GetById(taskId);
    }

    public async Task<Guid> CreateTask(Tasks task)
    {
        var result = await _projectsrepository.GetById(task.ProjectId);
        if (result == null)
        {
            throw new InvalidOperationException("Такого проекта не существует");
        }

            return await _taskRepository.Create(task);
        
    }
    public async Task<bool> UpdateTask(Tasks task)
    { 
      return await _taskRepository.Update(task);  
    }
    public async Task<bool> DeleteTask(Guid taskId)
    { 
        return await _taskRepository.Delete(taskId);
    }
}
