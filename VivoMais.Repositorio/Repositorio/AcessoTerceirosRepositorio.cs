using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class AcessoTerceirosRepositorio
    {
        DAO dao;

        public AcessoTerceirosRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public bool RecusarSenhaAcesso(string Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            "  SET [RejeitarSenha] = GETDATE() " +
            " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool AceitaSenhaAcesso(string Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            "  SET [AceiteSenha] = GETDATE() " +
            " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public List<AcessoTerceiros> ObterLoginSenha(string Login)
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[ID] " +
            "          ,(UPPER([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[Nome]) + ' ' + UPPER([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[Sobrenome])) AS NOME " +
            "          ,UPPER([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[Perfil]) AS PERFIL " +
            "          ,UPPER([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[Funcao]) AS FUNCAO " +
            "          ,[Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[LOGIN] " +
            "          ,[Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[SENHA] " +
            "  FROM [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            "  INNER JOIN [Vivo_SIM].[dbo].[ACESSO] " +
            "  ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].idAcesso = [Vivo_SIM].[dbo].[ACESSO].idAcesso) " +
            "  WHERE RejeitarSenha IS NULL " +
            "  AND AceiteSenha IS NULL " +
            "  AND STATUS = 'CADASTRADO' " +
            "  AND MobileToken IS NOT NULL " +
            "  AND DataExtracao IS NOT NULL " +
            "  AND [Vivo_SIM].[dbo].[ACESSO].Login = '" + Login + "'";
            

            DataTable dsAcessos = null;
            List<AcessoTerceiros> retorno = new List<AcessoTerceiros>();

            dsAcessos = this.dao.returnaDataTable(SQL);

            if (dsAcessos.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcessos.Rows)
                {
                    AcessoTerceiros acc = new AcessoTerceiros();
                    acc.Id = Convert.ToInt32(item[0]);
                    acc.Nome = Convert.ToString(item[1]);
                    acc.Perfil = Convert.ToString(item[2]);
                    acc.Funcao = Convert.ToString(item[3]);
                    acc.Login = Convert.ToString(item[4]);
                    acc.Senha = Convert.ToString(item[5]);

                    retorno.Add(acc);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public bool AtualizarMobileToken(string Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            "  SET [MobileToken] = GETDATE() " +
            " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public List<AcessoTerceiros> ObterEsperandoMobileToken(string Login)
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[ID] " +
            "   ,(UPPER([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[Nome]) + ' ' + UPPER([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[Sobrenome])) AS NOME " +
            "   ,UPPER([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[Perfil]) " +
            "   ,UPPER([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[Funcao]) " +
            "   ,DataCadastro " +
            " FROM [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            " INNER JOIN [Vivo_SIM].[dbo].[ACESSO] " +
            " ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].idAcesso = [Vivo_SIM].[dbo].[ACESSO].idAcesso) " +
            " WHERE [MobileToken] IS NULL " +
            " AND [DataExtracao] IS NOT NULL " +
            " AND STATUS = 'CADASTRADO'" +
            " AND [Vivo_SIM].[dbo].[ACESSO_TERCEIROS].Login IS NULL " +
            " AND [Vivo_SIM].[dbo].[ACESSO_TERCEIROS].Senha  IS NULL " +
            " AND [Vivo_SIM].[dbo].[ACESSO].Login = '" + Login + "'" +
            " ORDER BY DataCadastro";

            DataTable dsAcessos = null;
            List<AcessoTerceiros> retorno = new List<AcessoTerceiros>();

            dsAcessos = this.dao.returnaDataTable(SQL);

            if (dsAcessos.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcessos.Rows)
                {
                    AcessoTerceiros acc = new AcessoTerceiros();
                    acc.Id = Convert.ToInt32(item[0]);
                    acc.Nome = Convert.ToString(item[1]);
                    acc.Perfil = Convert.ToString(item[2]);
                    acc.Funcao = Convert.ToString(item[3]);
                    acc.DataCadastro = Convert.ToDateTime(item[4]);

                    retorno.Add(acc);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public bool AceitarSenhaAcesso(string Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            "  SET AceiteSenha = GETDATE() " +
            " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool RejeiteSenhaAcesso(string Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            "  SET RejeitarSenha = GETDATE() " +
            " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool MotivoRecusaAcesso(string Id, string Obs)
        {
            string SQL = "UPDATE [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            "  SET OBS = '" + Obs + "' " +
            " ,Status = 'REJEITADO' " +
            " ,DataStatus = GETDATE() " +
            " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public bool InsereLoginSenhaAcesso(string Id, string Login, string Senha)
        {
            string SQL = "UPDATE [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
            "  SET Login = '" + Login + "' " +
            " ,Senha = '" + Senha + "' " +
            " ,Status = 'FINALIZADO' " +
            " ,DataStatus = GETDATE() " +
            " WHERE ID = " + Id;

            return dao.ExecutarSql(SQL);
        }

        public DataTable RetornaAtualizacaoAcessos()
        {
            string SQL = " SELECT [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[ID] " + 
            "                   ,ACAO " +
            "                   ,UPPER((Nome + ' ' + Sobrenome)) as NOME " +
            "                   ,CPF " +
            "                   ,PERFIL " +
            "                   ,MATRICULA " +
            "                   ,CONVERT(VARCHAR, DataCadastro, 103) AS [DataCadastro] " +
            "                   ,CONVERT(VARCHAR, DataExtracao, 103) AS [DataExtracao] " +
            "                   ,CONVERT(VARCHAR, MobileToken, 103) AS [MobileToken] " +
            "                   ,LOGIN " +
            "                   ,SENHA " +
            " FROM [VIVO_SIM].[dbo].[ACESSO_TERCEIROS] " +
            " INNER JOIN  Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS " +
            " ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].ADABAS = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.VENDEDOR) " +
            " WHERE (([VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Status] = 'CADASTRADO') OR ([VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Status] = 'FINALIZADO'))  " +
            " AND [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[RejeitarSenha] IS NULL " +
            " AND [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[AceiteSenha] IS NULL " ;


            return this.dao.returnaDataTable(SQL);

        }

        public DataTable RetornaAtualizacaoAcessos(string Uf, string Gc, string DtIni, string DtFim)
        {
            string SQL = " SELECT [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[ID] " +
            "                   ,ACAO " +
            "                   ,UPPER((Nome + ' ' + Sobrenome)) as NOME " +
            "                   ,CPF " +
            "                   ,PERFIL " +
            "                   ,MATRICULA " +
            "                   ,CONVERT(VARCHAR, DataCadastro, 103) AS [DataCadastro] " +
            "                   ,CONVERT(VARCHAR, DataExtracao, 103) AS [DataExtracao] " +
            "                   ,CONVERT(VARCHAR, MobileToken, 103) AS [MobileToken] " +
            "                   ,LOGIN " +
            "                   ,SENHA " +
            " FROM [VIVO_SIM].[dbo].[ACESSO_TERCEIROS] " +
            " INNER JOIN  Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS " +
            " ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].ADABAS = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.VENDEDOR) " +
            " WHERE (([VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Status] = 'CADASTRADO') OR ([VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Status] = 'FINALIZADO'))  " +
            " AND Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.[UF] = '" + Uf + "' " +
            " AND Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.[GESTOR] = '" + Gc + "'" +
            " AND CONVERT(DATETIME,DataCadastro,103) BETWEEN CONVERT(DATETIME,'" + Convert.ToDateTime(DtIni) + "',103) AND CONVERT(DATETIME,'" + Convert.ToDateTime(DtFim) + "',103) ";
            //" AND [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[RejeitarSenha] IS NULL " +
            //" AND [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[AceiteSenha] IS NULL ";


            return this.dao.returnaDataTable(SQL);

        }

        public DataTable ExtrairSolicitacoesAcessos(string Uf, string Gc, string DtIni, string DtFim)
        {
            
            string SQL = " SELECT * FROM [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] LEFT JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS " +
            " ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].ADABAS = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.VENDEDOR) " +
            " WHERE UF = '" + Uf + "' " +
            " AND GESTOR = '" + Gc + "' " +
            " AND [Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[STATUS] <> 'REJEITADO' ";

            if(DtIni != "" && DtFim != "")
            {
                SQL = SQL + " AND CONVERT(DATETIME,DataCadastro,103) BETWEEN CONVERT(DATETIME,'" + Convert.ToDateTime(DtIni) + "',103) AND CONVERT(DATETIME,'" + Convert.ToDateTime(DtFim) + "',103) " + 
                            " AND DataExtracao IS NOT NULL ";
            }
            else
            {
                SQL = SQL + " AND DataExtracao IS NULL ";
            }

            DataTable dt = this.dao.returnaDataTable(SQL);

            if (AtualizaDataExtracao(Uf, Gc, DtIni, DtFim))
            {
                return dt;
            }
            else
            {
                return null;
            }
            
        }

        public bool AtualizaDataExtracao(string Uf, string Gc, string DtIni, string DtFim) 
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
                         " SET DataExtracao = GETDATE() " +
                         " FROM [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] LEFT JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS " +
                         " ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].ADABAS = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.VENDEDOR) " +
                         " WHERE UF = '" + Uf + "' " +
                         " AND GESTOR = '" + Gc + "' " +
                         " AND [Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[STATUS] <> 'REJEITADO' ";

            if (DtIni != "" && DtFim != "")
            {
                SQL = SQL + " AND CONVERT(DATETIME,DataCadastro,103) BETWEEN CONVERT(DATETIME,'" + Convert.ToDateTime(DtIni) + "',103) AND CONVERT(DATETIME,'" + Convert.ToDateTime(DtFim) + "',103) " +
                            " AND DataExtracao IS NOT NULL ";
            }
            else
            {
                SQL = SQL + " AND DataExtracao IS NULL ";
            }

            return dao.ExecutarSql(SQL);
        }

        public DataTable ObterAcessosEmAbertoGC(string GC)
        {
            string SQL = " SELECT " +
            " [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].ID " +
            " ,UPPER((Nome + ' ' + Sobrenome)) as NOME " +
            " ,MATRICULA " +
            " ,CASE WHEN [DataExtracao] IS NULL THEN 'AGUARDANDO HUMANOS' " +
            "       WHEN [DataExtracao] IS NOT NULL AND [MobileToken] IS NULL THEN 'AGUARDANDO CADASTRO NO MOBILE TOKEN' " +
            "       WHEN [MobileToken] IS NOT NULL AND [Senha] IS NULL THEN 'AGUARDANDO SENHA' " +
            "       WHEN [Login] IS NOT NULL AND [Senha] IS NOT NULL THEN 'ACESSO LIBERADO'  " +
            "       ELSE '' END AS STATUS " +
            " ,DATEDIFF(DD,DataCadastro,GETDATE()) as SLA " +
            " ,LOGIN " +
            " ,SENHA " +
            " FROM [VIVO_SIM].[dbo].[ACESSO_TERCEIROS] " +
            " WHERE (([VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Status] = 'CADASTRADO') OR ([VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Status] = 'FINALIZADO')) " +
            " AND [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[RejeitarSenha] IS NULL " +
            " AND [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[AceiteSenha] IS NULL ";
            //" AND ( " +
            //"     ( " +
            //"     [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Senha] IS NULL)  " +
            //"     OR ( " +
            //"         [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Senha] IS NOT NULL AND DataCadastro BETWEEN (GETDATE()-5) AND GETDATE() " +
            //"         ) " +
            //" )  ";


            if (GC != "TODOS")
            {
                SQL = SQL + " AND GESTOR = '" + GC + "' ";
            }


            return this.dao.returnaDataTable(SQL);
        }

        public DataTable ObterAcessosEmAbertoGC(DateTime dtIni, DateTime dtFim, string GerenteContas, string Status)
        {
            string SQL = " SELECT " +
            " UPPER((Nome + ' ' + Sobrenome)) as NOME " +
            " ,MATRICULA " +
            " ,CASE WHEN [DataExtracao] IS NULL THEN 'AGUARDANDO HUMANOS' " +
            "       WHEN [DataExtracao] IS NOT NULL AND [MobileToken] IS NULL THEN 'AGUARDANDO CADASTRO NO MOBILE TOKEN' " +
            "       WHEN [MobileToken] IS NOT NULL AND [Senha] IS NULL THEN 'AGUARDANDO SENHA'  " +
            "       ELSE '' END AS STATUS " +
            " ,DATEDIFF(DD,DataCadastro,GETDATE()) as SLA " +
            " ,LOGIN " +
            " ,SENHA " +
            " FROM [VIVO_SIM].[dbo].[ACESSO_TERCEIROS] " +
            " INNER JOIN  Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS " +
            " ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].ADABAS = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.VENDEDOR) " +
            " WHERE [VIVO_SIM].[dbo].[ACESSO_TERCEIROS].[Status] = 'CADASTRADO' " +
            " AND CONVERT(DATETIME,DataCadastro,103) BETWEEN CONVERT(DATETIME,'" + dtIni + "',103) AND CONVERT(DATETIME,'" + dtFim + "',103) ";

            if (GerenteContas != "TODOS")
            {
                SQL = SQL + " AND GESTOR = '" + GerenteContas + "' ";
            }

            if (Status != "TODOS")
            {
                SQL = SQL + " AND STATUS = '" + Status + "' ";
            }


            return this.dao.returnaDataTable(SQL);
        }

        public DataTable ObterAcessosEmAberto(DateTime DataIni, DateTime DataFim, string GC)
        {
            string SQL = " SELECT [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].UF " +
            "                    ,[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].GESTOR " +
            "                    ,CASE WHEN DataExtracao IS NOT NULL THEN 'SIM' ELSE 'NÃO' END AS EXTRAIDO  " +
            "                    ,CONVERT(VARCHAR,MIN(DataCadastro),103) AS DataCadastro " +
            "                    ,DATEDIFF(DD,[DataCadastro],GETDATE()) as SLA " +
            "                    ,COUNT(*) AS TOTAL " +
            " FROM [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] LEFT JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS " +
            " ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].ADABAS = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.VENDEDOR) " +
            " WHERE CONVERT(DATETIME,DataCadastro,103) BETWEEN CONVERT(DATETIME,'" + DataIni + "',103) AND CONVERT(DATETIME,'" + DataFim + "',103) " +
            " AND [Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[STATUS] <> 'REJEITADO' ";
            //" AND DataExtracao IS NOT NULL ";

            if (GC != "TODOS")
            {
                SQL = SQL + " AND GESTOR = '" + GC + "' ";
            }

            SQL = SQL + " GROUP BY " +
            "                   [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].UF " +
            "                  ,[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].GESTOR " +
            "                  ,CASE WHEN DataExtracao IS NOT NULL THEN 'SIM' ELSE 'NÃO' END " +
            "                  ,DATEDIFF(DD,[DataCadastro],GETDATE()) " +
            "  ORDER BY [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].GESTOR  ";

            return this.dao.returnaDataTable(SQL);
        }

        public DataTable ObterAcessosEmAberto() 
        { 
            string SQL = " SELECT "+
		           "         [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].UF " +
	               "        ,[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].GESTOR " +
                   "        ,CASE WHEN DataExtracao IS NOT NULL THEN 'SIM' ELSE 'NÃO' END AS EXTRAIDO  " +
                   "        ,CONVERT(VARCHAR,MIN(DataCadastro),103) AS DataCadastro " +
                   "        ,DATEDIFF(DD,[DataCadastro],GETDATE()) as SLA " +
	               "        ,COUNT(*) AS TOTAL " +
                   "  FROM [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] LEFT JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS " +
                   "  ON ([Vivo_SIM].[dbo].[ACESSO_TERCEIROS].ADABAS = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.VENDEDOR) " +
                   "  WHERE DataExtracao IS NULL " +
                   "  AND [Vivo_SIM].[dbo].[ACESSO_TERCEIROS].[STATUS] <> 'REJEITADO' " +
                   "  GROUP BY " +
		           "         [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].UF " +
	               "        ,[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].GESTOR  " +
                   "        ,CASE WHEN DataExtracao IS NOT NULL THEN 'SIM' ELSE 'NÃO' END " +
                   "        ,DATEDIFF(DD,[DataCadastro],GETDATE()) " +
                   "  ORDER BY [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].GESTOR  ";

            return this.dao.returnaDataTable(SQL);
        }

        public bool cadastrarAcessosTerceiros(AcessoTerceiros acesso)
        {
            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[ACESSO_TERCEIROS] " +
                " ([Adabas] " +
                " ,[Acao] " +
                " ,[TipoAcesso] " +
                " ,[Nome] " +
                " ,[Sobrenome] " +
                " ,[Sexo] " +
                " ,[Cpf] " +
                " ,[Rg] " +
                " ,[OrgaoEmissor] " +
                " ,[DataNascimento] " +
                " ,[Rua] " +
                " ,[Numero] " +
                " ,[Complemento] " +
                " ,[Cidade] " +
                " ,[Bairro] " +
                " ,[Cep] " +
                " ,[Estado] " +
                " ,[Email] " +
                " ,[Telefone] " +
                " ,[Celular] " +
                " ,[Cnpj] " +
                " ,[NomeContato] " +
                " ,[SubGrupo] " +
                " ,[DataContratoInicio] " +
                " ,[DataContratoFim] " +
                " ,[Area] " +
                " ,[SubArea] " +
                " ,[Ddd] " +
                " ,[idAcesso] " +
                " ,[DataCadastro] " +
                " ,[Perfil] " +
                " ,[Matricula] " +
                " ,[Funcao] " +
                " ,[Status] " +
                " ,[DataFinalizacao] " +
                " ) " +
                " VALUES ( " +
                " '" + acesso.Parceiro.Vendedor + "'" +
                " ,'" + acesso.Acao + "'" +
                " ,'" + acesso.TipoTerceiro + "'" +
                " ,'" + acesso.Nome + "'" +
                " ,'" + acesso.Sobrenome + "'" +
                " ,'" + acesso.Sexo + "'" +
                " ,'" + acesso.Cpf + "'" +
                " ,'" + acesso.Rg + "'" +
                " ,'" + acesso.OrgaoEmissor + "'" +
                " ,CONVERT(DATETIME,'" + Convert.ToDateTime(acesso.DataNascimento) + "',103)" +
                " ,'" + acesso.Parceiro.Endereco + "'" +
                " ,'" + acesso.Parceiro.Numero + "'" +
                " ,'" + acesso.Parceiro.Complemento + "'" +
                " ,'" + acesso.Parceiro.Cidade + "'" +
                " ,'" + acesso.Parceiro.Bairro + "'" +
                " ,'" + acesso.Parceiro.Cep + "'" +
                " ,'" + acesso.Estado + "'" +
                " ,'" + acesso.Parceiro.EmailComercial + "'" +
                " ,'" + acesso.Parceiro.Telefone + "'" +
                " ,'" + acesso.Parceiro.Celular + "'" +
                " ,'" + acesso.Parceiro.Cnpj + "'" +
                " ,'" + acesso.Parceiro.Contato + "'" +
                " ,'" + acesso.SubGrupo + "'" +
                " ,CONVERT(DATETIME,'" + Convert.ToDateTime(acesso.DtAtividadeIni) + "',103)" +
                " ,CONVERT(DATETIME,'" + Convert.ToDateTime(acesso.DtAtividadeFim) + "',103)" +    
                " ,'" + acesso.Area + "'" +
                " ,'" + acesso.SubArea + "' " +
                " ,'" + acesso.Ddd + "' " +
                " ,1" +
                " ,GetDate() " +
                " ,'" + acesso.Perfil + "' " +
                " ,'" + acesso.Matricula + "' " +
                " ,'" + acesso.Funcao + "' " +
                " ,'CADASTRADO' " +
                " ,GetDate() " +
                    " ) ";

            return this.dao.ExecutarSql(SQL);

        }

    }
}
