using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class VivoVisitaRepositorio
    {
        DAO dao;

        public VivoVisitaRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public bool CadastrarPassagemAberturaVisita(VivoVisitaPassagemAbertura Visita, string Login)
        {
            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_ABERTURA] " +
                "  ( " +
                "        [Ddd] " +
                "       ,[Loja] " +
                "       ,[TipoAbertura] " +
                "       ,[Descricao] " +
                "       ,[Data] " +
                "       ,[DataCadastro] " +
                "       ,[IdAcesso] " +
                "  ) " +
                "  VALUES ( " +
                "  '" + Visita.Parceiro.Ddd + "' " +
                "  ,'" + Visita.Parceiro.Rede + "' " +
                "  ,'" + Visita.TipoAbertura + "' " +
                "  ,'" + Visita.Descricao + "' " +
                "  ,CONVERT(DATETIME,'" + Visita.Data + "') " +
                "  ,GETDATE() " +
                "  ," + Login +
                "  ) ";
      

            return dao.ExecutarSql(SQL);
        }

        public bool CadastrarCanalTerritorioAberturaVisita(VivoVisitaPassagemAbertura Visita, string Login)
        {
            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[VIVO_VISITA_CANAL_TERRITORIO_ABERTURA] " +
                "  ( " +
                "        [Ddd] " +
                "       ,[Loja] " +
                "       ,[TipoAbertura] " +
                "       ,[Descricao] " +
                "       ,[Data] " +
                "       ,[DataCadastro] " +
                "       ,[IdAcesso] " +
                "  ) " +
                "  VALUES ( " +
                "  '" + Visita.Parceiro.Ddd + "' " +
                "  ,'" + Visita.Parceiro.Rede + "' " +
                "  ,'" + Visita.TipoAbertura + "' " +
                "  ,'" + Visita.Descricao + "' " +
                "  ,CONVERT(DATETIME,'" + Visita.Data + "') " +
                "  ,GETDATE() " +
                "  ," + Login +
                "  ) ";


            return dao.ExecutarSql(SQL);
        }

        public VivoVisitaPassagemAbertura ObterPassagemVivoVisita(string idAcesso)
        {
            string SQL = " SELECT [Id] " +
            "      ,[Ddd] " +
            "      ,[Loja] " +
            "      ,[TipoAbertura] " +
            "      ,[Descricao] " +
            " FROM [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_ABERTURA] " +
            " WHERE ID = (SELECT MAX(Id) FROM [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_ABERTURA] WHERE idAcesso = '" + idAcesso + "' ) ";

            DataTable dsVivoVisita = null;
            VivoVisitaPassagemAbertura retorno = new VivoVisitaPassagemAbertura();

            dsVivoVisita = this.dao.returnaDataTable(SQL);

            if (dsVivoVisita.Rows.Count > 0)
            {
                foreach (DataRow item in dsVivoVisita.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Ddd  = Convert.ToString(item[1]);
                    p.Rede  = Convert.ToString(item[2]);

                    retorno.Parceiro = p;
                    retorno.Id = Convert.ToInt32(item[0]);
                    retorno.TipoAbertura = Convert.ToString(item[3]);
                    retorno.Descricao = Convert.ToString(item[4]);

                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        public VivoVisitaCanalTerritorioAbertura ObterCanalTerritorioVivoVisita(string idAcesso)
        {
            string SQL = " SELECT [Id] " +
            "      ,[Ddd] " +
            "      ,[Loja] " +
            "      ,[TipoAbertura] " +
            "      ,[Descricao] " +
            " FROM [Vivo_SIM].[dbo].[VIVO_VISITA_CANAL_TERRITORIO_ABERTURA] " +
            " WHERE ID = (SELECT MAX(Id) FROM [Vivo_SIM].[dbo].[VIVO_VISITA_CANAL_TERRITORIO_ABERTURA] WHERE idAcesso = '" + idAcesso + "' ) ";

            DataTable dsVivoVisita = null;
            VivoVisitaCanalTerritorioAbertura retorno = new VivoVisitaCanalTerritorioAbertura();

            dsVivoVisita = this.dao.returnaDataTable(SQL);

            if (dsVivoVisita.Rows.Count > 0)
            {
                foreach (DataRow item in dsVivoVisita.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Ddd = Convert.ToString(item[1]);
                    p.Rede = Convert.ToString(item[2]);

                    retorno.Parceiro = p;
                    retorno.Id = Convert.ToInt32(item[0]);
                    retorno.TipoAbertura = Convert.ToString(item[3]);
                    retorno.Descricao = Convert.ToString(item[4]);

                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        public bool AtualizarPassagemLojaEstoque(VivoVisitaPassagemEstoque Estoque, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_LOJA_ESTOQUE] " +
                  " SET [EstoqueQtdSeriaisAparelhosSAPSimNao] = '" + Estoque.EstoqueQtdSeriaisAparelhosSAPSimNao + "'" +
                  " ,[EstoqueQtdSeriaisAparelhosSAPObs] = '" + Estoque.EstoqueQtdSeriaisAparelhosSAPObs + "'" +
                  " ,[EstoqueQtdSeriaisAparelhosSAPPrazo] = '" + Estoque.EstoqueQtdSeriaisAparelhosSAPPrazo + "'" +
                  " ,[EstoqueQtdSeriaisAparelhosSAPResponsavel] = '" + Estoque.EstoqueQtdSeriaisAparelhosSAPResponsavel + "'" +
                  " ,[EstoqueQtdSeriaisChipsSAPSimNao] = '" + Estoque.EstoqueQtdSeriaisChipsSAPSimNao + "'" +
                  " ,[EstoqueQtdSeriaisChipsSAPObs] = '" + Estoque.EstoqueQtdSeriaisChipsSAPObs + "'" +
                  " ,[EstoqueQtdSeriaisChipsSAPPrazo] = '" + Estoque.EstoqueQtdSeriaisChipsSAPPrazo + "'" +
                  " ,[EstoqueQtdSeriaisChipsSAPResponsavel] = '" + Estoque.EstoqueQtdSeriaisChipsSAPResponsavel + "'" +
                  " ,[EstoqueQtdSeriaisCartõesRecargaSAPSimNao] = '" + Estoque.EstoqueQtdSeriaisCartoesRecargaSAPSimNao + "'" +
                  " ,[EstoqueQtdSeriaisCartõesRecargaSAPObs] = '" + Estoque.EstoqueQtdSeriaisCartoesRecargaSAPObs + "'" +
                  " ,[EstoqueQtdSeriaisCartõesRecargaSAPPrazo] = '" + Estoque.EstoqueQtdSeriaisCartoesRecargaSAPPrazo + "'" +
                  " ,[EstoqueQtdSeriaisCartõesRecargaSAPResponsavel] = '" + Estoque.EstoqueQtdSeriaisCartoesRecargaSAPResponsavel + "'" +
                  " ,[EstoqueQtdSeriaisAcessoriosSAPSimNao] = '" + Estoque.EstoqueQtdSeriaisAcessoriosSAPSimNao + "'" +
                  " ,[EstoqueQtdSeriaisAcessoriosSAPObs] = '" + Estoque.EstoqueQtdSeriaisAcessoriosSAPObs + "'" +
                  " ,[EstoqueQtdSeriaisAcessoriosSAPPrazo] = '" + Estoque.EstoqueQtdSeriaisAcessoriosSAPPrazo + "'" +
                  " ,[EstoqueQtdSeriaisAcessoriosSAPResponsavel] = '" + Estoque.EstoqueQtdSeriaisAcessoriosSAPResponsavel + "'" +
                  " ,[EstoqueQtdSeriaisWearablesSAPSimNao] = '" + Estoque.EstoqueQtdSeriaisWearablesSAPSimNao + "'" +
                  " ,[EstoqueQtdSeriaisWearablesSAPObs] = '" + Estoque.EstoqueQtdSeriaisWearablesSAPObs + "'" +
                  " ,[EstoqueQtdSeriaisWearablesSAPPrazo] = '" + Estoque.EstoqueQtdSeriaisWearablesSAPPrazo + "'" +
                  " ,[EstoqueQtdSeriaisWearablesSAPResponsavel] = '" + Estoque.EstoqueQtdSeriaisWearablesSAPResponsavel + "'" +
                  " ,[EstoqueCaixasAbertasKitsCompletosSimNao] = '" + Estoque.EstoqueCaixasAbertasKitsCompletosSimNao + "'" +
                  " ,[EstoqueCaixasAbertasKitsCompletosObs] = '" + Estoque.EstoqueCaixasAbertasKitsCompletosObs + "'" +
                  " ,[EstoqueCaixasAbertasKitsCompletosPrazo] = '" + Estoque.EstoqueCaixasAbertasKitsCompletosPrazo + "'" +
                  " ,[EstoqueCaixasAbertasKitsCompletosResponsavel] = '" + Estoque.EstoqueCaixasAbertasKitsCompletosResponsavel + "'" +
                  " ,[EstoqueAparelhosDemonstracaoKitCompletoSimNao] = '" + Estoque.EstoqueAparelhosDemonstracaoKitCompletoSimNao + "'" +
                  " ,[EstoqueAparelhosDemonstracaoKitCompletoObs] = '" + Estoque.EstoqueAparelhosDemonstracaoKitCompletoObs + "'" +
                  " ,[EstoqueAparelhosDemonstracaoKitCompletoPrazo] = '" + Estoque.EstoqueAparelhosDemonstracaoKitCompletoPrazo + "'" +
                  " ,[EstoqueAparelhosDemonstracaoKitCompletoResponsavel] = '" + Estoque.EstoqueAparelhosDemonstracaoKitCompletoResponsavel + "'" +
                  " ,[EstoqueAparelhosAlocadosEstoqueCorretoSimNao] = '" + Estoque.EstoqueAparelhosAlocadosEstoqueCorretoSimNao + "'" +
                  " ,[EstoqueAparelhosAlocadosEstoqueCorretoObs] = '" + Estoque.EstoqueAparelhosAlocadosEstoqueCorretoObs + "'" +
                  " ,[EstoqueAparelhosAlocadosEstoqueCorretoPrazo] = '" + Estoque.EstoqueAparelhosAlocadosEstoqueCorretoPrazo + "'" +
                  " ,[EstoqueAparelhosAlocadosEstoqueCorretoResponsavel] = '" + Estoque.EstoqueAparelhosAlocadosEstoqueCorretoResponsavel + "'" +
                  " ,[EstoqueDemaisConsideracoes] = '" + Estoque.EstoqueDemaisConsideracoes + "'" +
                  " ,[DataCadastro] = GetDate() " +
                  " WHERE [IdAbertura] = " + Id ;


            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaPassagemLojaCaixa(VivoVisitaPassagemCaixa Caixa, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_LOJA_CAIXA] " +
                  "  SET [CaixaValorFundoTrocoCorretoSimNao] = '" + Caixa.CaixaValorFundoTrocoCorretoSimNao + "'" +
                  " ,[CaixaValorFundoTrocoCorretoObs] = '" + Caixa.CaixaValorFundoTrocoCorretoObs + "'" +
                  " ,[CaixaValorFundoTrocoCorretoPrazo] = '" + Caixa.CaixaValorFundoTrocoCorretoPrazo + "'" +
                  " ,[CaixaValorFundoTrocoCorretoResponsavel] = '" + Caixa.CaixaValorFundoTrocoCorretoResponsavel + "'" +
                  " ,[CaixaNaoExisteReembolsoPendenteSimNao] = '" + Caixa.CaixaNaoExisteReembolsoPendenteSimNao + "'" +
                  " ,[CaixaNaoExisteReembolsoPendenteObs] = '" + Caixa.CaixaNaoExisteReembolsoPendenteObs + "'" +
                  " ,[CaixaNaoExisteReembolsoPendentePrazo] = '" + Caixa.CaixaNaoExisteReembolsoPendentePrazo + "'" +
                  " ,[CaixaNaoExisteReembolsoPendenteResponsavel] = '" + Caixa.CaixaNaoExisteReembolsoPendenteResponsavel + "'" +
                  " ,[CaixaNaoExistePendenciaMovimentosSAPSimNao] = '" + Caixa.CaixaNaoExistePendenciaMovimentosSAPSimNao + "'" +
                  " ,[CaixaNaoExistePendenciaMovimentosSAPObs] = '" + Caixa.CaixaNaoExistePendenciaMovimentosSAPObs + "'" +
                  " ,[CaixaNaoExistePendenciaMovimentosSAPPrazo] = '" + Caixa.CaixaNaoExistePendenciaMovimentosSAPPrazo + "'" +
                  " ,[CaixaNaoExistePendenciaMovimentosSAPResponsavel] = '" + Caixa.CaixaNaoExistePendenciaMovimentosSAPResponsavel + "'" +
                  " ,[CaixaDemaisConsideracoes] = '" + Caixa.CaixaDemaisConsideracoes + "'" + 
                  " ,[DataCadastro] = GetDate() " +
                  " WHERE [IdAbertura] = " + Id ;


            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaPassagemLojaEstruturaProcessos(VivoVisitaPassagemEstruturaProcessos EstruturaProcessos, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_LOJA_ESTRUTURA_PROCESSOS] " +
                  "   SET [EstruturaProcessosMobiliariosReparoOSAbertaRegularizacaoSimNao] = '" + EstruturaProcessos.EstruturaProcessosMobiliariosReparoOSAbertaRegularizacaoSimNao + "'" +
                  "  ,[EstruturaProcessosMobiliariosReparoOSAbertaRegularizacaoObs] = '" + EstruturaProcessos.EstruturaProcessosMobiliariosReparoOSAbertaRegularizacaoObs + "'" +
                  "  ,[EstruturaProcessosMobiliariosReparoOSAbertaRegularizacaoPrazo] = '" + EstruturaProcessos.EstruturaProcessosMobiliariosReparoOSAbertaRegularizacaoPrazo + "'" +
                  "  ,[EstruturaProcessosMobiliariosReparoOSAbertaRegularizacaoResponsavel] = '" + EstruturaProcessos.EstruturaProcessosMobiliariosReparoOSAbertaRegularizacaoResponsavel + "'" +
                  "  ,[EstruturaProcessosEquipamentosReparoChamadosAbertosRegularizacaoSimNao] = '" + EstruturaProcessos.EstruturaProcessosEquipamentosReparoChamadosAbertosRegularizacaoSimNao + "'" +
                  "  ,[EstruturaProcessosEquipamentosReparoChamadosAbertosRegularizacaoObs] = '" + EstruturaProcessos.EstruturaProcessosEquipamentosReparoChamadosAbertosRegularizacaoObs + "'" +
                  "  ,[EstruturaProcessosEquipamentosReparoChamadosAbertosRegularizacaoPrazo] = '" + EstruturaProcessos.EstruturaProcessosEquipamentosReparoChamadosAbertosRegularizacaoPrazo + "'" +
                  "  ,[EstruturaProcessosEquipamentosReparoChamadosAbertosRegularizacaoResponsavel] = '" + EstruturaProcessos.EstruturaProcessosEquipamentosReparoChamadosAbertosRegularizacaoResponsavel + "'" +
                  "  ,[EstruturaProcessosChamadosAbertosDentroPrazoCobrançaSimNao] = '" + EstruturaProcessos.EstruturaProcessosChamadosAbertosDentroPrazoCobrançaSimNao + "'" +
                  "  ,[EstruturaProcessosChamadosAbertosDentroPrazoCobrançaObs] = '" + EstruturaProcessos.EstruturaProcessosChamadosAbertosDentroPrazoCobrançaObs + "'" +
                  "  ,[EstruturaProcessosChamadosAbertosDentroPrazoCobrançaPrazo] = '" + EstruturaProcessos.EstruturaProcessosChamadosAbertosDentroPrazoCobrançaPrazo + "'" +
                  "  ,[EstruturaProcessosChamadosAbertosDentroPrazoCobrançaResponsavel] = '" + EstruturaProcessos.EstruturaProcessosChamadosAbertosDentroPrazoCobrançaResponsavel + "'" +
                  "  ,[EstruturaProcessosLojaPossuiControleNovoGerenteSimNao] = '" + EstruturaProcessos.EstruturaProcessosLojaPossuiControleNovoGerenteSimNao + "'" +
                  "  ,[EstruturaProcessosLojaPossuiControleNovoGerenteObs] = '" + EstruturaProcessos.EstruturaProcessosLojaPossuiControleNovoGerenteObs + "'" +
                  "  ,[EstruturaProcessosLojaPossuiControleNovoGerentePrazo] = '" + EstruturaProcessos.EstruturaProcessosLojaPossuiControleNovoGerentePrazo + "'" +
                  "  ,[EstruturaProcessosLojaPossuiControleNovoGerenteResponsavel] = '" + EstruturaProcessos.EstruturaProcessosLojaPossuiControleNovoGerenteResponsavel + "'" +
                  "  ,[EstruturaProcessosLojaOrganizadaSimNao] = '" + EstruturaProcessos.EstruturaProcessosLojaOrganizadaSimNao + "'" +
                  "  ,[EstruturaProcessosLojaOrganizadaObs] = '" + EstruturaProcessos.EstruturaProcessosLojaOrganizadaObs + "'" +
                  "  ,[EstruturaProcessosLojaOrganizadaPrazo] = '" + EstruturaProcessos.EstruturaProcessosLojaOrganizadaPrazo + "'" +
                  "  ,[EstruturaProcessosLojaOrganizadaResponsavel] = '" + EstruturaProcessos.EstruturaProcessosLojaOrganizadaResponsavel + "'" +
                  "  ,[EstruturaProcessosEquipamentosMobiliariosNUsadosAguardandoColetaSimNao] = '" + EstruturaProcessos.EstruturaProcessosEquipamentosMobiliariosNUsadosAguardandoColetaSimNao + "'" +
                  "  ,[EstruturaProcessosEquipamentosMobiliariosNUsadosAguardandoColetaObs] = '" + EstruturaProcessos.EstruturaProcessosEquipamentosMobiliariosNUsadosAguardandoColetaObs + "'" +
                  "  ,[EstruturaProcessosEquipamentosMobiliariosNUsadosAguardandoColetaPrazo] = '" + EstruturaProcessos.EstruturaProcessosEquipamentosMobiliariosNUsadosAguardandoColetaPrazo + "'" +
                  "  ,[EstruturaProcessosEquipamentosMobiliariosNUsadosAguardandoColetaResponsavel] = '" + EstruturaProcessos.EstruturaProcessosEquipamentosMobiliariosNUsadosAguardandoColetaResponsavel + "'" +
                  "  ,[EstruturaProcessosAlarmesReinicializadosSenhaNovosGerentesSimNao] = '" + EstruturaProcessos.EstruturaProcessosAlarmesReinicializadosSenhaNovosGerentesSimNao + "'" +
                  "  ,[EstruturaProcessosAlarmesReinicializadosSenhaNovosGerentesObs] = '" + EstruturaProcessos.EstruturaProcessosAlarmesReinicializadosSenhaNovosGerentesObs + "'" +
                  "  ,[EstruturaProcessosAlarmesReinicializadosSenhaNovosGerentesPrazo] = '" + EstruturaProcessos.EstruturaProcessosAlarmesReinicializadosSenhaNovosGerentesPrazo + "'" +
                  "  ,[EstruturaProcessosAlarmesReinicializadosSenhaNovosGerentesResponsavel] = '" + EstruturaProcessos.EstruturaProcessosAlarmesReinicializadosSenhaNovosGerentesResponsavel + "'" +
                  "  ,[EstruturaProcessosLojaPastaAtendeProcedimentoContingenciaGSSSimNao] = '" + EstruturaProcessos.EstruturaProcessosLojaPastaAtendeProcedimentoContingenciaGSSSimNao + "'" +
                  "  ,[EstruturaProcessosLojaPastaAtendeProcedimentoContingenciaGSSObs] = '" + EstruturaProcessos.EstruturaProcessosLojaPastaAtendeProcedimentoContingenciaGSSObs + "'" +
                  "  ,[EstruturaProcessosLojaPastaAtendeProcedimentoContingenciaGSSPrazo] = '" + EstruturaProcessos.EstruturaProcessosLojaPastaAtendeProcedimentoContingenciaGSSPrazo + "'" +
                  "  ,[EstruturaProcessosLojaPastaAtendeProcedimentoContingenciaGSSResponsavel] = '" + EstruturaProcessos.EstruturaProcessosLojaPastaAtendeProcedimentoContingenciaGSSResponsavel + "'" +
                  "  ,[EstruturaProcessosLojaQuadroAvisosSimNao] = '" + EstruturaProcessos.EstruturaProcessosLojaQuadroAvisosSimNao + "'" +
                  "  ,[EstruturaProcessosLojaQuadroAvisosObs] = '" + EstruturaProcessos.EstruturaProcessosLojaQuadroAvisosObs + "'" +
                  "  ,[EstruturaProcessosLojaQuadroAvisosPrazo] = '" + EstruturaProcessos.EstruturaProcessosLojaQuadroAvisosPrazo + "'" +
                  "  ,[EstruturaProcessosLojaQuadroAvisosResponsavel] = '" + EstruturaProcessos.EstruturaProcessosLojaQuadroAvisosResponsavel + "'" +
                  "  ,[EstruturaProcessosEstaoGuardadosDocumentosFiscaisSimNao] = '" + EstruturaProcessos.EstruturaProcessosEstaoGuardadosDocumentosFiscaisSimNao + "'" +
                  "  ,[EstruturaProcessosEstaoGuardadosDocumentosFiscaisObs] = '" + EstruturaProcessos.EstruturaProcessosEstaoGuardadosDocumentosFiscaisObs + "'" +
                  "  ,[EstruturaProcessosEstaoGuardadosDocumentosFiscaisPrazo] = '" + EstruturaProcessos.EstruturaProcessosEstaoGuardadosDocumentosFiscaisPrazo + "'" +
                  "  ,[EstruturaProcessosEstaoGuardadosDocumentosFiscaisResponsavel] = '" + EstruturaProcessos.EstruturaProcessosEstaoGuardadosDocumentosFiscaisResponsavel + "'" +
                  "  ,[EstruturaProcessosDemaisConsideracoes] = '" + EstruturaProcessos.EstruturaProcessosDemaisConsideracoes + "'" +
                  "  ,[DataCadastro] = GetDate() " +
                  " WHERE [IdAbertura] = " + Id ;

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaPassagemLojaPositivacao(VivoVisitaPassagemPositivacao Positivacao, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_LOJA_POSITIVACAO] " +
                  "  SET [PositivacaoMateriaisPositivadosManualPDVSimNao] = '" + Positivacao.PositivacaoMateriaisPositivadosManualPDVSimNao + "'" +
                  " ,[PositivacaoMateriaisPositivadosManualPDVObs] = '" + Positivacao.PositivacaoMateriaisPositivadosManualPDVObs + "'" +
                  " ,[PositivacaoMateriaisPositivadosManualPDVPrazo] = '" + Positivacao.PositivacaoMateriaisPositivadosManualPDVPrazo + "'" +
                  " ,[PositivacaoMateriaisPositivadosManualPDVResponsavel] = '" + Positivacao.PositivacaoMateriaisPositivadosManualPDVResponsavel + "'" +
                  " ,[PositivacaoAreaArmazenamentoOrganizadaSimNao] = '" + Positivacao.PositivacaoAreaArmazenamentoOrganizadaSimNao + "'" +
                  " ,[PositivacaoAreaArmazenamentoOrganizadaObs] = '" + Positivacao.PositivacaoAreaArmazenamentoOrganizadaObs + "'" +
                  " ,[PositivacaoAreaArmazenamentoOrganizadaPrazo] = '" + Positivacao.PositivacaoAreaArmazenamentoOrganizadaPrazo + "'" +
                  " ,[PositivacaoAreaArmazenamentoOrganizadaResponsavel] = '" + Positivacao.PositivacaoAreaArmazenamentoOrganizadaResponsavel + "'" +
                  " ,[PositivacaoPossuiFolheteriaCompletaSimNao] = '" + Positivacao.PositivacaoPossuiFolheteriaCompletaSimNao + "'" +
                  " ,[PositivacaoPossuiFolheteriaCompletaObs] = '" + Positivacao.PositivacaoPossuiFolheteriaCompletaObs + "'" +
                  " ,[PositivacaoPossuiFolheteriaCompletaPrazo] = '" + Positivacao.PositivacaoPossuiFolheteriaCompletaPrazo + "'" +
                  " ,[PositivacaoPossuiFolheteriaCompletaResponsavel] = '" + Positivacao.PositivacaoPossuiFolheteriaCompletaResponsavel + "'" +
                  " ,[PositivacaoPossuiPrecificadoresSimNao] = '" + Positivacao.PositivacaoPossuiPrecificadoresSimNao + "'" +
                  " ,[PositivacaoPossuiPrecificadoresObs] = '" + Positivacao.PositivacaoPossuiPrecificadoresObs + "'" +
                  " ,[PositivacaoPossuiPrecificadoresPrazo] = '" + Positivacao.PositivacaoPossuiPrecificadoresPrazo + "'" +
                  " ,[PositivacaoPossuiPrecificadoresResponsavel] = '" + Positivacao.PositivacaoPossuiPrecificadoresResponsavel + "'" +
                  " ,[PositivacaoDemaisConsideracoes] = '" + Positivacao.PositivacaoDemaisConsideracoes + "'" +
                  " ,[DataCadastro] = GetDate() " +
                  " WHERE [IdAbertura] = " + Id ;

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaPassagemLojaPessoas(VivoVisitaPassagemPessoas Pessoas, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_LOJA_PESSOAS] " +
                  " SET [PessoasSolicitadoSPPVagasAbertoSimNao] = '" + Pessoas.PessoasSolicitadoSPPVagasAbertoSimNao + "'" +
                  " ,[PessoasSolicitadoSPPVagasAbertoObs] = '" + Pessoas.PessoasSolicitadoSPPVagasAbertoObs + "'" +
                  " ,[PessoasSolicitadoSPPVagasAbertoPrazo] = '" + Pessoas.PessoasSolicitadoSPPVagasAbertoPrazo + "'" +
                  " ,[PessoasSolicitadoSPPVagasAbertoResponsavel] = '" + Pessoas.PessoasSolicitadoSPPVagasAbertoResponsavel + "'" +
                  " ,[PessoasNaoExisteColaboradoresPendenciaTreinamentoSimNao] = '" + Pessoas.PessoasNaoExisteColaboradoresPendenciaTreinamentoSimNao + "'" +
                  " ,[PessoasNaoExisteColaboradoresPendenciaTreinamentoObs] = '" + Pessoas.PessoasNaoExisteColaboradoresPendenciaTreinamentoObs + "'" +
                  " ,[PessoasNaoExisteColaboradoresPendenciaTreinamentoPrazo] = '" + Pessoas.PessoasNaoExisteColaboradoresPendenciaTreinamentoPrazo + "'" +
                  " ,[PessoasNaoExisteColaboradoresPendenciaTreinamentoResponsavel] = '" + Pessoas.PessoasNaoExisteColaboradoresPendenciaTreinamentoResponsavel + "'" +
                  " ,[PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasSimNao] = '" + Pessoas.PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasSimNao + "'" +
                  " ,[PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasObs] = '" + Pessoas.PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasObs + "'" +
                  " ,[PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasPrazo] = '" + Pessoas.PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasPrazo + "'" +
                  " ,[PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasResponsavel] = '" + Pessoas.PessoasLojaPossuiArquivoFolhaPontoTodasAssinadasResponsavel + "'" +
                  " ,[PessoasLojaPossuiProgramacaoFeriasSimNao] = '" + Pessoas.PessoasLojaPossuiProgramacaoFeriasSimNao + "'" +
                  " ,[PessoasLojaPossuiProgramacaoFeriasObs] = '" + Pessoas.PessoasLojaPossuiProgramacaoFeriasObs + "'" +
                  " ,[PessoasLojaPossuiProgramacaoFeriasPrazo] = '" + Pessoas.PessoasLojaPossuiProgramacaoFeriasPrazo + "'" +
                  " ,[PessoasLojaPossuiProgramacaoFeriasResponsavel] = '" + Pessoas.PessoasLojaPossuiProgramacaoFeriasResponsavel + "'" +
                  " ,[PessoasTodosColaboradoresPossuemCrachaSimNao] = '" + Pessoas.PessoasTodosColaboradoresPossuemCrachaSimNao + "'" +
                  " ,[PessoasTodosColaboradoresPossuemCrachaObs] = '" + Pessoas.PessoasTodosColaboradoresPossuemCrachaObs + "'" +
                  " ,[PessoasTodosColaboradoresPossuemCrachaPrazo] = '" + Pessoas.PessoasTodosColaboradoresPossuemCrachaPrazo + "'" +
                  " ,[PessoasTodosColaboradoresPossuemCrachaResponsavel] = '" + Pessoas.PessoasTodosColaboradoresPossuemCrachaResponsavel + "'" +
                  " ,[DataCadastro] = GetDate() " +
                  " WHERE [IdAbertura] = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaPassagemLojaVagas(VivoVisitaPassagemVagas Vagas, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_PASSAGEM_LOJA_VAGAS] " +
                  " SET [VagasQuantidadeVagasGerenteGeral] = '" + Vagas.VagasQuantidadeVagasGerenteGeral + "'" +
                  "    ,[VagasQuantidadeVagasGerenteNegocios] = '" + Vagas.VagasQuantidadeVagasGerenteNegocios + "'" +
                  "    ,[VagasQuantidadeVagasConsultorRelacionamento] = '" + Vagas.VagasQuantidadeVagasConsultorRelacionamento + "'" +
                  "    ,[VagasQuantidadeVagasConsultorPremium] = '" + Vagas.VagasQuantidadeVagasConsultorPremium + "'" +
                  "    ,[VagasQuantidadeVagasConsultorNegocios] = '" + Vagas.VagasQuantidadeVagasConsultorNegocios + "'" +
                  "    ,[VagasQuantidadeVagasAnalistaSuporteComercial] = '" + Vagas.VagasQuantidadeVagasAnalistaSuporteComercial + "'" +
                  "    ,[VagasQuantidadeVagasCaixa] = '" + Vagas.VagasQuantidadeVagasCaixa + "'" +
                  "    ,[VagasQuantidadeVagasRecepcionista] = '" + Vagas.VagasQuantidadeVagasRecepcionista + "'" +
                  "    ,[VagasAtivosGerenteGeral] = '" + Vagas.VagasAtivosGerenteGeral + "'" +
                  "    ,[VagasAtivosGerenteNegocios] = '" + Vagas.VagasAtivosGerenteNegocios + "'" +
                  "    ,[VagasAtivosConsultorRelacionamento] = '" + Vagas.VagasAtivosConsultorRelacionamento + "'" +
                  "    ,[VagasAtivosConsultorPremium] = '" + Vagas.VagasAtivosConsultorPremium + "'" +
                  "    ,[VagasAtivosConsultorNegocios] = '" + Vagas.VagasAtivosConsultorNegocios + "'" +
                  "    ,[VagasAtivosAnalistaSuporteComercial] = '" + Vagas.VagasAtivosAnalistaSuporteComercial + "'" +
                  "    ,[VagasAtivosCaixa] = '" + Vagas.VagasAtivosCaixa + "'" +
                  "    ,[VagasAtivosRecepcionista] = '" + Vagas.VagasAtivosRecepcionista + "'" +
                  "    ,[VagasVagasAbertoGerenteGeral] = '" + Vagas.VagasVagasAbertoGerenteGeral + "'" +
                  "    ,[VagasVagasAbertoGerenteNegocios] = '" + Vagas.VagasVagasAbertoGerenteNegocios + "'" +
                  "    ,[VagasVagasAbertoConsultorRelacionamento] = '" + Vagas.VagasVagasAbertoConsultorRelacionamento + "'" +
                  "    ,[VagasVagasAbertoConsultorPremium] = '" + Vagas.VagasVagasAbertoConsultorPremium + "'" +
                  "    ,[VagasVagasAbertoConsultorNegocios] = '" + Vagas.VagasVagasAbertoConsultorNegocios + "'" +
                  "    ,[VagasVagasAbertoAnalistaSuporteComercial] = '" + Vagas.VagasVagasAbertoAnalistaSuporteComercial + "'" +
                  "    ,[VagasVagasAbertoCaixa] = '" + Vagas.VagasVagasAbertoCaixa + "'" +
                  "    ,[VagasVagasAbertoRecepcionista] = '" + Vagas.VagasVagasAbertoRecepcionista + "'" +
                  "    ,[VagasLicencaAfastadosGerenteGeral] = '" +  Vagas.VagasLicencaAfastadosGerenteGeral + "'" +
                  "    ,[VagasLicencaAfastadosGerenteNegocios] = '" + Vagas.VagasLicencaAfastadosGerenteNegocios + "'" +
                  "    ,[VagasLicencaAfastadosConsultorRelacionamento] = '" + Vagas.VagasLicencaAfastadosConsultorRelacionamento + "'" +
                  "    ,[VagasLicencaAfastadosConsultorPremium] = '" + Vagas.VagasLicencaAfastadosConsultorPremium + "'" +
                  "    ,[VagasLicencaAfastadosConsultorNegocios] = '" +  Vagas.VagasLicencaAfastadosConsultorNegocios + "'" +
                  "    ,[VagasLicencaAfastadosAnalistaSuporteComercial] = '" + Vagas.VagasLicencaAfastadosAnalistaSuporteComercial + "'" +
                  "    ,[VagasLicencaAfastadosCaixa] = '" + Vagas.VagasLicencaAfastadosCaixa + "'" +
                  "    ,[VagasLicencaAfastadosRecepcionista] = '" + Vagas.VagasLicencaAfastadosRecepcionista + "'" +
                  "    ,[VagasQuadroTotalGerenteGeral] = '" + Vagas.VagasQuadroTotalGerenteGeral + "'" +
                  "    ,[VagasQuadroTotalGerenteNegocios] = '" + Vagas.VagasQuadroTotalGerenteNegocios + "'" +
                  "    ,[VagasQuadroTotalConsultorRelacionamento] = '" + Vagas.VagasQuadroTotalConsultorRelacionamento + "'" +
                  "    ,[VagasQuadroTotalConsultorPremium] = '" + Vagas.VagasQuadroTotalConsultorPremium + "'" +
                  "    ,[VagasQuadroTotalConsultorNegocios] = '" + Vagas.VagasQuadroTotalConsultorNegocios + "'" +
                  "    ,[VagasQuadroTotalAnalistaSuporteComercial] = '" + Vagas.VagasQuadroTotalAnalistaSuporteComercial + "'" +
                  "    ,[VagasQuadroTotalCaixa] = '" + Vagas.VagasQuadroTotalCaixa + "'" +
                  "    ,[VagasQuadroTotalRecepcionista] = '" + Vagas.VagasQuadroTotalRecepcionista + "'" +
                  "    ,[DataCadastro] = GetDate() " +
                  " WHERE [IdAbertura] = " + Id;
        
            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaCanalTerritorioPessoas(VivoVisitaCanalTerritorioPessoas Pessoas, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_CANAL_TERRITORIO_PESSOAS] " +
                  " SET PessoasExisteVagaOpenSimNao = '" + Pessoas.PessoasExisteVagaOpenSimNao + "'" +
                  " ,PessoasExisteVagaOpenObs = '" + Pessoas.PessoasExisteVagaOpenObs + "'" +
                  " ,PessoasPossuiVagaExcedenteReintegracaoSimNao = '" + Pessoas.PessoasPossuiVagaExcedenteReintegracaoSimNao + "'" +
                  " ,PessoasPossuiVagaExcedenteReintegracaoObs = '" + Pessoas.PessoasPossuiVagaExcedenteReintegracaoObs + "'" +
                  " ,PessoasVerificarExisteFuncionarioAfastadoSimNao = '" + Pessoas.PessoasVerificarExisteFuncionarioAfastadoSimNao + "'" +
                  " ,PessoasVerificarExisteFuncionarioAfastadoObs = '" + Pessoas.PessoasVerificarExisteFuncionarioAfastadoObs + "'" +
                  " ,PessoasVerificarexisteFuncionarioEstabilidadeSimNao = '" + Pessoas.PessoasVerificarexisteFuncionarioEstabilidadeSimNao + "'" +
                  " ,PessoasVerificarexisteFuncionarioEstabilidadeObs = '" + Pessoas.PessoasVerificarexisteFuncionarioEstabilidadeObs + "'" +
                  " ,PessoasVerificarUniformeSimNao = '" + Pessoas.PessoasVerificarUniformeSimNao + "'" +
                  " ,PessoasVerificarUniformeObs = '" + Pessoas.PessoasVerificarUniformeObs + "'" +
                  " ,PessoasVerificarAbsenteismoSimNao = '" + Pessoas.PessoasVerificarAbsenteismoSimNao + "'" +
                  " ,PessoasVerificarAbsenteismoObs = '" + Pessoas.PessoasVerificarAbsenteismoObs + "'" +
                  " ,PessoasVerificarFechamentoPontoHCSimNao = '" + Pessoas.PessoasVerificarFechamentoPontoHCSimNao + "'" +
                  " ,PessoasVerificarFechamentoPontoHCObs = '" + Pessoas.PessoasVerificarFechamentoPontoHCObs + "'" +
                  " ,PessoasVerificarDocumentacaoFiscalizacaoSimNao = '" + Pessoas.PessoasVerificarDocumentacaoFiscalizacaoSimNao + "'" +
                  " ,PessoasVerificarDocumentacaoFiscalizacaoObs = '" + Pessoas.PessoasVerificarDocumentacaoFiscalizacaoObs + "'" +
                  " ,PessoasTodosEstaoCrachaSimNao = '" + Pessoas.PessoasTodosEstaoCrachaSimNao + "'" +
                  " ,PessoasTodosEstaoCrachaObs = '" + Pessoas.PessoasTodosEstaoCrachaObs + "'" +
                  " ,PessoasVerificarApresentacaoPessoalEquipeSimNao = '" + Pessoas.PessoasVerificarApresentacaoPessoalEquipeSimNao + "'" +
                  " ,PessoasVerificarApresentacaoPessoalEquipeObs = '" + Pessoas.PessoasVerificarApresentacaoPessoalEquipeObs + "'" +
                  " ,PessoasVerificarAdesaoEscalaSimNao = '" + Pessoas.PessoasVerificarAdesaoEscalaSimNao + "'" +
                  " ,PessoasVerificarAdesaoEscalaObs = '" + Pessoas.PessoasVerificarAdesaoEscalaObs + "'" +
                  " ,PessoasVerificarClimaEquipeSimNao = '" + Pessoas.PessoasVerificarClimaEquipeSimNao + "'" +
                  " ,PessoasVerificarClimaEquipeObs = '" + Pessoas.PessoasVerificarClimaEquipeObs + "'" +
                  " ,PessoasRealizarInteracaoEquipeSimNao = '" + Pessoas.PessoasRealizarInteracaoEquipeSimNao + "'" +
                  " ,PessoasRealizarInteracaoEquipeObs = '" + Pessoas.PessoasRealizarInteracaoEquipeObs + "'" +
                  " ,PessoasTodosEstaoSenhasLiberadasAcessoSistemasSimNao = '" + Pessoas.PessoasTodosEstaoSenhasLiberadasAcessoSistemasSimNao + "'" +
                  " ,PessoasTodosEstaoSenhasLiberadasAcessoSistemasObs = '" + Pessoas.PessoasTodosEstaoSenhasLiberadasAcessoSistemasObs + "'" +
                  " ,PessoasVerificarAdesaoTreinamentosSimNao = '" + Pessoas.PessoasVerificarAdesaoTreinamentosSimNao + "'" +
                  " ,PessoasVerificarAdesaoTreinamentosObs = '" + Pessoas.PessoasVerificarAdesaoTreinamentosObs + "'" +
                  " ,PessoasVerificarEscalaFeriasSimNao = '" + Pessoas.PessoasVerificarEscalaFeriasSimNao + "'" +
                  " ,PessoasVerificarEscalaFeriasObs = '" + Pessoas.PessoasVerificarEscalaFeriasObs + "'" +
                  " ,PessoasParticiparMatinaisVespertinasSimNao = '" + Pessoas.PessoasParticiparMatinaisVespertinasSimNao + "'" +
                  " ,PessoasParticiparMatinaisVespertinasObs = '" + Pessoas.PessoasParticiparMatinaisVespertinasObs + "'" +
                  " ,PessoasConfirmarTodosCargosEstaoRealizandoAtuacaoSimNao = '" + Pessoas.PessoasConfirmarTodosCargosEstaoRealizandoAtuacaoSimNao + "'" +
                  " ,PessoasConfirmarTodosCargosEstaoRealizandoAtuacaoObs = '" + Pessoas.PessoasConfirmarTodosCargosEstaoRealizandoAtuacaoObs + "'" +
                  " ,[DataCadastro] = GetDate() " +
                  " WHERE [IdAbertura] = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaCanalTerritorioEstruturasOperacao(VivoVisitaCanalTerritorioEstruturaOperacoes EstruturaOperacao, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_CANAL_TERRITORIO_ESTRUTURAS_OPERACAO] " +
                  "  SET [EstruturaOperacoesPositivacaoVerificarMaterialPositivadoMapaPDVSimNao] = '" + EstruturaOperacao.EstruturaOperacoesPositivacaoVerificarMaterialPositivadoMapaPDVSimNao + "'" +
                  " ,[EstruturaOperacoesPositivacaoVerificarMaterialPositivadoMapaPDVObs] = '" + EstruturaOperacao.EstruturaOperacoesPositivacaoVerificarMaterialPositivadoMapaPDVObs + "'" +
                  " ,[EstruturaOperacoesPositivacaoVerificarExisteFaltaMaterialSimNao] = '" + EstruturaOperacao.EstruturaOperacoesPositivacaoVerificarExisteFaltaMaterialSimNao + "'" +
                  " ,[EstruturaOperacoesPositivacaoVerificarExisteFaltaMaterialObs] = '" + EstruturaOperacao.EstruturaOperacoesPositivacaoVerificarExisteFaltaMaterialObs + "'" +
                  " ,[EstruturaOperacoesPositivacaoVerificarExisteMaterialVencidoAcumuladoLojaSimNao] = '" + EstruturaOperacao.EstruturaOperacoesPositivacaoVerificarExisteMaterialVencidoAcumuladoLojaSimNao + "'" +
                  " ,[EstruturaOperacoesPositivacaoVerificarExisteMaterialVencidoAcumuladoLojaObs] = '" + EstruturaOperacao.EstruturaOperacoesPositivacaoVerificarExisteMaterialVencidoAcumuladoLojaObs + "'" +
                  " ,[EstruturaOperacoesPositivacaoVerificarAparelhosLigadosVitrineSimNao] = '" + EstruturaOperacao.EstruturaOperacoesPositivacaoVerificarAparelhosLigadosVitrineSimNao + "'" +
                  " ,[EstruturaOperacoesPositivacaoVerificarAparelhosLigadosVitrineObs] = '" + EstruturaOperacao.EstruturaOperacoesPositivacaoVerificarAparelhosLigadosVitrineObs + "'" +
                  " ,[EstruturaOperacoesVerificarPendenciaManutencaoSimNao] = '" + EstruturaOperacao.EstruturaOperacoesVerificarPendenciaManutencaoSimNao + "'" +
                  " ,[EstruturaOperacoesVerificarPendenciaManutencaoObs] = '" + EstruturaOperacao.EstruturaOperacoesVerificarPendenciaManutencaoObs + "'" +
                  " ,[EstruturaOperacoesVerificarExisteChamadadoAbertoManutencaoSimNao] = '" + EstruturaOperacao.EstruturaOperacoesVerificarExisteChamadadoAbertoManutencaoSimNao + "'" +
                  " ,[EstruturaOperacoesVerificarExisteChamadadoAbertoManutencaoObs] = '" + EstruturaOperacao.EstruturaOperacoesVerificarExisteChamadadoAbertoManutencaoObs + "'" +
                  " ,[EstruturaOperacoesConferirEquipamentoInformaticaSimNao] = '" + EstruturaOperacao.EstruturaOperacoesConferirEquipamentoInformaticaSimNao + "'" +
                  " ,[EstruturaOperacoesConferirEquipamentoInformaticaObs] = '" + EstruturaOperacao.EstruturaOperacoesConferirEquipamentoInformaticaObs + "'" +
                  " ,[EstruturaOperacoesConferirOrganizacaoAreaInternaSimNao] = '" + EstruturaOperacao.EstruturaOperacoesConferirOrganizacaoAreaInternaSimNao + "'" +
                  " ,[EstruturaOperacoesConferirOrganizacaoAreaInternaObs] = '" + EstruturaOperacao.EstruturaOperacoesConferirOrganizacaoAreaInternaObs + "'" +
                  " ,[EstruturaOperacoesVerificarExisteAcumuloEquipamentoMobiliarioDevolucaoSimNao] = '" + EstruturaOperacao.EstruturaOperacoesVerificarExisteAcumuloEquipamentoMobiliarioDevolucaoSimNao + "'" +
                  " ,[EstruturaOperacoesVerificarExisteAcumuloEquipamentoMobiliarioDevolucaoObs] = '" + EstruturaOperacao.EstruturaOperacoesVerificarExisteAcumuloEquipamentoMobiliarioDevolucaoObs + "'" +
                  " ,[EstruturaOperacoesConferirLimpezaLojaAreaInternaExternaSimNao] = '" + EstruturaOperacao.EstruturaOperacoesConferirLimpezaLojaAreaInternaExternaSimNao + "'" +
                  " ,[EstruturaOperacoesConferirLimpezaLojaAreaInternaExternaObs] = '" + EstruturaOperacao.EstruturaOperacoesConferirLimpezaLojaAreaInternaExternaObs + "'" +
                  " ,[EstruturaOperacoesConferirVitrinesEstaoTrancadasSimNao] = '" + EstruturaOperacao.EstruturaOperacoesConferirVitrinesEstaoTrancadasSimNao + "'" +
                  " ,[EstruturaOperacoesConferirVitrinesEstaoTrancadasObs] = '" + EstruturaOperacao.EstruturaOperacoesConferirVitrinesEstaoTrancadasObs + "'" +
                  " ,[EstruturaOperacoesConferirAlarmeEstaFuncionandoSimNao] = '" + EstruturaOperacao.EstruturaOperacoesConferirAlarmeEstaFuncionandoSimNao + "'" +
                  " ,[EstruturaOperacoesConferirAlarmeEstaFuncionandoObs] = '" +  EstruturaOperacao.EstruturaOperacoesConferirAlarmeEstaFuncionandoObs + "'" +
                  " ,[DataCadastro] = GetDate() " +
                  " WHERE [IdAbertura] = " + Id;











            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaCanalTerritorioProcessos(VivoVisitaCanalTerritorioProcessos Processos, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_CANAL_TERRITORIO_PROCESSOS] " +
                  "  SET [ProcessosVerificarRealizacaoContagemDiariaEstoqueSimNao] = '" + Processos.ProcessosVerificarRealizacaoContagemDiariaEstoqueSimNao + "'" +
                  "  ,[ProcessosVerificarRealizacaoContagemDiariaEstoqueObs] = '" + Processos.ProcessosVerificarRealizacaoContagemDiariaEstoqueObs + "'" +
                  "  ,[ProcessosVerificarPlanilhaVendasConferidaSinalizacaoEspecialSimNao] = '" + Processos.ProcessosVerificarPlanilhaVendasConferidaSinalizacaoEspecialSimNao + "'" +
                  "  ,[ProcessosVerificarPlanilhaVendasConferidaSinalizacaoEspecialObs] = '" + Processos.ProcessosVerificarPlanilhaVendasConferidaSinalizacaoEspecialObs + "'" +
                  "  ,[ProcessosVerificarOrganizacaoEstoqueSimNao] = '" + Processos.ProcessosVerificarOrganizacaoEstoqueSimNao + "'" +
                  "  ,[ProcessosVerificarOrganizacaoEstoqueObs] = '" + Processos.ProcessosVerificarOrganizacaoEstoqueObs + "'" +
                  "  ,[ProcessosVerificarCofreTrancadoSenhaEquipeGerencialSimNao] = '" + Processos.ProcessosVerificarCofreTrancadoSenhaEquipeGerencialSimNao + "'" +
                  "  ,[ProcessosVerificarCofreTrancadoSenhaEquipeGerencialObs] = '" + Processos.ProcessosVerificarCofreTrancadoSenhaEquipeGerencialObs + "'" +
                  "  ,[ProcessosVerificarExisteAcumuloRPARDevolucaoSimNao] = '" + Processos.ProcessosVerificarExisteAcumuloRPARDevolucaoSimNao + "'" +
                  "  ,[ProcessosVerificarExisteAcumuloRPARDevolucaoObs] = '" + Processos.ProcessosVerificarExisteAcumuloRPARDevolucaoObs + "'" +
                  "  ,[ProcessosRealizarEventualmenteContagemConjuntoGerenteAuditoriaProcessosSimNao] = '" + Processos.ProcessosRealizarEventualmenteContagemConjuntoGerenteAuditoriaProcessosSimNao + "'" +
                  "  ,[ProcessosRealizarEventualmenteContagemConjuntoGerenteAuditoriaProcessosObs] = '" + Processos.ProcessosRealizarEventualmenteContagemConjuntoGerenteAuditoriaProcessosObs + "'" +
                  "  ,[ProcessosVerificarEnvioNumerarioSimNao] = '" + Processos.ProcessosVerificarEnvioNumerarioSimNao + "'" +
                  "  ,[ProcessosVerificarEnvioNumerarioObs] = '" + Processos.ProcessosVerificarEnvioNumerarioObs + "'" +
                  "  ,[ProcessosConferirFundoTrocoSimNao] = '" + Processos.ProcessosConferirFundoTrocoSimNao + "'" +
                  "  ,[ProcessosConferirFundoTrocoObs] = '" + Processos.ProcessosConferirFundoTrocoObs + "'" +
                  "  ,[ProcessosGestaoDocumentalConferirDocumentosAcumuladosLojaNDigitalizadosSimNao] = '" + Processos.ProcessosGestaoDocumentalConferirDocumentosAcumuladosLojaNDigitalizadosSimNao + "'" +
                  "  ,[ProcessosGestaoDocumentalConferirDocumentosAcumuladosLojaNDigitalizadosObs] = '" + Processos.ProcessosGestaoDocumentalConferirDocumentosAcumuladosLojaNDigitalizadosObs + "'" +
                  "  ,[ProcessosGestaoDocumentalObservarLojaEstaUtilizandoTabletsAssinaturaDigitalSimNao] = '" + Processos.ProcessosGestaoDocumentalObservarLojaEstaUtilizandoTabletsAssinaturaDigitalSimNao + "'" +
                  "  ,[ProcessosGestaoDocumentalObservarLojaEstaUtilizandoTabletsAssinaturaDigitalObs] = '" + Processos.ProcessosGestaoDocumentalObservarLojaEstaUtilizandoTabletsAssinaturaDigitalObs + "'" +
                  "  ,[ProcessosAnatelVerificarPastaGSSSimNao] = '" + Processos.ProcessosAnatelVerificarPastaGSSSimNao + "'" +
                  "  ,[ProcessosAnatelVerificarPastaGSSObs] = '" + Processos.ProcessosAnatelVerificarPastaGSSObs + "'" +
                  "  ,[ProcessosAnatelVerificarJustificativaSMP14SimNao] = '" + Processos.ProcessosAnatelVerificarJustificativaSMP14SimNao + "'" +
                  "  ,[ProcessosAnatelVerificarJustificativaSMP14Obs] = '" + Processos.ProcessosAnatelVerificarJustificativaSMP14Obs + "'" +
                  "  ,[ProcessosAnatelVerificarAtendimentoDevidamenteRegistradosSimNao] = '" + Processos.ProcessosAnatelVerificarAtendimentoDevidamenteRegistradosSimNao + "'" +
                  "  ,[ProcessosAnatelVerificarAtendimentoDevidamenteRegistradosObs] = '" + Processos.ProcessosAnatelVerificarAtendimentoDevidamenteRegistradosObs + "'" +
                  "  ,[ProcessosAnatelObservarGestaoFluxoGeralLojaSimNao] = '" + Processos.ProcessosAnatelObservarGestaoFluxoGeralLojaSimNao + "'" +
                  "  ,[ProcessosAnatelObservarGestaoFluxoGeralLojaObs] = '" + Processos.ProcessosAnatelObservarGestaoFluxoGeralLojaObs + "'" +
                  "  ,[ProcessosVerificarQuadroAvisosAtualizacaoPadraoPEXSimNao] = '" + Processos.ProcessosVerificarQuadroAvisosAtualizacaoPadraoPEXSimNao + "'" +
                  "  ,[ProcessosVerificarQuadroAvisosAtualizacaoPadraoPEXObs] = '" + Processos.ProcessosVerificarQuadroAvisosAtualizacaoPadraoPEXObs + "'" +
                  "  ,[ProcessosSuprimentosVerificarEstoqueMaterialEscritorioSimNao] = '" + Processos.ProcessosSuprimentosVerificarEstoqueMaterialEscritorioSimNao + "'" +
                  "  ,[ProcessosSuprimentosVerificarEstoqueMaterialEscritorioObs]  = '" + Processos.ProcessosSuprimentosVerificarEstoqueMaterialEscritorioObs + "'" +
                  "  ,[DataCadastro] = GetDate() " + 
                  "  WHERE [IdAbertura] = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaCanalTerritorioIndicadores(VivoVisitaCanalTerritorioIndicadores Indicadores, int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[VIVO_VISITA_CANAL_TERRITORIO_INDICADORES] " +
                  "  SET [ResultadosIndicadoresAvaliarPerformanceCorrenteSimNao] = '" + Indicadores.ResultadosIndicadoresAvaliarPerformanceCorrenteSimNao + "'" +
                  "  ,[ResultadosIndicadoresAvaliarPerformanceCorrenteObs] = '" + Indicadores.ResultadosIndicadoresAvaliarPerformanceCorrenteObs + "'" +
                  "  ,[ResultadosIndicadoresIndicadoresTempoSimNao] = '" + Indicadores.ResultadosIndicadoresIndicadoresTempoSimNao + "'" +
                  "  ,[ResultadosIndicadoresIndicadoresTempoObs] = '" + Indicadores.ResultadosIndicadoresIndicadoresTempoObs + "'" +
                  "  ,[ResultadosIndicadoresAcompanharAlgunsAtendimentosSimNao] = '" + Indicadores.ResultadosIndicadoresAcompanharAlgunsAtendimentosSimNao + "'" +
                  "  ,[ResultadosIndicadoresAcompanharAlgunsAtendimentosObs] = '" +Indicadores.ResultadosIndicadoresAcompanharAlgunsAtendimentosObs + "'" +
                  "  ,[ResultadosIndicadoresRentabilidadeSimNao] = '" + Indicadores.ResultadosIndicadoresRentabilidadeSimNao + "'" +
                  "  ,[ResultadosIndicadoresRentabilidadeObs]  = '" + Indicadores.ResultadosIndicadoresRentabilidadeObs + "'" +
                  "  ,[DataCadastro] = GetDate() " +
                  "   WHERE [IdAbertura] = " + Id;

            return dao.ExecutarSql(SQL);
        }
    }
}