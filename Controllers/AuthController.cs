using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;
using ProjetoMidasAPI.Models;
using ProjetoMidasAPI.Models.Auth;
using ProjetoMidasAPI.Services;
using System.Security.Cryptography;
using System.Text;

namespace ProjetoMidasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;
        public AuthController(AppDbContext db, TokenService tokenService) { _db = db; _tokenService = tokenService; }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequest model)
        {
            if (await _db.Usuarios.AnyAsync(u => u.Email == model.Email))
                return BadRequest("E-mail já cadastrado.");

            var hash = HashPassword(model.Senha);
            var usuario = new Usuario { NomeUsuario = model.Email, Email = model.Email, SenhaHash = hash };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return Created(string.Empty, new { usuario.IdUsuario, usuario.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (usuario is null) return Unauthorized("Credenciais inválidas.");

            var hash = HashPassword(model.Senha);
            if (usuario.SenhaHash != hash) return Unauthorized("Credenciais inválidas.");

            var token = _tokenService.GenerateToken(usuario);
            return Ok(new { token });
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}