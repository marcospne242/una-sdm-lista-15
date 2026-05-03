using Microsoft.AspNetCore.Mvc;
using GreenDriveApi.Data;
using GreenDriveApi.Models;

[Route("api/[controller]")]
[ApiController]
public class TelemetriaController : ControllerBase
{
    private readonly AppDbContext _context;
    public TelemetriaController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<ActionResult> PostTelemetria(RegistroTelemetria telemetria)
    {
        var bateria = await _context.Baterias.FindAsync(telemetria.BateriaId);
        if (bateria == null) return NotFound("Bateria não encontrada.");

        if (telemetria.Temperatura > 85)
        {
            Console.WriteLine($"⚠️ ALERTA DE SEGURANÇA: Risco térmico detectado na bateria {bateria.NumeroSerie}! Registro bloqueado para investigação.");
            return BadRequest("Risco térmico detectado. Registro bloqueado.");
        }

        _context.RegistrosTelemetria.Add(telemetria);
        await _context.SaveChangesAsync();
        return Ok(telemetria);
    }
}