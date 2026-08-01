namespace SpiralDev.Api.Models;

/// <summary>
/// Representa una carrera de aprendizaje (C o C#).
/// </summary>
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Relación inversa: un Course tiene muchos Topics (capítulos)
    public List<Topic> Topics { get; set; } = [];
}
