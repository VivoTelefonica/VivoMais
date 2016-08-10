using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class ParceiroRepositorio
    {
        DAO dao;

        public ParceiroRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public List<Parceiro> ObterParceiros()
        {
            string SQL = " SELECT [CNPJ] " +
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
            "  ,[Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL].[UF] " +
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
            "  ,UPPER(CANAL) AS [CANAL] " +
            "  ,[CÓDIGO CLIENTE] " +
            "  ,[CÓDIGO FORNECEDOR] " +
            "  ,[MATRICULA_GER_CONTA] " +
            "  ,[Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL].[COD_IBGE] " +
            "  ,[DDD] " +
            "  ,[TELEFONE] " +
            "  ,[UF_LOCALIDADE_PDV] " +
            "  ,[IXOS] " +
            "  ,[DDD GESTOR] " +
            "  ,[AREA ATUAÇÃO DDD GESTOR] " +
            "  ,[GERENTE TERRITORIAL] " +
            "  ,[NOVO_GESTOR] " +
            "  ,[OBS] " +
            " FROM [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') ";


            DataTable dsResetSenha = null;
            List<Parceiro> retorno = new List<Parceiro>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Cnpj = Convert.ToString(item[0]);

                    if (item[1].ToString() != "")
                    {
                        p.DataInicioContrato = Convert.ToDateTime(item[1]);
                    }

                    if (item[2].ToString() != "")
                    {
                        p.DataFimContrato = Convert.ToDateTime(item[2]);
                    }



                    p.RazaoSocial = Convert.ToString(item[3]);
                    p.NomeFantasia = Convert.ToString(item[4]);
                    p.InscricaoEstadual = Convert.ToString(item[5]);
                    p.OptanteSimples = Convert.ToString(item[6]);
                    p.Regional = Convert.ToString(item[7]);
                    p.Territorial = Convert.ToString(item[8]);
                    p.Cep = Convert.ToString(item[9]);
                    p.Endereco = Convert.ToString(item[10]);
                    p.Bairro = Convert.ToString(item[11]);
                    p.Cidade = Convert.ToString(item[12]);
                    p.Numero = Convert.ToString(item[13]);
                    p.Complemento = Convert.ToString(item[14]);
                    p.Contato = Convert.ToString(item[15]);
                    p.Celular = Convert.ToString(item[16]);
                    p.Fax = Convert.ToString(item[17]);
                    p.EmailFinanceiro = Convert.ToString(item[18]);
                    p.EmailComercial = Convert.ToString(item[19]);
                    p.EmailDivulgacao = Convert.ToString(item[20]);
                    p.HomePage = Convert.ToString(item[21]);
                    p.CodigoBanco = Convert.ToString(item[22]);
                    p.Agencia = Convert.ToString(item[23]);
                    p.NumeroConta = Convert.ToString(item[24]);
                    p.DigitoVerificador = Convert.ToString(item[25]);
                    p.RegiaoGeografica = Convert.ToString(item[26]);
                    p.SetorAtividade = Convert.ToString(item[27]);
                    p.AreaCredito = Convert.ToString(item[28]);
                    p.OrganizacaoCompras = Convert.ToString(item[29]);
                    p.OrganizacaoVendas = Convert.ToString(item[30]);
                    p.GrupoVendedor = Convert.ToString(item[31]);
                    p.PagadorFatura = Convert.ToString(item[32]);
                    p.RecebedorMercadoria = Convert.ToString(item[33]);
                    p.RecebedorFatura = Convert.ToString(item[34]);
                    p.CodigoVendedor = Convert.ToString(item[35]);
                    p.Regional2 = Convert.ToString(item[36]);
                    p.Territorial2 = Convert.ToString(item[37]);
                    p.Uf = Convert.ToString(item[38]);
                    p.RecebedorComissionamento = Convert.ToString(item[39]);
                    p.PontoVenda = Convert.ToString(item[40]);
                    p.Gestor = Convert.ToString(item[41]);

                    if (item[42].ToString() != "")
                    {
                        p.DataCadastroGestor = Convert.ToDateTime(item[42]);
                    }

                    p.SellIn = Convert.ToString(item[43]);

                    if (item[44].ToString() != "")
                    {
                        p.DataCadastroSellIn = Convert.ToDateTime(item[44]);
                    }


                    p.StatusCallidus = Convert.ToString(item[45]);
                    p.Instancia = Convert.ToString(item[46]);
                    p.Atividade = Convert.ToString(item[47]);
                    p.Limite = Convert.ToString(item[48]);
                    p.PrazoLimite = Convert.ToString(item[49]);
                    p.MargemAparelho = Convert.ToString(item[50]);
                    p.MargemChip = Convert.ToString(item[51]);
                    p.MargemRecarga = Convert.ToString(item[52]);
                    p.CondicoesPagamento = Convert.ToString(item[53]);
                    p.Vpc = Convert.ToString(item[54]);
                    p.AtualEstrelagem = Convert.ToString(item[55]);

                    if (item[56].ToString() != "")
                    {
                        p.DataCartSellIn = Convert.ToDateTime(item[56]);
                    }

                    if (item[57].ToString() != "")
                    {
                        p.DataCartGestor = Convert.ToDateTime(item[57]);
                    }

                    p.CanalDistribuicao = Convert.ToString(item[58]);
                    p.Vendedor = Convert.ToString(item[59]);
                    p.Rede = Convert.ToString(item[60]);
                    p.Canal = Convert.ToString(item[61]);
                    p.CodigoCliente = Convert.ToString(item[62]);
                    p.CodigoFornecedor = Convert.ToString(item[63]);
                    p.MatriculaGerenteConta = Convert.ToString(item[64]);
                    p.CodIbge = Convert.ToString(item[65]);
                    p.Ddd = Convert.ToString(item[66]);
                    p.Telefone = Convert.ToString(item[67]);
                    p.UfLocalidadePdv = Convert.ToString(item[68]);
                    p.Ixos = Convert.ToString(item[69]);
                    p.DddGestor = Convert.ToString(item[70]);
                    p.AreaAtuacaoDddGestor = Convert.ToString(item[71]);
                    p.GerenteTerritorial = Convert.ToString(item[72]);
                    p.NovoGestor = Convert.ToString(item[73]);
                    p.Observacao = Convert.ToString(item[74]);

                    retorno.Add(p);

                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<Parceiro> ObterParceiros(string Login)
        {
            string SQL = " SELECT DISTINCT [CNPJ] " +
            " ,[RAZÃO SOCIAL] " +
            " ,[NOME FANTASIA] " +
            " ,[REGIONAL] " +
            " ,[TERRITORIAL] " +
            " ,[CEP] " +
            " ,[ENDEREÇO] " +
            " ,[BAIRRO ] " +
            " ,[CIDADE] " +
            " ,[NUMERO] " +
            " ,[COMPLEMENTO] " +
            " ,[CONTATO] " +
            " ,[CELULAR] " +
            " ,[E-MAIL COMERCIAL] " +
            " ,[E-MAIL DIVULGAÇÃO] " +
            " ,[CODIGO VENDEDOR] " +
            " ,[UF] " +
            " ,[GESTOR] " +
            " ,[SELL IN] " +
            " ,[STATUS CALLIDUS] " +
            " ,[VENDEDOR] " +
            " ,[REDE] " +
            " ,UPPER(CANAL) AS [CANAL] " +
            " ,[CÓDIGO CLIENTE] " +
            " ,[CÓDIGO FORNECEDOR] " +
            " ,[MATRICULA_GER_CONTA] " +
            " ,[COD_IBGE] " +
            " ,[DDD] " +
            " ,[IXOS] " +
            " ,[DDD GESTOR] " +
            " ,[AREA ATUAÇÃO DDD GESTOR] " +
            " ,[GERENTE TERRITORIAL] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [MATRICULA_GER_CONTA] = '" + Login + "' " +
            " ORDER BY [VENDEDOR] ";

            DataTable dsResetSenha = null;
            List<Parceiro> retorno = new List<Parceiro>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    Parceiro p = new Parceiro();

                    p.Cnpj = Convert.ToString(item[0]);
                    p.RazaoSocial = Convert.ToString(item[1]);
                    p.NomeFantasia = Convert.ToString(item[2]);
                    p.Regional = Convert.ToString(item[3]);
                    p.Territorial = Convert.ToString(item[4]);
                    p.Cep = Convert.ToString(item[5]);
                    p.Endereco = Convert.ToString(item[6]);
                    p.Bairro = Convert.ToString(item[7]);
                    p.Cidade = Convert.ToString(item[8]);
                    p.Numero = Convert.ToString(item[9]);
                    p.Complemento = Convert.ToString(item[10]);
                    p.Contato = Convert.ToString(item[11]);
                    p.Celular = Convert.ToString(item[12]);
                    p.EmailComercial = Convert.ToString(item[13]);
                    p.EmailDivulgacao = Convert.ToString(item[14]);
                    p.CodigoVendedor = Convert.ToString(item[15]);
                    p.Uf = Convert.ToString(item[16]);
                    p.Gestor = Convert.ToString(item[17]);
                    p.SellIn = Convert.ToString(item[18]);
                    p.StatusCallidus = Convert.ToString(item[19]);
                    p.Vendedor = Convert.ToString(item[20]);
                    p.Rede = Convert.ToString(item[21]);
                    p.Canal = Convert.ToString(item[22]);
                    p.CodigoCliente = Convert.ToString(item[23]);
                    p.CodigoFornecedor = Convert.ToString(item[24]);
                    p.MatriculaGerenteConta = Convert.ToString(item[25]);
                    p.CodIbge = Convert.ToString(item[26]);
                    p.Ddd = Convert.ToString(item[27]);

                    if (item[28].ToString() == "")
                    {
                        p.Ixos = retornarIxos(Convert.ToString(item[20]));
                    }
                    else
                    {
                        p.Ixos = Convert.ToString(item[28]);
                    }

                    p.DddGestor = Convert.ToString(item[29]);
                    p.AreaAtuacaoDddGestor = Convert.ToString(item[30]);
                    p.GerenteTerritorial = Convert.ToString(item[31]);


                    retorno.Add(p);

                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public Parceiro ObterInformacoesParceiros(string Adabas)
        {
            string SQL = " SELECT [CNPJ] " +
            "  ,[RAZÃO SOCIAL] " +
            "  ,[NOME FANTASIA] " +
            "  ,[REGIONAL] " +
            "  ,[TERRITORIAL] " +
            "  ,[CEP] " +
            "  ,[ENDEREÇO] " +
            "  ,[BAIRRO ] " +
            "  ,[CIDADE] " +
            "  ,[NUMERO] " +
            "  ,[COMPLEMENTO] " +
            "  ,[CONTATO] " +
            "  ,[CELULAR] " +
            "  ,[E-MAIL COMERCIAL] " +
            "  ,[E-MAIL DIVULGAÇÃO] " +
            "  ,[CODIGO VENDEDOR] " +
            "  ,[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].[UF] " +
            "  ,[GESTOR] " +
            "  ,[SELL IN] " +
            "  ,[STATUS CALLIDUS] " +
            "  ,[VENDEDOR] " +
            "  ,[REDE] " +
            "  ,UPPER(CANAL) AS [CANAL] " +
            "  ,[CÓDIGO CLIENTE] " +
            "  ,[CÓDIGO FORNECEDOR] " +
            "  ,[MATRICULA_GER_CONTA] " +
            "  ,[Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].[COD_IBGE] " +
            "  ,[DDD] " +
            "  ,[IXOS] " +
            "  ,[DDD GESTOR] " +
            "  ,[AREA ATUAÇÃO DDD GESTOR] " +
            "  ,[GERENTE TERRITORIAL] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [VENDEDOR] = '" + Adabas + "' ";

            DataTable dsParceiro = null;
            Parceiro p = new Parceiro();

            dsParceiro = this.dao.returnaDataTable(SQL);

            if (dsParceiro.Rows.Count > 0)
            {
                foreach (DataRow item in dsParceiro.Rows)
                {
                    p.Cnpj = Convert.ToString(item[0]);
                    p.RazaoSocial = Convert.ToString(item[1]);
                    p.NomeFantasia = Convert.ToString(item[2]);
                    p.Regional = Convert.ToString(item[3]);
                    p.Territorial = Convert.ToString(item[4]);
                    p.Cep = Convert.ToString(item[5]);
                    p.Endereco = Convert.ToString(item[6]);
                    p.Bairro = Convert.ToString(item[7]);
                    p.Cidade = Convert.ToString(item[8]);
                    p.Numero = Convert.ToString(item[9]);
                    p.Complemento = Convert.ToString(item[10]);
                    p.Contato = Convert.ToString(item[11]);
                    p.Celular = Convert.ToString(item[12]);
                    p.EmailComercial = Convert.ToString(item[13]);
                    p.EmailDivulgacao = Convert.ToString(item[14]);
                    p.CodigoVendedor = Convert.ToString(item[15]);
                    p.Uf = Convert.ToString(item[16]);
                    p.Gestor = Convert.ToString(item[17]);
                    p.SellIn = Convert.ToString(item[18]);
                    p.StatusCallidus = Convert.ToString(item[19]);
                    p.Vendedor = Convert.ToString(item[20]);
                    p.Rede = Convert.ToString(item[21]);
                    p.Canal = Convert.ToString(item[22]);
                    p.CodigoCliente = Convert.ToString(item[23]);
                    p.CodigoFornecedor = Convert.ToString(item[24]);
                    p.MatriculaGerenteConta = Convert.ToString(item[25]);
                    p.CodIbge = Convert.ToString(item[26]);
                    p.Ddd = Convert.ToString(item[27]);
                    
                    if (item[28].ToString() == "")
                    {
                        p.Ixos = retornarIxos(Adabas);
                    }
                    else
                    {
                        p.Ixos = Convert.ToString(item[28]);
                    }

                    p.DddGestor = Convert.ToString(item[29]);
                    p.AreaAtuacaoDddGestor = Convert.ToString(item[30]);
                    p.GerenteTerritorial = Convert.ToString(item[31]);
                }
                return p;
            }
            else
            {
                return null;
            }
        }

        public string retornarIxos(string adabas)
        {
            string SQL = " SELECT TOP 1 * ";
            SQL = SQL + " FROM [Vivo_VPC].[dbo].[VIEW_IXOS] ";
            SQL = SQL + " WHERE Adabas = '" + adabas + "' ";
            SQL = SQL + " ORDER BY DataCadastro DESC ";

            try
            {
                string retorno = null;
                DataTable dsCadastro;

                dsCadastro = this.dao.returnaDataTable(SQL);
                if (dsCadastro != null)
                {
                    if (dsCadastro.Rows.Count > 0)
                    {
                        foreach (DataRow item in dsCadastro.Rows)
                        {
                            retorno = item[1].ToString();
                        }
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> ObterUf(string Login)
        {
            string SQL = " SELECT DISTINCT [UF] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [MATRICULA_GER_CONTA] = '" + Login + "'" + 
            " ORDER BY UF ";

            DataTable dsResetSenha = null;
            List<string> retorno = new List<string>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    string p = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
            //return ObterParceiros().OrderBy(x => x.Uf).Select(x => x.Uf).Distinct().ToList();
        }

        public List<string> ObterUf()
        {
            string SQL = " SELECT DISTINCT [UF] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " ORDER BY UF ";

            DataTable dsResetSenha = null;
            List<string> retorno = new List<string>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    string p = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
            //return ObterParceiros().OrderBy(x => x.Uf).Select(x => x.Uf).Distinct().ToList();
        }

        public List<string> ObterRedes(string Login)
        {
            string SQL = " SELECT DISTINCT [REDE] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [MATRICULA_GER_CONTA] = '" + Login + "'" +
            " AND [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " ORDER BY [REDE] ";

            DataTable dsResetSenha = null;
            List<string> retorno = new List<string>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    string p = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
            //return ObterParceiros().OrderBy(x => x.Uf).Select(x => x.Uf).Distinct().ToList();
        }

        public List<string> ObterDdd(string Login)
        {
            string SQL = " SELECT DISTINCT [DDD] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [MATRICULA_GER_CONTA] = '" + Login + "'" + 
            " AND [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND DDD <> '' " +
            " ORDER BY [DDD] ";

            DataTable dsResetSenha = null;
            List<string> retorno = new List<string>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    string p = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<string> ObterDddPorUfLogin(string uf, string Login)
        {
            string SQL = " SELECT DISTINCT [DDD] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [MATRICULA_GER_CONTA] = '" + Login + "'" +
            " AND [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND UF = '" + uf + "' " +
            " AND DDD <> '' " +
            " ORDER BY [DDD] ";

            DataTable dsResetSenha = null;
            List<string> retorno = new List<string>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    string p = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<string> ObterDddPorUf(string uf)
        {
            string SQL = " SELECT DISTINCT [DDD] " +
            " FROM Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " WHERE UF = '" + uf + "'" +
            " ORDER BY [DDD] ";

            DataTable dsResetSenha = null;
            List<string> retorno = new List<string>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    string p = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<Parceiro> ObterGerenteContas()
        {
            string SQL = " SELECT DISTINCT UPPER([GESTOR]) AS GESTOR " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND GESTOR IS NOT NULL " +
            " AND GESTOR NOT LIKE '%SEM GC%' " +
            " ORDER BY GESTOR ";

            DataTable dsResetSenha = null;
            List<Parceiro> retorno = new List<Parceiro>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Gestor = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
            //return ObterParceiros().Where(x => x.Gestor != "" && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();
        }

        public List<string> ObterGerenteContas(string Uf, string Login)
        {
            string SQL = " SELECT DISTINCT UPPER([GESTOR]) AS GESTOR " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND GESTOR IS NOT NULL " +
            " AND GESTOR NOT LIKE '%SEM GC%' " +
            " AND [UF] = '" + Uf + "'" +
            " AND [MATRICULA_GER_CONTA] = '" + Login + "' " +
            " ORDER BY GESTOR ";

            DataTable dsResetSenha = null;
            List<string> retorno = null;

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                retorno = new List<string>();
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
            //return ObterParceiros().Where(x => x.Gestor != "" && x.Uf == Uf && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();
        }

        public List<string> ObterGerenteContasDDD(string Ddd)
        {
            string SQL = " SELECT DISTINCT [GESTOR] " +
            " FROM [Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_SIM].[dbo].[CARTEIRA_VALIDACAO_TERRITORIAL].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [DDD] = '" + Ddd + "' " +
            " AND GESTOR IS NOT NULL" +
            " AND GESTOR NOT LIKE 'SEM%'";


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

            //return ObterParceiros().Where(x => x.Gestor != "" && x.Ddd == Ddd && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();

        }

        public List<string> ObterAdabas(string Rede, string Login)
        {
            string SQL = " SELECT DISTINCT UPPER([VENDEDOR]) AS VENDEDOR " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [REDE] = '" + Rede + "' " +
            " AND [MATRICULA_GER_CONTA] = '" + Login + "' " +
            " ORDER BY VENDEDOR ";

            DataTable dsResetSenha = null;
            List<string> retorno = null;

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                retorno = new List<string>();
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
            //return ObterParceiros().Where(x => x.Rede == Rede).OrderBy(x => x.Vendedor).Select(x => x.Vendedor).Distinct().ToList();
        }

        public List<string> ObterAdabasLogin(string Login)
        {
            string SQL = " SELECT DISTINCT UPPER([VENDEDOR]) AS REDE " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [MATRICULA_GER_CONTA] = '" + Login + "' " +
            " ORDER BY REDE ";

            DataTable dsResetSenha = null;
            List<string> retorno = new List<string>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    string p = Convert.ToString(item[0]);

                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
            //return ObterParceiros().Where(x => x.Rede == Rede).OrderBy(x => x.Vendedor).Select(x => x.Vendedor).Distinct().ToList();
        }

        public List<Parceiro> ObterAdabasDDD(string Rede, string DDD, string TipoVerba)
        {
            string SQL = " SELECT DISTINCT UPPER([VENDEDOR]) AS VENDEDOR " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND GESTOR IS NOT NULL " +
            " AND GESTOR NOT LIKE '%SEM GC%' " +
            " AND [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].[REDE] = '" + Rede + "' " +
            " AND Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].[DDD] = '" + DDD + "' " +
            " ORDER BY [VENDEDOR] ";

            DataTable dsResetSenha = null;
            List<Parceiro> retorno = new List<Parceiro>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Vendedor = Convert.ToString(item[0]);

                    if (TipoVerba == "Verba Cooperada")
                    {
                        p.Saldo = RetornaValorVPC(Rede);
                    }
                    else
                    {
                        p.Saldo = RetornaValorVIO(Rede);
                    }
                    
                    retorno.Add(p);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public string RetornaValorVIO(string Rede)
        {
            SaldoRede SaldoParceiro = ObterValorParceiros(Rede);
            AcaoMarketing ValorInserido = ObterSaldoParceiros(Rede, "Verba de Incentivo a Open");
            decimal valor = 0;

            if(SaldoParceiro != null)
            {
                valor = Convert.ToDecimal(SaldoParceiro.SaldoVio);
            }
            
            

            if (ValorInserido != null)
            {
                valor = Convert.ToDecimal(SaldoParceiro.SaldoVio) + Convert.ToDecimal(ValorInserido.ValorTotalVivo);
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            string Saldo = string.Format("{0:C}", Convert.ToDouble(valor));
            Saldo = Saldo.Substring(3);

            return Saldo;

        }

        public string RetornaValorVPC(string Rede)
        {
            SaldoRede SaldoParceiro = ObterValorParceiros(Rede);
            AcaoMarketing ValorInserido = ObterSaldoParceiros(Rede, "Verba Cooperada");
            decimal valor = 0;

            if(SaldoParceiro != null)
            {
                valor = Convert.ToDecimal(SaldoParceiro.SaldoVpc);
            }

            if (ValorInserido != null)
            {
                valor = Convert.ToDecimal(SaldoParceiro.SaldoVpc) + Convert.ToDecimal(ValorInserido.ValorTotalVivo);
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            string Saldo = string.Format("{0:C}", Convert.ToDouble(valor));
            Saldo = Saldo.Substring(3);

            return Saldo;
        }

        public IEnumerable<string> ObterGerenteContas(string Ddd, string Canal, string TipoCarteira)
        {
            if (TipoCarteira != "TODOS")
            {
                if (TipoCarteira == "FIXO FIXA")
                {
                    if (Canal == "TODOS")
                    {
                        return ObterParceiros().Where(x => x.Ddd == Ddd && x.Gestor != "" && x.Atividade == "PROJETO LAURA" && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();
                    }
                    else
                    {
                        return ObterParceiros().Where(x => x.Ddd == Ddd && x.Gestor != "" && x.Canal == Canal && x.Atividade == "PROJETO LAURA" && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();
                    }
                }
                else
                {
                    if (Canal == "TODOS")
                    {
                        return ObterParceiros().Where(x => x.Ddd == Ddd && x.Gestor != "" && x.Atividade != "PROJETO LAURA" && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();
                    }
                    else
                    {
                        return ObterParceiros().Where(x => x.Ddd == Ddd && x.Gestor != "" && x.Canal == Canal && x.Atividade != "PROJETO LAURA" && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();
                    }
                    
                }
            }
            else
            {
                if(Canal == "TODOS")
                {
                    return ObterParceiros().Where(x => x.Ddd == Ddd && x.Gestor != "" && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();
                }
                else
                {
                    return ObterParceiros().Where(x => x.Ddd == Ddd && x.Gestor != "" && x.Canal == Canal && !x.Gestor.Contains("SEM GC")).OrderBy(x => x.Gestor).Select(x => x.Gestor).Distinct().ToList();
                }
            }

            
        }

        public List<string> ObterParceirosRedeLogin(string Ddd, string Login)
        {
            string SQL = "  SELECT DISTINCT [REDE] " +
            "  FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC] " +
            "  LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            "  ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            "  WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].[DDD] = '" + Ddd + "'" +
            " AND [MATRICULA_GER_CONTA] = '" + Login + "' ";

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

            //return ObterParceiros().Where(x => x.Ddd == Ddd && x.Ddd != "").OrderBy(x => x.Rede).Select(x => x.Rede).Distinct().ToList();
        }

        public List<string> ObterParceirosRede(string Ddd)
        {
            string SQL = " SELECT DISTINCT [REDE] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].[DDD] = '" + Ddd + "' ";

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

            //return ObterParceiros().Where(x => x.Ddd == Ddd && x.Ddd != "").OrderBy(x => x.Rede).Select(x => x.Rede).Distinct().ToList();
        }

        public List<string> ObterParceirosRedeVarejo(string Ddd)
        {
            string SQL = " SELECT DISTINCT [REDE] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].[DDD] = '" + Ddd + "' " +
            " AND CANAL = 'Varejo'";

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

            //return ObterParceiros().Where(x => x.Ddd == Ddd && x.Ddd != "").OrderBy(x => x.Rede).Select(x => x.Rede).Distinct().ToList();
        }

        public List<string> ObterParceirosRedeLLPP(string Ddd)
        {
            string SQL = " SELECT DISTINCT [NOME FANTASIA] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].[DDD] = '" + Ddd + "' " +
            " AND CANAL = 'Loja Própria'";

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

            //return ObterParceiros().Where(x => x.Ddd == Ddd && x.Ddd != "").OrderBy(x => x.Rede).Select(x => x.Rede).Distinct().ToList();
        }

        public AcaoMarketing ObterSaldoParceiros(string Rede, string TipoVerba)
        {
            string SQL = " SELECT REDE " +
            "              ,SUM(VALOR) AS VALOR " +
            " FROM [Vivo_SIM].[dbo].[ACAO_VALOR] " +
            " WHERE REDE = '" + Rede + "' " +
            " AND ORIGEM_VERBA = '" + TipoVerba + "' " +
            " GROUP BY REDE ";

            DataTable dsResetSenha = null;
            AcaoMarketing retorno = null;

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    retorno = new AcaoMarketing();
                    retorno.ValorTotalVivo = item[1].ToString();
                }
                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public SaldoRede ObterValorParceiros(string Rede)
        {
            string SQL = " SELECT [REDE] " +
            " ,[Saldo_VPC] " +
            " ,[Saldo_VIO] " +
            " ,[Regional] " +
            " FROM [Vivo_SIM].[dbo].[ACAO_SALDO_REDE] " +
            " WHERE REDE = '" + Rede + "'";

            DataTable dsResetSenha = null;
            SaldoRede retorno = null;

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    retorno = new SaldoRede();
                    retorno.Rede = item[0].ToString();
                    retorno.SaldoVpc = Convert.ToDecimal(item[1]);
                    retorno.SaldoVio = Convert.ToDecimal(item[2]);
                    retorno.Regional = item[3].ToString();
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
