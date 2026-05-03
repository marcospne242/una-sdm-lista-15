using Microsoft.AspNetCore.Mvc;
using GreenDriveApi.Data;
using GreenDriveApi.Models;

[Route("api/[controller]")]
[ApiController]
public class BateriasController : ControllerBase
{
    private readonly AppDbContext _context;
    public BateriasController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<ActionResult> PostBateria(Bateria bateria)
    {
        _context.Baterias.Add(bateria);
        await _context.SaveChangesAsync();
        return Ok(bateria);
    }

[HttpPatch("{id}/saude")]
public async Task<ActionResult> PatchSaude(int id, [FromBody] int novaSaude)
{
    var bateria = await _context.Baterias.FindAsync(id);
    if (bateria == null) return NotFound();

    // REGRA DE OURO: Se a saúde atual for <= 10%, ela é considerada "Inativa".
    // Tentar aumentar esse valor (fraude) deve retornar Conflict (409).
    if (bateria.SaudeBateria <= 10 && novaSaude > bateria.SaudeBateria)
    {
        return Conflict("Tentativa de fraude de dados: Bateria inativa não pode ter a saúde aumentada.");
    }

    bateria.SaudeBateria = novaSaude;
    await _context.SaveChangesAsync();
    return Ok(bateria);
}
}