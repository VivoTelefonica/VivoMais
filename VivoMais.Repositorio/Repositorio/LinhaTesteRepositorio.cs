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
    public class LinhaTesteRepositorio
    {
        DAO dao;
        string connectioString = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;

        public LinhaTesteRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public bool InserirLinhaTeste(HttpPostedFileBase file, LinhaTeste Linha, string Extensao)
        {
            SqlConnection connection = new SqlConnection(connectioString);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("INSERT INTO [Vivo_SIM].[dbo].[LINHA_TESTE_CADASTRO] " +
            "       ([COLABORADOR] " +
            "       ,[EMAIL] " +
            "       ,[TIPO] " +
            "       ,[MODALIDADE] " +
            "       ,[PLANO] " +
            "       ,[DIRETORIA] " +
            "       ,[USUARIO_LINHA] " +
            "       ,[STATUS_LINHA] " +
            "       ,[LINHA] " +
            "       ,[ICCID] " +
            "       ,[CANAL] " +
            "       ,[DATA_INICIO] " +
            "       ,[DATA_FIM] " +
            "       ,[LOCALIDADE] " +
            "       ,[OBS] " +
            "       ,[REGIONAL] " +
            "       ,[EXTENSAO] " +
            "       ,[DATA_SOLICITACAO] " +
            "       ,[ARQUIVO] " +            
            " ) VALUES " +
            "('" + Linha.Colaborador + "' "+
            " ,'" + Linha.Email + "' " +
            " ,'" + Linha.Tipo + "' " +
            " ,'" + Linha.Modalidade + "' " +
            " ,'" + Linha.Plano + "' " +
            " ,'" + Linha.Diretoria + "' " +
            " ,'" + Linha.UsuarioLinha + "' " +
            " ,'" + Linha.StatusLinha + "' " +
            " ,'" + Linha.Linha + "' " +
            " ,'" + Linha.Iccid + "' " +
            " ,'" + Linha.Canal + "' " +
            " ,CONVERT(DATETIME,'" + Linha.DataInicio + "',103) " +
            " ,CONVERT(DATETIME,'" + Linha.DataFim + "',103) " +
            " ,'" + Linha.Localidade + "' " +
            " ,'" + Linha.Obs + "' " +
            " ,'" + Linha.Regional + "' " +
            " ,'" + Extensao + "' " +
            " ,GetDate() " +
            " ," + "(@binaryValue))", connection))


            {
                cmd.Parameters.Add("@binaryValue", SqlDbType.Image).Value = ConvertToBytes(file);
                cmd.ExecuteNonQuery();
            }

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public bool InserirLinhaTeste(LinhaTeste Linha) 
        //{
        //    string SQL = " 
        //    " VALUES " +
        //    " ( " +
        //    " '" + 
        //    " ) ";

        //    return dao.ExecutarSql(SQL);
        //}

        public List<LinhaTeste> ObterTodosLinhaTeste()
        {
            string SQL = " SELECT [ID] " +
            "  ,[COLABORADOR] " +
            "  ,[EMAIL] " +
            "  ,[TIPO] " +
            "  ,[MODALIDADE] " +
            "  ,[PLANO] " +
            "  ,[DIRETORIA] " +
            "  ,[USUARIO_LINHA] " +
            "  ,[STATUS_LINHA] " +
            "  ,[LINHA] " +
            "  ,[ICCID] " +
            "  ,[CANAL] " +
            "  ,[DATA_INICIO] " +
            "  ,[DATA_FIM] " +
            "  ,[LOCALIDADE] " +
            "  ,[OBS] " +
            "  ,[REGIONAL] " +
            " FROM [Vivo_SIM].[dbo].[LINHA_TESTE_CADASTRO] ";

            DataTable dsAcessos = null;
            List<LinhaTeste> retorno = new List<LinhaTeste>();

            dsAcessos = this.dao.returnaDataTable(SQL);

            if (dsAcessos.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcessos.Rows)
                {
                    LinhaTeste LinhaT = new LinhaTeste();
                    LinhaT.Id = Convert.ToInt32(item[0]);
                    LinhaT.Colaborador = Convert.ToString(item[1]);
                    LinhaT.Email = Convert.ToString(item[2]);
                    LinhaT.Tipo = Convert.ToString(item[3]);
                    LinhaT.Modalidade = Convert.ToString(item[4]);
                    LinhaT.Plano = Convert.ToString(item[5]);
                    LinhaT.Diretoria = Convert.ToString(item[6]);
                    LinhaT.UsuarioLinha = Convert.ToString(item[7]);
                    LinhaT.StatusLinha = Convert.ToString(item[8]);
                    LinhaT.Linha = Convert.ToString(item[9]);
                    LinhaT.Iccid = Convert.ToString(item[10]);
                    LinhaT.Canal = Convert.ToString(item[11]);
                    LinhaT.DataInicio = Convert.ToDateTime(item[12]);
                    LinhaT.DataFim = Convert.ToDateTime(item[13]);
                    LinhaT.Localidade = Convert.ToString(item[14]);
                    LinhaT.Obs = Convert.ToString(item[15]);
                    LinhaT.Regional = Convert.ToString(item[16]);


                    retorno.Add(LinhaT);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public LinhaTeste ObterLinhaTesteId(string Id)
        {
            string SQL = " SELECT [ID] " +
            "  ,[COLABORADOR] " +
            "  ,[EMAIL] " +
            "  ,[TIPO] " +
            "  ,[MODALIDADE] " +
            "  ,[PLANO] " +
            "  ,[DIRETORIA] " +
            "  ,[USUARIO_LINHA] " +
            "  ,[STATUS_LINHA] " +
            "  ,[LINHA] " +
            "  ,[ICCID] " +
            "  ,[CANAL] " +
            "  ,[DATA_INICIO] " +
            "  ,[DATA_FIM] " +
            "  ,[LOCALIDADE] " +
            "  ,[OBS] " +
            "  ,[REGIONAL] " +
            "  ,[EXTENSAO] " +
            " FROM [Vivo_SIM].[dbo].[LINHA_TESTE_CADASTRO] " +
            " WHERE ID = '" + Id + "' ";

            DataTable dsAcessos = null;
            LinhaTeste retorno = null;

            dsAcessos = this.dao.returnaDataTable(SQL);

            if (dsAcessos.Rows.Count > 0)
            {
                foreach (DataRow item in dsAcessos.Rows)
                {
                    retorno = new LinhaTeste();
                    retorno.Id = Convert.ToInt32(item[0]);
                    retorno.Colaborador = Convert.ToString(item[1]);
                    retorno.Email = Convert.ToString(item[2]);
                    retorno.Tipo = Convert.ToString(item[3]);
                    retorno.Modalidade = Convert.ToString(item[4]);
                    retorno.Plano = Convert.ToString(item[5]);
                    retorno.Diretoria = Convert.ToString(item[6]);
                    retorno.UsuarioLinha = Convert.ToString(item[7]);
                    retorno.StatusLinha = Convert.ToString(item[8]);
                    retorno.Linha = Convert.ToString(item[9]);
                    retorno.Iccid = Convert.ToString(item[10]);
                    retorno.Canal = Convert.ToString(item[11]);
                    retorno.DataInicio = Convert.ToDateTime(item[12]);
                    retorno.DataFim = Convert.ToDateTime(item[13]);
                    retorno.Localidade = Convert.ToString(item[14]);
                    retorno.Obs = Convert.ToString(item[15]);
                    retorno.Regional = Convert.ToString(item[16]);
                    retorno.Extensao = Convert.ToString(item[17]);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase arquivo)
        {
            byte[] arquivoBytes = null;
            BinaryReader reader = new BinaryReader(arquivo.InputStream);

            arquivoBytes = reader.ReadBytes((int)arquivo.ContentLength);
            return arquivoBytes;
        }
    }
}