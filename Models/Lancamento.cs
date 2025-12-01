using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ProjetoMidasAPI.Models.Enums;

namespace ProjetoMidasAPI.Models
{
    public class Lancamento
    {
        [Key]
        public int IdLancamento { get; set; } //pk Lançamentos
        public DateTime DataLancamento { get; set; } //Data do registro
        public float Valor { get; set; } //valor do registro
        public TipoLancamento TipoLancamento{ get; set; }//é o tipo do lançamento de acordo com o enum

        public string DescricaoLancamento = string.Empty; // DescriÇão do Lançamentos, possível campo de filtro para apresentar os relatórios

        public bool Recorrencia { get; set; } // Para indicar recorrência
        public int? QtdRecorrencia { get; set; } // Números de recorrência
        public string? DescricaoRecorrencia{ get; set; }// Campo aceita vazio, pois não está defindo se será realmente utilziado e qual o ganho se habilitado.

        //Relacionamento com chave estrangeiro para garantir o referencial com projeção
        [ForeignKey(nameof(Projecao))]
        public int FK_IdProjecao { get; set; }
        public Projecao? Projecao{ get;set; }
    }
}