using Microsoft.AspNetCore.Mvc;
using SpiralDev.Api.Dtos;
using SpiralDev.Api.Services;

namespace SpiralDev.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExecuteController : ControllerBase
{
    private readonly ICodeRunner _runner;

    public ExecuteController(ICodeRunner runner)
    {
        _runner = runner;
    }

    /// <summary>
    /// Ejecuta código C o C# y devuelve la salida del programa.
    /// POST /api/execute
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ExecuteResponseDto>> Execute([FromBody] ExecuteRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
        {
            return BadRequest(new { message = "El campo 'code' no puede estar vacío." });
        }

        try
        {
            var result = await _runner.ExecuteAsync(
                request.Language, request.Code, request.Stdin ?? string.Empty);

            return Ok(new ExecuteResponseDto
            {
                Success = result.Success,
                Stdout = result.Stdout,
                Stderr = result.Stderr,
                ExitCode = result.ExitCode,
                Status = result.Status
            });
        }
        catch (Exception)
        {
            // Error al contactar al motor de ejecución (no es culpa del código del alumno)
            return StatusCode(502, new { message = "No se pudo ejecutar el código. Intentalo de nuevo en unos segundos." });
        }
    }
}
