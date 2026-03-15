namespace WorkingTimeTracker.Application.DTOs
{
    public class UpdateProjectDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
