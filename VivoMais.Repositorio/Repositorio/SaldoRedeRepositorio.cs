using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class SaldoRedeRepositorio
    {
        DAO dao;

        public SaldoRedeRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public bool DeletarSaldoRede()
        {
            string SQL = "DELETE FROM [Vivo_SIM].[dbo].[SALDO_REDE] ";

            return dao.ExecutarSql(SQL);
        }

        public bool InsereParceiros()
        {
            if(DeletarSaldoRede())
            {
                string SQL = "INSERT INTO [Vivo_SIM].[dbo].[SALDO_REDE] " +
                " SELECT DISTINCT REDE " +
                " ,0 " +
                " ,0 " +
                " ,MONTH(GETDATE()) " +
                " ,YEAR(GETDATE()) " +
                " ,REGIONAL " +
                " ,Getdate()  " +
                " FROM [Vivo_GRC].[dbo].[TAB_CADASTRO_GERAL_PDVS] " +
                " WHERE [STATUS CALLIDUS] IN ('Ativo','ATIVA - EM OPERAÇÃO','ESCRITÓRIO','EM PROCESSO DE CREDENCIAMENTO','Em Credenciamento') ";

                return dao.ExecutarSql(SQL);
            }
            else
            {
                return false;
            }
        }

        public bool InserirSaldoVio(string Rede, string Regional, string Ano, string Mes, string Saldo)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[SALDO_REDE] " +
                " SET [Regional] = '" + Regional + "' " +
                "    ,[Saldo_VIO] = '" + Saldo + "' " +
                "    ,[Ano] = '" + Ano + "' " +
                "    ,[Mes] = '" + Mes + "' " +
                " WHERE [Rede] = '" + Rede + "' " +
                " AND [Regional] = '" + Regional + "' ";

            return dao.ExecutarSql(SQL);
        }

        public bool InserirSaldoVpc(string Rede, string Regional, string Ano, string Mes, string Saldo)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[SALDO_REDE] " +
                " SET [Regional] = '" + Regional + "' " +
                "    ,[Saldo_VPC] = '" + Saldo + "' " +
                "    ,[Ano] = '" + Ano + "' " +
                "    ,[Mes] = '" + Mes + "' " +
                " WHERE [Rede] = '" + Rede + "' " + 
                " AND [Regional] = '" + Regional + "' ";

            return dao.ExecutarSql(SQL);
        }


    }
}