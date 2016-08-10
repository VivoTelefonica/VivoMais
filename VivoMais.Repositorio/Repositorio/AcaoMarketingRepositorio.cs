using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class AcaoMarketingRepositorio
    {
        DAO dao;

        public AcaoMarketingRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public bool CadastroHistorico(int Id)
        {
            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[ACAO_HISTORICO] (IDVPC,LIBERACAO_VERBA, CADASTRO_ACAO) " +
            " SELECT IDVPC, LIBERACAO_VERBA,GETDATE() " +
            " FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] " +
            " WHERE ID = (SELECT MAX(ID) FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] WHERE IDVPC = '" + Id + "' ) ";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaHistoricoMKTTerritorial(int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_HISTORICO] " +
            " SET [VALIDACAO_MKT_TERRITORIAL] = GETDATE() " +
            " WHERE ID = (SELECT MAX(ID) FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] WHERE IDVPC = '" + Id + "' ) ";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaHistoricoAprovar(int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_HISTORICO] " +
            " SET [APROVACAO] = GETDATE() " +
            " WHERE ID = (SELECT MAX(ID) FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] WHERE IDVPC = '" + Id + "' ) ";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaHistoricoMKTRegional(int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_HISTORICO] " +
            " SET [VALIDACAO_MKT_REGIONAL] = GETDATE() " +
            " WHERE ID = (SELECT MAX(ID) FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] WHERE IDVPC = '" + Id + "' ) ";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaHistoricoGerenteContas(int Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_HISTORICO] " +
            " SET [RETORNAR_GC] = GETDATE() " +
            " WHERE ID = (SELECT MAX(ID) FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] WHERE IDVPC = '" + Id + "' ) ";

            return dao.ExecutarSql(SQL);
        }


        public bool VerificaHistorico(int Id)
        {
            string SQL = " SELECT * " +
                " FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] " +
                " WHERE CADASTRO_ACAO IS NOT NULL " +
                " AND VALIDACAO_MKT_TERRITORIAL IS NULL " +
                " AND VALIDACAO_MKT_REGIONAL IS NULL " +
                " AND APROVACAO IS NULL " +
                " AND RETORNAR_GC IS NULL " +
                " AND ID = (SELECT MAX(ID) FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] WHERE IDVPC = '" + Id + "' ) ";

            DataTable dsAcompanhamento;

            try
            {
                dsAcompanhamento = this.dao.returnaDataTable(SQL);

                if (dsAcompanhamento != null)
                {
                    if (dsAcompanhamento.Rows.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<AcaoMarketing> ObterTimeline(string Protocolo) 
        {
            AcaoAcompanhamentoRepositorio _respositorio = new AcaoAcompanhamentoRepositorio();
            AcaoAcompanhamento Acompanhamento = _respositorio.ObterAcaoAcompanhamentoProtocolo(Protocolo);
            
            string SQL = " SELECT DISTINCT " +
                         "        Id " +
                         "       ,CASE WHEN idStatus = 3 THEN 'Cadastrado' " +
	                     "             WHEN idStatus = 4 THEN 'Aprovado pelo Territorial' " +
	                     "             WHEN idStatus = 5 THEN 'Validado pelo MKT Regional' " +
	                     "             WHEN idStatus = 7 THEN 'Cancelado' " +
	                     "             WHEN idStatus = 8 THEN 'Finalizada' " +
	                     "             WHEN idStatus = 9 THEN 'Excluído' " +
	                     "             WHEN idStatus = 10 THEN 'Reprovado' " +
	                     "             WHEN idStatus = 11 THEN 'Validado pela MKT Territorial' " +
	                     "             WHEN idStatus = 13 THEN 'Executada' " +
	                     "             WHEN idStatus = 14 THEN 'Postergada' " +
	                     "             WHEN idStatus = 15 THEN 'Devolvida ao Gerente de Contas' " +
	                     "             ELSE 'Outros' END AS Status " +
                         "       ,Consideracao " +
                         "       ,CONVERT(DATE,Data,103) AS Data " +
                         "   FROM [Vivo_VPC].[dbo].[CADASTRO_VPC_BKP] " +
                         "   WHERE ID = 4730 " +
                         "   AND idStatus NOT IN (13,14,15,8) " +
                         "   ORDER BY Data ";

            List<AcaoMarketing> retorno = null;
            DataTable dsAcao = dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                retorno = new List<AcaoMarketing>();

                foreach (DataRow item in dsAcao.Rows)
                {
                    AcaoMarketing Acoes = new AcaoMarketing();
                    Acoes.Acompanhamento = Acompanhamento;

                    StatusAcao Status = new StatusAcao();
                    Status.Descricao = item[1].ToString();

                    Acoes.Status = Status;
                    Acoes.Protocolo = Protocolo;
                    Acoes.Id = Convert.ToInt32(item[0]);
                    Acoes.Consideracao = item[2].ToString();
                    Acoes.DataCadastro = Convert.ToDateTime(item[3]);

                    retorno.Add(Acoes);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<AcaoMarketing> ObterAcompanhamentoAcoes(AcaoMarketing acao, string Login)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();

            string SQL = " SELECT Protocolo " +
            "       ,origemVerba " +
            "       ,Adabas " +
            "       ,SUM(valorTotalAcao) as valorTotalAcao " +
            "       ,SUM(valorTotalVivo) as valorTotalVivo " +
            " FROM Vivo_SIM.dbo.ACAO_CADASTRO INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC " +
            " ON (Vivo_SIM.dbo.ACAO_CADASTRO.adabas = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC.VENDEDOR) " +
            " WHERE MATRICULA_GER_CONTA = '" + Login + "' " +
            " AND Vivo_SIM.dbo.ACAO_CADASTRO.DDD = '" + acao.Parceiro.Ddd + "' " +
            " AND MesAcao = '" + acao.MesAcao + "' " +
            " AND Vivo_SIM.dbo.ACAO_CADASTRO.IdStatus = 4 ";

            if (acao.Parceiro.Rede != "TODOS") 
            {
                SQL = SQL + " AND REDE = '" + acao.Parceiro.Rede + "' ";
            }

            SQL = SQL + " GROUP BY Protocolo " +
            "       ,origemVerba " +
            "       ,Adabas " +
            "       ,mesAcao";


            List<AcaoMarketing> retorno = null;
            DataTable dsAcao = dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                retorno = new List<AcaoMarketing>();

                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _respositorio.ObterInformacoesParceiros(item[2].ToString());
                    
                    AcaoMarketing Acoes = new AcaoMarketing();
                    Acoes.Parceiro = p;

                    Acoes.Protocolo = item[0].ToString();
                    Acoes.OrigemVerba = item[1].ToString();
                    Acoes.ValorTotalAcao = item[3].ToString();
                    Acoes.ValorTotalVivo = item[4].ToString();

                    retorno.Add(Acoes);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public List<AcaoMarketing> SelecionarRel(string MesAcao, string Login)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();
            AcaoAcompanhamentoRepositorio _respositorioAcaoAcompanhamento = new AcaoAcompanhamentoRepositorio();

            string SQL = " SELECT VPC.Protocolo " +
            " ,VPC.Adabas " +
            " ,SUM(valorTotalAcao) AS [TOTALPPC] " +
            " ,SUM(valorTotalVivo) AS [REEMBOLSOREC] " +
            " ,mesAcao AS [COMPETENCIA] " +
            " ,CC.CENTRO_CUSTO " +
            " FROM Vivo_SIM.dbo.ACAO_CADASTRO VPC INNER JOIN Vivo_SIM.dbo.ACAO_ACOMPANHAMENTO ON (Vivo_SIM.dbo.ACAO_ACOMPANHAMENTO.PROTOCOLO = VPC.Protocolo) " +
            " INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC ON (Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC.VENDEDOR = VPC.ADABAS) " +
            " LEFT JOIN Vivo_DE_PARA.dbo.DE_PARA_CENTRO_CUSTO CC  ON (Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC.CANAL = CC.Sigla AND VPC.praca = CC.Divisão) " +
            " WHERE ((Vivo_SIM.dbo.ACAO_ACOMPANHAMENTO.NUMERO_PROTOCOLO IS NOT NULL) AND (Vivo_SIM.dbo.ACAO_ACOMPANHAMENTO.NUMERO_PROTOCOLO <> '')) " +
            " AND Vivo_SIM.dbo.ACAO_ACOMPANHAMENTO.DATA_ACOES_REL IS NULL " +
            " AND mesAcao = '" + MesAcao + "' " +
            " AND Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC.MATRICULA_GER_CONTA = '" + Login + "'" +
            " GROUP BY " +
            "      VPC.Protocolo " +
            "     ,VPC.Adabas " +
            "     ,CC.CENTRO_CUSTO " +
            "     ,mesAcao ";


            List<AcaoMarketing> retorno = new List<AcaoMarketing>();
            DataTable dsAcao = dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _respositorio.ObterInformacoesParceiros(item[1].ToString());
                    p.CentroCusto = item[5].ToString();

                    AcaoAcompanhamento Acompanhamento = _respositorioAcaoAcompanhamento.ObterAcaoAcompanhamentoProtocolo(item[0].ToString());

                    AcaoMarketing Acoes = new AcaoMarketing();
                    Acoes.Parceiro = p;
                    Acoes.Acompanhamento = Acompanhamento;
                    Acoes.Protocolo = item[0].ToString();
                    Acoes.ValorTotalAcao = item[2].ToString();
                    Acoes.ValorTotalVivo = item[3].ToString();
                    Acoes.MesAcao = item[4].ToString();


                    retorno.Add(Acoes);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public List<AcaoMarketing> ExtrairProtocolo(string MesAcao, string Login)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();
            AcaoAcompanhamentoRepositorio _respositorioAcaoAcompanhamento = new AcaoAcompanhamentoRepositorio();

            string SQL = " SELECT ACAO.ADABAS " +
                        " ,CC.CENTRO_CUSTO " +
                        " ,SUM(ACAO.valorTotalAcao) AS [TOTAL_PPC] " +
                        " ,SUM(ACAO.valorTotalVivo) AS [TOTAL_VIVO] " +
                        " ,ACAO.mesAcao AS [MES_ACAO] " +
                        " ,ACAO.origemVerba " +
                        " ,ACAO.Protocolo AS Identificador " +
                        " FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] ACAO LEFT JOIN [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO]" +
                        " ON (ACAO.[PROTOCOLO] = [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO].[PROTOCOLO])" +
                        " INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC CAD  ON (ACAO.adabas = CAD.VENDEDOR)" +
                        " INNER JOIN [Vivo_SIM].[dbo].[ACAO_STATUS] SV  ON (ACAO.idStatus = SV.id)" +
                        " LEFT JOIN Vivo_DE_PARA.dbo.DE_PARA_CENTRO_CUSTO CC  ON (CAD.CANAL = CC.Sigla AND ACAO.praca = CC.Divisão) " +
                        " WHERE mesAcao = '" + MesAcao + "' " +
                        " AND MATRICULA_GER_CONTA = '" + Login + "'" +
                        " AND SV.ID IN (4) " +
                        " GROUP BY " +
                        " praca " +
                        " ,CC.CENTRO_CUSTO " +
                        " ,ACAO.ADABAS " +
                        " ,ACAO.mesAcao " +
                        " ,ACAO.origemVerba " +
                        " ,ACAO.Protocolo ";
                        
            

                        List<AcaoMarketing> retorno = null;
                        DataTable dsAcao = dsAcao = this.dao.returnaDataTable(SQL);

                        if (dsAcao.Rows.Count > 0)
                        {
                            retorno = new List<AcaoMarketing>();

                            foreach (DataRow item in dsAcao.Rows)
                            {
                                Parceiro p = _respositorio.ObterInformacoesParceiros(item[0].ToString());
                                p.CentroCusto = item[1].ToString();

                                AcaoAcompanhamento Acompanhamento = _respositorioAcaoAcompanhamento.ObterAcaoAcompanhamentoProtocolo(item[6].ToString());

                                AcaoMarketing Acoes = new AcaoMarketing();
                                Acoes.Parceiro = p;
                                Acoes.Acompanhamento = Acompanhamento;

                                Acoes.ValorTotalAcao = item[2].ToString();
                                Acoes.ValorTotalVivo = item[3].ToString();
                                Acoes.MesAcao = item[4].ToString();
                                Acoes.OrigemVerba = item[5].ToString();
                                Acoes.Protocolo = item[6].ToString();

                                retorno.Add(Acoes);
                            }

                            return retorno;
                        }
                        else
                        {
                            return retorno;
                        }

        }

        public List<AcaoMarketing> ExtrairRel(string MesAcao, string Login)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();
            AcaoAcompanhamentoRepositorio _respositorioAcaoAcompanhamento = new AcaoAcompanhamentoRepositorio();

            string SQL = " SELECT ACAO.ADABAS " +
                        " ,SUM(ACAO.valorTotalAcao) AS [TOTAL_PPC] " +
                        " ,SUM(ACAO.valorTotalVivo) AS [TOTAL_VIVO] " +
                        " ,ACAO.mesAcao AS [MES_ACAO] " +
                        " ,ACAO.origemVerba " +
                        " ,ACAO.Protocolo AS Identificador " +
                        " ,CC.CENTRO_CUSTO " +
                        " FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] ACAO LEFT JOIN [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO]" +
                        " ON (ACAO.[PROTOCOLO] = [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO].[PROTOCOLO])" +
                        " INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC CAD  ON (ACAO.adabas = CAD.VENDEDOR)" +
                        " INNER JOIN [Vivo_SIM].[dbo].[ACAO_STATUS] SV  ON (ACAO.idStatus = SV.id)" +
                        " LEFT JOIN Vivo_DE_PARA.dbo.DE_PARA_CENTRO_CUSTO CC  ON (CAD.CANAL = CC.Sigla AND ACAO.praca = CC.Divisão) " +
                        " WHERE mesAcao = '" + MesAcao + "' " +
                        " AND CAD.MATRICULA_GER_CONTA = '" + Login + "'" +
                        " AND DATA_ACOES_REL IS NOT NULL " +
                        " AND ((NUMERO_PROTOCOLO IS NOT NULL) AND (NUMERO_PROTOCOLO <> '')) " +
                        " GROUP BY " +
                        " praca " +
                        " ,CC.CENTRO_CUSTO " +
                        " ,ACAO.ADABAS " +
                        " ,ACAO.mesAcao " +
                        " ,ACAO.origemVerba " +
                        " ,ACAO.Protocolo ";



            List<AcaoMarketing> retorno = null;
            DataTable dsAcao = dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                retorno = new List<AcaoMarketing>();

                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _respositorio.ObterInformacoesParceiros(item[0].ToString());
                    p.CentroCusto = item[6].ToString();

                    AcaoAcompanhamento Acompanhamento = _respositorioAcaoAcompanhamento.ObterAcaoAcompanhamentoProtocolo(item[5].ToString());

                    AcaoMarketing Acoes = new AcaoMarketing();
                    Acoes.Parceiro = p;
                    Acoes.Acompanhamento = Acompanhamento;

                    Acoes.ValorTotalAcao = item[1].ToString();
                    Acoes.ValorTotalVivo = item[2].ToString();
                    Acoes.MesAcao = item[3].ToString();
                    Acoes.OrigemVerba = item[4].ToString();
                    Acoes.Protocolo = item[5].ToString();

                    retorno.Add(Acoes);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public List<AcaoMarketing> ConsultarStatusAcoes(string Rede, string Status, string MesAcao)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();
            TipoMidiaRepositorio _respositorioMidia = new TipoMidiaRepositorio();
            StatusAcaoRepositorio _respositorioStatus = new StatusAcaoRepositorio();


            string SQL = " SELECT ADABAS " +
            " ,PROTOCOLO " +
            " ,[idTipo] AS MIDIA " +
            " ,[idTipo] AS [MIDIA DETALHADA] " +
            " ,ValorTotalAcao " +
            " ,ValorTotalVivo " +
            " ,percentParticipacaoVivo " +
            " ,idStatus " +
            " FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
            " INNER JOIN [Vivo_SIM].[dbo].[ACAO_STATUS] " +
            " ON ([Vivo_SIM].[dbo].[ACAO_CADASTRO].[idStatus] = [Vivo_SIM].[dbo].[ACAO_STATUS].[id]) " +
            " INNER JOIN [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " ON ([Vivo_SIM].[dbo].[ACAO_CADASTRO].[ADABAS] = [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].[VENDEDOR]) " +
            " WHERE [mesAcao] = '" + MesAcao + "'";

            if (!Rede.Equals("TODOS"))
            {
                SQL = SQL + " AND [REDE] = '" + Rede + "'";
            }

            if (!Status.Equals("TODOS"))
            {
                SQL = SQL + " AND [Vivo_SIM].[dbo].[ACAO_STATUS].DESCRICAO = '" + Status + "'";
            }

            List<AcaoMarketing> retorno = null;
            DataTable dsAcao = dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                retorno = new List<AcaoMarketing>();

                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _respositorio.ObterInformacoesParceiros(item[0].ToString());
                    TipoMidia tm = _respositorioMidia.ObterTipoMidiaId(Convert.ToInt32(item[2]));
                    TipoMidia md = _respositorioMidia.ObterMidiaDetalhadaId(item[3].ToString());
                    StatusAcao sa = _respositorioStatus.ObterTipoMidiaId(Convert.ToInt32(item[7]));

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Parceiro = p;
                    Acao.Midia = tm;
                    Acao.MidiaDetalhada = md;
                    Acao.Status = sa;

                    Acao.Protocolo = item[1].ToString();
                    Acao.ValorTotalAcao = item[4].ToString();
                    Acao.ValorTotalVivo = item[5].ToString();
                    Acao.PercentualParticipacaoVivo = item[6].ToString();

                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public SaldoRede ObterSaldoRede (string Rede, string OrigemVerba, string Regional)
        {
            string SQL = "SELECT [Rede] " +
            "      ,CASE WHEN SUM([Saldo_VPC]) IS NULL THEN 0 ELSE SUM([Saldo_VPC]) END AS [Saldo_VPC] " +
            "      ,CASE WHEN SUM([Saldo_VIO]) IS NULL THEN 0 ELSE SUM([Saldo_VIO]) END AS [Saldo_VIO] " +
            " FROM [Vivo_SIM].[dbo].[ACAO_SALDO_REDE] " +
            " WHERE [Rede] = '" + Rede + "' " +
            " AND Regional = '" + Regional + "'  " +
            " GROUP BY [Rede] ";

            DataTable dsColaborador = null;
            SaldoRede retorno = null;

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    retorno = new SaldoRede();

                    retorno.Rede = Convert.ToString(item[0]);
                    retorno.SaldoVpc = Convert.ToDecimal(item[1]);
                    retorno.SaldoVio = Convert.ToDecimal(item[2]);
                    retorno.Usado = ObterSaldoUtilizado(Rede, OrigemVerba, Regional);
                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        public decimal ObterSaldoUtilizado(string Rede, string OrigemVerba, string Regional)
        {
            string SQL = "SELECT " +
            "       CASE WHEN SUM([VALOR]) IS NULL THEN 0 ELSE SUM([VALOR]) END AS VALOR " +
            "  FROM [Vivo_SIM].[dbo].[ACAO_VALOR] " +
            "  WHERE [REDE] = '" + Rede + "' " +
            "  AND [REGIONAL] = '" + Regional + "' " +
            "  AND [ORIGEM_VERBA] = '" + OrigemVerba + "' ";

            DataTable dsColaborador = null;
            decimal retorno = 0;

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    retorno = Convert.ToDecimal(item[0]);
                }

                return retorno;
            }
            else
            {
                return 0;
            }
        }

        public List<AcaoMarketing> ObterAcoesCadastradas(string Mes, string DDD) 
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();
            TipoMidiaRepositorio _respositorioMidia = new TipoMidiaRepositorio();


            string SQL = "SELECT DISTINCT " +
            "     Vivo_SIM.dbo.ACAO_CADASTRO.idTipo as [Tipo de Mídia] " +
            "    ,Vivo_SIM.dbo.ACAO_CADASTRO.idDetalhada as [Mídia Detalhada] " +
            "    ,veiculo as [Veículo] " +
            "    ,Vivo_SIM.dbo.ACAO_CADASTRO.descricaoAcao as [Descrição da ação] " +
            "    ,campanhaMacro as [Campanha Macro] " +
            "    ,mesAcao as [Mês da ação] " +
            "    ,midiaExclusivaVivo as [Mídia Exclusiva para Vivo] " +
            "    ,valorTotalAcao as [Valor Total da Ação] " +
            "    ,valorTotalVivo as [Valor de Participação da Vivo] " +
            "    ,percentParticipacaoVivo as [% de Participação da Vivo] " +
            "    ,focoPrincipal as [Foco Principal Campanha] " +
            "    ,1 as [Quantidade de Ações] " +
            "    ,CASE WHEN (insercoes = 0) THEN qtdAcoes ELSE insercoes END as [Inserções] " +
            "    ,CASE WHEN Vivo_SIM.dbo.ACAO_CADASTRO.idStatus IN ('3','4','11') THEN 'Planejada' " +
            "    WHEN Vivo_SIM.dbo.ACAO_CADASTRO.idStatus IN ('15') THEN 'Devolvido ao gerente de contas' " +
            "    WHEN Vivo_SIM.dbo.ACAO_CADASTRO.idStatus IN ('7','9','10','14') THEN 'Cancelada' " +
            "    WHEN Vivo_SIM.dbo.ACAO_CADASTRO.idStatus IN ('13') THEN 'Executada' " +
            "    ELSE '' END [Status da ação] " +
            "    ,CASE WHEN Vivo_SIM.dbo.ACAO_CADASTRO.origemVerba = 'Verba Cooperada' THEN 'VPC' ELSE 'VIO' END AS [Origem da Verba] " +
            "    ,Vivo_SIM.dbo.ACAO_CADASTRO.Adabas as [Código Adabas Dealer] " +
            "    ,observacoes as [Observações] " +
            " FROM Vivo_SIM.dbo.ACAO_CADASTRO INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO " +
            " ON(Vivo_SIM.dbo.ACAO_CADASTRO.adabas = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO.VENDEDOR) " +
            " INNER JOIN Vivo_SIM.dbo.ACAO_TIPO_MIDIA " +
            " ON(Vivo_SIM.dbo.ACAO_CADASTRO.idTipo = Vivo_SIM.dbo.ACAO_TIPO_MIDIA.idTipo) " +
            " INNER JOIN Vivo_SIM.dbo.ACAO_MIDIA_DETALHADA " +
            " ON(Vivo_SIM.dbo.ACAO_CADASTRO.idDetalhada = Vivo_SIM.dbo.ACAO_MIDIA_DETALHADA.idTipo) " +
            " WHERE [mesAcao] = '" + Mes + "'";

            if(!DDD.Equals("TODOS"))
            {
                SQL = SQL + " AND [DDD] = '" + DDD + "'";
            }

            

            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = new List<AcaoMarketing>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _respositorio.ObterInformacoesParceiros(item[15].ToString());
                    TipoMidia tm = _respositorioMidia.ObterTipoMidiaId(Convert.ToInt32(item[0]));
                    TipoMidia md = _respositorioMidia.ObterMidiaDetalhadaId(item[1].ToString());


                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Parceiro = p;
                    Acao.Midia = tm;
                    Acao.MidiaDetalhada = md;

                    Acao.Veiculo = item[2].ToString();
                    Acao.DescricaoAcao = item[3].ToString();
                    Acao.CampanhaMacro = item[4].ToString();
                    Acao.MesAcao = item[5].ToString();
                    Acao.MidiaExclusivaVivo = item[6].ToString();
                    Acao.ValorTotalAcao = item[7].ToString();
                    Acao.ValorTotalVivo = item[8].ToString();
                    Acao.PercentualParticipacaoVivo = item[9].ToString();
                    Acao.FocoPrincipal = item[10].ToString();
                    Acao.QtdAcoes = Convert.ToInt32(item[11]);
                    Acao.Insercoes = Convert.ToInt32(item[12]);
                    Acao.Justificativa = item[13].ToString();
                    Acao.OrigemVerba = item[14].ToString();
                    Acao.Observacoes = item[16].ToString();
                    
                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public DateTime retornaDataInicialAcao(string[] Id)
        {
            string result = string.Join(",", Id);

            if (result != "")
            {
                result = result.Substring(1, result.Count() - 1);
            }

            string SQL = "SELECT MIN(DataPreenchimentoInicial) ";
            SQL = SQL + " FROM Vivo_SIM.dbo.ACAO_CADASTRO ";
            SQL = SQL + " WHERE ID IN ('" + result + "') ";

            DateTime retorno = DateTime.MinValue;
            System.Data.DataTable dsCadastro;

            try
            {
                dsCadastro = this.dao.returnaDataTable(SQL);
                if (dsCadastro != null)
                {
                    if (dsCadastro.Rows.Count > 0)
                    {
                        foreach (DataRow item in dsCadastro.Rows)
                        {
                            retorno = Convert.ToDateTime(item[0]);
                        }
                    }
                }

                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool AtribuiProtocolo(string[] Id)
        {
            string result = string.Join(",", Id);

            if (result != "")
            {
                result = result.Substring(1, result.Count() - 1);
            }

            string Protocolo = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Protocolo = Protocolo.Replace("/", "");
            Protocolo = Protocolo.Replace(":", "");
            Protocolo = Protocolo.Replace(" ", "");

            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
                         " SET Protocolo = " + Protocolo +
                         " WHERE ID IN (" + result + ")";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizarStatusAcao(string Status, string Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
                         " SET IdStatus = " + Status +
                         " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizarStatusAcaoProtocolo(string Status, string Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
                         " SET IdStatus = " + Status +
                         " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool ApagarAcao(string Id)
        {
            string SQL = " DELETE FROM [Vivo_SIM].[dbo].[ACAO_VALOR] " +
                         " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public DataTable extrairPPC(string[] Id)
        {
            string result = string.Join(",", Id);
            
            if(result != "")
            {
                result = result.Substring(1, result.Count() - 1);
            }

            string SQL = "  SELECT " +
            " [VPC].Adabas " +
            " ,[CRED].[RAZÃO SOCIAL] " +
            " ,[CRED].[NOME FANTASIA] " +
            " ,[TM].Descricao " +
            " ,UPPER([CRED].[CIDADE]) AS CIDADE " +
            " ,[CRED].GESTOR " +
            " ,[CRED].[CANAL] AS [CANAL] " +
            " ,[CRED].[UF] AS [UF] " +
            " ,[VPC].[veiculo] AS [VEICULO] " +
            " ,[VPC].[descricaoAcao] AS [EMISSORA/ESTAÇÃO/PERIÓDICO] " +
            " ,[VPC].[formatoMaterial] AS [FORMATO DO MATERIAL] " +
            " ,[VPC].[focoPrincipal] AS [FOCO DA COMUNICAÇÃO] " +
            " ,[VPC].[dia1] AS [1] " +
            " ,[VPC].[dia2] AS [2] " +
            " ,[VPC].[dia3] AS [3] " +
            " ,[VPC].[dia4] AS [4] " +
            " ,[VPC].[dia5] AS [5] " +
            " ,[VPC].[dia6] AS [6] " +
            " ,[VPC].[dia7] AS [7] " +
            " ,[VPC].[dia8] AS [8] " +
            " ,[VPC].[dia9] AS [9] " +
            " ,[VPC].[dia10] AS [10] " +
            " ,[VPC].[dia11] AS [11] " +
            " ,[VPC].[dia12] AS [12] " +
            " ,[VPC].[dia13] AS [13] " +
            " ,[VPC].[dia14] AS [14] " +
            " ,[VPC].[dia15] AS [15] " +
            " ,[VPC].[dia16] AS [16] " +
            " ,[VPC].[dia17] AS [17] " +
            " ,[VPC].[dia18] AS [18] " +
            " ,[VPC].[dia19] AS [19] " +
            " ,[VPC].[dia20] AS [20] " +
            " ,[VPC].[dia21] AS [21] " +
            " ,[VPC].[dia22] AS [22] " +
            " ,[VPC].[dia23] AS [23] " +
            " ,[VPC].[dia24] AS [24] " +
            " ,[VPC].[dia25] AS [25] " +
            " ,[VPC].[dia26] AS [26] " +
            " ,[VPC].[dia27] AS [27] " +
            " ,[VPC].[dia28] AS [28] " +
            " ,[VPC].[dia29] AS [29] " +
            " ,[VPC].[dia30] AS [30] " +
            " ,[VPC].[dia31] AS [31] " +
            " ,CASE WHEN ((TM.idTipo IN (16,25,23,5)) OR (MD.idTipo = 75)) THEN insercoes ELSE CASE WHEN (SUBSTRING (qtdAcoes,LEN(qtdAcoes)-2,1) = '.') THEN (SUBSTRING(qtdAcoes,0,LEN(qtdAcoes)-2) + ',' + SUBSTRING(qtdAcoes,LEN(qtdAcoes)-1,2)) ELSE qtdAcoes END END AS QUANTIDADE " +
            " ,[VPC].[CustoUnitario] AS [CUSTO UNITÁRIO] " +
            " ,[VPC].[percentParticipacaoVivo]  AS [% VIVO] " + 
            " ,[valorTotalVivo] AS [CUSTO VIVO] " +
            " ,[valorTotalAcao] AS [CUSTO TOTAL] " +
            " ,[VPC].observacoes " +
            " ,[VPC].mesAcao " +
            " ,[VPC].IXO AS IXOS " + 
            " ,[VPC].qtdAcoes " + 
            " ,[VPC].origemVerba " + 
            " ,CONVERT(VARCHAR(50),[VPC].DataCadastro,103) AS DataCadastro " +
            " ,NomeContatoRede " + 
            " ,TelefoneContatoRede " + 
            " ,EmailContatoRede " + 
            " ,MD.Descricao as MidiaDetalhada " +
            " ,CONVERT(VARCHAR(50),[VPC].DataPreenchimentoInicial,103) AS DataPreenchimentoInicial " +
            " ,CONVERT(VARCHAR(50),[VPC].DataPreenchimentoFinal,103) AS DataPreenchimentoFinal " +
            " ,[VPC].Protocolo " + 
            " FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] VPC " +  
		    " INNER JOIN [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO] CRED ON (VPC.Adabas = CRED.VENDEDOR) " +
            " INNER JOIN [Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA] TM ON (VPC.idTipo = TM.idTipo) " +
            " INNER JOIN [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA] MD ON (VPC.idDetalhada = MD.idTipo) " +
            " WHERE [VPC].[ID] IN ('" + result + "') " +
            " AND [VPC].[idStatus] = '4'";

            try
            {
                return this.dao.returnaDataTable(SQL);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AcaoMarketing RetornaAcaoID(int Id)
        {
            ParceiroRepositorio _repositorio = new ParceiroRepositorio();
            TipoMidiaRepositorio _repositorioMidia = new TipoMidiaRepositorio();
            StatusAcaoRepositorio _repositorioStatusAcao = new StatusAcaoRepositorio();

            string SQL = " SELECT [id] " +
            "      ,[DataCadastro] " +
            "      ,[adabas] " +
            "      ,[idTipo] " +
            "      ,[idDetalhada] " +
            "      ,[veiculo] " +
            "      ,[descricaoAcao] " +
            "      ,[dia1] " +
            "      ,[dia2] " +
            "      ,[dia3] " +
            "      ,[dia4] " +
            "      ,[dia5] " +
            "      ,[dia6] " +
            "      ,[dia7] " +
            "      ,[dia8] " +
            "      ,[dia9] " +
            "      ,[dia10] " +
            "      ,[dia11] " +
            "      ,[dia12] " +
            "      ,[dia13] " +
            "      ,[dia14] " +
            "      ,[dia15] " +
            "      ,[dia16] " +
            "      ,[dia17] " +
            "      ,[dia18] " +
            "      ,[dia19] " +
            "      ,[dia20] " +
            "      ,[dia21] " +
            "      ,[dia22] " +
            "      ,[dia23] " +
            "      ,[dia24] " +
            "      ,[dia25] " +
            "      ,[dia26] " +
            "      ,[dia27] " +
            "      ,[dia28] " +
            "      ,[dia29] " +
            "      ,[dia30] " +
            "      ,[dia31] " +
            "      ,[campanhaMacro] " +
            "      ,[mesAcao] " +
            "      ,[midiaExclusivaVivo] " +
            "      ,[valorTotalAcao] " +
            "      ,[valorTotalVivo] " +
            "      ,[percentParticipacaoVivo] " +
            "      ,[justificativa] " +
            "      ,[focoPrincipal] " +
            "      ,[qtdAcoes] " +
            "      ,[insercoes] " +
            "      ,[origemVerba] " +
            "      ,[observacoes] " +
            "      ,[idStatus] " +
            "      ,[idAcesso] " +
            "      ,[idAcessoCadastro] " +
            "      ,[formatoMaterial] " +
            "      ,[CustoUnitario] " +
            "      ,[dataFinalizacao] " +
            "      ,[Protocolo] " +
            "      ,[IXO] " +
            "      ,[NomeContatoRede] " +
            "      ,[TelefoneContatoRede] " +
            "      ,[EmailContatoRede] " +
            "      ,[Consideracao] " +
            "      ,CONVERT(VARCHAR,[DataPreenchimentoInicial],103) AS DataPreenchimentoInicial " +
            "      ,CONVERT(VARCHAR,[DataPreenchimentoFinal],103) AS DataPreenchimentoFinal " +
            "      ,[PercentualRede] " +
            "      ,[ValorTotalRede] " +
            "  FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
            "  WHERE [ID] = " + Id;

            DataTable dsAcao = null;
            AcaoMarketing retorno = null;

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _repositorio.ObterInformacoesParceiros(item[2].ToString());
                    TipoMidia Midia = _repositorioMidia.ObterTipoMidiaId(Convert.ToInt32(item[3]));
                    TipoMidia MidiaDetalhada = _repositorioMidia.ObterMidiaDetalhadaId(item[4].ToString());
                    StatusAcao sa = _repositorioStatusAcao.ObterTipoMidiaId(Convert.ToInt32(item[50]));


                    retorno = new AcaoMarketing();
                    retorno.Id = Convert.ToInt32(item[0]);
                    retorno.DataCadastro = Convert.ToDateTime(item[1]);
                    retorno.Veiculo = item[5].ToString();
                    retorno.DescricaoAcao = item[6].ToString();
                    retorno.Dia1 = item[7].ToString();
                    retorno.Dia2 = item[8].ToString();
                    retorno.Dia3 = item[9].ToString();
                    retorno.Dia4 = item[10].ToString();
                    retorno.Dia5 = item[11].ToString();
                    retorno.Dia6 = item[12].ToString();
                    retorno.Dia7 = item[13].ToString();
                    retorno.Dia8 = item[14].ToString();
                    retorno.Dia9 = item[15].ToString();
                    retorno.Dia10 = item[16].ToString();
                    retorno.Dia11 = item[17].ToString();
                    retorno.Dia12 = item[18].ToString();
                    retorno.Dia13 = item[19].ToString();
                    retorno.Dia14 = item[20].ToString();
                    retorno.Dia15 = item[21].ToString();
                    retorno.Dia16 = item[22].ToString();
                    retorno.Dia17 = item[23].ToString();
                    retorno.Dia18 = item[24].ToString();
                    retorno.Dia19 = item[25].ToString();
                    retorno.Dia20 = item[26].ToString();
                    retorno.Dia21 = item[27].ToString();
                    retorno.Dia22 = item[28].ToString();
                    retorno.Dia23 = item[29].ToString();
                    retorno.Dia24 = item[30].ToString();
                    retorno.Dia25 = item[31].ToString();
                    retorno.Dia26 = item[32].ToString();
                    retorno.Dia27 = item[33].ToString();
                    retorno.Dia28 = item[34].ToString();
                    retorno.Dia29 = item[35].ToString();
                    retorno.Dia30 = item[36].ToString();
                    retorno.Dia31 = item[37].ToString();

                    retorno.CampanhaMacro = item[38].ToString();
                    retorno.MesAcao = item[39].ToString();
                    retorno.MidiaExclusivaVivo = item[40].ToString();
                    retorno.ValorTotalAcao = item[41].ToString();
                    retorno.ValorTotalVivo = item[42].ToString();
                    retorno.PercentualParticipacaoVivo = item[43].ToString();

                    retorno.Justificativa = item[44].ToString();
                    retorno.FocoPrincipal = item[45].ToString();
                    retorno.QtdAcoes = Convert.ToInt32(item[46]);
                    retorno.Insercoes = Convert.ToInt32(item[47]);
                    retorno.OrigemVerba = item[48].ToString();
                    retorno.Observacoes = item[49].ToString();
                    
                    

                    retorno.IdAcesso = Convert.ToInt32(item[51]);
                    retorno.IdAcessoCadastro = Convert.ToInt32(item[52]);

                    retorno.FormatoMaterial = item[53].ToString();
                    retorno.CustoUnitario = item[54].ToString();
                    retorno.Protocolo = item[56].ToString();
                    retorno.NomeContatoRede = item[58].ToString();
                    retorno.TelefoneContatoRede = item[59].ToString();
                    retorno.EmailContatoRede = item[60].ToString();
                    retorno.Consideracao = item[61].ToString();
                    retorno.DtPreencherDiasIni = item[62].ToString();
                    retorno.DtPreencherDiasFim = item[63].ToString();

                    retorno.PercentualRede = item[64].ToString();
                    retorno.ValorTotalRede = item[65].ToString();

                    retorno.Parceiro = p;
                    retorno.Midia = Midia;
                    retorno.MidiaDetalhada = MidiaDetalhada;
                    retorno.Status = sa;


                    return retorno;
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        public AcaoMarketing RetornaAcaoProtocolo(string Protocolo)
        {
            ParceiroRepositorio _repositorio = new ParceiroRepositorio();
            TipoMidiaRepositorio _repositorioMidia = new TipoMidiaRepositorio();
            StatusAcaoRepositorio _repositorioStatusAcao = new StatusAcaoRepositorio();

            string SQL = " SELECT [id] " +
            "      ,[DataCadastro] " +
            "      ,[adabas] " +
            "      ,[idTipo] " +
            "      ,[idDetalhada] " +
            "      ,[veiculo] " +
            "      ,[descricaoAcao] " +
            "      ,[dia1] " +
            "      ,[dia2] " +
            "      ,[dia3] " +
            "      ,[dia4] " +
            "      ,[dia5] " +
            "      ,[dia6] " +
            "      ,[dia7] " +
            "      ,[dia8] " +
            "      ,[dia9] " +
            "      ,[dia10] " +
            "      ,[dia11] " +
            "      ,[dia12] " +
            "      ,[dia13] " +
            "      ,[dia14] " +
            "      ,[dia15] " +
            "      ,[dia16] " +
            "      ,[dia17] " +
            "      ,[dia18] " +
            "      ,[dia19] " +
            "      ,[dia20] " +
            "      ,[dia21] " +
            "      ,[dia22] " +
            "      ,[dia23] " +
            "      ,[dia24] " +
            "      ,[dia25] " +
            "      ,[dia26] " +
            "      ,[dia27] " +
            "      ,[dia28] " +
            "      ,[dia29] " +
            "      ,[dia30] " +
            "      ,[dia31] " +
            "      ,[campanhaMacro] " +
            "      ,[mesAcao] " +
            "      ,[midiaExclusivaVivo] " +
            "      ,[valorTotalAcao] " +
            "      ,[valorTotalVivo] " +
            "      ,[percentParticipacaoVivo] " +
            "      ,[justificativa] " +
            "      ,[focoPrincipal] " +
            "      ,[qtdAcoes] " +
            "      ,[insercoes] " +
            "      ,[origemVerba] " +
            "      ,[observacoes] " +
            "      ,[idStatus] " +
            "      ,[idAcesso] " +
            "      ,[idAcessoCadastro] " +
            "      ,[formatoMaterial] " +
            "      ,[CustoUnitario] " +
            "      ,[dataFinalizacao] " +
            "      ,[Protocolo] " +
            "      ,[IXO] " +
            "      ,[NomeContatoRede] " +
            "      ,[TelefoneContatoRede] " +
            "      ,[EmailContatoRede] " +
            "      ,[Consideracao] " +
            "      ,CONVERT(VARCHAR,[DataPreenchimentoInicial],103) AS DataPreenchimentoInicial " +
            "      ,CONVERT(VARCHAR,[DataPreenchimentoFinal],103) AS DataPreenchimentoFinal " +
            "      ,[PercentualRede] " +
            "      ,[ValorTotalRede] " +
            "  FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
            "  WHERE [Protocolo] = '" + Protocolo + "'";

            DataTable dsAcao = null;
            AcaoMarketing retorno = null;

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _repositorio.ObterInformacoesParceiros(item[2].ToString());
                    TipoMidia Midia = _repositorioMidia.ObterTipoMidiaId(Convert.ToInt32(item[3]));
                    TipoMidia MidiaDetalhada = _repositorioMidia.ObterMidiaDetalhadaId(item[4].ToString());
                    StatusAcao sa = _repositorioStatusAcao.ObterTipoMidiaId(Convert.ToInt32(item[50]));


                    retorno = new AcaoMarketing();
                    retorno.Id = Convert.ToInt32(item[0]);
                    retorno.DataCadastro = Convert.ToDateTime(item[1]);
                    retorno.Veiculo = item[5].ToString();
                    retorno.DescricaoAcao = item[6].ToString();
                    retorno.Dia1 = item[7].ToString();
                    retorno.Dia2 = item[8].ToString();
                    retorno.Dia3 = item[9].ToString();
                    retorno.Dia4 = item[10].ToString();
                    retorno.Dia5 = item[11].ToString();
                    retorno.Dia6 = item[12].ToString();
                    retorno.Dia7 = item[13].ToString();
                    retorno.Dia8 = item[14].ToString();
                    retorno.Dia9 = item[15].ToString();
                    retorno.Dia10 = item[16].ToString();
                    retorno.Dia11 = item[17].ToString();
                    retorno.Dia12 = item[18].ToString();
                    retorno.Dia13 = item[19].ToString();
                    retorno.Dia14 = item[20].ToString();
                    retorno.Dia15 = item[21].ToString();
                    retorno.Dia16 = item[22].ToString();
                    retorno.Dia17 = item[23].ToString();
                    retorno.Dia18 = item[24].ToString();
                    retorno.Dia19 = item[25].ToString();
                    retorno.Dia20 = item[26].ToString();
                    retorno.Dia21 = item[27].ToString();
                    retorno.Dia22 = item[28].ToString();
                    retorno.Dia23 = item[29].ToString();
                    retorno.Dia24 = item[30].ToString();
                    retorno.Dia25 = item[31].ToString();
                    retorno.Dia26 = item[32].ToString();
                    retorno.Dia27 = item[33].ToString();
                    retorno.Dia28 = item[34].ToString();
                    retorno.Dia29 = item[35].ToString();
                    retorno.Dia30 = item[36].ToString();
                    retorno.Dia31 = item[37].ToString();

                    retorno.CampanhaMacro = item[38].ToString();
                    retorno.MesAcao = item[39].ToString();
                    retorno.MidiaExclusivaVivo = item[40].ToString();
                    retorno.ValorTotalAcao = item[41].ToString();
                    retorno.ValorTotalVivo = item[42].ToString();
                    retorno.PercentualParticipacaoVivo = item[43].ToString();

                    retorno.Justificativa = item[44].ToString();
                    retorno.FocoPrincipal = item[45].ToString();
                    retorno.QtdAcoes = Convert.ToInt32(item[46]);
                    retorno.Insercoes = Convert.ToInt32(item[47]);
                    retorno.OrigemVerba = item[48].ToString();
                    retorno.Observacoes = item[49].ToString();



                    retorno.IdAcesso = Convert.ToInt32(item[51]);
                    retorno.IdAcessoCadastro = Convert.ToInt32(item[52]);

                    retorno.FormatoMaterial = item[53].ToString();
                    retorno.CustoUnitario = item[54].ToString();
                    retorno.Protocolo = item[56].ToString();
                    retorno.NomeContatoRede = item[58].ToString();
                    retorno.TelefoneContatoRede = item[59].ToString();
                    retorno.EmailContatoRede = item[60].ToString();
                    retorno.Consideracao = item[61].ToString();
                    retorno.DtPreencherDiasIni = item[62].ToString();
                    retorno.DtPreencherDiasFim = item[63].ToString();

                    retorno.PercentualRede = item[64].ToString();
                    retorno.ValorTotalRede = item[65].ToString();

                    retorno.Parceiro = p;
                    retorno.Midia = Midia;
                    retorno.MidiaDetalhada = MidiaDetalhada;
                    retorno.Status = sa;


                    return retorno;
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        public List<AcaoMarketing> RetornaAcaoRedeMes(string Rede, string Mes)
        {
            ParceiroRepositorio _repositorio = new ParceiroRepositorio();
            TipoMidiaRepositorio _repositorioMidia = new TipoMidiaRepositorio();

            string SQL = " SELECT DISTINCT [id] " +
            "      ,[DataCadastro] " +
            "      ,[adabas] " +
            "      ,[idTipo] " +
            "      ,[idDetalhada] " +
            "      ,[veiculo] " +
            "      ,[descricaoAcao] " +
            "      ,[dia1] " +
            "      ,[dia2] " +
            "      ,[dia3] " +
            "      ,[dia4] " +
            "      ,[dia5] " +
            "      ,[dia6] " +
            "      ,[dia7] " +
            "      ,[dia8] " +
            "      ,[dia9] " +
            "      ,[dia10] " +
            "      ,[dia11] " +
            "      ,[dia12] " +
            "      ,[dia13] " +
            "      ,[dia14] " +
            "      ,[dia15] " +
            "      ,[dia16] " +
            "      ,[dia17] " +
            "      ,[dia18] " +
            "      ,[dia19] " +
            "      ,[dia20] " +
            "      ,[dia21] " +
            "      ,[dia22] " +
            "      ,[dia23] " +
            "      ,[dia24] " +
            "      ,[dia25] " +
            "      ,[dia26] " +
            "      ,[dia27] " +
            "      ,[dia28] " +
            "      ,[dia29] " +
            "      ,[dia30] " +
            "      ,[dia31] " +
            "      ,[campanhaMacro] " +
            "      ,[mesAcao] " +
            "      ,[midiaExclusivaVivo] " +
            "      ,[valorTotalAcao] " +
            "      ,[valorTotalVivo] " +
            "      ,[percentParticipacaoVivo] " +
            "      ,[justificativa] " +
            "      ,[focoPrincipal] " +
            "      ,[qtdAcoes] " +
            "      ,[insercoes] " +
            "      ,[origemVerba] " +
            "      ,[observacoes] " +
            "      ,[idStatus] " +
            "      ,[idAcesso] " +
            "      ,[idAcessoCadastro] " +
            "      ,[formatoMaterial] " +
            "      ,[CustoUnitario] " +
            "      ,[dataFinalizacao] " +
            "      ,[Protocolo] " +
            "      ,[IXO] " +
            "      ,[NomeContatoRede] " +
            "      ,[TelefoneContatoRede] " +
            "      ,[EmailContatoRede] " +
            "      ,[Consideracao] " +
            "      ,CONVERT(VARCHAR,[DataPreenchimentoInicial],103) AS DataPreenchimentoInicial " +
            "      ,CONVERT(VARCHAR,[DataPreenchimentoFinal],103) AS DataPreenchimentoFinal " +
            "      ,[PercentualRede] " +
            "      ,[ValorTotalRede] " +
            "  FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] [ACAO] INNER JOIN [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] [CRED] " +
            "  ON ([ACAO].[Adabas] = CRED.VENDEDOR) " +
            "  WHERE [MesAcao] = '" + Mes + "'" +
            "  AND [CRED].[REDE] = '" + Rede + "'";

            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = null;

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                retorno = new List<AcaoMarketing>();

                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _repositorio.ObterInformacoesParceiros(item[2].ToString());
                    TipoMidia Midia = _repositorioMidia.ObterTipoMidiaId(Convert.ToInt32(item[3]));
                    TipoMidia MidiaDetalhada = _repositorioMidia.ObterMidiaDetalhadaId(item[4].ToString());

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Id = Convert.ToInt32(item[0]);
                    Acao.DataCadastro = Convert.ToDateTime(item[1]);
                    Acao.Veiculo = item[5].ToString();
                    Acao.DescricaoAcao = item[6].ToString();
                    Acao.Dia1 = item[7].ToString();
                    Acao.Dia2 = item[8].ToString();
                    Acao.Dia3 = item[9].ToString();
                    Acao.Dia4 = item[10].ToString();
                    Acao.Dia5 = item[11].ToString();
                    Acao.Dia6 = item[12].ToString();
                    Acao.Dia7 = item[13].ToString();
                    Acao.Dia8 = item[14].ToString();
                    Acao.Dia9 = item[15].ToString();
                    Acao.Dia10 = item[16].ToString();
                    Acao.Dia11 = item[17].ToString();
                    Acao.Dia12 = item[18].ToString();
                    Acao.Dia13 = item[19].ToString();
                    Acao.Dia14 = item[20].ToString();
                    Acao.Dia15 = item[21].ToString();
                    Acao.Dia16 = item[22].ToString();
                    Acao.Dia17 = item[23].ToString();
                    Acao.Dia18 = item[24].ToString();
                    Acao.Dia19 = item[25].ToString();
                    Acao.Dia20 = item[26].ToString();
                    Acao.Dia21 = item[27].ToString();
                    Acao.Dia22 = item[28].ToString();
                    Acao.Dia23 = item[29].ToString();
                    Acao.Dia24 = item[30].ToString();
                    Acao.Dia25 = item[31].ToString();
                    Acao.Dia26 = item[32].ToString();
                    Acao.Dia27 = item[33].ToString();
                    Acao.Dia28 = item[34].ToString();
                    Acao.Dia29 = item[35].ToString();
                    Acao.Dia30 = item[36].ToString();
                    Acao.Dia31 = item[37].ToString();

                    Acao.CampanhaMacro = item[38].ToString();
                    Acao.MesAcao = item[39].ToString();
                    Acao.MidiaExclusivaVivo = item[40].ToString();
                    Acao.ValorTotalAcao = item[41].ToString();
                    Acao.ValorTotalVivo = item[42].ToString();
                    Acao.PercentualParticipacaoVivo = item[43].ToString();

                    Acao.Justificativa = item[44].ToString();
                    Acao.FocoPrincipal = item[45].ToString();
                    Acao.QtdAcoes = Convert.ToInt32(item[46]);
                    Acao.Insercoes = Convert.ToInt32(item[47]);
                    Acao.OrigemVerba = item[48].ToString();
                    Acao.Observacoes = item[49].ToString();

                    StatusAcao sa = new StatusAcao();
                    sa.Id = Convert.ToInt32(item[50]);
                    Acao.Status = sa;

                    Acao.IdAcesso = Convert.ToInt32(item[51]);
                    Acao.IdAcessoCadastro = Convert.ToInt32(item[52]);

                    Acao.FormatoMaterial = item[53].ToString();
                    Acao.CustoUnitario = item[54].ToString();
                    Acao.Protocolo = item[56].ToString();
                    Acao.NomeContatoRede = item[58].ToString();
                    Acao.TelefoneContatoRede = item[59].ToString();
                    Acao.EmailContatoRede = item[60].ToString();
                    Acao.Consideracao = item[61].ToString();
                    Acao.DtPreencherDiasIni = item[62].ToString();
                    Acao.DtPreencherDiasFim = item[63].ToString();

                    Acao.PercentualRede = item[64].ToString();
                    Acao.ValorTotalRede = item[65].ToString();

                    Acao.Parceiro = p;
                    Acao.Midia = Midia;
                    Acao.MidiaDetalhada = MidiaDetalhada;

                    retorno.Add(Acao);
                }

                return retorno;;
            }
            else
            {
                return null;
            }
        }

        public List<AcaoMarketing> ObterAcoesProtocoloStatus(string Protocolo, string Status)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();
            TipoMidiaRepositorio _respositorioMidia = new TipoMidiaRepositorio();
            StatusAcaoRepositorio _respositorioStatus = new StatusAcaoRepositorio();

            string Grupo = "0";
            string Usuario = "0";

            string SQL = " SELECT [id] " +
            "        ,[DataCadastro] " +
            "        ,[adabas] " +
            "        ,[idTipo] " +
            "        ,[idDetalhada] " +
            "        ,[veiculo] " +
            "        ,[descricaoAcao] " +
            "        ,[dia1] " +
            "        ,[dia2] " +
            "        ,[dia3] " +
            "        ,[dia4] " +
            "        ,[dia5] " +
            "        ,[dia6] " +
            "        ,[dia7] " +
            "        ,[dia8] " +
            "        ,[dia9] " +
            "        ,[dia10] " +
            "        ,[dia11] " +
            "        ,[dia12] " +
            "        ,[dia13] " +
            "        ,[dia14] " +
            "        ,[dia15] " +
            "        ,[dia16] " +
            "        ,[dia17] " +
            "        ,[dia18] " +
            "        ,[dia19] " +
            "        ,[dia20] " +
            "        ,[dia21] " +
            "        ,[dia22] " +
            "        ,[dia23] " +
            "        ,[dia24] " +
            "        ,[dia25] " +
            "        ,[dia26] " +
            "        ,[dia27] " +
            "        ,[dia28] " +
            "        ,[dia29] " +
            "        ,[dia30] " +
            "        ,[dia31] " +
            "        ,[campanhaMacro] " +
            "        ,[mesAcao] " +
            "        ,[midiaExclusivaVivo] " +
            "        ,[valorTotalAcao] " +
            "        ,[valorTotalVivo] " +
            "        ,[percentParticipacaoVivo] " +
            "        ,[justificativa] " +
            "        ,[focoPrincipal] " +
            "        ,[qtdAcoes] " +
            "        ,[insercoes] " +
            "        ,[origemVerba] " +
            "        ,[observacoes] " +
            "        ,[idStatus] " +
            "        ,[idAcesso] " +
            "        ,[idAcessoCadastro] " +
            "        ,[formatoMaterial] " +
            "        ,[CustoUnitario] " +
            "        ,[dataFinalizacao] " +
            "        ,[Protocolo] " +
            "        ,[IXO] " +
            "        ,[NomeContatoRede] " +
            "        ,[TelefoneContatoRede] " +
            "        ,[EmailContatoRede] " +
            "        ,[Consideracao] " +
            "        ,[DataPreenchimentoInicial] " +
            "        ,[DataPreenchimentoFinal] " +
            "        ,[PercentualRede] " +
            "        ,[ValorTotalRede] " +
            "    FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] [ACAO] INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS [CRED] " +
            "    ON ([ACAO].[Adabas] = CRED.VENDEDOR) " +
            "    WHERE [CRED].[STATUS CALLIDUS] IN ('ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            "    AND [ACAO].[PROTOCOLO] = " + Protocolo +
            "    AND [ACAO].[idStatus] = " + Status;

            if (Grupo != "0")
            {
                if (Usuario.ToUpper().Contains("A00"))
                {
                    SQL = SQL + " AND RIGHT([CRED].[MATRICULA_GER_CONTA],5) = '" + Usuario.ToUpper().Replace("A00", "") + "'";
                }
                else
                {
                    SQL = SQL + " AND [CRED].[MATRICULA_GER_CONTA] = '" + Usuario + "'";
                }
            }

            SQL = SQL + " GROUP BY ORDER BY [ACAO].[DataCadastro] DESC ";
            

            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = new List<AcaoMarketing>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _respositorio.ObterInformacoesParceiros(item[2].ToString());
                    TipoMidia tm = _respositorioMidia.ObterTipoMidiaId(Convert.ToInt32(item[6]));
                    TipoMidia md = _respositorioMidia.ObterMidiaDetalhadaId(item[7].ToString());
                    StatusAcao sa = _respositorioStatus.ObterTipoMidiaId(Convert.ToInt32(item[53]));

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Parceiro = p;
                    Acao.Midia = tm;
                    Acao.MidiaDetalhada = md;
                    Acao.Status = sa;

                    Acao.Id = Convert.ToInt32(item[0]);
                    Acao.DataCadastro = Convert.ToDateTime(item[1]);
                    Acao.Veiculo = item[8].ToString();
                    Acao.DescricaoAcao = item[9].ToString();
                    Acao.Dia1 = item[10].ToString();
                    Acao.Dia2 = item[11].ToString();
                    Acao.Dia3 = item[12].ToString();
                    Acao.Dia4 = item[13].ToString();
                    Acao.Dia5 = item[14].ToString();
                    Acao.Dia6 = item[15].ToString();
                    Acao.Dia7 = item[16].ToString();
                    Acao.Dia8 = item[17].ToString();
                    Acao.Dia9 = item[18].ToString();
                    Acao.Dia10 = item[19].ToString();
                    Acao.Dia11 = item[20].ToString();
                    Acao.Dia12 = item[21].ToString();
                    Acao.Dia13 = item[22].ToString();
                    Acao.Dia14 = item[23].ToString();
                    Acao.Dia15 = item[24].ToString();
                    Acao.Dia16 = item[25].ToString();
                    Acao.Dia17 = item[26].ToString();
                    Acao.Dia18 = item[27].ToString();
                    Acao.Dia19 = item[28].ToString();
                    Acao.Dia20 = item[29].ToString();
                    Acao.Dia21 = item[30].ToString();
                    Acao.Dia22 = item[31].ToString();
                    Acao.Dia23 = item[32].ToString();
                    Acao.Dia24 = item[33].ToString();
                    Acao.Dia25 = item[34].ToString();
                    Acao.Dia26 = item[35].ToString();
                    Acao.Dia27 = item[36].ToString();
                    Acao.Dia28 = item[37].ToString();
                    Acao.Dia29 = item[38].ToString();
                    Acao.Dia30 = item[39].ToString();
                    Acao.Dia31 = item[40].ToString();

                    Acao.CampanhaMacro = item[41].ToString();
                    Acao.MesAcao = item[42].ToString();
                    Acao.MidiaExclusivaVivo = item[43].ToString();
                    Acao.ValorTotalAcao = item[44].ToString();
                    Acao.ValorTotalVivo = item[45].ToString();
                    Acao.PercentualParticipacaoVivo = item[46].ToString();
                    Acao.Justificativa = item[47].ToString();
                    Acao.FocoPrincipal = item[48].ToString();
                    Acao.QtdAcoes = Convert.ToInt32(item[49]);
                    Acao.Insercoes = Convert.ToInt32(item[50].ToString());
                    Acao.OrigemVerba = item[51].ToString();
                    Acao.Observacoes = item[52].ToString();
                    Acao.IdAcesso = Convert.ToInt32(item[54]);
                    Acao.IdAcessoCadastro = Convert.ToInt32(item[55]);
                    Acao.FormatoMaterial = item[56].ToString();
                    Acao.CustoUnitario = item[57].ToString();
                    Acao.Protocolo = item[59].ToString();
                    Acao.NomeContatoRede = item[60].ToString();
                    Acao.TelefoneContatoRede = item[61].ToString();
                    Acao.EmailContatoRede = item[62].ToString();
                    Acao.Consideracao = item[63].ToString();
                    Acao.PercentualRede = item[67].ToString();
                    Acao.ValorTotalRede = item[68].ToString();
                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<AcaoMarketing> ObterAcoesPlanejamento(string mesAcao)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();

            string SQL = " SELECT " +
                         "         [VPC].ID AS ID " +
                         "        ,[CRED].CANAL AS [CANAL] " +
                         "        ,[CRED].[RAZÃO SOCIAL] AS [DEALER] " +
                         "        ,MAX([VPC].percentParticipacaoVivo) AS [% VIVO] " +
                         "        ,SUM([VPC].valorTotalVivo) AS [CUSTO VIVO] " +
                         "        ,SUM([VPC].valorTotalAcao) AS [CUSTO TOTAL] " +
                         "    FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] VPC " +
                         "    INNER JOIN [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO] CRED ON (VPC.adabas = CRED.VENDEDOR) " +
                         "    WHERE MesAcao = '" + mesAcao + "' " +
                         "    AND [VPC].[idStatus] IN ('4') " +
                         "    GROUP BY " +
                         "         [VPC].ID " +
                         "        ,[CRED].CANAL " +
                         "        ,[CRED].[RAZÃO SOCIAL] ";


            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = new List<AcaoMarketing>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = new Parceiro(); //_respositorio.ObterInformacoesParceiros(Adabas);

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Parceiro = p;
                    Acao.Id = Convert.ToInt32(item[0]);
                    Acao.PercentualParticipacaoVivo = item[3].ToString();
                    Acao.ValorTotalVivo = item[4].ToString();
                    Acao.ValorTotalAcao = item[5].ToString();

                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<AcaoMarketing> ObterAcoesParaExtracao(string mesAcao, string Adabas, string OrigemVerba)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();

            string SQL = " SELECT " +
	                     "         [VPC].ID AS ID " +
	                     "        ,[CRED].CANAL AS [CANAL] " +
	                     "        ,[CRED].[RAZÃO SOCIAL] AS [DEALER] " +
	                     "        ,MAX([VPC].percentParticipacaoVivo) AS [% VIVO] " +
	                     "        ,SUM([VPC].valorTotalVivo) AS [CUSTO VIVO] " +
	                     "        ,SUM([VPC].valorTotalAcao) AS [CUSTO TOTAL] " +
                         "    FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] VPC " +
                         "    INNER JOIN [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO] CRED ON (VPC.adabas = CRED.VENDEDOR) " +
                         "    WHERE MesAcao = '" + mesAcao + "' " +
                         "    AND [VPC].[ADABAS] = '" + Adabas + "' " +
                         "    AND [VPC].[origemVerba] = '" + OrigemVerba + "' " +
                         "    AND [VPC].[idStatus] IN ('4') " +
                         "    GROUP BY " +
	                     "         [VPC].ID " +
	                     "        ,[CRED].CANAL " +
	                     "        ,[CRED].[RAZÃO SOCIAL] ";


            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = new List<AcaoMarketing>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _respositorio.ObterInformacoesParceiros(Adabas);

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Parceiro = p;
                    Acao.Id = Convert.ToInt32(item[0]);
                    Acao.PercentualParticipacaoVivo = item[3].ToString();
                    Acao.ValorTotalVivo = item[4].ToString();
                    Acao.ValorTotalAcao = item[5].ToString();

                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<AcaoMarketing> ObterAcoesParaExtracao(string mesAcao)
        {
            ParceiroRepositorio _respositorio = new ParceiroRepositorio();

            string SQL = " SELECT " +
                         "         [VPC].ID AS ID " +
                         "        ,[CRED].CANAL AS [CANAL] " +
                         "        ,[CRED].[RAZÃO SOCIAL] AS [DEALER] " +
                         "        ,MAX([VPC].percentParticipacaoVivo) AS [% VIVO] " +
                         "        ,SUM([VPC].valorTotalVivo) AS [CUSTO VIVO] " +
                         "        ,SUM([VPC].valorTotalAcao) AS [CUSTO TOTAL] " +
                         "        ,[VPC].ADABAS AS [ADABAS] " +
                         "    FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] VPC " +
                         "    INNER JOIN [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO] CRED ON (VPC.adabas = CRED.VENDEDOR) " +
                         "    WHERE MesAcao = '" + mesAcao + "' " +
                         "    GROUP BY " +
                         "         [VPC].ID " +
                         "        ,[CRED].CANAL " +
                         "        ,[CRED].[RAZÃO SOCIAL] " +
                         "        ,[VPC].ADABAS ";


            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = new List<AcaoMarketing>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = _respositorio.ObterInformacoesParceiros(item[6].ToString());

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Parceiro = p;
                    Acao.Id = Convert.ToInt32(item[0]);
                    Acao.PercentualParticipacaoVivo = item[3].ToString();
                    Acao.ValorTotalVivo = item[4].ToString();
                    Acao.ValorTotalAcao = item[5].ToString();

                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<AcaoMarketing> ListarValidacaoMarketingRegionalEspecifico(string Protocolo)
        {
            string Grupo = "0";
            string Usuario = "0";

            string SQL = " SELECT [ACAO].[ID] " +
            "             ,[ACAO].Adabas " +
            "             ,[ACAO].[mesAcao] AS mesAcao " +
            "             ,CASE WHEN [ACAO].[origemVerba] = 'Verba Cooperada' THEN 'VPC' " +
            "                   WHEN [ACAO].[origemVerba] = 'Verba de Incentivo a Open' THEN 'VIO' " +
            "              END AS origemVerba " +
            "             ,CRED.[REDE] " +
            "             ,[CRED].CANAL " +
            "             ,[ACAO].DDD " +
            "             ,[STATUS].[DESCRICAO] AS [STATUS] " +
            "             ,SUM([ACAO].valorTotalAcao) AS valorTotalAcao " +
            "             ,SUM([ACAO].valorTotalVivo) AS valorTotalVivo " +
            " FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] [ACAO] " +
            " INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO [CRED] " +
            " ON ([ACAO].[Adabas] = CRED.VENDEDOR) " +
            " INNER JOIN Vivo_SIM.dbo.ACAO_STATUS [STATUS] " +
            " ON ([ACAO].[idStatus] = [STATUS].[id]) " +
            " WHERE [CRED].[STATUS CALLIDUS] IN ('ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [ACAO].[PROTOCOLO] = " + Protocolo;
            
            if (Grupo != "0")
            {
                if (Usuario.ToUpper().Contains("A00"))
                {
                    SQL = SQL + " AND RIGHT([CRED].[MATRICULA_GER_CONTA],5) = '" + Usuario.ToUpper().Replace("A00", "") + "'";
                }
                else
                {
                    SQL = SQL + " AND [CRED].[MATRICULA_GER_CONTA] = '" + Usuario + "'";
                }
            }

            SQL = SQL + " AND [ACAO].[idStatus] = 1 " +
            " GROUP BY [ACAO].[ID] " +
            "         ,[ACAO].Adabas  " +
            "         ,[ACAO].[mesAcao] " +
            "         ,CASE WHEN [ACAO].[origemVerba] = 'Verba Cooperada' THEN 'VPC' " +
            "               WHEN [ACAO].[origemVerba] = 'Verba de Incentivo a Open' THEN 'VIO' " +
            "          END " +
            "         ,CRED.[REDE] " +
            "         ,LEFT([ACAO].[descricaoAcao],80) " +
            "         ,[CRED].CANAL " +
            "         ,[ACAO].DDD  " +
            "         ,[STATUS].[DESCRICAO] " +
            " ORDER BY [ACAO].[mesAcao] DESC ";

            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = new List<AcaoMarketing>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Vendedor = item[1].ToString();
                    p.Rede = item[4].ToString();
                    p.Canal = item[5].ToString();
                    p.Ddd = item[6].ToString();

                    StatusAcao sa = new StatusAcao();
                    sa.Descricao = item[7].ToString();

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Id = Convert.ToInt32(item[0]);
                    Acao.MesAcao = item[2].ToString();
                    Acao.OrigemVerba = item[3].ToString();
                    Acao.ValorTotalAcao = item[8].ToString();
                    Acao.ValorTotalVivo = item[9].ToString();
                    Acao.Parceiro = p;
                    Acao.Status = sa; 

                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<AcaoMarketing> ListarValidacaoMarketingRegional(string Usuario, string Grupo) 
        {

            string SQL = " SELECT [ACAO].[ID] " +
            "             ,[ACAO].Adabas " +
            "             ,[ACAO].[mesAcao] AS mesAcao " +
            "             ,CASE WHEN [ACAO].[origemVerba] = 'Verba Cooperada' THEN 'VPC' " +
            "                   WHEN [ACAO].[origemVerba] = 'Verba de Incentivo a Open' THEN 'VIO' " +
            "              END AS origemVerba " +
            "             ,CRED.[REDE] " +
            "             ,[CRED].CANAL " +
            "             ,[ACAO].DDD " +
            "             ,[STATUS].[DESCRICAO] AS [STATUS] " +
            "             ,SUM([ACAO].valorTotalAcao) AS valorTotalAcao " +
            "             ,SUM([ACAO].valorTotalVivo) AS valorTotalVivo " +
            " FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] [ACAO] " +
            " INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO [CRED] " +
            " ON ([ACAO].[Adabas] = CRED.VENDEDOR) " +
            " INNER JOIN Vivo_SIM.dbo.ACAO_STATUS [STATUS] " +
            " ON ([ACAO].[idStatus] = [STATUS].[id]) " +
            " WHERE [CRED].[STATUS CALLIDUS] IN ('ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') ";

            if (Grupo == "GERENTE DE CONTAS")
            {
                if (Usuario.ToUpper().Contains("A00"))
                {
                    SQL = SQL + " AND RIGHT([CRED].[MATRICULA_GER_CONTA],5) = '" + Usuario.ToUpper().Replace("A00", "") + "'";
                }
                else
                {
                    SQL = SQL + " AND [CRED].[MATRICULA_GER_CONTA] = '" + Usuario + "'";
                }
            }
            else if(Grupo == "MARKETING REGIONAL")
            {
                SQL = SQL + " AND [ACAO].[idStatus] = 3 ";
            }
            else if (Grupo == "MARKETING TERRITORIAL")
            {
                SQL = SQL + " AND [ACAO].[idStatus] = 2 ";
            }
            else if (Grupo == "ADMINISTRATIVO")
            {
                SQL = SQL + " AND [ACAO].[idStatus] IN (2,3) ";
            }
            
            SQL = SQL + " GROUP BY [ACAO].[ID] " +
            "         ,[ACAO].Adabas  " +
            "         ,[ACAO].[mesAcao] " +
            "         ,CASE WHEN [ACAO].[origemVerba] = 'Verba Cooperada' THEN 'VPC' " +
            "               WHEN [ACAO].[origemVerba] = 'Verba de Incentivo a Open' THEN 'VIO' " +
            "          END " +
            "         ,CRED.[REDE] " +
            "         ,[CRED].CANAL " +
            "         ,[ACAO].DDD  " +
            "         ,[STATUS].[DESCRICAO] " +
            " ORDER BY [ACAO].[mesAcao] DESC ";

            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = new List<AcaoMarketing>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Vendedor = item[1].ToString();
                    p.Rede = item[4].ToString();
                    p.Canal = item[5].ToString();
                    p.Ddd = item[6].ToString();

                    StatusAcao sa = new StatusAcao();
                    sa.Descricao = item[7].ToString();

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Id = Convert.ToInt32(item[0]);
                    Acao.MesAcao = item[2].ToString();
                    Acao.OrigemVerba = item[3].ToString();
                    Acao.ValorTotalAcao = item[8].ToString();
                    Acao.ValorTotalVivo = item[9].ToString();
                    Acao.Parceiro = p;
                    Acao.Status = sa; 

                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<AcaoMarketing> ListarAprovacaoAcao(string Usuario, string Grupo)
        {

            string SQL = " SELECT [ACAO].[Id] " +
            "             ,[ACAO].Adabas " +
            "             ,[ACAO].[mesAcao] AS mesAcao " +
            "             ,CASE WHEN [ACAO].[origemVerba] = 'Verba Cooperada' THEN 'VPC' " +
            "                   WHEN [ACAO].[origemVerba] = 'Verba de Incentivo a Open' THEN 'VIO' " +
            "              END AS origemVerba " +
            "             ,CRED.[REDE] " +
            "             ,[CRED].CANAL " +
            "             ,[ACAO].DDD " +
            "             ,[STATUS].[DESCRICAO] AS [STATUS] " +
            "             ,SUM([ACAO].valorTotalAcao) AS valorTotalAcao " +
            "             ,SUM([ACAO].valorTotalVivo) AS valorTotalVivo " +
            " FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] [ACAO] " +
            " INNER JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS_AGRUPADO [CRED] " +
            " ON ([ACAO].[Adabas] = CRED.VENDEDOR) " +
            " INNER JOIN Vivo_SIM.dbo.ACAO_STATUS [STATUS] " +
            " ON ([ACAO].[idStatus] = [STATUS].[id]) " +
            " WHERE [CRED].[STATUS CALLIDUS] IN ('ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') ";

            if (Grupo != "ADMINISTRATIVO")
            {
                if (Usuario.ToUpper().Contains("A00"))
                {
                    SQL = SQL + " AND RIGHT([CRED].[MATRICULA_GER_CONTA],5) = '" + Usuario.ToUpper().Replace("A00", "") + "'";
                }
                else
                {
                    SQL = SQL + " AND [CRED].[MATRICULA_GER_CONTA] = '" + Usuario + "'";
                }
            }

            SQL = SQL + " AND [ACAO].[Protocolo] NOT IN ( " +
            "    	                                                SELECT DISTINCT Protocolo " +
	        "                                                       FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
	        "                                                       WHERE EXISTS " +
	        "                                                               ( " +
		    "                                                                   SELECT * " +
		    "                                                                   FROM [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
		    "                                                                   WHERE IdStatus = 3 " +
	        "                                                               ) " +
	        "                                                       AND idStatus <> 3 " +
            "                                                    ) " +
            " AND [ACAO].[idStatus] = 3 " +
            " GROUP BY [ACAO].[Id] " +
            "         ,[ACAO].Adabas  " +
            "         ,[ACAO].[mesAcao] " +
            "         ,CASE WHEN [ACAO].[origemVerba] = 'Verba Cooperada' THEN 'VPC' " +
            "               WHEN [ACAO].[origemVerba] = 'Verba de Incentivo a Open' THEN 'VIO' " +
            "          END " +
            "         ,CRED.[REDE] " +
            "         ,[CRED].CANAL " +
            "         ,[ACAO].DDD  " +
            "         ,[STATUS].[DESCRICAO] " +
            " ORDER BY [ACAO].[mesAcao] DESC ";

            DataTable dsAcao = null;
            List<AcaoMarketing> retorno = new List<AcaoMarketing>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    TipoMidia tm = new TipoMidia();

                    Parceiro p = new Parceiro();
                    p.Vendedor = item[1].ToString();
                    p.Rede = item[4].ToString();
                    p.Canal = item[5].ToString();
                    p.Ddd = item[6].ToString();

                    StatusAcao sa = new StatusAcao();
                    sa.Descricao = item[7].ToString();

                    AcaoMarketing Acao = new AcaoMarketing();
                    Acao.Id = Convert.ToInt32(item[0]);
                    Acao.MesAcao = item[2].ToString();
                    Acao.OrigemVerba = item[3].ToString();
                    Acao.ValorTotalAcao = item[8].ToString();
                    Acao.ValorTotalVivo = item[9].ToString();
                    Acao.Parceiro = p;
                    Acao.Status = sa;
                    Acao.Midia = tm;
                    Acao.MidiaDetalhada = tm;

                    retorno.Add(Acao);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public bool InserirAcao(AcaoMarketing Acao) 
        {
            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
            "   ([DataCadastro] " +
            "   ,[adabas] " +
            "   ,[regional] " +
            "   ,[praca] " +
            "   ,[ddd] " +
            "   ,[idTipo] " +
            "   ,[idDetalhada] " +
            "   ,[veiculo] " +
            "   ,[descricaoAcao] " +
            "   ,[dia1] " +
            "   ,[dia2] " +
            "   ,[dia3] " +
            "   ,[dia4] " +
            "   ,[dia5] " +
            "   ,[dia6] " +
            "   ,[dia7] " +
            "   ,[dia8] " +
            "   ,[dia9] " +
            "   ,[dia10] " +
            "   ,[dia11] " +
            "   ,[dia12] " +
            "   ,[dia13] " +
            "   ,[dia14] " +
            "   ,[dia15] " +
            "   ,[dia16] " +
            "   ,[dia17] " +
            "   ,[dia18] " +
            "   ,[dia19] " +
            "   ,[dia20] " +
            "   ,[dia21] " +
            "   ,[dia22] " +
            "   ,[dia23] " +
            "   ,[dia24] " +
            "   ,[dia25] " +
            "   ,[dia26] " +
            "   ,[dia27] " +
            "   ,[dia28] " +
            "   ,[dia29] " +
            "   ,[dia30] " +
            "   ,[dia31] " +
            "   ,[campanhaMacro] " +
            "   ,[mesAcao] " +
            "   ,[midiaExclusivaVivo] " +
            "   ,[valorTotalAcao] " +
            "   ,[valorTotalVivo] " +
            "   ,[percentParticipacaoVivo] " +
            "   ,[focoPrincipal] " +
            "   ,[qtdAcoes] " +
            "   ,[insercoes] " +
            "   ,[origemVerba] " +
            "   ,[idStatus] " +
            "   ,[idAcesso] " +
            "   ,[idAcessoCadastro] " +
            "   ,[formatoMaterial] " +
            "   ,[CustoUnitario] " +
            "   ,[Protocolo] " +
            "   ,[IXO] " +
            "   ,[NomeContatoRede] " +
            "   ,[TelefoneContatoRede] " +
            "   ,[EmailContatoRede] " +
            "   ,[DataPreenchimentoInicial] " +
            "   ,[DataPreenchimentoFinal] " +
            "   ,[PercentualRede] " +
            "   ,[ValorTotalRede]) " +
            " VALUES ( " +
            " GetDate() " +
            " ,'" + Acao.Parceiro.Vendedor + "'" +
            " ,'" + Acao.Parceiro.Regional + "'" +
            " ,'" + Acao.Parceiro.Uf + "'" +
            " ,'" + Acao.Parceiro.Ddd + "'" +
            " ," + Acao.Midia.IdTipoMidia +
            " ," + Acao.MidiaDetalhada.IdMidiaDetalhada +
            " ,'" + Acao.Veiculo + "'" +
            " ,'" + Acao.DescricaoAcao + "'" +
            " ," + Acao.Dia1 +
            " ," + Acao.Dia2 +
            " ," + Acao.Dia3 +
            " ," + Acao.Dia4 +
            " ," + Acao.Dia5 +
            " ," + Acao.Dia6 +
            " ," + Acao.Dia7 +
            " ," + Acao.Dia8 +
            " ," + Acao.Dia9 +
            " ," + Acao.Dia10 +
            " ," + Acao.Dia11 +
            " ," + Acao.Dia12 +
            " ," + Acao.Dia13 +
            " ," + Acao.Dia14 +
            " ," + Acao.Dia15 +
            " ," + Acao.Dia16 +
            " ," + Acao.Dia17 +
            " ," + Acao.Dia18 +
            " ," + Acao.Dia19 +
            " ," + Acao.Dia20 +
            " ," + Acao.Dia21 +
            " ," + Acao.Dia22 +
            " ," + Acao.Dia23 +
            " ," + Acao.Dia24 +
            " ," + Acao.Dia25 +
            " ," + Acao.Dia26 +
            " ," + Acao.Dia27 +
            " ," + Acao.Dia28 +
            " ," + Acao.Dia29 +
            " ," + Acao.Dia30 +
            " ," + Acao.Dia31 +
            " ,'" + Acao.CampanhaMacro + "'" +
            " ,'" + Acao.MesAcao + "'" +
            " ,'" + Acao.MidiaExclusivaVivo + "'" +
            " ,convert(numeric(18,2),'" + Acao.ValorTotalAcao.Replace(".", "").Replace(",", ".") + "')" +
            " ,convert(numeric(18,2),'" + Acao.ValorTotalVivo.Replace(".", "").Replace(",", ".") + "')" +
            " ,'" + Acao.PercentualParticipacaoVivo + "'" +
            " ,'" + Acao.FocoPrincipal + "'" +
            " ,'" + Acao.QtdAcoes + "'" +
            " ," + Acao.Insercoes +
            " ,'" + Acao.OrigemVerba + "'" +
            " ," + Acao.Status.Id +
            " ," + Acao.IdAcesso +
            " ," + Acao.IdAcesso +
            " ,'" + Acao.FormatoMaterial + "'" +
            " ,convert(numeric(18,2),'" + Acao.CustoUnitario.Replace(".", "").Replace(",", ".") + "')" +
            " ,'" + Acao.Protocolo + "'" +
            " ,'" + Acao.Parceiro.Ixos + "'" +
            " ,'" + Acao.NomeContatoRede + "'" +
            " ,'" + Acao.TelefoneContatoRede + "'" +
            " ,'" + Acao.EmailContatoRede + "'" +
            " ,CONVERT(DATETIME,'" + Acao.DtPreencherDiasIni + "',103) " +
            " ,CONVERT(DATETIME,'" + Acao.DtPreencherDiasFim + "',103) " +
            " ,'" + Acao.PercentualRede + "'" +
            " ,convert(numeric(18,2),'" + Acao.ValorTotalRede.Replace(".", "").Replace(",", ".") + "')" +
            " ) "; 

            try 
	        {
                return dao.ExecutarSql(SQL);
	        }
	        catch (Exception)
	        {
		
		        throw;
	        }
        }

        public bool AtualizarAcao(AcaoMarketing Acao)
        {
            TipoMidiaRepositorio tr = new TipoMidiaRepositorio();
            TipoMidia TM = tr.ObterMidiaDetalhadaNome(Acao.Midia.IdTipoMidia, Acao.MidiaDetalhada.MidiaDetalhada);

            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
            "   SET [adabas] = '" + Acao.Parceiro.Vendedor + "'" +
            "   ,[regional] = '" + Acao.Parceiro.Regional + "'" +
            "   ,[praca] = '" + Acao.Parceiro.Uf + "'" +
            "   ,[ddd] = '" + Acao.Parceiro.Ddd + "'" +
            "   ,[idTipo] = '" + Acao.Midia.IdTipoMidia + "'" +
            "   ,[idDetalhada] = '" + TM.IdMidiaDetalhada + "'" +
            "   ,[veiculo] = '" + Acao.Veiculo + "'" +
            "   ,[descricaoAcao] = '" + Acao.DescricaoAcao + "'" +
            "   ,[dia1] = " + Acao.Dia1 +
            "   ,[dia2] = " + Acao.Dia2 +
            "   ,[dia3] = " + Acao.Dia3 +
            "   ,[dia4] = " + Acao.Dia4 +
            "   ,[dia5] = " + Acao.Dia5 +
            "   ,[dia6] = " + Acao.Dia6 +
            "   ,[dia7] = " + Acao.Dia7 +
            "   ,[dia8] = " + Acao.Dia8 +
            "   ,[dia9] = " + Acao.Dia9 +
            "   ,[dia10] = " + Acao.Dia10 +
            "   ,[dia11] = " + Acao.Dia11 +
            "   ,[dia12] = " + Acao.Dia12 +
            "   ,[dia13] = " + Acao.Dia13 +
            "   ,[dia14] = " + Acao.Dia14 +
            "   ,[dia15] = " + Acao.Dia15 +
            "   ,[dia16] = " + Acao.Dia16 +
            "   ,[dia17] = " + Acao.Dia17 +
            "   ,[dia18] = " + Acao.Dia18 +
            "   ,[dia19] = " + Acao.Dia19 +
            "   ,[dia20] = " + Acao.Dia20 +
            "   ,[dia21] = " + Acao.Dia21 +
            "   ,[dia22] = " + Acao.Dia22 +
            "   ,[dia23] = " + Acao.Dia23 +
            "   ,[dia24] = " + Acao.Dia24 +
            "   ,[dia25] = " + Acao.Dia25 +
            "   ,[dia26] = " + Acao.Dia26 +
            "   ,[dia27] = " + Acao.Dia27 +
            "   ,[dia28] = " + Acao.Dia28 +
            "   ,[dia29] = " + Acao.Dia29 +
            "   ,[dia30] = " + Acao.Dia30 +
            "   ,[dia31] = " + Acao.Dia31 +
            "   ,[campanhaMacro] = '" + Acao.CampanhaMacro + "'" +
            "   ,[midiaExclusivaVivo] = '" + Acao.MidiaExclusivaVivo + "'" +
            "   ,[valorTotalAcao] = convert(numeric(18,2),'" + Acao.ValorTotalAcao.Replace(".", "").Replace(",", ".") + "')" +
            "   ,[valorTotalVivo] = convert(numeric(18,2),'" + Acao.ValorTotalVivo.Replace(".", "").Replace(",", ".") + "')" +
            "   ,[percentParticipacaoVivo] = '" + Acao.PercentualParticipacaoVivo + "'" +
            "   ,[focoPrincipal] = '" + Acao.FocoPrincipal + "'" +
            "   ,[qtdAcoes] = '" + Acao.QtdAcoes + "'" +
            "   ,[insercoes] = '" + Acao.Insercoes + "'" +
            "   ,[origemVerba] = '" + Acao.OrigemVerba + "'";

            if(Acao.OrigemVerba.Equals("Verba Cooperada"))
            {
                SQL = SQL + "  ,[idStatus] = 2 ";
            }
            else
            {
                SQL = SQL + "  ,[idStatus] = 3 ";
            }


            SQL = SQL + "  ,[idAcesso] = 1" +
            "   ,[formatoMaterial] = '" + Acao.FormatoMaterial + "'" +
            "   ,[CustoUnitario] = convert(numeric(18,2),'" + Acao.CustoUnitario.Replace(".", "").Replace(",", ".") + "')" +
            "   ,[IXO] = '" + Acao.Parceiro.Ixos + "'" +
            "   ,[NomeContatoRede] = '" + Acao.NomeContatoRede + "'" +
            "   ,[TelefoneContatoRede] = '" + Acao.TelefoneContatoRede + "'" +
            "   ,[EmailContatoRede] = '" + Acao.EmailContatoRede + "'" +
            "   ,[DataPreenchimentoInicial] = CONVERT(DATETIME,'" + Acao.DtPreencherDiasIni + "',103)" +
            "   ,[DataPreenchimentoFinal] = CONVERT(DATETIME,'" + Acao.DtPreencherDiasFim + "',103)" +
            " WHERE ID = " + Acao.Id;

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
