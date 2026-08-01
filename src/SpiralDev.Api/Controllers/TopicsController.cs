using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiralDev.Api.Data;
using SpiralDev.Api.Dtos;

namespace SpiralDev.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly SpiralDbContext _context;

    public TopicsController(SpiralDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Devuelve un capítulo con sus lecciones ordenadas (listado liviano, sin Markdown).
    /// GET /api/topics/{id}
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TopicDetailDto>> GetTopic(int id)
    {
        var topic = await _context.Topics
            .AsNoTracking() // Lectura pura: no rastreamos cambios en memoria (ahorra memoria)
            .Where(t => t.Id == id)
            .Select(t => new TopicDetailDto
            {
                Id = t.Id,
                Title = t.Title,
                Order = t.Order,
                // Proyección: EF Core traduce este Select a SQL y trae SOLO estos campos.
                // El Include() no hace falta acá: la proyección genera el JOIN automáticamente.
                Lessons = t.Lessons
                    .OrderBy(l => l.Order)
                    .Select(l => new LessonSummaryDto
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Order = l.Order
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (topic is null)
        {
            return NotFound(); // 404: el capítulo no existe
        }

        return Ok(topic); // 200 con el detalle
    }
}
