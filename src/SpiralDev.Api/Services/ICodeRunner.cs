namespace SpiralDev.Api.Services;

/// <summary>
/// Motor de ejecución de código. El resto de la app habla con ESTA interfaz,
/// no con Judge0 directamente — así podemos cambiar de proveedor (URL, API,
/// instancia propia) tocando una sola implementación.
/// </summary>
public interface ICodeRunner
{
    /// <summary>
    /// Ejecuta código fuente y devuelve el resultado del programa.
    /// </summary>
    Task<CodeRunResult> ExecuteAsync(string language, string sourceCode, string stdin, CancellationToken ct = default);
}

/// <summary>
/// Resultado de una ejecución: lo que imprimió el programa, los errores
/// y si terminó bien (ExitCode 0) o falló (exit code, timeout, etc.).
/// </summary>
public class CodeRunResult
{
    public bool Success { get; init; }
    public string Stdout { get; init; } = string.Empty;
    public string Stderr { get; init; } = string.Empty;
    public int? ExitCode { get; init; }
    public string Status { get; init; } = string.Empty; // Accepted, Compilation Error, Timeout...
}
