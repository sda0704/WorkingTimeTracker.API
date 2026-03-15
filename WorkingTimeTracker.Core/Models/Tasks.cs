using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.Core.Models
{
    public class Tasks
    {
        private Tasks(Guid id, string title, bool isActive, Guid projectId)
        {
            Id = id;
            Title = title;
            IsActive = isActive;
            ProjectId = projectId;
        }

        public Guid Id { get; }
        public string Title { get; }
        public bool IsActive { get; }
        public Guid ProjectId { get; }

        public static (Tasks Tasks, string error) Create(Guid id, string title, bool isActive, Guid projectId)
        {
            var errors = new List<string>();


            if (string.IsNullOrEmpty(title))
            {
                errors.Add( "Название не может быть пустым");
            }
            if (projectId == Guid.Empty)
            {
                errors.Add("Проект должен быть указан");
            }
            if(errors.Any())
                return (null, string.Join(", ", errors));

            var task = new Tasks(id, title, isActive, projectId);
            return (task, string.Empty);
        }
    }
}
