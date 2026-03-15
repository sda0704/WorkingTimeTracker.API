
using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.Application.DTOs;

namespace WorkingTimeTracker.Application.Mappers;

public class ProjectMapper
{
    public static Project ToDomain(CreateProjectDTO dto)
    {
        var (project, error) = Project.Create(
             id: Guid.NewGuid(),
             title: dto.Title,
             code: dto.Code,
             isActive: dto.IsActive
             );

        if (!string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException(error);
        }

        return project;
    }

    public static Project ToDomain(UpdateProjectDTO dto, Guid id)
    {
        var (project, error) = Project.Create(
            id: dto.Id,
            title: dto.Title,
            code: dto.Code,
            isActive: dto.IsActive
            
            );
        if (!string.IsNullOrEmpty(error)) { throw new InvalidOperationException(error); }

        return project;
    }
    public static ProjectDTO ToDto(Project project)
    {
        return new ProjectDTO
        {
            Id = project.Id,
            Title = project.Title,
            Code = project.Code,
            IsActive = project.IsActive,

        };
    }


}
