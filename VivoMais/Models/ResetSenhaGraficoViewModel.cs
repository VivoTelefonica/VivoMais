using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class ResetSenhaGraficoViewModel
    {
        public string Mes { get; set; }
        public string Ddd { get; set; }
        public decimal Valor { get; set; }
        public string Qtd { get; set; }
        public decimal Sla { get; set; }
        public DateTime DataSolicitacao { get; set; }
    }
}