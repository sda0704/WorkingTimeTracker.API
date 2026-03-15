namespace WorkingTimeTracker.Application.DTOs;

public class TimeEntryDTO
{

    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public DateTime Date { get; set; }

    public decimal Hours { get; set; }

    public string Description { get; set; } = string.Empty;
}
