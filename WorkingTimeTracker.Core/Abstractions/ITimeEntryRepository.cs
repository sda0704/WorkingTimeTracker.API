using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.DataAccess.Repositories
{
    public interface ITimeEntryRepository
    {
        Task<Guid> Create(Time time);
        Task<bool> Delete(Guid id);
        Task<List<Time>> Get();
        Task<List<Time>> GetByDate(DateTime selectedDate);
        Task<Time?> GetById(Guid id);
        Task<List<Time>> GetByMonth(DateTime selectedDate);
        Task<decimal> GetTotalHoursForDate(DateTime date, Guid? excludeEntryId = null);
        Task<bool> Update(Time time);
    }
}