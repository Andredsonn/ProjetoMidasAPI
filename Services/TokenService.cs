using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProjetoMidasAPI.Models;
using ProjetoMidasAPI.Settings;

namespace ProjetoMidasAPI.Services
{
    public class TokenService
    {
        private readonly JwtSettings _settings;
        public TokenService(JwtSettings settings) { _settings = settings; }

        public string GenerateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", usuario.IdUsuario.ToString()),
                new Claim("nome", usuario.NomeUsuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiresMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
