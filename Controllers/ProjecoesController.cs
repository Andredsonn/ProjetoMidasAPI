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
    public class ProjecoesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProjecoesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetAll()
        {
            return Ok(await _db.Projecoes.Include(p => p.lancamentos).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Projecao>> GetById(int id)
        {
            var projecao = await _db.Projecoes.Include(p => p.lancamentos).FirstOrDefaultAsync(p => p.IdProjecao == id);
            return projecao is null ? NotFound() : Ok(projecao);
        }

        [HttpPost]
        public async Task<ActionResult<Projecao>> Create(Projecao model)
        {
            _db.Projecoes.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.IdProjecao }, model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Projecao model)
        {
            if (id != model.IdProjecao) return BadRequest();
            var existing = await _db.Projecoes.FindAsync(id);
            if (existing is null) return NotFound();

            existing.NomeProjecao = model.NomeProjecao;
            existing.DescricaoProjecao = model.DescricaoProjecao;
            existing.DtInicial = model.DtInicial;
            existing.DtFinal = model.DtFinal;
            existing.ProjecaoReceita = model.ProjecaoReceita;
            existing.ProjecaoDespesa = model.ProjecaoDespesa;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Projecoes.FindAsync(id);
            if (existing is null) return NotFound();

            _db.Projecoes.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}