using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class TipoMidiaRepositorio
    {
        DAO dao;

        public TipoMidiaRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public TipoMidia ObterTipoMidiaId(int id)
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA].[idTipo] " +
            "   ,UPPER([Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA].[Descricao]) AS Midia " +
            " FROM [Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA] " +
            " WHERE idTipo = " + id +
            " ORDER BY [Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA].[Descricao]";

            DataTable dsMidia = null;
            TipoMidia retorno = null;

            dsMidia = this.dao.returnaDataTable(SQL);

            if (dsMidia.Rows.Count > 0)
            {
                foreach (DataRow item in dsMidia.Rows)
                {
                    retorno = new TipoMidia();

                    retorno.IdTipoMidia = item[0].ToString();
                    retorno.Midia = Convert.ToString(item[1]);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<TipoMidia> ObterTipoMidia() 
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA].[idTipo] " +
            "   ,UPPER([Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA].[Descricao]) AS Midia " +
            " FROM [Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA] " +
            " ORDER BY [Vivo_SIM].[dbo].[ACAO_TIPO_MIDIA].[Descricao]";

            DataTable dsMidia = null;
            List<TipoMidia> retorno = new List<TipoMidia>();

            dsMidia = this.dao.returnaDataTable(SQL);

            if (dsMidia.Rows.Count > 0)
            {
                foreach (DataRow item in dsMidia.Rows)
                {
                    TipoMidia midia = new TipoMidia();
                    midia.IdTipoMidia = item[0].ToString();
                    midia.Midia = Convert.ToString(item[1]);

                    retorno.Add(midia);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public TipoMidia ObterMidiaDetalhadaId(string Midia)
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[idTipo] " +
                         " ,UPPER([Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[Descricao]) AS MidiaDetalhada " +
                         " FROM [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA] " +
                         " WHERE [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[idTipo] = " + Midia +
                         " ORDER BY [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[Descricao] ";

            DataTable dsMidia = null;
            TipoMidia retorno = null;

            dsMidia = this.dao.returnaDataTable(SQL);

            if (dsMidia.Rows.Count > 0)
            {
                foreach (DataRow item in dsMidia.Rows)
                {
                    retorno = new TipoMidia();

                    retorno.IdMidiaDetalhada = item[0].ToString();
                    retorno.MidiaDetalhada = Convert.ToString(item[1]);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<TipoMidia> ObterMidiaDetalhada(string Midia)
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[idTipo] " +
                         " ,UPPER([Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[Descricao]) AS MidiaDetalhada " +
                         " FROM [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA] " +
                         " WHERE [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[idTipoMidia] = " + Midia +
                         " ORDER BY [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[Descricao] ";

            DataTable dsMidia = null;
            List<TipoMidia> retorno = new List<TipoMidia>();

            dsMidia = this.dao.returnaDataTable(SQL);

            if (dsMidia.Rows.Count > 0)
            {
                foreach (DataRow item in dsMidia.Rows)
                {
                    TipoMidia midia = new TipoMidia();
                    midia.IdMidiaDetalhada = item[0].ToString();
                    midia.MidiaDetalhada = Convert.ToString(item[1]);

                    retorno.Add(midia);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public TipoMidia ObterMidiaDetalhadaNome(string Midia, string MidiaDetalhada)
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[idTipo] " +
            " ,UPPER([Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[Descricao]) AS MidiaDetalhada " +
            " FROM [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA] " +
            " WHERE [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[Descricao] = '" + MidiaDetalhada +"'" +
            " AND [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[idTipoMidia] = '" + Midia + "'" +
            " ORDER BY [Vivo_SIM].[dbo].[ACAO_MIDIA_DETALHADA].[Descricao] ";

            DataTable dsMidia = null;
            TipoMidia midia = null;

            dsMidia = this.dao.returnaDataTable(SQL);

            if (dsMidia.Rows.Count > 0)
            {
                foreach (DataRow item in dsMidia.Rows)
                {
                    midia = new TipoMidia();
                    midia.IdMidiaDetalhada = item[0].ToString();
                    midia.MidiaDetalhada = Convert.ToString(item[1]);
                }

                return midia;
            }
            else
            {
                return null;
            }
        }
    }
}
