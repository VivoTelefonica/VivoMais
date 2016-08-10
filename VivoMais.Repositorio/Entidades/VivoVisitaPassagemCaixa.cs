using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class VivoVisitaPassagemCaixa
    {
          public int Id { get; set; }
          public string CaixaValorFundoTrocoCorretoSimNao { get; set; }
          public string CaixaValorFundoTrocoCorretoObs { get; set; }
          public string CaixaValorFundoTrocoCorretoPrazo { get; set; }
          public string CaixaValorFundoTrocoCorretoResponsavel { get; set; }
          public string CaixaNaoExisteReembolsoPendenteSimNao { get; set; }
          public string CaixaNaoExisteReembolsoPendenteObs { get; set; }
          public string CaixaNaoExisteReembolsoPendentePrazo { get; set; }
          public string CaixaNaoExisteReembolsoPendenteResponsavel { get; set; }
          public string CaixaNaoExistePendenciaMovimentosSAPSimNao { get; set; }
          public string CaixaNaoExistePendenciaMovimentosSAPObs { get; set; }
          public string CaixaNaoExistePendenciaMovimentosSAPPrazo { get; set; }
          public string CaixaNaoExistePendenciaMovimentosSAPResponsavel { get; set; }
          public string CaixaDemaisConsideracoes { get; set; }
    }
}