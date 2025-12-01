using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMidasAPI.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public int ExpiresMinutes { get; set; } = 60;
    }
}