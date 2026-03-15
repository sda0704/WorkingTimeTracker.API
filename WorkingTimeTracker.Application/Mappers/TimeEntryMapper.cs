using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.Application.DTOs;

namespace WorkingTimeTracker.Application.Mappers;

public class TimeEntryMapper
{

    public static Time ToDomain(CreateTimeEntryDTO dto)
    {
        var (time, error) = Time.Create(
            id: Guid.NewGuid(),
            taskId: dto.TaskId,
            date: dto.Date,
            hours: dto.Hours,
            description: dto.Description
            );
        if (!string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException(error);

        }
        return time;
    }

    public static TimeEntryDTO ToDto(Time time)
    {
        return new TimeEntryDTO
        {
            Id = time.Id,
            TaskId = time.TaskId,
            Date = time.Date,
            Hours = time.Hours,
            Description = time.Description
        };
    } 
}
