using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class CadastroUsuarioRepositorio
    {
        DAO dao;

        public CadastroUsuarioRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public bool CadastrarTabelaAcesso(CadastroUsuario cadUsuario, Parceiro Parca)
        {

            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[ACESSO] (" +
               "[Login] ," +
               "[Senha] ," +
               "[Nome] ," +
               "[Email] ," +
               "[Regional] )" +
               "VALUES (" +
                           "'" + cadUsuario.Login + "'" +
                           ",Convert(varbinary(150),'" +  cadUsuario.Senha + "')" +
                           ",'" + cadUsuario.Nome + "'" +
                           ",'" + cadUsuario.Email + "'" +
                           ",'" + Parca.Regional + "'" +
                        ") ";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CadastrarTabelaAcessoPermissaoMenu(CadastroUsuario cadUsuario)
        {
            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU] " +
                       "( [idAcesso] " +
                       ",[TipoMenu] " +
                       ",[DescricaoMenu] " +
                       ",[TipoAcesso] )" +
                   "VALUES ( " +
                       "( SELECT idAcesso FROM [Vivo_SIM].[dbo].[ACESSO] WHERE Login = " + "'" + cadUsuario.Login + "')" +
                       ",' 0 '" +
                       ",'" + cadUsuario.Perfil + "'" +
                       ",'" + cadUsuario.TipoAcesso + "'" +
                  ") ";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CadastrarTabelaCadastroVivoSIM(CadastroUsuario cadUsuario, Parceiro Parca)
        {
            string SQL = " INSERT INTO [Vivo_GRC].[dbo].[CADASTRO_X_VIVO_SIM] " +
                           "( [LOGIN] ," +
                           " [UF] ," +
                           " [DDD] )" +
                           "VALUES ( " +
                           "'" + cadUsuario.Login + "'" +
                           ",'" + Parca.Uf + "'" +
                           ",'" + Parca.Ddd + "')";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CadastrarTabelaCadastroVivoSIMSellIn(CadastroUsuario cadUsuario, string DDD, string Uf)
        {
            string SQL = " INSERT INTO [Vivo_GRC].[dbo].[CADASTRO_X_VIVO_SIM] " +
                           "( [LOGIN] ," +
                           " [UF] ," +
                           " [DDD] )" +
                           "VALUES ( " +
                           "'" + cadUsuario.Login + "'" +
                           ",'" + Uf + "'" +
                           ",'" + DDD + "')";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CadastrarTabelaCadastroVivoSIM(CadastroUsuario cadUsuario, string DDD)
        {

            string SQL = " INSERT INTO [Vivo_GRC].[dbo].[CADASTRO_X_VIVO_SIM]" +
                         "( [LOGIN] ," +
                         " [UF] ," +
                         " [DDD] )" +
                         " VALUES ( " +
                         "'" + cadUsuario.Login + "'" +
                         ",(SELECT DISTINCT [UF] FROM Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] WHERE [DDD] = '" + DDD + "')" +
                         ",'" + DDD + "'" +
                         ") ";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public bool CadastrarTabelaCadastroSELLIN(CadastroUsuario cadastroUsuario, string Rede, Parceiro Parca)
        {
            string SQL = "INSERT INTO [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_SELL_IN]" +
                            "SELECT [CNPJ]" +
                                ",[DATA INICIO CONTRATO]" +
                                ",[DATA FIM CONTRATO]" +
                                ",[RAZÃO SOCIAL]" +
                                ",[NOME FANTASIA]" +
                                ",[INSCRIÇÃO ESTADUAL]" +
                                ",[OPTANTE SIMPLES]" +
                                ",[REGIONAL]" +
                                ",[TERRITORIAL]" +
                                ",[CEP]" +
                                ",[ENDEREÇO]" +
                                ",[BAIRRO ]" +
                                ",[CIDADE]" +
                                ",[NUMERO]" +
                                ",[COMPLEMENTO]" +
                                ",[CONTATO]" +
                                ",[TELEFONE]" +
                                ",[CELULAR]" +
                                ",[FAX]" +
                                ",[E-MAIL FINANCEIRO]" +
                                ",[E-MAIL COMERCIAL]" +
                                ",[E-MAIL DIVULGAÇÃO]" +
                                ",[HOME PAGE]" +
                                ",[CODIGO BANCO]" +
                                ",[AGENCIA]" +
                                ",[NUMERO CONTA]" +
                                ",[DIGITO VERIFICADOR]" +
                                ",[REGIÃO GEOGRAFICA]" +
                                ",[SETOR ATIVIDADE]" +
                                ",[ÁREA DE CREDITO]" +
                                ",[ORGANIZAÇÃO DE COMPRAS]" +
                                ",[ORGANIZAÇÃO DE VENDAS]" +
                                ",[GRUPO VENDEDOR/NOME GC]" +
                                ",[PAGADOR FATURA]" +
                                ", '1' " +
                                ",[RECEBEDOR FATURA]" +
                                ",[CODIGO VENDEDOR]" +
                                ",[REGIONAL2]" +
                                ",[TERRITORIAL2]" +
                                ",[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].[UF]" +
                                ",[RECEBEDOR DO COMISSIONAMENTO]" +
                                ",[PONTO DE VENDA]" +
                                ",'" + cadastroUsuario.Nome + "'" +
                                ",[DATA_CADASTRO_GESTOR]" +
                                ",[SELL IN]" +
                                ",[DATA_CADASTRO_SELL_IN]" +
                                ",[STATUS CALLIDUS]" +
                                ",[INSTANCIA]" +
                                ",[ATIVIDADE]" +
                                ",[LIMITE]" +
                                ",[PRAZO LIMITE]" +
                                ",[MARGEM APARELHO]" +
                                ",[MARGEM CHIP]" +
                                ",[MARGEM  RECARGA]" +
                                ",[CONDIÇÕES DE PAGAMENTO]" +
                                ",[VPC (Sell in ou Out)]" +
                                ",[ATUAL ESTRELAGEM]" +
                                ",[DATA_EFETI_CART_SELL_IN]" +
                                ",[DATA_EFETI_CART_GESTOR]" +
                                ",[CANAL DISTRIBUIÇÃO]" +
                                ",[VENDEDOR]" +
                                ",'" + Rede + "'" +
                                ",[CANAL]" +
                                ",[CÓDIGO CLIENTE]" +
                                ",[CÓDIGO FORNECEDOR]" +
                                ",'" + cadastroUsuario.Login + "'" +
                                ",[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].[COD_IBGE]" +
                                ",[UF_LOCALIDADE_PDV]" +
                                ",[IXOS]" +
                                ",[DDD GESTOR]" +
                                ",[AREA ATUAÇÃO DDD GESTOR]" +
                                ",[GERENTE TERRITORIAL]" +
                            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
                            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE)" +
                            " WHERE [CANAL] = '" + Parca.Canal + "'" +
                                    "AND [DDD] = '" + Parca.Ddd + "'";

            if (Parca.Canal.Equals("Varejo"))
            {
                SQL = SQL + " AND [REDE] = '" + Rede + "'";
            }
            else if (Parca.Canal.Equals("Loja Própria"))
            {
                SQL = SQL + " AND [NOME FANTASIA] = '" + Rede + "'";
            }

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CadastrarTabelaCadastroVivoSIMCanal(CadastroUsuario cadUsuario, Parceiro Parca)
        {
            string SQL = "INSERT INTO VIVO_GRC.dbo.CADASTRO_X_VIVO_SIM_X_CANAL " +
                           "([LOGIN] ," +
                           " [CANAL])" +
                               "VALUES " +
                                   "('" + cadUsuario.Login + "'" +
                                   ", '" + Parca.Canal + "')";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CadastrarTabelaCadastroVivoSIMCanalTodosCanais(CadastroUsuario cadUsuario)
        {
            string SQL = " INSERT INTO VIVO_GRC.dbo.CADASTRO_X_VIVO_SIM_X_CANAL " +
                         "   SELECT DISTINCT '" + cadUsuario.Login + "' , [CANAL] " +
                         "   FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
                         "   WHERE [CANAL] NOT IN ('Call Center','Alternativos Pj')";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CadastraTodosDddUfs(CadastroUsuario cadUsuario)
        {
            string SQL = " INSERT INTO [VIVO_GRC].[dbo].[CADASTRO_X_VIVO_SIM] " +
                         "  SELECT DISTINCT '" + cadUsuario.Login + "' " +
                         "     ,[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].[UF] " +
                         "     ,[DDD] " +
                         "  FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
                         "  LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
                         "  ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
                         "  WHERE DDD IS NOT NULL ";


            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AlteraTabelaAcesso(CadastroUsuario cadUsuario, Parceiro Parc)
        {
            string SQL = "UPDATE [Vivo_SIM].[dbo].[ACESSO] " +
                            " SET SENHA = Convert(varbinary(255),'" + cadUsuario.Senha + "') " +
                            ", NOME = '" + cadUsuario.Nome + "'" +
                            ", EMAIL = '" + cadUsuario.Email + "'" +
                            ", REGIONAL = '" + Parc.Regional + "'" +
                         " WHERE LOGIN = '" + cadUsuario.Login + "'";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AlteraTabelaAcessoPermissaoMenu(CadastroUsuario cadUsuario)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU] " +
                                " SET DescricaoMenu = '" + cadUsuario.Perfil +
                            "' ( SELECT idAcesso FROM [Vivo_SIM].[dbo].[ACESSO] " +
                                "WHERE Login = " + "'" + cadUsuario.Login + "')";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AlteraTabelaCadastroVivoSIM(CadastroUsuario cadUsuario, Parceiro Parca)
        {
            string SQL = " UPDATE [Vivo_GRC].[dbo].[CADASTRO_X_VIVO_SIM] " +
                                " SET UF = '" + Parca.Uf + "'" +
                                " ,DDD = '" + Parca.Ddd + "'" +
                            " WHERE LOGIN = '" + cadUsuario.Login + "'";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletaTabelaCadastroVivoSIM(CadastroUsuario cadUsuario)
        {
            string SQL = " DELETE FROM [Vivo_GRC].[dbo].[CADASTRO_X_VIVO_SIM] " +
                            " WHERE LOGIN = '" + cadUsuario.Login + "'";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletaTabelaCadastroVivoSIMCanal(CadastroUsuario cadUsuario)
        {
            string SQL = " DELETE FROM [Vivo_GRC].[dbo].[CADASTRO_X_VIVO_SIM_X_CANAL] " +
                            " WHERE LOGIN = '" + cadUsuario.Login + "'";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AlteraTabelaCadastroVivoSIMCanal(CadastroUsuario cadUsuario, Parceiro Parca)
        {
            string SQL = "UPDATE VIVO_GRC.dbo.CADASTRO_X_VIVO_SIM_X_CANAL" +
                            " SET CANAL = '" + Parca.Canal + "'" +
                            " WHERE LOGIN = '" + cadUsuario.Login + "'";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeletarTabelaCadastroSELLIN(CadastroUsuario cadUsuario)
        {

            string SQL = "DELETE FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_SELL_IN]" +
                            "WHERE [MATRICULA_GER_CONTA] = '" + cadUsuario.Login + "'";

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CadastroUsuario obterDadosUsuario(string Login)
        {
            string SQL = "SELECT DISTINCT ACESSO.[Nome] " +
                                ",ACESSO.[Email] " +
                                ",ACESSO_PERMISSAO_MENU.[TipoAcesso] " +
                                ",ACESSO_PERMISSAO_MENU.[DescricaoMenu] " +
                                ",CADASTRO_X_VIVO_SIM.[UF]  " +
                                ",CADASTRO_X_VIVO_SIM.[DDD] " +
                                ",TAB_CADASTRO_GERAL_PDVS_SELL_IN.[NOME FANTASIA] " +
                          "FROM [VIVO_SIM].[dbo].[ACESSO] AS ACESSO " +
                            "LEFT JOIN [VIVO_SIM].[dbo].[ACESSO_PERMISSAO_MENU] AS ACESSO_PERMISSAO_MENU ON ACESSO.[idAcesso] = ACESSO_PERMISSAO_MENU.[idAcesso] " +
                            "LEFT JOIN [VIVO_GRC].[dbo].[CADASTRO_X_VIVO_SIM] AS CADASTRO_X_VIVO_SIM ON ACESSO.[Login] = CADASTRO_X_VIVO_SIM.[LOGIN] " +
                            "LEFT JOIN [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_SELL_IN] AS TAB_CADASTRO_GERAL_PDVS_SELL_IN ON ACESSO.[Login] = TAB_CADASTRO_GERAL_PDVS_SELL_IN.[MATRICULA_GER_CONTA] " +
                            "LEFT JOIN [VIVO_GRC].[dbo].[CADASTRO_X_VIVO_SIM_X_CANAL] AS TAB_CADASTRO_X_VIVO_SIM_X_CANAL ON ACESSO.[Login] = TAB_CADASTRO_X_VIVO_SIM_X_CANAL.[LOGIN] " +
                          "WHERE ACESSO.[Login] = '" + Login + "' ";

            DataTable dsColaborador = null;
            CadastroUsuario retorno = new CadastroUsuario();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Uf = Convert.ToString(item[4]);
                    p.Ddd = Convert.ToString(item[5]);
                    p.Canal = Convert.ToString(item[6]);

                    retorno.Nome = Convert.ToString(item[0]);
                    retorno.Email = Convert.ToString(item[1]);
                    retorno.TipoAcesso = Convert.ToString(item[2]);
                    retorno.Perfil = Convert.ToString(item[3]);
                    //retorno.Parceiro = p;


                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        public List<string> buscarRedePorUsuario(string Login)
        {
            string SQL = "SELECT DISTINCT [REDE] FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_SELL_IN] WHERE [MATRICULA_GER_CONTA] = '" + Login + "'";

            DataTable dsColaborador = null;
            List<string> retorno = new List<string>();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    retorno.Add(Convert.ToString(item[0]));
                }
                return retorno;
            }
            else
            {
                return null;
            }
        }

        public List<string> buscarListaDDDUsuario(string Login)
        {
            string SQL = "SELECT [DDD] FROM  [VIVO_GRC].[dbo].[CADASTRO_X_VIVO_SIM] WHERE [LOGIN] = '" + Login + "'";

            DataTable dsColaborador = null;
            List<string> retorno = new List<string>();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    retorno.Add(Convert.ToString(item[0]));
                }
                return retorno;
            }
            else
            {
                return null;
            }
        }


        public List<string> buscarDDDPorUsuario(string Login)
        {
            string SQL = "SELECT [DDD] FROM [Vivo_GRC].[dbo].[CADASTRO_X_VIVO_SIM] WHERE Login = '" + Login + "'";

            DataTable dsColaborador = null;
            List<string> retorno = new List<string>();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    retorno.Add(Convert.ToString(item[0]));
                }
                return retorno;
            }
            else
            {
                return null;
            }
        }

        //public List<CadastroUsuario> buscarUF(String UF)
        //{
        //    string SQL = "SELECT DISTINCT UF FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS]";

        //    DataTable dsColaborador = null;
        //    List<CadastroUsuario> retorno = new List<CadastroUsuario>();

        //    dsColaborador = this.dao.returnaDataTable(SQL);

        //    if (dsColaborador.Rows.Count > 0)
        //    {
        //        foreach (DataRow item in dsColaborador.Rows)
        //        {
        //            Parceiro p = new Parceiro();
        //            p.Uf = Convert.ToString(item[0]);

        //            CadastroUsuario cu = new CadastroUsuario();
        //            cu.Parceiro = p;

        //            retorno.Add(cu);
        //        }

        //        return retorno;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public List<Parceiro> buscarLoja(String DDD)
        {
            string SQL = " SELECT DISTINCT [NOME FANTASIA]  " +
                            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
                            " LEFT JOIN  Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] ON   " +
                            " ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
                            " WHERE Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].[DDD] = '" + DDD + "' " +
                            " AND [CANAL] = 'Loja Própria' ";

            DataTable dsColaborador = null;
            List<Parceiro> retorno = new List<Parceiro>();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.NomeFantasia = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return null;
            }
        }
        //ListaCanalUsuario

        public List<string> buscarCanais()
        {
            string SQL = " SELECT DISTINCT CANAL FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] ";

            DataTable dsColaborador = null;
            List<string> retorno = new List<string>();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    retorno.Add(Convert.ToString(item[0]));
                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        public List<Parceiro> buscarUF()
        {
            string SQL = "SELECT DISTINCT[UF] from [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] ORDER BY UF";

            DataTable dsColaborador = null;
            List<Parceiro> retorno = new List<Parceiro>();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Uf = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        public List<string> ObterParceirosRedePorDDD(string Login, string Ddd)
        {
            string SQL = " SELECT DISTINCT [REDE] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].[DDD] = '" + Ddd + "'" +
            " AND REDE NOT IN ( " +
            "	            SELECT DISTINCT [REDE] FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_SELL_IN] WHERE [MATRICULA_GER_CONTA] = '" + Login + "'" +
            "	) ";

            DataTable dsResetSenha = null;
            List<string> retorno = new List<string>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    retorno.Add(Convert.ToString(item[0]));
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