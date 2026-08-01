namespace SpiralDev.Api.Dtos;

/// <summary>
/// Detalle de un capítulo (Topic) para la pantalla "Capítulos" del frontend.
/// Es un DTO: la forma de los datos que expone la API, desacoplada del modelo interno.
/// </summary>
public class TopicDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<LessonSummaryDto> Lessons { get; set; } = [];
}
