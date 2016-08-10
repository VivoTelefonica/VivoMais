using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class AcessoRepositorio
    {
        DAO dao;

        public AcessoRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public Acesso ObterColaborador(string Login, string Senha)
        {
            string SQL = "SELECT [Vivo_SIM].[dbo].[ACESSO].[idAcesso] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Login] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Nome] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Email] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Regional] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU].[DescricaoMenu] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU].[TipoAcesso]  ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Imagem] ";
            SQL = SQL + " FROM [Vivo_SIM].[dbo].[ACESSO] ";
            SQL = SQL + " INNER JOIN [Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU] ";
            SQL = SQL + " ON ([Vivo_SIM].[dbo].[ACESSO].[idAcesso] = [Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU].[idAcesso]) ";
            SQL = SQL + " WHERE [Login] = '" + Login + "' ";
            SQL = SQL + " AND [Senha] = (CONVERT(VARBINARY(150), '" + Senha + "'))";
            
            DataTable dsColaborador = null;
            Acesso retorno = new Acesso();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    retorno.IdAcesso = Convert.ToInt32(item[0]);
                    retorno.Login = Convert.ToString(item[1]);
                    retorno.Nome = Convert.ToString(item[2]);
                    retorno.Email = Convert.ToString(item[3]);
                    retorno.Regional = Convert.ToString(item[4]);
                    retorno.Funcao = Convert.ToString(item[5]);
                    retorno.Perfil = Convert.ToString(item[6]);
                    retorno.Imagem = Convert.ToString(item[7]);
                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        public Acesso ObterColaborador(string Login)
        {
            string SQL = "SELECT [Vivo_SIM].[dbo].[ACESSO].[idAcesso] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Login] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Nome] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Email] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO].[Regional] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU].[DescricaoMenu] ";
            SQL = SQL + "       ,[Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU].[TipoAcesso]  ";
            SQL = SQL + " FROM [Vivo_SIM].[dbo].[ACESSO] ";
            SQL = SQL + " INNER JOIN [Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU] ";
            SQL = SQL + " ON ([Vivo_SIM].[dbo].[ACESSO].[idAcesso] = [Vivo_SIM].[dbo].[ACESSO_PERMISSAO_MENU].[idAcesso]) ";
            SQL = SQL + " WHERE [Login] = '" + Login + "' ";

            DataTable dsColaborador = null;
            Acesso retorno = new Acesso();

            dsColaborador = this.dao.returnaDataTable(SQL);

            if (dsColaborador.Rows.Count > 0)
            {
                foreach (DataRow item in dsColaborador.Rows)
                {
                    retorno.IdAcesso = Convert.ToInt32(item[0]);
                    retorno.Login = Convert.ToString(item[1]);
                    retorno.Nome = Convert.ToString(item[2]);
                    retorno.Email = Convert.ToString(item[3]);
                    retorno.Regional = Convert.ToString(item[4]);
                    retorno.Funcao = Convert.ToString(item[5]);
                    retorno.Perfil = Convert.ToString(item[6]);
                }

                return retorno;
            }
            else
            {
                return null;
            }
        }
    }
}
