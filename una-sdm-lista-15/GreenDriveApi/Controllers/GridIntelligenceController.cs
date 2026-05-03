using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenDriveApi.Data;

[Route("api/intelligence")]
[ApiController]
public class GridIntelligenceController : ControllerBase
{
    private readonly AppDbContext _context;
    public GridIntelligenceController(AppDbContext context) => _context = context;

    [HttpGet("carbon-footprint")]
    public async Task<IActionResult> GetCarbonFootprint()
    {
        await Task.Delay(3000); // Simulação de latência IoT

        var dados = await _context.OrdensReciclagem
            .Include(o => o.Estacao)
            .GroupBy(o => o.Estacao!.Localizacao)
            .Select(g => new {
                Cidade = g.Key,
                CustoTotalReciclagem = g.Sum(x => x.CustoProcessamento)
            }).ToListAsync();

        return Ok(dados);
    }
}