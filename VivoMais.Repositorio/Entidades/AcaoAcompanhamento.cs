using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class AcaoAcompanhamento
    {
        public string Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime EnviadoMarketingTerritorial { get; set; }
        public DateTime SolicitacaoREL { get; set; }
        public DateTime EnviadoMarketingRegional { get; set; }
        public DateTime EnviadoMidia { get; set; }
        public DateTime EnviadoAuditoria { get; set; }
        public DateTime EnviadoJU { get; set; }
        public DateTime DataSolicitacaoProtocolo { get; set; }
        public DateTime DataVencimentoProtocolo { get; set; }
        public DateTime DataSolicitacaoRel { get; set; }
        public DateTime DataVencimentoRel { get; set; }
        public string Protocolo { get; set; }
        public string Obs { get; set; }
        public string StatusPagamento { get; set; }
        public string NumeroProtocolo { get; set; }
        public string Rel { get; set; }
        public string DocSAP { get; set; }
        public string TotalReceber { get; set; }
    }
}