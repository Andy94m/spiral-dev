using SpiralDev.Api.Models;

namespace SpiralDev.Api.Dtos;

/// <summary>
/// Desafío (Exercise) tal como lo ve el alumno: enunciado y datos de entrada.
/// NOTA: NO se exponen CorrectOptionIndex ni ExpectedOutput (las respuestas),
/// porque la validación ocurre en el servidor (Fase 3), no en el cliente.
/// </summary>
public class ExerciseDto
{
    public int Id { get; set; }
    public int Order { get; set; }
    public ExerciseType Type { get; set; }   // MultipleChoice | CodeWriting (serializado como texto)
    public string Title { get; set; } = string.Empty;
    public string Statement { get; set; } = string.Empty;

    // Solo para MultipleChoice
    public string? Question { get; set; }
    public string? Options { get; set; }     // opciones separadas por ';'

    // Solo para CodeWriting
    public string? StarterCode { get; set; } // código inicial que el alumno completa
}
