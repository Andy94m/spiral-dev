namespace SpiralDev.Api.Models;

/// <summary>
/// Una sección dentro de un tema.
/// Ej: "Operadores aritméticos" (sección del cap. 2).
/// </summary>
public class Lesson
{
    public int Id { get; set; }
    public int Order { get; set; }
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// El contenido teórico de la lección en formato Markdown.
    /// Así el frontend puede renderizarlo bonito (títulos, código, tablas).
    /// </summary>
    public string ContentMarkdown { get; set; } = string.Empty;

    // Relación: una Lesson pertenece a un Topic
    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;

    // Relación inversa: una Lesson tiene muchos Exercises
    public List<Exercise> Exercises { get; set; } = [];
}
