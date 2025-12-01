using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;
using ProjetoMidasAPI.Models;
using Microsoft.AspNetCore.Authorization;


namespace ProjetoMidasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LancamentosController : ControllerBase
    {
        private readonly AppDbContext _db;
        public LancamentosController(AppDbContext db) { _db = db; }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetAll() =>
            Ok(await _db.Lancamentos.Include(l => l.Projecao).ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Lancamento>> GetById(int id)
        {
            var lanc = await _db.Lancamentos.Include(l => l.Projecao).FirstOrDefaultAsync(l => l.IdLancamento == id);
            return lanc is null ? NotFound() : Ok(lanc);
        }

        [HttpPost]
        public async Task<ActionResult<Lancamento>> Create(Lancamento model)
        {
            var projExists = await _db.Projecoes.AnyAsync(p => p.IdProjecao == model.FK_IdProjecao);
            if (!projExists) return BadRequest("Projeção associada não existe.");

            _db.Lancamentos.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.IdLancamento }, model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Lancamento model)
        {
            if (id != model.IdLancamento) return BadRequest();
            var existing = await _db.Lancamentos.FindAsync(id);
            if (existing is null) return NotFound();

            existing.DataLancamento = model.DataLancamento;
            existing.Valor = model.Valor;
            existing.TipoLancamento = model.TipoLancamento;
            existing.DescricaoLancamento = model.DescricaoLancamento;
            existing.Recorrencia = model.Recorrencia;
            existing.QtdRecorrencia = model.QtdRecorrencia;
            existing.DescricaoRecorrencia = model.DescricaoRecorrencia;
            existing.FK_IdProjecao = model.FK_IdProjecao;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Lancamentos.FindAsync(id);
            if (existing is null) return NotFound();

            _db.Lancamentos.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
