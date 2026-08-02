using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiralDev.Api.Data;
using SpiralDev.Api.Dtos;
using SpiralDev.Api.Models;

namespace SpiralDev.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly SpiralDbContext _context;

    public CoursesController(SpiralDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Devuelve las carreras de aprendizaje disponibles, leídas de la base de datos.
    /// GET /api/courses
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        var courses = await _context.Courses
            .OrderBy(c => c.Id)
            .ToListAsync();

        return Ok(courses);
    }

    /// <summary>
    /// Devuelve una carrera con sus capítulos ordenados, para la pantalla
    /// "Capítulos" del frontend. Proyección a DTO para no exponer datos internos.
    /// GET /api/courses/{id}
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CourseDetailDto>> GetCourse(int id)
    {
        var course = await _context.Courses
            .Where(c => c.Id == id)
            .Select(c => new CourseDetailDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Topics = c.Topics
                    .OrderBy(t => t.Order)
                    .Select(t => new TopicSummaryDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Order = t.Order,
                    })
                    .ToList(),
            })
            .AsNoTracking()
            .SingleOrDefaultAsync();

        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }
}
