using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjetoMidasAPI.Models.Auth
{
    public class LoginRequest
    {
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Senha { get; set; } = string.Empty;
    }
}