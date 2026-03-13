using Microsoft.EntityFrameworkCore;
using WorkingTimeTracker.Core.Models;
using WorkingTimeTracker.DataAccess.Entities;

namespace WorkingTimeTracker.DataAccess.Repositories;

public class TimeEntryRepository : ITimeEntryRepository
{

    private readonly AppDbContext _context;

    public TimeEntryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Time>> Get()
    {
        var timeEntities = await _context.TimeEntries
            .AsNoTracking()
            .ToListAsync();

        var time = timeEntities
            .Select(b => Time.Create(b.Id, b.TaskId, b.Date, b.Hours, b.Description).time)
            .ToList();

        return time;
    }


    public async Task<List<Time>> GetByDate(DateTime selectedDate)
    {
        var entities = await _context.TimeEntries
              .Where(t => t.Date.Date == selectedDate.Date)
              .AsNoTracking()
              .ToListAsync();



        return entities
            .Select(e => Time.Create(e.Id, e.TaskId, e.Date, e.Hours, e.Description).time)
            .ToList();

    }
    public async Task<List<Time>> GetByMonth(DateTime selectedDate)
    {
        var entiites = await _context.TimeEntries
            .Where(t => t.Date.Year == selectedDate.Year && t.Date.Month == selectedDate.Month)
            .AsNoTracking()
            .ToListAsync();

        return entiites
            .Select(e => Time.Create(e.Id, e.TaskId, e.Date, e.Hours, e.Description).time)
            .ToList();
    }

    public async Task<Time?> GetById(Guid id)
    {

        var entities = await _context.TimeEntries
            .FirstOrDefaultAsync(t => t.Id == id);

        if (entities == null) return null;

        return Time.Create(entities.Id, entities.TaskId, entities.Date, entities.Hours, entities.Description).time;
    }

    public async Task<Guid> Create(Time time)
    {
        if (time == null) throw new ArgumentNullException(nameof(time));

        var entities = new TimeEntryEntity
        {
            Id = time.Id,
            TaskId = time.TaskId,
            Date = time.Date,
            Hours = time.Hours,
            Description = time.Description
        };

        await _context.TimeEntries.AddAsync(entities);
        await _context.SaveChangesAsync();

        return entities.Id;
    }

    public async Task<bool> Update(Time time)
    {
        if (time == null) throw new ArgumentNullException(nameof(time));

        var rowsAffected = await _context.TimeEntries
            .Where(t => t.Id == time.Id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(t => t.TaskId, time.TaskId)
            .SetProperty(t => t.Date, time.Date)
            .SetProperty(t => t.Hours, time.Hours)
            .SetProperty(t => t.Description, time.Description));

        return rowsAffected > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var rowsAffected = await _context.TimeEntries
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();

        return rowsAffected > 0;
    }


    //Сложно
    public async Task<decimal> GetTotalHoursForDate(DateTime date, Guid? excludeEntryId = null)
    {

        var entities = await _context.TimeEntries
           .Where(t => t.Date.Date == date.Date)
           .AsNoTracking()
           .ToListAsync();


        var filteredEntities = entities;

        if (excludeEntryId.HasValue)
        {
            filteredEntities = entities.Where(e => e.Id != excludeEntryId.Value).ToList();


        }
        return filteredEntities.Sum(e => e.Hours);

    }
}
