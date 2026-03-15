namespace WorkingTimeTracker.Application.DTOs;

public class CreateTaskDTO
{
    public string Title { get; set; } = string.Empty;

    public bool IsActive { get; set; }
    public Guid ProjectId { get; set; }
}
