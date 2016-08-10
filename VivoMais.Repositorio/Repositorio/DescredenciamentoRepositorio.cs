using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace VivoMais.Repositorio.Repositorio
{
    public class DescredenciamentoRepositorio
    {
        DAO dao;
        string connectioString = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;

        public DescredenciamentoRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public Descredenciamento ObterDescredenciamentoID(int Id)
        {
            ParceiroRepositorio parceiro = new ParceiroRepositorio();

            string SQL = "SELECT [ID] " +
            "   ,[ADABAS] " +
            "   ,[TIPO_DESCREDENCIAMENTO] " +
            "   ,[MOTIVO] " +
            "   FROM [Vivo_SIM].[dbo].[DESCREDENCIAMENTO_CADASTRO] " +
            " WHERE ID = " + Id;

            DataTable dsDescredenciamento = null;
            Descredenciamento retorno = null;

            dsDescredenciamento = this.dao.returnaDataTable(SQL);

            if (dsDescredenciamento.Rows.Count > 0)
            {
                foreach (DataRow item in dsDescredenciamento.Rows)
                {
                    retorno = new Descredenciamento();
                    retorno.Parceiro = parceiro.ObterInformacoesParceiros(item[1].ToString());
                    retorno.TipoDescredenciamento = item[2].ToString();
                    retorno.Motivo = item[3].ToString();

                    if (item[2].ToString().Equals("AMIGÁVEL (DISTRATO)"))
                    {
                        retorno.Amigavel = ObterAmigavelID(Id);
                    }
                    else if (item[2].ToString().Equals("INATIVIDADE (NOTIFICAÇÃO)"))
                    {
                        retorno.Inatividade = ObterInatividadeID(Id);
                    } else
                    {
                        retorno.Vigencia = ObterVigenciaID(Id);
                    }

                    
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public DescredenciamentoAmigavel ObterAmigavelID(int Id)
        {
            string SQL = "SELECT [ID_DESCREDENCIAMENTO] " +
            "  ,[SOLICITACAO_PARCEIRO] " +
            "  ,[INFORME] " +
            "  ,[LIBERADO_CHANCELA] " +
            "  ,[ENVIO_TERRITORIO] " +
            "  ,[RECEBIMENTO_DISTRATO] " +
            "  ,[STATUS] " +
            "  ,[ASSINATURA_DIVISAO] " +
            "  ,[ASSINATURA_DIRETORIA] " +
            "  ,[RECONHECIMENTO_FIRMA] " +
            "  ,[EXCLUSAO_ACESSOS] " +
            " FROM [Vivo_SIM].[dbo].[DESCREDENCIAMENTO_AMIGAVEL] " +
            " WHERE ID_DESCREDENCIAMENTO = " + Id;

            DataTable dsDescredenciamento = null;
            DescredenciamentoAmigavel retorno = null;

            dsDescredenciamento = this.dao.returnaDataTable(SQL);

            if (dsDescredenciamento.Rows.Count > 0)
            {
                foreach (DataRow item in dsDescredenciamento.Rows)
                {
                    retorno = new DescredenciamentoAmigavel();

                    retorno.Id = Convert.ToInt32(item[0]);
                    
                    if ((item[1].ToString().Equals(null)) || item[1].ToString().Equals(""))
                    {
                        retorno.SolicitacaoParceiro = DateTime.MinValue;
                    }
                    else
                    {
                        retorno.SolicitacaoParceiro = Convert.ToDateTime(item[1]);
                    }
                    
                    retorno.Informe = Convert.ToString(item[2]);

                    if ((item[3].ToString().Equals(null)) || item[3].ToString().Equals(""))
                    {
                        retorno.LiberadoChancela = null;
                    }
                    else
                    {
                        retorno.LiberadoChancela = Convert.ToDateTime(item[3]);
                    }
                    if ((item[4].ToString().Equals(null)) || item[4].ToString().Equals(""))
                    {
                        retorno.EnvioTerritorio = null;
                    }
                    else
                    {
                        retorno.EnvioTerritorio = Convert.ToDateTime(item[4]);
                    }
                    if ((item[5].ToString().Equals(null)) || item[5].ToString().Equals(""))
                    {
                        retorno.RecebimentoDistrato = null;
                    }
                    else
                    {
                        retorno.RecebimentoDistrato = Convert.ToDateTime(item[5]);
                    }
                    
                        retorno.Status = Convert.ToString(item[6]);
                    
                    if ((item[7].ToString().Equals(null)) || item[7].ToString().Equals(""))
                    {
                        retorno.AssinaturaDivisao = null;
                    }
                    else
                    {
                        retorno.AssinaturaDivisao = Convert.ToDateTime(item[7]);
                    }
                    if ((item[8].ToString().Equals(null)) || item[8].ToString().Equals(""))
                    {
                        retorno.AssinaturaDiretoria = null;
                    }
                    else
                    {
                        retorno.AssinaturaDiretoria = Convert.ToDateTime(item[8]);
                    }
                    if ((item[9].ToString().Equals(null)) || item[9].ToString().Equals(""))
                    {
                        retorno.ReconhecimentoFirma = null;
                    }
                    else
                    {
                        retorno.ReconhecimentoFirma = Convert.ToDateTime(item[9]);
                    }
                    if ((item[10].ToString().Equals(null)) || item[10].ToString().Equals(""))
                    {
                        retorno.ExclusaoAcessos = null;
                    }
                    else
                    {
                        retorno.ExclusaoAcessos = Convert.ToDateTime(item[10]);
                    }

                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public DescredenciamentoInatividade ObterInatividadeID(int Id)
        {
            string SQL = "SELECT [ID_DESCREDENCIAMENTO] " +
            "  ,[SOLICITACAO_PARCEIRO] " +
            "  ,[INFORME] " +
            "  ,[LIBERADO_CHANCELA] " +
            "  ,[CHANCELA] " +
            "  ,[ASSINATURA_DIVISAO] " +
            "  ,[ASSINATURA_DIRETORIA] " +
            "  ,[RECONHECIMENTO_FIRMA] " +
            "  ,[ENVIO_TERRITORIO] " +
            "  ,[RECEBIMENTO_NOTIFICACAO_CERTIDAO] " +
            "  ,[STATUS] " +
            "  ,[DATA_PREVISTA_DESCREDENCIAMENTO] " +
            "  ,[EXCLUSAO_ACESSOS] " +
            " FROM [Vivo_SIM].[dbo].[DESCREDENCIAMENTO_INATIVIDADE] " +
            " WHERE ID_DESCREDENCIAMENTO = " + Id;

            DataTable dsDescredenciamento = null;
            DescredenciamentoInatividade retorno = null;

            dsDescredenciamento = this.dao.returnaDataTable(SQL);

            if (dsDescredenciamento.Rows.Count > 0)
            {
                foreach (DataRow item in dsDescredenciamento.Rows)
                {
                    retorno = new DescredenciamentoInatividade();

                    retorno.Id = Convert.ToInt32(item[0]);
                    if ((item[1].ToString().Equals(null)) || item[1].ToString().Equals(""))
                    {
                        retorno.SolicitacaoParceiro = DateTime.MinValue;
                    }
                    else
                    {
                        retorno.SolicitacaoParceiro = Convert.ToDateTime(item[1]);
                    }
                    retorno.Informe = Convert.ToString(item[2]);
                    if ((item[3].ToString().Equals(null)) || item[3].ToString().Equals(""))
                    {
                        retorno.LiberadoChancela = null;
                    }
                    else
                    {
                        retorno.LiberadoChancela = Convert.ToDateTime(item[3]);
                    }
                    if ((item[4].ToString().Equals(null)) || item[4].ToString().Equals(""))
                    {
                        retorno.Chancela = null;
                    }
                    else
                    {
                        retorno.Chancela = Convert.ToDateTime(item[4]);
                    }
                    if ((item[5].ToString().Equals(null)) || item[5].ToString().Equals(""))
                    {
                        retorno.AssinaturaDivisao = null;
                    }
                    else
                    {
                        retorno.AssinaturaDivisao = Convert.ToDateTime(item[5]);
                    }
                    if ((item[6].ToString().Equals(null)) || item[6].ToString().Equals(""))
                    {
                        retorno.AssinaturaDiretoria = null;
                    }
                    else
                    {
                        retorno.AssinaturaDiretoria = Convert.ToDateTime(item[6]);
                    }
                    if ((item[7].ToString().Equals(null)) || item[7].ToString().Equals(""))
                    {
                        retorno.ReconhecimentoFirma = null;
                    }
                    else
                    {
                        retorno.ReconhecimentoFirma = Convert.ToDateTime(item[7]);
                    }
                    if ((item[8].ToString().Equals(null)) || item[8].ToString().Equals(""))
                    {
                        retorno.EnvioTerritorio = null;
                    }
                    else
                    {
                        retorno.EnvioTerritorio = Convert.ToDateTime(item[8]);
                    }
                    if ((item[9].ToString().Equals(null)) || item[9].ToString().Equals(""))
                    {
                        retorno.RecebimentoNotificacaoCertidao = null;
                    }
                    else
                    {
                        retorno.RecebimentoNotificacaoCertidao = Convert.ToDateTime(item[9]);
                    }
                    
                        retorno.Status = Convert.ToString(item[10]);
                    
                    if ((item[11].ToString().Equals(null)) || item[11].ToString().Equals(""))
                    {
                        retorno.DataPrevistaDescredenciamento = null;
                    }
                    else
                    {
                        retorno.DataPrevistaDescredenciamento = Convert.ToDateTime(item[11]);
                    }
                    if ((item[12].ToString().Equals(null)) || item[12].ToString().Equals(""))
                    {
                        retorno.ExclusaoAcessos = null;
                    }
                    else
                    {
                        retorno.ExclusaoAcessos = Convert.ToDateTime(item[12]);
                    }
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public DescredenciamentoVigencia ObterVigenciaID(int Id)
        {
            string SQL = "SELECT [ID_DESCREDENCIAMENTO] " +
            "  ,[SOLICITACAO_PARCEIRO] " +
            "  ,[INFORME] " +
            "  ,[LIBERADO_CHANCELA] " +
            "  ,[CHANCELA] " +
            "  ,[ASSINATURA_DIVISAO] " +
            "  ,[ASSINATURA_DIRETORIA] " +
            "  ,[RECONHECIMENTO_FIRMA] " +
            "  ,[TERMINO_CONTRATO] " +
            "  ,[ENVIO_RECEBIMENTO_NOTIFICACAO_CERTIDAO] " +
            "  ,[STATUS] " +
            "  ,[EXCLUSAO_ACESSOS] " +
            " FROM [Vivo_SIM].[dbo].[DESCREDENCIAMENTO_VIGENCIA_CONTRATO] " +
            " WHERE ID_DESCREDENCIAMENTO = " + Id;

            DataTable dsDescredenciamento = null;
            DescredenciamentoVigencia retorno = null;

            dsDescredenciamento = this.dao.returnaDataTable(SQL);

            if (dsDescredenciamento.Rows.Count > 0)
            {
                foreach (DataRow item in dsDescredenciamento.Rows)
                {
                    retorno = new DescredenciamentoVigencia();

                    retorno.Id = Convert.ToInt32(item[0]);

                    if((item[1].ToString().Equals(null)) || item[1].ToString().Equals(""))
                    {
                        retorno.SolicitacaoParceiro = DateTime.MinValue;
                    }
                    else
                    {
                        retorno.SolicitacaoParceiro = Convert.ToDateTime(item[1]);
                    }

                    retorno.Informe = Convert.ToString(item[2]);

                    if ((item[3].ToString().Equals(null)) || item[3].ToString().Equals(""))
                    {
                        retorno.LiberadoChancela = null;
                    }
                    else
                    {
                        retorno.LiberadoChancela = Convert.ToDateTime(item[3]);
                    }
                    if ((item[4].ToString().Equals(null)) || item[4].ToString().Equals(""))
                    {
                        retorno.Chancela = null;
                    }
                    else
                    {
                        retorno.Chancela = Convert.ToDateTime(item[4]);
                    }
                    if ((item[5].ToString().Equals(null)) || item[5].ToString().Equals(""))
                    {
                        retorno.AssinaturaDivisao = null;
                    }
                    else
                    {
                        retorno.AssinaturaDivisao = Convert.ToDateTime(item[5]);
                    }
                    if ((item[6].ToString().Equals(null)) || item[6].ToString().Equals(""))
                    {
                        retorno.AssinaturaDiretoria = null;
                    }
                    else
                    {
                        retorno.AssinaturaDiretoria = Convert.ToDateTime(item[6]);
                    }
                    if ((item[7].ToString().Equals(null)) || item[7].ToString().Equals(""))
                    {
                        retorno.ReconhecimentoFirma = null;
                    }
                    else
                    {
                        retorno.ReconhecimentoFirma = Convert.ToDateTime(item[7]);
                    }
                    if ((item[8].ToString().Equals(null)) || item[8].ToString().Equals(""))
                    {
                        retorno.TerminoContrato = null;
                    }
                    else
                    {
                        retorno.TerminoContrato = Convert.ToDateTime(item[8]);
                    }
                    if ((item[9].ToString().Equals(null)) || item[9].ToString().Equals(""))
                    {
                        retorno.RecebimentoNotificacaoCertidao = null;
                    }
                    else
                    {
                        retorno.RecebimentoNotificacaoCertidao = Convert.ToDateTime(item[9]);
                    }
                    
                        retorno.Status = Convert.ToString(item[10]);
                    
                    if ((item[11].ToString().Equals(null)) || item[11].ToString().Equals(""))
                    {
                        retorno.ExclusaoAcessos = null;
                    }
                    else
                    {
                        retorno.ExclusaoAcessos = Convert.ToDateTime(item[11]);
                    }

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

        public bool SolicitarDescredenciamento(HttpPostedFileBase file, Descredenciamento descredenciamento)
        {
            descredenciamento.CartaSolicitacao = ConvertToBytes(file);

            SqlConnection connection = new SqlConnection(connectioString);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("INSERT INTO [VIVO_SIM].[dbo].[DESCREDENCIAMENTO_CADASTRO]" +
                         " ([TIPO_DESCREDENCIAMENTO] " +
                         " ,[MOTIVO] " +
                         " ,[ADABAS] " +
                         " ,[TIPO_ARQUIVO] " +
                         " ,[EXTENSAO_ARQUIVO] " +
                         " ,[IDACESSO] " +
                         " ,[DATA_CADASTRO] " +
                         " ,[ARQUIVO_DESCREDENCIAMENTO] " +
                         " ) VALUES " +
                         "('" + descredenciamento.TipoDescredenciamento + "'" +
                         ",'" + descredenciamento.Motivo + "'" +
                         ",'" + descredenciamento.Adabas + "'" +
                         ",'" + descredenciamento.TipoArquivo + "'" +
                         ",'" + descredenciamento.ExtensaoArquivo + "'" +
                         ", '1'" +
                         ",     GETDATE() " +
                         " ," + "(@binaryValue))", connection))
            {
                cmd.Parameters.Add("@binaryValue", SqlDbType.Image).Value = descredenciamento.CartaSolicitacao;
                cmd.ExecuteNonQuery();
            }

            try
            {
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AlterarDescredenciamentoPorInatividade(Descredenciamento descredenciamento)
        {

            string SQL = "UPDATE [VIVO_SIM].[dbo].[DESCREDENCIAMENTO_INATIVIDADE] SET " +
                         "   [SOLICITACAO_PARCEIRO] = GETDATE()" +
                         ",  [DATA_SOLICITACAO_PARCEIRO] = GETDATE()" +
                         ",  [INFORME] = '" + descredenciamento.Inatividade.Informe + "' " +
                         ",  [LIBERADO_CHANCELA] = '" + descredenciamento.Inatividade.LiberadoChancela + "'" +
                         ",  [DATA_LIBERADO_CHANCELA] = GETDATE()" +
                         ",  [CHANCELA] = '" + descredenciamento.Inatividade.Chancela + "'" +
                         ",  [DATA_CHANCELA] = GETDATE()" +
                         ",  [ASSINATURA_DIVISAO] = '" + descredenciamento.Inatividade.AssinaturaDivisao + "'" +
                         ",  [DATA_ASSINATURA_DIVISAO] = GETDATE()" +
                         ",  [ASSINATURA_DIRETORIA] = '" + descredenciamento.Inatividade.AssinaturaDiretoria + "'" +
                         ",  [DATA_ASSINATURA_DIRETORIA] = GETDATE()" +
                         ",  [RECONHECIMENTO_FIRMA] = '" + descredenciamento.Inatividade.ReconhecimentoFirma + "'" +
                         ",  [DATA_RECONHECIMENTO_FIRMA] = GETDATE()" +
                         ",  [ENVIO_TERRITORIO] = '" + descredenciamento.Inatividade.EnvioTerritorio + "'" +
                         ",  [DATA_ENVIO_TERRITORIO] = GETDATE()" +
                         ",  [RECEBIMENTO_NOTIFICACAO_CERTIDAO] = '" + descredenciamento.Inatividade.RecebimentoNotificacaoCertidao + "'" +
                         ",  [DATA_RECEBIMENTO_NOTIFICACAO_CERTIDAO] = GETDATE()" +
                         ",  [STATUS] = '" + descredenciamento.Inatividade.Status + "'" +
                         ",  [DATA_DATA_CERTIDAO] = GETDATE()" +
                         ",  [DATA_PREVISTA_DESCREDENCIAMENTO] = '" + descredenciamento.Inatividade.DataPrevistaDescredenciamento + "'" +
                         ",  [DATA_DATA_PREVISTA_DESCREDENCIAMENTO] = GETDATE()" +
                         ",  [EXCLUSAO_ACESSOS] = '" + descredenciamento.Inatividade.ExclusaoAcessos + "'" +
                         ",  [DATA_EXCLUSAO_ACESSOS] = GETDATE()" +
                         " WHERE [ID_DESCREDENCIAMENTO] = '" + descredenciamento.Inatividade.Id + "'";
            
            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AlterarDescredenciamentoAmigavel(Descredenciamento descredenciamento)
        {
              string SQL = "UPDATE [VIVO_SIM].[dbo].[DESCREDENCIAMENTO_AMIGAVEL] SET " +
                  "   [SOLICITACAO_PARCEIRO] = GETDATE()" +
                  ",  [DATA_SOLICITACAO_PARCEIRO] = GETDATE()" +
                  ",  [INFORME] = '" + descredenciamento.Amigavel.Informe + "' " +
                  ",  [LIBERADO_CHANCELA] = '" + descredenciamento.Amigavel.LiberadoChancela + "'" +
                  ",  [DATA_LIBERADO_CHANCELA] = GETDATE()" +
                  ",  [DATA_CHANCELA_DISTRATO] = '" + descredenciamento.Amigavel.ChancelaDistrato + "'" +
                  ",  [ASSINATURA_DIVISAO] = '" + descredenciamento.Amigavel.AssinaturaDivisao + "'" +
                  ",  [DATA_ASSINATURA_DIVISAO] = GETDATE()" +
                  ",  [ASSINATURA_DIRETORIA] = '" + descredenciamento.Amigavel.AssinaturaDiretoria + "'" +
                  ",  [DATA_ASSINATURA_DIRETORIA] = GETDATE()" +
                  ",  [RECONHECIMENTO_FIRMA] = '" + descredenciamento.Amigavel.ReconhecimentoFirma + "'" +
                  ",  [DATA_RECONHECIMENTO_FIRMA] = GETDATE()" +
                  ",  [ENVIO_TERRITORIO] = '" + descredenciamento.Amigavel.EnvioTerritorio + "'" +
                  ",  [DATA_ENVIO_TERRITORIO] = GETDATE()" +
                  ",  [RECEBIMENTO_DISTRATO] = '" + descredenciamento.Amigavel.RecebimentoDistrato + "'" +
                  ",  [DATA_RECEBIMENTO_DISTRATO] = GETDATE()" +
                  ",  [STATUS] = '" + descredenciamento.Amigavel.Status + "'" +
                  ",  [EXCLUSAO_ACESSOS] = '" + descredenciamento.Amigavel.ExclusaoAcessos + "'" +
                  ",  [DATA_EXCLUSAO_ACESSOS] = GETDATE()" +
                  " WHERE [ID_DESCREDENCIAMENTO] = '" + descredenciamento.Amigavel.Id + "'";
            

            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AlterarDescredenciamentoPorVigencia(Descredenciamento descredenciamento)
        {
            string SQL = "UPDATE [VIVO_SIM].[dbo].[DESCREDENCIAMENTO_VIGENCIA_CONTRATO] SET " +
                         "   [SOLICITACAO_PARCEIRO] = GETDATE()" +
                         ",  [DATA_SOLICITACAO_PARCEIRO] = GETDATE()" +
                         ",  [INFORME] = '" + descredenciamento.Vigencia.Informe + "' " +
                         ",  [LIBERADO_CHANCELA] = '" + descredenciamento.Vigencia.LiberadoChancela + "'" +
                         ",  [DATA_LIBERADO_CHANCELA] = GETDATE()" +
                         ",  [CHANCELA] = '" + descredenciamento.Vigencia.Chancela + "'" +
                         ",  [DATA_CHANCELA] = GETDATE()" +
                         ",  [ASSINATURA_DIVISAO] = '" + descredenciamento.Vigencia.AssinaturaDivisao + "'" +
                         ",  [DATA_ASSINATURA_DIVISAO] = GETDATE()" +
                         ",  [ASSINATURA_DIRETORIA] = '" + descredenciamento.Vigencia.AssinaturaDiretoria + "'" +
                         ",  [DATA_ASSINATURA_DIRETORIA] = GETDATE()" +
                         ",  [RECONHECIMENTO_FIRMA] = '" + descredenciamento.Vigencia.ReconhecimentoFirma + "'" +
                         ",  [DATA_RECONHECIMENTO_FIRMA] = GETDATE()" +
                         ",  [TERMINO_CONTRATO] = '" + descredenciamento.Vigencia.TerminoContrato + "'" +
                         ",  [DATA_TERMINO_CONTRATO] = GETDATE()" +
                         ",  [ENVIO_RECEBIMENTO_NOTIFICACAO_CERTIDAO] = '" + descredenciamento.Vigencia.RecebimentoNotificacaoCertidao + "'" +
                         ",  [DATA_ENVIO_RECEBIMENTO_NOTIFICACAO_CERTIDAO] = GETDATE()" +
                         ",  [STATUS] = '" + descredenciamento.Vigencia.Status + "'" +
                         ",  [EXCLUSAO_ACESSOS] = '" + descredenciamento.Vigencia.ExclusaoAcessos + "'" +
                         ",  [DATA_EXCLUSAO_ACESSOS] = GETDATE()" +
                         " WHERE [ID_DESCREDENCIAMENTO] = '" + descredenciamento.Vigencia.Id + "'";
            
            try
            {
                return dao.ExecutarSql(SQL);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Descredenciamento> buscarDescredenciamento(string Rede, string Adabas, string TipoDescredenciamento, string Status)
        {
            ParceiroRepositorio Parceiro = new ParceiroRepositorio();

            string SQL = "SELECT [ID] " +
                         ", [ADABAS] " +
                         ", [TIPO_DESCREDENCIAMENTO] " +
                         ", [MOTIVO] " +
                         ", [ARQUIVO_DESCREDENCIAMENTO] " +
                         ", [TIPO_ARQUIVO] " +
                         " FROM VIVO_SIM.dbo.DESCREDENCIAMENTO_CADASTRO";

            if (Rede == "TODOS" || Rede != null && TipoDescredenciamento == null || TipoDescredenciamento == "TODOS" && Status == null || Status == "TODOS")
            {
                SQL = SQL + " WHERE [ADABAS] = '" + Adabas + "'";
            }
            else if (Status == "TODOS")
            {
                SQL = SQL + " WHERE [ADABAS] = '" + Adabas + "'" +
                    " AND [TIPO_DESCREDENCIAMENTO] = '" + TipoDescredenciamento + "'";
            }
            else if (TipoDescredenciamento == "TODOS")
            {
                SQL = SQL + " WHERE [ADABAS] = '" + Adabas + "'" +
                    " AND [STATUS] = '" + Status + "'";
            }
            else
            {
                SQL = SQL + " WHERE [ADABAS] = '" + Adabas + "'" +
                    " AND [TIPO_DESCREDENCIAMENTO] = '" + TipoDescredenciamento + "'" +
                    " AND [STATUS] = '" + Status + "'";
            }

            DataTable dsDescredenciamento = null;
            List<Descredenciamento> retorno = new List<Descredenciamento>();

            dsDescredenciamento = this.dao.returnaDataTable(SQL);

            if (dsDescredenciamento.Rows.Count > 0)
            {
                foreach (DataRow item in dsDescredenciamento.Rows)
                {
                    Descredenciamento Descred = new Descredenciamento();
                    Descred.Id = Convert.ToInt32(item[0]);
                    Descred.Parceiro = Parceiro.ObterInformacoesParceiros(item[1].ToString());
                    Descred.TipoDescredenciamento = item[2].ToString();
                    Descred.Motivo = item[3].ToString();

                    retorno.Add(Descred);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public DataTable buscarDescredenciamentoPorId(string Id)
        {
            string SQL = " SELECT [ID] " +
                         ", VIVO_GRC.dbo.[TAB_CADASTRO_GERAL_PDVS].[UF] " +
                         ", [ADABAS] " +
                         ", [TIPO_DESCREDENCIAMENTO] " +
                         ", [MOTIVO] " +
                         ", [STATUS] " +
                         ", [ARQUIVO_DESCREDENCIAMENTO] " +
                         ", VIVO_GRC.dbo.[TAB_CADASTRO_GERAL_PDVS].[REDE] " +
                         ", [RAZAO_SOCIAL] " +
                         ", VIVO_GRC.dbo.[TAB_CADASTRO_GERAL_PDVS].CNPJ " +
                         ",[DATA_CADASTRO] " +
                         ",[TIPO_ARQUIVO] " +
                         ",[EXTENSAO_ARQUIVO] " + 
                    "FROM VIVO_SIM.dbo.DESCREDENCIAMENTO_CADASTRO INNER JOIN VIVO_GRC.dbo.[TAB_CADASTRO_GERAL_PDVS] " +
                    "ON(VIVO_SIM.dbo.DESCREDENCIAMENTO_CADASTRO.ADABAS = VIVO_GRC.dbo.[TAB_CADASTRO_GERAL_PDVS].VENDEDOR) " +
                    "WHERE [ID] = '" + Id + "'";

            return this.dao.returnaDataTable(SQL);
        }

        public DataTable buscarArquivoPorId(string Id)
        {
            string SQL = "SELECT [ARQUIVO_DESCREDENCIAMENTO] " +
                         " FROM VIVO_SIM.dbo.DESCREDENCIAMENTO_CADASTRO" +
                         " WHERE [ID] = '" + Id + "'";

            return this.dao.returnaDataTable(SQL);
        }

        public List<string> ObterCNPJ(string Adabas)
        {
            string SQL = " SELECT DISTINCT [CNPJ] " +
            " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
            " LEFT JOIN Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE] " +
            " ON ([Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].COD_IBGE = Vivo_DE_PARA.[dbo].[TAB MUNICIPIOS IBGE].COD_IBGE) " +
            " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') " +
            " AND GESTOR IS NOT NULL " +
            " AND GESTOR NOT LIKE '%SEM GC%' " +
            " AND [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS].[VENDEDOR] = '" + Adabas + "'";

            DataTable dsDescredenciamento = null;
            List<string> retorno = new List<string>();

            dsDescredenciamento = this.dao.returnaDataTable(SQL);

            if (dsDescredenciamento.Rows.Count > 0)
            {
                foreach (DataRow item in dsDescredenciamento.Rows)
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

        public bool VerificarAdabas(Descredenciamento d)
        {
            string SQL = "SELECT ADABAS FROM [Vivo_SIM].[dbo].[DESCREDENCIAMENTO_CADASTRO] WHERE ADABAS = '" + d.Adabas + "'";

            DataTable dsDescredenciamento = null;
            List<string> retorno = new List<string>();

            dsDescredenciamento = this.dao.returnaDataTable(SQL);

            if (dsDescredenciamento.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}