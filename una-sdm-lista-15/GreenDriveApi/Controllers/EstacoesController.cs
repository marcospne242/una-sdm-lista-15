using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenDriveApi.Data;
using GreenDriveApi.Models;

namespace GreenDriveApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstacaoCarga>>> GetEstacoes()
        {
            return await _context.EstacoesCarga.ToListAsync();
        }

        // POST: api/Estacoes
        [HttpPost]
        public async Task<ActionResult<EstacaoCarga>> PostEstacao(EstacaoCarga estacao)
        {
            _context.EstacoesCarga.Add(estacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstacoes), new { id = estacao.Id }, estacao);
        }
    }
}