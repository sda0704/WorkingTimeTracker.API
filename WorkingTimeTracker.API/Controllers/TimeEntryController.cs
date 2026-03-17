using WorkingTimeTracker.DataAccess;
using WorkingTimeTracker.Application.Abstractions;
using WorkingTimeTracker.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkingTimeTracker.Core.Models;
using Microsoft.EntityFrameworkCore;
using WorkingTimeTracker.Application.Mappers;
using WorkingTimeTracker.DataAccess.Repositories;

namespace WorkingTimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeService _timeService;

        public TimeEntryController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        [HttpGet]

        public async Task<ActionResult<List<TimeEntryDTO>>> GetAll(
            [FromQuery] DateTime? date = null,
            [FromQuery] DateTime? month = null)
        {

            List<Time> timeEntries;

            if (date.HasValue)
            {
                timeEntries = await _timeService.GetTimesByDate(date.Value);
            }
            else if(month.HasValue)
            {
                timeEntries = await _timeService.GetTimeByMonth(month.Value);
            }
            else
            {
                timeEntries = await _timeService.GetTimes();
            }


    

            var dtoTimeEntry = timeEntries.Select(p => TimeEntryMapper.ToDto(p)).ToList();

            return Ok(dtoTimeEntry);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<TimeEntryDTO>> GetById(Guid id)
        {
            var timeEntry = await _timeService.GetTimesById(id);

            if (timeEntry == null) return NotFound();

            var dtoTimeEntry = TimeEntryMapper.ToDto(timeEntry);

            return Ok(dtoTimeEntry);
        }
        [HttpPost]

        public async Task<ActionResult<TimeEntryDTO>> Create(CreateTimeEntryDTO dto)
        {
            var timeEntry = TimeEntryMapper.ToDomain(dto);

            var createdId = await _timeService.CreateTimes(timeEntry);

            var resultDTO =  TimeEntryMapper.ToDto(timeEntry);

            return CreatedAtAction(nameof(GetById), new { id = createdId }, resultDTO);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<TimeEntryDTO>> Update(Guid id, UpdateTimeEntryDTO dto)
        {
            var existingTimeEntry = await _timeService.GetTimesById(id);
            if(existingTimeEntry == null) return NotFound();

            var TimeEntry = TimeEntryMapper.ToDomain(dto, id);

            var updatedTimeEntry = await _timeService.UpdateTimes(TimeEntry);
            if (!updatedTimeEntry) return NotFound();

            var resultDTO = TimeEntryMapper.ToDto(TimeEntry);
            return Ok(resultDTO);



        }

        [HttpDelete("{id}")]


        public async Task<ActionResult> Delete (Guid id)
        {
            var result = await _timeService.DeleteTimes(id);

            if (!result) return NotFound();

            return NoContent();
        }
        [HttpGet("summary/daily")]

        public async Task<ActionResult<DailySummaryDTO>> GetDailySummary([FromQuery] DateTime date)
        {
            var summary = await _timeService.GetDailySummary(date);
            return Ok(summary);
        }
    }
}
