using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiralDev.Api.Data;
using SpiralDev.Api.Dtos;

namespace SpiralDev.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly SpiralDbContext _context;

    public LessonsController(SpiralDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Devuelve una lección completa: Markdown + sus desafíos (sin respuestas).
    /// GET /api/lessons/{id}
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LessonDetailDto>> GetLesson(int id)
    {
        var lesson = await _context.Lessons
            .AsNoTracking() // Lectura pura
            .Where(l => l.Id == id)
            .Select(l => new LessonDetailDto
            {
                Id = l.Id,
                Title = l.Title,
                Order = l.Order,
                ContentMarkdown = l.ContentMarkdown,
                Exercises = l.Exercises
                    .OrderBy(e => e.Order)
                    .Select(e => new ExerciseDto
                    {
                        Id = e.Id,
                        Order = e.Order,
                        Type = e.Type,
                        Title = e.Title,
                        Statement = e.Statement,
                        Question = e.Question,
                        Options = e.Options,
                        StarterCode = e.StarterCode
                        // CorrectOptionIndex y ExpectedOutput se omiten a propósito
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (lesson is null)
        {
            return NotFound(); // 404
        }

        return Ok(lesson); // 200
    }
}
