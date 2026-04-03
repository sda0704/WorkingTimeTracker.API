using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.Application.DTOs;

namespace WorkingTimeTracker.Application.Mappers;

public class TaskMapper
{
    public static Tasks ToDomain(CreateTaskDTO dto)
    {
        var (task, error) = Tasks.Create(

            id: Guid.NewGuid(),
            projectId: dto.ProjectId,
            title: dto.Title,
            isActive: dto.IsActive
            

            );
        if (!string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException(error);
        }
        return task;
    }
    public static TaskDTO ToDto (Tasks task)
    {
        return new TaskDTO { 
            
            Id  = task.Id, 
            Title = task.Title,
            IsActive = task.IsActive,
            ProjectId = task.ProjectId,
        
        };

    }

    public static Tasks ToDomain(UpdateTaskDTO dto, Guid id)
    {
        var (task, error) = Tasks.Create(
            id: id,
            title: dto.Title,
            isActive: dto.IsActive,
            projectId: dto.ProjectId

            );
        if (!string.IsNullOrEmpty(error)) { throw new InvalidOperationException(error); }

        return task;
    }
}
