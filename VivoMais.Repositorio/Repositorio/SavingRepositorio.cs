using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class SavingRepositorio
    {
        DAO dao;
        string connectioString = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;

        public SavingRepositorio()
        {
            this.dao = DAO.Instance;
        }

        

        //public bool CadastrarSaving(HttpPostedFileBase file, Saving saving)
        //{
        //    saving.ComprovanteAcao = ConvertToBytes(file);

        //    SqlConnection connection = new SqlConnection(connectioString);
        //    connection.Open();

        //    using (SqlCommand cmd = new SqlCommand("INSERT INTO [VIVO_SIM].[dbo].[SAVING]" +
        //                 " ([mesAcao] " +
        //                 " ,[ddd] " +
        //                 " ,[rede] " +
        //                 " ,[tipoAcao] " +
        //                 " ,[opcaoAcao] " +
        //                 " ,[oferta] " +
        //                 " ,[periodoInicial] " +
        //                 " ,[periodoFinal] " +
        //                 " ,[valorAcao] " +
        //                 " ,[descricao] " +
        //                 " ,[comprovanteAcao] " +
        //                 " ) VALUES " +
        //                 "('" + saving.MesAcao + "'" +
        //                 ",'" + saving.Parceiro.Ddd + "'" +
        //                 ",'" + saving.Parceiro.Rede + "'" +
        //                 ",'" + saving.TipoAcao + "'" +
        //                 ",'" + saving.OpcaoAcao + "'" +
        //                 ",'" + saving.Oferta + "'" +
        //                 ",'" + saving.PeriodoInicial + "'" +
        //                 ",'" + saving.PeriodoFinal + "'" +
        //                 ",'" + saving.ValorAcao + "'" +
        //                 ",'" + saving.Descricao + "'" +
        //                 " ," + "(@binaryValue))", connection))
        //    {
        //        cmd.Parameters.Add("@binaryValue", SqlDbType.Image).Value = saving.ComprovanteAcao;
        //        cmd.ExecuteNonQuery();
        //    }

        //    try
        //    {
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public Saving retornaFaturamentoChipSaving(string Rede)
        {
            string SQL = "  SELECT REDE " +
                         "   ,CASE WHEN (UF IN ('AL','CE','PB','PE','PI','RN') AND REDE IN ('GM DISTRIBUIDORA RC','IP TELECOM RC','R S DISTRIBUIDORA','NET CARTOES RC','PROSERT RC','NET CARTOES RC')) THEN (COUNT(*) * 1.00) " +
                         " WHEN (UF IN ('AL','CE','PB','PE','PI','RN') AND REDE IN ('D LIMA RE','GMEX DISTRIBUIDORA RC','ATHOS TELECOM RC')) THEN (COUNT(*) * 1.50)  " +
                         "        WHEN UF IN ('BA','SE') THEN (COUNT(*) * 2.00)  " +
                         "  ELSE 0 END AS TOTAL   " +
                         " FROM Rel_Ativ_Fat_Dpav.dbo.SAP_ZS118_TRATADA LEFT JOIN Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS " +
                         " ON (Rel_Ativ_Fat_Dpav.dbo.SAP_ZS118_TRATADA.SAP_CPF_CNPJ = Vivo_GRC.dbo.TAB_CADASTRO_GERAL_PDVS.CNPJ) " +
                         " WHERE SAP_DATA_NF BETWEEN '2016-03-01' AND GETDATE() " +
                         " AND SAP_MATERIAL LIKE 'Y%' " +
                         " AND SAP_VALOR_NF = '2.00' " +
                         " AND CANAL = 'Recarga' " +
                         " AND REDE = '" + Rede + "' " +
                         " GROUP BY REDE,UF " +
                         " ORDER BY REDE ";

            Saving retorno = new Saving();
            DataTable dsCadastro;
            decimal Valor = 0;

            try
            {
                dsCadastro = this.dao.returnaDataTable(SQL);
                if (dsCadastro != null)
                {
                    if (dsCadastro.Rows.Count > 0)
                    {
                        foreach (DataRow item in dsCadastro.Rows)
                        {
                            Valor = Convert.ToDecimal(item[1]);
                        }

                        if (Rede == "NET CARTOES RC")
                        {
                            Valor = Valor + Convert.ToDecimal(13056.70);
                        }
                        else if (Rede == "PROSERT RC")
                        {
                            Valor = Valor + Convert.ToDecimal(7068.50);
                        }
                        else if (Rede == "LOG DISTRIBUIDORA RC")
                        {
                            Valor = Valor + Convert.ToDecimal(830.72);
                        }
                        else if (Rede == "NET CARTOES RC")
                        {
                            Valor = Valor + Convert.ToDecimal(13056.70);
                        }
                        else if (Rede == "GM DISTRIBUIDORA RC")
                        {
                            Valor = Valor + Convert.ToDecimal(6352.06);
                        }
                        else if (Rede == "RV TECNOLOGIA RC")
                        {
                            Valor = Valor + Convert.ToDecimal(51683.94);
                        }
                        else if (Rede == "VALLE CARD RC")
                        {
                            Valor = Valor + Convert.ToDecimal(14390.54);
                        }
                        else if (Rede == "R S DISTRIBUIDORA")
                        {
                            Valor = Valor + Convert.ToDecimal(41164.30);
                        }
                        else if (Rede == "IP TELECOM RC")
                        {
                            Valor = Valor + Convert.ToDecimal(10949.00);
                        }

                        retorno.SaldoFaturado = Valor.ToString("N2");
                    }
                    else
                    {
                        retorno = null;
                    }
                }

                return retorno;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Saving> ObterSaving(string MesAcao)
        {

            string SQL = " SELECT [id]" +
                            ",[mesAcao] " +
                            ",[ddd] " +
                            ",[rede] " +
                            ",[tipoAcao] " +
                            ",[opcaoAcao] " +
                            ",[oferta] " +
                            ",[periodoInicial] " +
                            ",[periodoFinal] " +
                            ",[valorAcao] " +
                            ",[descricao] " +
                    " FROM [Vivo_SIM].[dbo].[SAVING] " +
                    " WHERE [mesAcao] = '" + MesAcao + "'" ;

            List<Saving> retorno = null;
            DataTable dsSaving = dsSaving = this.dao.returnaDataTable(SQL);

            if (dsSaving.Rows.Count > 0)
            {
                retorno = new List<Saving>();

                foreach (DataRow item in dsSaving.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Ddd = Convert.ToString(item[2]);
                    p.Rede = Convert.ToString(item[3]);

                    Saving s = new Saving();
                    s.Id = Convert.ToInt32(item[0]);
                    s.MesAcao = Convert.ToString(item[1]);
                    s.Parceiro = p;

                    s.TipoAcao = Convert.ToString(item[4]);
                    s.OpcaoAcao = Convert.ToString(item[5]);
                    s.Oferta = Convert.ToString(item[6]);
                    s.PeriodoInicial = Convert.ToDateTime(item[7]);
                    s.PeriodoFinal = Convert.ToDateTime(item[8]);
                    s.ValorAcao = Convert.ToString(item[9]);
                    s.Descricao = Convert.ToString(item[10]);

                    retorno.Add(s);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public List<Saving> ObterSavingPorID(string Id)
        {

            string SQL = " SELECT [id]" +
                            ",[mesAcao] " +
                            ",[ddd] " +
                            ",[rede] " +
                            ",[tipoAcao] " +
                            ",[opcaoAcao] " +
                            ",[oferta] " +
                            ",[periodoInicial] " +
                            ",[periodoFinal] " +
                            ",[valorAcao] " +
                            ",[descricao] " +
                    " FROM [Vivo_SIM].[dbo].[SAVING] " +
                    " WHERE [id] = '" + Id + "'";

            List<Saving> retorno = null;
            DataTable dsSaving = dsSaving = this.dao.returnaDataTable(SQL);

            if (dsSaving.Rows.Count > 0)
            {
                retorno = new List<Saving>();

                foreach (DataRow item in dsSaving.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Ddd = Convert.ToString(item[2]);
                    p.Rede = Convert.ToString(item[3]);

                    Saving s = new Saving();
                    s.Id = Convert.ToInt32(item[0]);
                    s.MesAcao = Convert.ToString(item[1]);
                    s.Parceiro = p;
                    s.TipoAcao = Convert.ToString(item[4]);
                    s.OpcaoAcao = Convert.ToString(item[5]);
                    s.Oferta = Convert.ToString(item[6]);
                    s.PeriodoInicial = Convert.ToDateTime(item[7]);
                    s.PeriodoFinal = Convert.ToDateTime(item[8]);
                    s.ValorAcao = Convert.ToString(item[9]);
                    s.Descricao = Convert.ToString(item[10]);

                    retorno.Add(s);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public List<Saving> extrairSaving(string[] Id)
        {
            string result = string.Join(",", Id);

            if (result != "")
            {
                result = result.Substring(1, result.Count() - 1);
            }

            string SQL = " SELECT [id]" + 
                            ",[mesAcao] " + 
                            ",[ddd] " +
                            ",[rede] " +
                            ",[tipoAcao] " +
                            ",[opcaoAcao] " +
                            ",[oferta] " +
                            ",[periodoInicial] " +
                            ",[periodoFinal] " +
                            ",[valorAcao] " +
                            ",[descricao] " +
                    " FROM [Vivo_SIM].[dbo].[SAVING] " +
                    " WHERE [id] IN (" + result + ")";

            DataTable dsAcao = null;
            List<Saving> retorno = new List<Saving>();

            dsAcao = this.dao.returnaDataTable(SQL);

            if (dsAcao.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcao.Rows)
                {
                    Parceiro p = new Parceiro();
                    p.Ddd = Convert.ToString(item[2]);
                    p.Rede = Convert.ToString(item[3]);

                    Saving saving = new Saving();
                    saving.Id = Convert.ToInt32(item[0]);
                    saving.MesAcao = Convert.ToString(item[1]);
                    saving.Parceiro = p;

                    saving.TipoAcao = Convert.ToString(item[4]);
                    saving.OpcaoAcao = Convert.ToString(item[5]);
                    saving.Oferta = Convert.ToString(item[6]);
                    saving.PeriodoInicial = Convert.ToDateTime(item[7]);
                    saving.PeriodoFinal = Convert.ToDateTime(item[8]);
                    saving.ValorAcao = Convert.ToString(item[9]);
                    saving.Descricao = Convert.ToString(item[10]);

                    retorno.Add(saving);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<string> ObterParceirosRedePorDDDeCanalRecarga(string Ddd)
        {
            string SQL = " SELECT DISTINCT [REDE] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC]  " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC].COD_IBGE = [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND [Vivo_DE_PARA].[dbo].[TAB MUNICIPIOS IBGE].[DDD] = '" + Ddd + "' " +
            " AND [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS_AGRUPADO_GC].[CANAL] = 'Recarga'";

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

        public byte[] GetImageFromDataBase(string id)
        {

            string sql = "SELECT [comprovanteAcao] FROM [Vivo_SIM].[dbo].[SAVING] WHERE Id = '" + id + "'";

            SqlConnection connection = new SqlConnection(connectioString);
            connection.Open();

            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                return (byte[])dr[0];
            }

            else
            {
                return null;
            }

        }

    }
}