using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;


namespace VivoMais.Repositorio.Repositorio
{
    public class CarteiraRepositorio
    {
        DAO dao;

        public CarteiraRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public DataTable ObterGraficoCarteira() 
        {
            string SQL = "SELECT " +
             "       DDD " +
             "      ,CASE WHEN NOVO_GESTOR IS NULL THEN COUNT(*) ELSE 0 END AS FALTANTES " +
             "      ,CASE WHEN NOVO_GESTOR IS NOT NULL THEN COUNT(*) ELSE 0 END AS PREENCHIDOS " +
             " FROM [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
             " ON ([Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
             " WHERE DDD IS NOT NULL " +
             " GROUP BY " +
             "       DDD " +
             "      ,NOVO_GESTOR " +
             " ORDER BY DDD ";

            return dao.returnaDataTable(SQL);
        }

        public bool VerificaGcRevenda(string Adabas) 
        {
            string SQL = " SELECT * FROM [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] " +
                         " WHERE [VENDEDOR] = '" + Adabas.Substring(0, 11) + "' ";
                         //" AND [DATA_NOVO_GESTOR] IS NULL ";

            DataTable dt = dao.returnaDataTable(SQL);
            if(dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool AtualizarCarteiraMesPDR(string Adabas, string Territorial, string NovoGc, string Obs)
        {
            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] " +
                         " SELECT [CNPJ] " +
                         "       ,[DATA INICIO CONTRATO] " +
                         "       ,[DATA FIM CONTRATO] " +
                         "       ,[RAZÃO SOCIAL] " +
                         "       ,[NOME FANTASIA] " +
                         "       ,[INSCRIÇÃO ESTADUAL] " +
                         "       ,[OPTANTE SIMPLES] " +
                         "       ,[REGIONAL] " +
                         "       ,[TERRITORIAL] " +
                         "       ,[CEP] " +
                         "       ,[ENDEREÇO] " +
                         "       ,[BAIRRO ] " +
                         "       ,[CIDADE] " +
                         "       ,[NUMERO] " +
                         "       ,[COMPLEMENTO] " +
                         "       ,[CONTATO] " +
                         "       ,[TELEFONE] " +
                         "       ,[CELULAR] " +
                         "       ,[FAX] " +
                         "       ,[E-MAIL FINANCEIRO] " +
                         "       ,[E-MAIL COMERCIAL] " +
                         "       ,[E-MAIL DIVULGAÇÃO] " +
                         "       ,[HOME PAGE] " +
                         "       ,[CODIGO BANCO] " +
                         "       ,[AGENCIA] " +
                         "       ,[NUMERO CONTA] " +
                         "       ,[DIGITO VERIFICADOR] " +
                         "       ,[REGIÃO GEOGRAFICA] " +
                         "       ,[SETOR ATIVIDADE] " +
                         "       ,[ÁREA DE CREDITO] " +
                         "       ,[ORGANIZAÇÃO DE COMPRAS] " +
                         "       ,[ORGANIZAÇÃO DE VENDAS] " +
                         "       ,[GRUPO VENDEDOR/NOME GC] " +
                         "       ,[PAGADOR FATURA] " +
                         "       ,[RECEBEDOR DA MERCADORIA] " +
                         "       ,[RECEBEDOR FATURA] " +
                         "       ,[CODIGO VENDEDOR] " +
                         "       ,[REGIONAL2] " +
                         "       ,[TERRITORIAL2] " +
                         "       ,[UF] " +
                         "       ,[RECEBEDOR DO COMISSIONAMENTO] " +
                         "       ,[PONTO DE VENDA] " +
                         "       ,[GESTOR] " +
                         "       ,[DATA_CADASTRO_GESTOR] " +
                         "       ,[SELL IN] " +
                         "       ,[DATA_CADASTRO_SELL_IN] " +
                         "       ,[STATUS CALLIDUS] " +
                         "       ,[INSTANCIA] " +
                         "       ,[ATIVIDADE] " +
                         "       ,[LIMITE] " +
                         "       ,[PRAZO LIMITE] " +
                         "       ,[MARGEM APARELHO] " +
                         "       ,[MARGEM CHIP] " +
                         "       ,[MARGEM  RECARGA] " +
                         "       ,[CONDIÇÕES DE PAGAMENTO] " +
                         "       ,[VPC (Sell in ou Out)] " +
                         "       ,[ATUAL ESTRELAGEM] " +
                         "       ,[DATA_EFETI_CART_SELL_IN] " +
                         "       ,[DATA_EFETI_CART_GESTOR] " +
                         "       ,[CANAL DISTRIBUIÇÃO] " +
                         "       ,'" + Adabas+ "'" +
                         "       ,[REDE] " +
                         "       ,[CANAL] " +
                         "       ,[CÓDIGO CLIENTE] " +
                         "       ,[CÓDIGO FORNECEDOR] " +
                         "       ,[MATRICULA_GER_CONTA] " +
                         "       ,[COD_IBGE] " +
                         "       ,[UF_LOCALIDADE_PDV] " +
                         "       ,[IXOS] " +
                         "       ,[DDD GESTOR] " +
                         "       ,[AREA ATUAÇÃO DDD GESTOR] " +
                         "       ,'" + Territorial  + "'" + 
                         "       ,'" + NovoGc + "'" + 
                         "       ,GETDATE() " +
                         "       ,'" + Obs + "'" + 
                         "       ,(SELECT DISTINCT MES_CARTEIRA FROM [Vivo_Criterios].[dbo].[LIBERA_CARTEIRA_TERRITORIAL]) " +
                         " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
                         " WHERE [VENDEDOR] = '" + Adabas.Substring(0, 11) + "' ";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizarCarteiraMes(string Adabas, string Territorial, string NovoGc, string Obs)
        { 
            string SQL = " UPDATE [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] " +
                " SET [NOVO_GESTOR] = '" + NovoGc + "' " +
                " ,[GERENTE TERRITORIAL] = '" + Territorial + "' " +
                " ,[DATA_NOVO_GESTOR] = GETDATE() " +
                " ,[OBS] = '" + Obs + "' " +
                " ,[MES_LIBERACAO] = (SELECT DISTINCT MES_CARTEIRA FROM [Vivo_Criterios].[dbo].[LIBERA_CARTEIRA_TERRITORIAL]) " +
                " WHERE [VENDEDOR] = '" + Adabas + "' ";

            return dao.ExecutarSql(SQL);
        }

        public bool AlterarIntervaloCarteira(DateTime DataDE, DateTime DataATE, string MesCarteira)
        {
            string SQL = " UPDATE [Vivo_Criterios].[dbo].[LIBERA_CARTEIRA_TERRITORIAL] " +
            "  SET [DATA_DE] = CONVERT(DATETIME,'" + DataDE + "',103), " +
            "      [DATA_ATE] = CONVERT(DATETIME,'" + DataATE + "',103),  " +
            "      [MES_CARTEIRA] = '" + MesCarteira + "',  " + 
            "      [DATA] = GETDATE() ";

            return dao.ExecutarSql(SQL);
        }

        public bool AtualizaCarteira(string MesCarteira)
        {
            string SQL = " DELETE FROM [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] " +

            "  INSERT INTO [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] " +
            "  SELECT [CNPJ] " +
            "  ,[DATA INICIO CONTRATO] " +
            "  ,[DATA FIM CONTRATO] " +
            "  ,[RAZÃO SOCIAL] " +
            "  ,[NOME FANTASIA] " +
            "  ,[INSCRIÇÃO ESTADUAL] " +
            "  ,[OPTANTE SIMPLES] " +
            "  ,[REGIONAL] " +
            "  ,[TERRITORIAL] " +
            "  ,[CEP] " +
            "  ,[ENDEREÇO] " +
            "  ,[BAIRRO ] " +
            "  ,[CIDADE] " +
            "  ,[NUMERO] " +
            "  ,[COMPLEMENTO] " +
            "  ,[CONTATO] " +
            "  ,[TELEFONE] " +
            "  ,[CELULAR] " +
            "  ,[FAX] " +
            "  ,[E-MAIL FINANCEIRO] " +
            "  ,[E-MAIL COMERCIAL] " +
            "  ,[E-MAIL DIVULGAÇÃO] " +
            "  ,[HOME PAGE] " +
            "  ,[CODIGO BANCO] " +
            "  ,[AGENCIA] " +
            "  ,[NUMERO CONTA] " +
            "  ,[DIGITO VERIFICADOR] " +
            "  ,[REGIÃO GEOGRAFICA] " +
            "  ,[SETOR ATIVIDADE] " +
            "  ,[ÁREA DE CREDITO] " +
            "  ,[ORGANIZAÇÃO DE COMPRAS] " +
            "  ,[ORGANIZAÇÃO DE VENDAS] " +
            "  ,[GRUPO VENDEDOR/NOME GC] " +
            "  ,[PAGADOR FATURA] " +
            "  ,[RECEBEDOR DA MERCADORIA] " +
            "  ,[RECEBEDOR FATURA] " +
            "  ,[CODIGO VENDEDOR] " +
            "  ,[REGIONAL2] " +
            "  ,[TERRITORIAL2] " +
            "  ,[UF] " +
            "  ,[RECEBEDOR DO COMISSIONAMENTO] " +
            "  ,[PONTO DE VENDA] " +
            "  ,[GESTOR] " +
            "  ,[DATA_CADASTRO_GESTOR] " +
            "  ,[SELL IN] " +
            "  ,[DATA_CADASTRO_SELL_IN] " +
            "  ,[STATUS CALLIDUS] " +
            "  ,[INSTANCIA] " +
            "  ,[ATIVIDADE] " +
            "  ,[LIMITE] " +
            "  ,[PRAZO LIMITE] " +
            "  ,[MARGEM APARELHO] " +
            "  ,[MARGEM CHIP] " +
            "  ,[MARGEM  RECARGA] " +
            "  ,[CONDIÇÕES DE PAGAMENTO] " +
            "  ,[VPC (Sell in ou Out)] " +
            "  ,[ATUAL ESTRELAGEM] " +
            "  ,[DATA_EFETI_CART_SELL_IN] " +
            "  ,[DATA_EFETI_CART_GESTOR] " +
            "  ,[CANAL DISTRIBUIÇÃO] " +
            "  ,[VENDEDOR] " +
            "  ,[REDE] " +
            "  ,[CANAL] " +
            "  ,[CÓDIGO CLIENTE] " +
            "  ,[CÓDIGO FORNECEDOR] " +
            "  ,[MATRICULA_GER_CONTA] " +
            "  ,[COD_IBGE] " +
            "  ,[UF_LOCALIDADE_PDV] " +
            "  ,NULL " +
            "  ,NULL " +
            "  ,NULL " +
            "  ,'" + MesCarteira + "' " +
            "  FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            "  WHERE CANAL IN ('ALTERNATIVOS PF','RECARGA','REVENDA','VAREJO','Loja Própria','Loja Autorizada') ";

            return dao.ExecutarSql(SQL);
        }

        public DataTable ConsultarCarteira(string Matricula)
        {
            string SQL = "SELECT [VENDEDOR] " +
            "      ,[UF] " +
            "      ,[DDD] " +
            "      ,[RAZÃO SOCIAL] " +
            "      ,[REDE] " +
            "      ,[CNPJ] " +
            "      ,UPPER(CANAL) AS [CANAL] " +
            "      ,UPPER(CIDADE) AS CIDADE " +
            "      ,(Select CONVERT(VARCHAR(255),UPPER(GESTOR)) collate sql_latin1_general_cp1251_ci_as) AS GESTOR " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento')" +
            " AND MATRICULA_GER_CONTA = '" + Matricula + "'";

            return dao.returnaDataTable(SQL);
        }

        public List<Parceiro> RetornarCarteiras(string Ddd, string Canal, string TipoCarteira, string GerenteContas)
        {
            string SQL = "SELECT DISTINCT " +
            "  VENDEDOR " +
            " ,DDD " +
            " ,[RAZÃO SOCIAL] " +
            " ,[CNPJ] " +
            " ,UPPER([CANAL]) AS [CANAL] " +
            " ,UPPER([GESTOR]) AS [GESTOR]  " +
            " ,UPPER([CIDADE]) AS [CIDADE] " +
            " ,CASE WHEN UPPER([GERENTE TERRITORIAL]) IS NULL THEN UPPER(GESTOR) ELSE [GERENTE TERRITORIAL] END AS [GERENTE TERRITORIAL] " +
            " ,CASE WHEN UPPER(NOVO_GESTOR) IS NULL THEN UPPER(GESTOR) ELSE [NOVO_GESTOR] END AS [NOVO_GESTOR] " +
            " ,UPPER([OBS]) AS [OBS] " +
            " FROM [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND GESTOR IS NOT NULL " +
            " AND GESTOR NOT LIKE '%SEM GC%' " +
            " AND GESTOR <> 'NÃO POSSUI GERENTE CONTA'" + 
            " AND DDD = '" + Ddd + "' ";

            if (GerenteContas != "TODOS")
            {
                SQL = SQL + " AND GESTOR = '" + GerenteContas + "' ";
            }

            if(Canal != "TODOS")
            {
                SQL = SQL + " AND CANAL = '" + Canal + "' ";
            }

            if (TipoCarteira != "TODOS")
            {
                if (TipoCarteira == "FIXO FIXA")
                {
                    SQL = SQL + " AND ATIVIDADE = 'PROJETO LAURA' ";
                }
                else
                {
                    SQL = SQL + " AND ATIVIDADE <> 'PROJETO LAURA' ";
                }
                
            }

            SQL = SQL + " ORDER BY GESTOR,[RAZÃO SOCIAL] ";

            DataTable dsParceiro = null;
            List<Parceiro> retorno = new List<Parceiro>();

            dsParceiro = this.dao.returnaDataTable(SQL);

            if (dsParceiro.Rows.Count > 0)
            {
                foreach (DataRow item in dsParceiro.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Vendedor = item[0].ToString();
                    p.Ddd = item[1].ToString();
                    p.RazaoSocial = item[2].ToString();
                    p.Cnpj = item[3].ToString();
                    p.Canal = item[4].ToString();
                    p.Gestor = item[5].ToString();
                    p.Cidade = item[6].ToString();
                    p.GerenteTerritorial = item[7].ToString();
                    p.NovoGestor = item[8].ToString();
                    p.Observacao = item[9].ToString();

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }
    }
}
