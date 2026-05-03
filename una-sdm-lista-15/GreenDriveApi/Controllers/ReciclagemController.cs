using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenDriveApi.Data;
using GreenDriveApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ReciclagemController : ControllerBase
{
    private readonly AppDbContext _context;
    public ReciclagemController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<ActionResult> PostOrdem(OrdemReciclagem ordem)
    {
        var bateria = await _context.Baterias.FindAsync(ordem.BateriaId);
        var estacao = await _context.EstacoesCarga.FindAsync(ordem.EstacaoId);

        if (bateria == null || estacao == null) return BadRequest("Bateria ou Estação inválida.");

        if (bateria.SaudeBateria > 60)
            return BadRequest("Bateria com saúde superior a 60%. Encaminhe para o programa de Reuso Doméstico (Second Life) em vez de reciclagem.");

        if (estacao.TipoCarga == "Ultra-Rapida")
            ordem.CustoProcessamento += 250;

        _context.OrdensReciclagem.Add(ordem);
        await _context.SaveChangesAsync();
        return Ok(ordem);
    }
}