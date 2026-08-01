namespace SpiralDev.Api.Dtos;

/// <summary>
/// Resumen liviano de una lección (sin el ContentMarkdown, que se pide aparte
/// con GET /api/lessons/{id} para no pesar en el listado).
/// </summary>
public class LessonSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
}
