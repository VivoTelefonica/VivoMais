using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class AcaoAcompanhamentoRepositorio
    {
        DAO dao;

        public AcaoAcompanhamentoRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public DataTable ObterGraficoAcompanhamento(string Protocolo)
        {
            string SQL = "SELECT [Vivo_SIM].[dbo].[ACAO_CADASTRO].ID " +
            " ,DATEDIFF (DD, LIBERACAO_VERBA,CADASTRO_ACAO)  AS TEMPO_LIBERACAO_CADASTRO " +
            " ,CASE WHEN (VALIDACAO_MKT_TERRITORIAL IS NOT NULL) THEN DATEDIFF (DD, CADASTRO_ACAO,VALIDACAO_MKT_TERRITORIAL) " +
            "       WHEN (RETORNAR_GC IS NOT NULL) THEN DATEDIFF (DD, CADASTRO_ACAO,RETORNAR_GC) " +
	        "       WHEN (VALIDACAO_MKT_TERRITORIAL IS NULL AND RETORNAR_GC IS NULL) THEN DATEDIFF (DD, CADASTRO_ACAO,GETDATE()) " +
	        "       ELSE 0 END AS TEMPO_MKT_TERRITORIAL " +
            " ,CASE WHEN (DATEDIFF (DD, VALIDACAO_MKT_TERRITORIAL,VALIDACAO_MKT_REGIONAL) IS NULL) THEN 0 ELSE DATEDIFF (DD, VALIDACAO_MKT_TERRITORIAL,VALIDACAO_MKT_REGIONAL) END AS TEMPO_MKT_TERRITORIAL_REGIONAL " +
            " ,CASE WHEN (DATEDIFF (DD, VALIDACAO_MKT_REGIONAL,APROVACAO) IS NULL) THEN 0 ELSE DATEDIFF (DD, VALIDACAO_MKT_REGIONAL,APROVACAO) END  AS TEMPO_APROVACAO " +
            " INTO #SLA " +
            " FROM [Vivo_SIM].[dbo].[ACAO_HISTORICO] INNER JOIN [Vivo_SIM].[dbo].[ACAO_CADASTRO] " +
            " ON([Vivo_SIM].[dbo].[ACAO_HISTORICO].IDVPC = [Vivo_SIM].[dbo].[ACAO_CADASTRO].id) " +
            " WHERE [Vivo_SIM].[dbo].[ACAO_CADASTRO].[Protocolo] = '" + Protocolo + "' " +

            " SELECT ID " +
            " ,MIN(TEMPO_LIBERACAO_CADASTRO)  AS TEMPO_LIBERACAO_CADASTRO " +
            " ,SUM(TEMPO_MKT_TERRITORIAL) AS TEMPO_MKT_TERRITORIAL " +
            " ,SUM(TEMPO_MKT_TERRITORIAL_REGIONAL) AS TEMPO_MKT_TERRITORIAL_REGIONAL " +
            " ,SUM(TEMPO_APROVACAO) AS TEMPO_APROVACAO " +
            " FROM #SLA " +
            " GROUP BY ID ";

            return dao.returnaDataTable(SQL);
        }

        public bool InserirNumeroREL(string NumeroProtocolo, string REL, string Documento, string DataSolicitacaoREL)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO] " +
                         "  SET REL = '" + REL + "' " +
                         " ,DOC_SAP = '" + Documento + "' " +
                         " ,DATA_SOLICITACAO_REL = CONVERT(DATETIME,'" + DataSolicitacaoREL + "',103) " +
                         " WHERE NUMERO_PROTOCOLO = '" + NumeroProtocolo + "'";

            return dao.ExecutarSql(SQL);
        }

        public bool SelecionaAcoesParaRel(string NumeroProtocolo)
        {
            string SQL = "UPDATE [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO] ";
            SQL = SQL + " SET [SOLICITA_REL] = GETDATE() ";
            SQL = SQL + " WHERE NUMERO_PROTOCOLO = '" + NumeroProtocolo + "'";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizarAcompanhamentoProtocolo(string Protocolo, string NumeroProtocolo, DateTime SolicitacaoProtocolo, DateTime VencimentoProtocolo)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO] " +
                         " SET [DATA_SOLICITACAO_PROTOCOLO] = CONVERT(DATETIME,'" + SolicitacaoProtocolo + "',103) " +
                         "    ,[DATA_VENCIMENTO_PROTOCOLO] = CONVERT(DATETIME,'" + VencimentoProtocolo + "',103) " +
                         "    ,[NUMERO_PROTOCOLO] = '" + NumeroProtocolo + "'" +
                         " WHERE [PROTOCOLO] = '" + Protocolo + "'";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizarAcompanhamento(AcaoAcompanhamento Acao)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO] " +
                         " SET [STATUS_PAGAMENTO] = '" + Acao.StatusPagamento + "' ";

                        if (Acao.EnviadoMarketingTerritorial != DateTime.MinValue && Acao.EnviadoMarketingTerritorial != null)
                        {
                            SQL = SQL + " ,[ENVIADO_MKT_TERRITORIAL] = '" + Acao.EnviadoMarketingTerritorial + "' ";
                        }

                        if (Acao.EnviadoMarketingRegional != DateTime.MinValue && Acao.EnviadoMarketingRegional != null)
                        {
                            SQL = SQL + " ,[ENVIADO_MKT_REGIONAL] = '" + Acao.EnviadoMarketingRegional + "' ";
                        }

                        if (Acao.EnviadoMidia != DateTime.MinValue && Acao.EnviadoMidia != null)
                        {
                            SQL = SQL + " ,[ENVIADO_MIDIA] = '" + Acao.EnviadoMidia + "' ";
                        }

                        if (Acao.EnviadoAuditoria != DateTime.MinValue && Acao.EnviadoAuditoria != null)
                        {
                            SQL = SQL + " ,[ENVIADO_AUDITORIA] = '" + Acao.EnviadoAuditoria + "' ";
                        }

                        if (Acao.EnviadoJU != DateTime.MinValue && Acao.EnviadoJU != null)
                        {
                            SQL = SQL + " ,[ENVIADO_JANELA_UNICA] = '" + Acao.EnviadoJU + "' ";
                        }

                        SQL = SQL + " WHERE [NUMERO_PROTOCOLO] = '" + Acao.NumeroProtocolo + "'";

            return dao.ExecutarSql(SQL);
        }

        public AcaoAcompanhamento ObterAcaoAcompanhamentoProtocolo(string Protocolo)
        {
            AcaoMarketingRepositorio _repositorio = new AcaoMarketingRepositorio();

            string SQL = " SELECT [Id] " +
            "  ,[PROTOCOLO] " +
            "  ,CONVERT(VARCHAR,[ENVIADO_MKT_TERRITORIAL],103) AS [ENVIADO_MKT_TERRITORIAL] " +
            "  ,CONVERT(VARCHAR,[SOLICITA_REL],103) AS [SOLICITA_REL] " +
            "  ,CONVERT(VARCHAR,[ENVIADO_MKT_REGIONAL],103) AS [ENVIADO_MKT_REGIONAL] " +
            "  ,CONVERT(VARCHAR,[ENVIADO_MIDIA],103) AS [ENVIADO_MIDIA] " +
            "  ,CONVERT(VARCHAR,[ENVIADO_AUDITORIA],103) AS [ENVIADO_AUDITORIA] " +
            "  ,CONVERT(VARCHAR,[ENVIADO_JANELA_UNICA],103) AS [ENVIADO_JANELA_UNICA] " +
            "  ,[OBS] " +
            "  ,[STATUS_PAGAMENTO] " +
            "  ,[NUMERO_PROTOCOLO] " +
            "  ,CONVERT(VARCHAR,[DATA_SOLICITACAO_PROTOCOLO],103) AS [DATA_SOLICITACAO_PROTOCOLO] " +
            "  ,CONVERT(VARCHAR,[DATA_VENCIMENTO_PROTOCOLO],103) AS [DATA_VENCIMENTO_PROTOCOLO] " +
            "  ,[REL] " +
            "  ,CONVERT(VARCHAR,[DATA_SOLICITACAO_REL],103) AS [DATA_SOLICITACAO_REL] " +
            "  ,CONVERT(VARCHAR,[DATA_VENCIMENTO_REL],103) AS [DATA_VENCIMENTO_REL] " +
            "  ,CONVERT(VARCHAR,[DATA_SOLICITACAO],103) AS [DATA_SOLICITACAO] " +
            "  ,[DOC_SAP] " +
            " FROM [Vivo_SIM].[dbo].[ACAO_ACOMPANHAMENTO] " +
            " WHERE [PROTOCOLO] = '" + Protocolo + "'";

            DataTable dsAcompanhamento = null;
            AcaoAcompanhamento Acompanhamento = null;

            dsAcompanhamento = this.dao.returnaDataTable(SQL);

            if (dsAcompanhamento.Rows.Count > 0)
            {
                Acompanhamento = new AcaoAcompanhamento();
                foreach (DataRow item in dsAcompanhamento.Rows)
                {
                    Acompanhamento.Id = item[0].ToString();
                    Acompanhamento.NumeroProtocolo = item[1].ToString();

                    if (item[2].ToString() != "")
                    {
                        Acompanhamento.EnviadoMarketingTerritorial = Convert.ToDateTime(item[2]);
                    }

                    if (item[3].ToString() != "")
                    {
                        Acompanhamento.SolicitacaoREL = Convert.ToDateTime(item[3]);
                    }

                    if (item[4].ToString() != "")
                    {
                        Acompanhamento.EnviadoMarketingRegional = Convert.ToDateTime(item[4]);
                    }

                    if (item[5].ToString() != "")
                    {
                        Acompanhamento.EnviadoMidia = Convert.ToDateTime(item[5]);
                    }

                    if (item[6].ToString() != "")
                    {
                        Acompanhamento.EnviadoAuditoria = Convert.ToDateTime(item[6]);
                    }

                    if (item[7].ToString() != "")
                    {
                        Acompanhamento.EnviadoJU = Convert.ToDateTime(item[7]);
                    }

                    Acompanhamento.Obs = item[8].ToString();
                    Acompanhamento.StatusPagamento = item[9].ToString();
                    Acompanhamento.NumeroProtocolo = item[10].ToString();

                    if (item[11].ToString() != "")
                    {
                        Acompanhamento.DataSolicitacaoProtocolo = Convert.ToDateTime(item[11]);
                    }

                    if (item[12].ToString() != "")
                    {
                        Acompanhamento.DataVencimentoProtocolo = Convert.ToDateTime(item[12]);
                    }

                    Acompanhamento.Rel = item[13].ToString();

                    if (item[14].ToString() != "")
                    {
                        Acompanhamento.DataSolicitacaoRel = Convert.ToDateTime(item[14]);
                        Acompanhamento.DataVencimentoRel = Convert.ToDateTime(item[14].ToString()).AddDays(90);
                    }

                    //if (item[15].ToString() != "")
                    //{
                    //    Acompanhamento.DataVencimentoRel = Convert.ToDateTime(Acompanhamento.DataSolicitacaoRel).AddDays(30).ToString("dd/MM/yyyy");
                    //}

                    if (item[16].ToString() != "")
                    {
                        Acompanhamento.DataSolicitacao = Convert.ToDateTime(item[16]);
                    }

                    Acompanhamento.DocSAP = item[17].ToString();

                }



                return Acompanhamento;
            }
            else
            {
                return null;
            }
        }
    }
}