using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class VivoVisitaPassagemPositivacao
    {
          public int Id { get; set; }
          public string PositivacaoMateriaisPositivadosManualPDVSimNao { get; set; }
          public string PositivacaoMateriaisPositivadosManualPDVObs { get; set; }
          public string PositivacaoMateriaisPositivadosManualPDVPrazo { get; set; }
          public string PositivacaoMateriaisPositivadosManualPDVResponsavel { get; set; }
          public string PositivacaoAreaArmazenamentoOrganizadaSimNao { get; set; }
          public string PositivacaoAreaArmazenamentoOrganizadaObs { get; set; }
          public string PositivacaoAreaArmazenamentoOrganizadaPrazo { get; set; }
          public string PositivacaoAreaArmazenamentoOrganizadaResponsavel { get; set; }
          public string PositivacaoPossuiFolheteriaCompletaSimNao { get; set; }
          public string PositivacaoPossuiFolheteriaCompletaObs { get; set; }
          public string PositivacaoPossuiFolheteriaCompletaPrazo { get; set; }
          public string PositivacaoPossuiFolheteriaCompletaResponsavel { get; set; }
          public string PositivacaoPossuiPrecificadoresSimNao { get; set; }
          public string PositivacaoPossuiPrecificadoresObs { get; set; }
          public string PositivacaoPossuiPrecificadoresPrazo { get; set; }
          public string PositivacaoPossuiPrecificadoresResponsavel { get; set; }
          public string PositivacaoDemaisConsideracoes { get; set; }
    }
}