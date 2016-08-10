using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class VivoVisitaPassagemPessoas
    {
          public int Id { get; set; }
          public string PessoasSolicitadoSPPVagasAbertoSimNao { get; set; }
          public string PessoasSolicitadoSPPVagasAbertoObs { get; set; }
          public string PessoasSolicitadoSPPVagasAbertoPrazo { get; set; }
          public string PessoasSolicitadoSPPVagasAbertoResponsavel { get; set; }
          public string PessoasNaoExisteColaboradoresPendenciaTreinamentoSimNao { get; set; }
          public string PessoasNaoExisteColaboradoresPendenciaTreinamentoObs { get; set; }
          public string PessoasNaoExisteColaboradoresPendenciaTreinamentoPrazo { get; set; }
          public string PessoasNaoExisteColaboradoresPendenciaTreinamentoResponsavel { get; set; }
          public string PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasSimNao { get; set; }
          public string PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasObs { get; set; }
          public string PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasPrazo { get; set; }
          public string PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasResponsavel { get; set; }
          public string PessoasLojaPossuiProgramacaoFeriasSimNao { get; set; }
          public string PessoasLojaPossuiProgramacaoFeriasObs { get; set; }
          public string PessoasLojaPossuiProgramacaoFeriasPrazo { get; set; }
          public string PessoasLojaPossuiProgramacaoFeriasResponsavel { get; set; }
          public string PessoasTodosColaboradoresPossuemCrachaSimNao { get; set; }
          public string PessoasTodosColaboradoresPossuemCrachaObs { get; set; }
          public string PessoasTodosColaboradoresPossuemCrachaPrazo { get; set; }
          public string PessoasTodosColaboradoresPossuemCrachaResponsavel { get; set; }
    }
}