

using System.Timers;
using WorkingTimeTracker.Application.Abstractions;
using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.DataAccess.Repositories;

namespace WorkingTimeTracker.Application.Services;

public class TimeService : ITimeService
{
    private readonly ITimeEntryRepository _timeEntryService;
    private readonly IProjectsRepository _projectsRepository;
    private readonly ITasksRepository _taskRepository;

    public TimeService(ITimeEntryRepository timeService, IProjectsRepository projectRepository, ITasksRepository tasksRepository)
    {
        _timeEntryService = timeService;
        _projectsRepository = projectRepository;
        _taskRepository = tasksRepository;
    }

    public async Task<List<Time>> GetTimes()
    {

        return await _timeEntryService.Get();
    }
    public async Task<Time?> GetTimesById(Guid timeId)
    {

        return await _timeEntryService.GetById(timeId);
    }
    public async Task<Guid> CreateTimes(Time time)
    {
        var TotalHoursForDay = await _timeEntryService.GetTotalHoursForDate(time.Date, null);
        if (TotalHoursForDay + time.Hours > 24)
        {
            throw new InvalidOperationException("Введенное количество часов превышает 24 часа!!");
        }
        var ActiveTask = await _taskRepository.GetById(time.TaskId);
        if (ActiveTask == null) throw new InvalidOperationException("Такой задачи не существует");
        if (ActiveTask.IsActive == false) throw new InvalidOperationException("Задача неактивна!");


        return await _timeEntryService.Create(time);
    }
    public async Task<bool> UpdateTimes(Time time)
    {
        var TotalHoursForDay = await _timeEntryService.GetTotalHoursForDate(time.Date, time.Id);
        if (TotalHoursForDay + time.Hours > 24)
        {
            throw new InvalidOperationException("Введенное количество часов превышает 24 часа!!");
        }
        var ActiveTask = await _taskRepository.GetById(time.TaskId);
        if (ActiveTask == null) throw new InvalidOperationException("Такой задачи не существует");
        if (ActiveTask.IsActive == false) throw new InvalidOperationException("Задача неактивна!");
        return await _timeEntryService.Update(time);
    }
    public async Task<bool> DeleteTimes(Guid timeId)
    {
        return await _timeEntryService.Delete(timeId);
    }
}
