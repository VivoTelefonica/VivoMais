using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class VivoVisitaPassagemAbertura
    {
        public int Id {get;set;}
        public Parceiro Parceiro {get;set;}

        [Required(ErrorMessage = "TIPO DE ABERTURA: campo obrigatório")]
        public string TipoAbertura {get;set;}

        [Required(ErrorMessage = "DESCRIÇÃO: campo obrigatório")]
        public string Descricao {get;set;}

        [Required(ErrorMessage = "DATA: campo obrigatório")]
        public DateTime Data {get;set;}

        public DateTime DataCadastro {get;set;}
        public Acesso Acesso { get; set; }
        public VivoVisitaPassagemCaixa Caixa { get; set; }
        public VivoVisitaPassagemEstoque Estoque { get; set; }
        public VivoVisitaPassagemEstruturaProcessos EstruturaProcessos { get; set; }
        public VivoVisitaPassagemPessoas Pessoas { get; set; }
        public VivoVisitaPassagemPositivacao Positivacao { get; set; }
        public VivoVisitaPassagemVagas Vagas { get; set; }
    }
}