using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjetoMidasAPI.Models
{
    public class Usuario
    {
        [Key] // chave primária
        public int IdUsuario { get; set; }

        [Required, MaxLength(120)]
        public string NomeUsuario { get; set; } = string.Empty;

        [Required, MaxLength(120)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;
    }
}
