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
    public class ProjectController : ControllerBase
    {

        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService) { _projectService = projectService; }


        [HttpGet]

        public async Task<ActionResult<List<ProjectDTO>>> GetAll()
        {
            var allprojects = await _projectService.GetAllProjects();

            var dtoList = allprojects.Select(p => ProjectMapper.ToDto(p)).ToList();
            return Ok(dtoList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetById(Guid id)
        {
            var project = await _projectService.GetById(id);
            if (project == null)
            {
                return NotFound();
            }
            var dto = ProjectMapper.ToDto(project);
            return Ok(dto);
        }

        [HttpPost]

        public async Task<ActionResult<ProjectDTO>> Create(CreateProjectDTO dto)
        {
            var project = ProjectMapper.ToDomain(dto);

            var createdId = await _projectService.CreateProject(project);

            var createdProject = await _projectService.GetById(createdId);

            var resultDto = ProjectMapper.ToDto(createdProject);

            return CreatedAtAction(nameof(GetById), new { id = createdId }, resultDto);

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<ProjectDTO>> Update( Guid id, UpdateProjectDTO dto)
        {
            var existingProject = await _projectService.GetById(id);
            if (existingProject == null) return NotFound();

            var project = ProjectMapper.ToDomain(dto, id);

            var updated = await _projectService.UpdateProject(id, project.Title, project.Code, project.IsActive);

            if(!updated) return NotFound();
            var resultDTO = ProjectMapper.ToDto(project);


            return Ok(resultDTO);


        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<ProjectDTO>> Delete(Guid id)
        {
            var result = await _projectService.DeleteProject(id);

            if(!result) return NotFound();  

            return NoContent();

            

        }
    }
}
