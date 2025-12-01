using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMidasAPI.Models
{
    public class Projecao
    {
        [Key]
        public int IdProjecao { get; set; } //PK projeção
        
        [Required, MaxLength(120)]
        public string NomeProjecao { get; set; } = string.Empty; //Nome da projeção
        
        [MaxLength(255)]
        public string DescricaoProjecao { get; set; }  = string.Empty;  //Descrição da projeção

        public DateTime DtInicial { get; set; } //Data inicio projeção
        public DateTime DtFinal { get; set; } //Data final projeção

        public float ProjecaoReceita { get; set; } //previção da Receita|Entrada|Recebimento|Positivo
        public float ProjecaoDespesa { get; set; } //previção da Despesa|Saida|Pagamento|Negativo

        public ICollection<Lancamento>? lancamentos { get; set; }// Relacionamento de 1 pra muitos com lançamentos

        }
}