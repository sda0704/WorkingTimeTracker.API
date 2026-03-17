using WorkingTimeTracker.DataAccess;
using WorkingTimeTracker.Application.Abstractions;
using WorkingTimeTracker.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkingTimeTracker.Core.Models;
using Microsoft.EntityFrameworkCore;
using WorkingTimeTracker.Application.Mappers;


namespace WorkingTimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService) { _tasksService = tasksService; }

        [HttpGet]

        public async Task<ActionResult<List<TaskDTO>>> GetAll()
        {
            var alltasks = await _tasksService.GetAllTasks();
            var dtoList = alltasks.Select(p => TaskMapper.ToDto(p)).ToList();

            return Ok(dtoList);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<TaskDTO>> GetById(Guid id)
        {
            var task = await _tasksService.GetTaskById(id);
            if (task == null) return NotFound();

            var dto = TaskMapper.ToDto(task);
            return Ok(dto);

        }

        [HttpPost]

        public async Task<ActionResult<TaskDTO>> Create(CreateTaskDTO dto)
        {
            var task = TaskMapper.ToDomain(dto);

            var createdId = await _tasksService.CreateTask(task);

          

            var resultDto = TaskMapper.ToDto(task);

            return CreatedAtAction(nameof(GetById), new { id = createdId }, resultDto);

        }
        [HttpPut("{id}")]

        public async Task<ActionResult<TaskDTO>> Update(Guid id, UpdateTaskDTO dto)
        {
            var existingTask = await _tasksService.GetTaskById(id);

            if (existingTask == null) return NotFound();

            var task = TaskMapper.ToDomain(dto, id);

            var updated = await _tasksService.UpdateTask(task);

            if (!updated) return NotFound();

            var resultDTO = TaskMapper.ToDto(task);

            return Ok(resultDTO);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _tasksService.DeleteTask(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
