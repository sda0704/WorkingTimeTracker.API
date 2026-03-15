using WorkingTimeTracker.Core.Models;

namespace WorkingTimeTracker.Application.Abstractions
{
    public interface ITimeService
    {
        Task<Guid> CreateTimes(Time time);
        Task<bool> DeleteTimes(Guid timeId);
        Task<List<Time>> GetTimes();
        Task<Time?> GetTimesById(Guid timeId);
        Task<bool> UpdateTimes(Time time);
    }
}