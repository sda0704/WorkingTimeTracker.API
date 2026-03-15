namespace WorkingTimeTracker.Application.DTOs;

public class CreateProjectDTO
{
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
