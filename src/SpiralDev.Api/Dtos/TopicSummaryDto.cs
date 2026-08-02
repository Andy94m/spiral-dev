namespace SpiralDev.Api.Dtos;

/// <summary>
/// Resumen liviano de un capítulo (Topic) para el listado de capítulos
/// de una carrera. No incluye las lecciones (se piden con GET /api/topics/{id}).
/// </summary>
public class TopicSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
}
