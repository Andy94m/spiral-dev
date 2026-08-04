using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpiralDev.Api.Services;

/// <summary>
/// CodeRunner que ejecuta código contra la instancia pública de Judge0 CE
/// (https://ce.judge0.com). Sin API key, sin costo.
/// Los IDs de lenguaje (C = 50, C# = 51) salen de GET /languages de Judge0.
/// </summary>
public class Judge0CodeRunner : ICodeRunner
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public Judge0CodeRunner(HttpClient http)
    {
        _http = http;
    }

    public async Task<CodeRunResult> ExecuteAsync(string language, string sourceCode, string stdin, CancellationToken ct = default)
    {
        var languageId = language switch
        {
            "c" => 50,          // C (GCC 9.2.0)
            "csharp" => 51,     // C# (Mono 6.6.0)
            _ => throw new ArgumentException($"Lenguaje no soportado: {language}")
        };

        var request = new
        {
            source_code = sourceCode,
            language_id = languageId,
            stdin = stdin ?? string.Empty
        };

        // ?wait=true hace que Judge0 responda recién cuando terminó de ejecutar
        // (sincrónico) — perfecto para nuestra app, no hace falta hacer polling.
        using var response = await _http.PostAsJsonAsync(
            "/submissions?base64_encoded=false&wait=true", request, JsonOpts, ct);

        if (!response.IsSuccessStatusCode)
        {
            var errBody = await response.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"Judge0 respondió {(int)response.StatusCode}: {errBody}");
        }

        var result = await response.Content.ReadFromJsonAsync<Judge0Submission>(JsonOpts, ct);
        if (result is null)
        {
            throw new HttpRequestException("Judge0 devolvió una respuesta vacía");
        }

        return new CodeRunResult
        {
            Success = result.Status?.Id == 3, // 3 = Accepted (ver tabla de estados)
            Stdout = result.Stdout ?? string.Empty,
            Stderr = result.Stderr ?? string.Empty,
            ExitCode = result.ExitCode,
            Status = result.Status?.Description ?? string.Empty
        };
    }

    private class Judge0Submission
    {
        [JsonPropertyName("stdout")] public string? Stdout { get; set; }
        [JsonPropertyName("stderr")] public string? Stderr { get; set; }
        [JsonPropertyName("exit_code")] public int? ExitCode { get; set; }
        [JsonPropertyName("status")] public Judge0Status? Status { get; set; }
    }

    private class Judge0Status
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("description")] public string? Description { get; set; }
    }
}
