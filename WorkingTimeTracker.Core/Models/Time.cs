

namespace WorkingTimeTracker.Core.Models
{
    public class Time
    {

        public Time(Guid id, Guid taskId, DateTime date, decimal hours, string description)
        {
            Id = id;
            TaskId = taskId;
            Date = date;
            Hours = hours;
            Description = description;
        }
        public Guid Id { get; }
        public Guid TaskId { get; }
        public DateTime Date { get; } 
        public decimal Hours { get; }
        public string Description { get; }


        public static (Time time, string error) Create(Guid id, Guid taskId, DateTime date, decimal hours, string description)
        {
            List<string> errors = new List<string>();
            DateTime TodayDate = DateTime.Now.Date;

            if (hours  <= 0 || hours > 24)
            {
                errors.Add("Кол-во часов не может быть отрицательным или более 24 часов");
            }

            if (String.IsNullOrWhiteSpace(description))
            {
                errors.Add("Описание не может быть пустым");
            }
            if(taskId == Guid.Empty)
            {
                errors.Add("Id задачи не может быть пустым");
            }
            if(date > TodayDate)
            {
                errors.Add("Дата не может быть в будущем");
            }



           if(errors.Any())
                return (null, string.Join(", ", errors));

           var time = new Time(id, taskId, date, hours, description);
            return (time, string.Empty);
        }
    }
}
