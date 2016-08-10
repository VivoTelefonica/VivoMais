using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class DescredenciamentoAmigavel
    {
        
        public int Id { get; set; }

        public DateTime? SolicitacaoParceiro { get; set; }

        public string Informe { get; set; }

        public DateTime? LiberadoChancela { get; set; }

        public DateTime? ChancelaDistrato { get; set; }

        public DateTime? EnvioTerritorio { get; set; }

        public DateTime? RecebimentoDistrato { get; set; }

        public DateTime? AssinaturaDivisao { get; set; }

        public DateTime? AssinaturaDiretoria { get; set; }

        public DateTime? ReconhecimentoFirma { get; set; }

        public DateTime? ExclusaoAcessos { get; set; }

        public string Status { get; set; }
    }
}