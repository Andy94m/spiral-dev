namespace SpiralDev.Api.Dtos;

/// <summary>
/// Lo que envía el alumno para ejecutar: código + entrada opcional.
/// </summary>
public class ExecuteRequestDto
{
    public string Language { get; set; } = "c";  // "c" | "csharp"
    public string Code { get; set; } = string.Empty;
    public string? Stdin { get; set; }            // datos de entrada (lo que leería scanf)
}

/// <summary>
/// Resultado de la ejecución tal como lo ve el frontend.
/// </summary>
public class ExecuteResponseDto
{
    public bool Success { get; set; }
    public string Stdout { get; set; } = string.Empty;
    public string Stderr { get; set; } = string.Empty;
    public int? ExitCode { get; set; }
    public string Status { get; set; } = string.Empty;
}
