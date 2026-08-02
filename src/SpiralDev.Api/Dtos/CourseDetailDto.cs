namespace SpiralDev.Api.Dtos;

/// <summary>
/// Detalle de una carrera (Course) con sus capítulos ordenados,
/// para la pantalla "Capítulos" del frontend.
/// </summary>
public class CourseDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<TopicSummaryDto> Topics { get; set; } = [];
}
