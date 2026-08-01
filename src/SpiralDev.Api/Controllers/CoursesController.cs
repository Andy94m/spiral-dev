using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiralDev.Api.Data;
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
}
