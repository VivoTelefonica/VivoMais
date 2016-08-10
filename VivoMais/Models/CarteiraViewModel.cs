using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class CarteiraViewModel
    {
        public Parceiro Parceiro { get; set; }

        public List<Parceiro> ListParceiro { get; set; }

        [Required(ErrorMessage = "DATA ABERTURA: campo obrigatório")]
        public DateTime DataAberturaCarteira { get; set; }

        [Required(ErrorMessage = "DATA FECHAMENTO: campo obrigatório")]
        public DateTime DataFechamentoCarteira { get; set; }

        [Required(ErrorMessage = "TIPO DE CARTEIRA: campo obrigatório")]
        public string TipoCarteira { get; set; }
        public string MesAbertura { get; set; }
        public bool Check { get; set; }


    }
}