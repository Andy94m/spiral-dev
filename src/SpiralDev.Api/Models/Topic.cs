namespace SpiralDev.Api.Models;

/// <summary>
/// Un tema (capítulo) dentro de una carrera.
/// Ej: "Fundamentos de C" o "Control de Flujo" (cap. 2 y 3 del libro).
/// </summary>
public class Topic
{
    public int Id { get; set; }
    public int Order { get; set; }              // Posición dentro de la carrera (cap 2 va antes que cap 3)
    public string Title { get; set; } = string.Empty;

    // Relación: un Topic pertenece a un Course
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    // Relación inversa: un Topic tiene muchas Lessons
    public List<Lesson> Lessons { get; set; } = [];
}
