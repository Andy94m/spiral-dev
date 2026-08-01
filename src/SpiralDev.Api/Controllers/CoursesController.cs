using Microsoft.AspNetCore.Mvc;
using SpiralDev.Api.Models;

namespace SpiralDev.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    /// <summary>
    /// Devuelve las carreras de aprendizaje disponibles.
    /// GET /api/courses
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetCourses()
    {
        var courses = new[]
        {
            new Course
            {
                Id = 1,
                Name = "C",
                Description = "Bajo nivel: memoria, punteros, hardware"
            },
            new Course
            {
                Id = 2,
                Name = "C#",
                Description = "Orientado a objetos: POO, LINQ, .NET"
            }
        };

        return Ok(courses);
    }
}
