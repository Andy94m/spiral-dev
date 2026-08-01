namespace SpiralDev.Api.Models;

/// <summary>
/// Tipos de ejercicio disponibles (la app tendrá ambos, como pediste).
/// </summary>
public enum ExerciseType
{
    MultipleChoice,   // Conceptos: elegir la opción correcta
    CodeWriting       // Práctica: escribir código real y ejecutarlo
}

/// <summary>
/// Un ejercicio dentro de una lección.
/// Ej: "Ingresar 3 valores enteros y calcular su promedio" (prob. 2 del cap. 2).
/// </summary>
public class Exercise
{
    public int Id { get; set; }
    public int Order { get; set; }
    public ExerciseType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Statement { get; set; } = string.Empty;

    // Para MultipleChoice: la pregunta y opciones (separadas por ;)
    public string? Question { get; set; }
    public string? Options { get; set; }
    public int CorrectOptionIndex { get; set; }

    // Para CodeWriting: código inicial (stub) que el alumno completa
    public string StarterCode { get; set; } = string.Empty;

    /// <summary>
    /// Lo que debe mostrar el programa al ejecutarse correctamente.
    /// El backend lo compara con la salida real para validar.
    /// </summary>
    public string ExpectedOutput { get; set; } = string.Empty;

    /// <summary>
    /// IDs de temas previos que este ejercicio necesita para resolverse.
    /// Esto es la CLAVE de la pedagogía en espiral: "no olvidas lo aprendido".
    /// Ej: un ejercicio del cap. 3 puede requerir saber el cap. 2.
    /// </summary>
    public List<int> RequiredTopicIds { get; set; } = [];

    // Relación: un Exercise pertenece a una Lesson
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
}
