namespace SpiralDev.Api.Dtos;

/// <summary>
/// Detalle completo de una lección para la pantalla de lectura:
/// el Markdown a renderizar + sus desafíos (sin las respuestas correctas).
/// </summary>
public class LessonDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public string ContentMarkdown { get; set; } = string.Empty;
    public List<ExerciseDto> Exercises { get; set; } = [];
}
