namespace WorkingTimeTracker.Application.DTOs
{
    public class TaskDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public Guid ProjectId { get; set; }
    }
}
