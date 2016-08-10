using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class BookPlanosRepositorio
    {
        DAO dao;

        public BookPlanosRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public List<BookPlanos> ObterBookPlanos7x(string Plano)
        {
            string SQL = "SELECT [PLANO] " +
            "      ,[NOMENCLATURA SISTÊMICA VIVO 360] " +
            "      ,[PREÇO ASSINATURA] " +
            "      ,[PREÇO FRANQUIA] " +
            "      ,[VALOR TOTAL] " +
            "      ,[NOME PLANO SAP TABELA MAIS VIVO] " +
            "      ,[NOME PLANO SAP TABELA RENOVA] " +
            "      ,[FRANQUIA OUTRAS OPERADORAS  - LIGAÇÃO LOCAL] " +
            "      ,[FRANQUIA DE DADOS] " +
            "      ,[N° DE MULTIVIVOS] " +
            "      ,[MULTIVIVOS COMPATÍVEIS] " +
            "      ,[CÓD# HOMOLOGAÇÃO] " +
            "      ,[MATRIZ OFERTA] " +
            "      ,[FIDELIZAÇÃO/ CONTRATO] " +
            "      ,[VALOR DA MULTA] " +
            "      ,[PACOTE ADICIONAL INTERNET] " +
            "      ,[BLOQUEIO DE DADOS] " +
            "      ,[PROMOÇÕES VIGENTES] " +
            "      ,[VALIDADE DAS PROMOÇÕES] " +
            "  FROM [Vivo_SIM].[dbo].[BOOK_PLANOS_7X] ";

            if(Plano != "TODOS")
            {
                SQL = SQL + "WHERE [PLANO] LIKE '%" + Plano + "%'";
            }

            DataTable dsBookPlanos = null;
            List<BookPlanos> retorno = new List<BookPlanos>();

            dsBookPlanos = this.dao.returnaDataTable(SQL);

            if (dsBookPlanos.Rows.Count > 0)
            {
                foreach (DataRow item in dsBookPlanos.Rows)
                {
                    BookPlanos plano = new BookPlanos();
                    plano.Plano = Convert.ToString(item[0]);
                    plano.NomenclaturaSistemicaVivo360 = Convert.ToString(item[1]);
                    plano.PrecoAssinatura = Convert.ToString(item[2]);
                    plano.PrecoFranquia = Convert.ToString(item[3]);
                    plano.ValorTotal = Convert.ToString(item[4]);
                    plano.NomePlanoSapTabelaMaisVivo = Convert.ToString(item[5]);
                    plano.NomePlanoSapTabelaRenova = Convert.ToString(item[6]);
                    plano.FranquiaOutrasOperadorasLigaçãoLocal = Convert.ToString(item[7]);
                    plano.FranquiaDados = Convert.ToString(item[8]);
                    plano.QtdMultivivos = Convert.ToString(item[9]);
                    plano.MultivivosCompatIveis = Convert.ToString(item[10]);
                    plano.CodigoHomologacao = Convert.ToString(item[11]);
                    plano.MatrizOferta = Convert.ToString(item[12]);
                    plano.FidelizaçãoContrato = Convert.ToString(item[13]);
                    plano.ValorMulta = Convert.ToString(item[14]);
                    plano.PacoteAdicionalInternet = Convert.ToString(item[15]);
                    plano.BloqueioDados = Convert.ToString(item[16]);
                    plano.PromoçõesVigentes = Convert.ToString(item[17]);
                    plano.ValidadePromoções = Convert.ToString(item[18]);

                    retorno.Add(plano);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

        }

        public List<BookPlanos> ObterBookPlanos8x(string Plano)
        {
            string SQL = "SELECT [PLANO] " +
            "      ,[NOMENCLATURA SISTÊMICA VIVO 360] " +
            "      ,[PREÇO ASSINATURA] " +
            "      ,[PREÇO FRANQUIA] " +
            "      ,[VALOR TOTAL] " +
            "      ,[NOME PLANO SAP TABELA MAIS VIVO] " +
            "      ,[NOME PLANO SAP TABELA RENOVA] " +
            "      ,[FRANQUIA OUTRAS OPERADORAS  - LIGAÇÃO LOCAL] " +
            "      ,[FRANQUIA DE DADOS] " +
            "      ,[N° DE MULTIVIVOS] " +
            "      ,[MULTIVIVOS COMPATÍVEIS] " +
            "      ,[CÓD# HOMOLOGAÇÃO] " +
            "      ,[MATRIZ OFERTA] " +
            "      ,[FIDELIZAÇÃO/ CONTRATO] " +
            "      ,[VALOR DA MULTA] " +
            "      ,[PACOTE ADICIONAL INTERNET] " +
            "      ,[BLOQUEIO DE DADOS] " +
            "      ,[PROMOÇÕES VIGENTES] " +
            "      ,[VALIDADE DAS PROMOÇÕES] " +
            "  FROM [Vivo_SIM].[dbo].[BOOK_PLANOS_8X] ";

            if(Plano != "TODOS")
            {
                SQL = SQL + "WHERE [PLANO] LIKE '%" + Plano + "%'";
            }

            DataTable dsBookPlanos = null;
            List<BookPlanos> retorno = new List<BookPlanos>();

            dsBookPlanos = this.dao.returnaDataTable(SQL);

            if (dsBookPlanos.Rows.Count > 0)
            {
                foreach (DataRow item in dsBookPlanos.Rows)
                {
                    BookPlanos plano = new BookPlanos();
                    plano.Plano = Convert.ToString(item[0]);
                    plano.NomenclaturaSistemicaVivo360 = Convert.ToString(item[1]);
                    plano.PrecoAssinatura = Convert.ToString(item[2]);
                    plano.PrecoFranquia = Convert.ToString(item[3]);
                    plano.ValorTotal = Convert.ToString(item[4]);
                    plano.NomePlanoSapTabelaMaisVivo = Convert.ToString(item[5]);
                    plano.NomePlanoSapTabelaRenova = Convert.ToString(item[6]);
                    plano.FranquiaOutrasOperadorasLigaçãoLocal = Convert.ToString(item[7]);
                    plano.FranquiaDados = Convert.ToString(item[8]);
                    plano.QtdMultivivos = Convert.ToString(item[9]);
                    plano.MultivivosCompatIveis = Convert.ToString(item[10]);
                    plano.CodigoHomologacao = Convert.ToString(item[11]);
                    plano.MatrizOferta = Convert.ToString(item[12]);
                    plano.FidelizaçãoContrato = Convert.ToString(item[13]);
                    plano.ValorMulta = Convert.ToString(item[14]);
                    plano.PacoteAdicionalInternet = Convert.ToString(item[15]);
                    plano.BloqueioDados = Convert.ToString(item[16]);
                    plano.PromoçõesVigentes = Convert.ToString(item[17]);
                    plano.ValidadePromoções = Convert.ToString(item[18]);

                    retorno.Add(plano);
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