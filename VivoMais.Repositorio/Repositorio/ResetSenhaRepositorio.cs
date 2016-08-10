using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class ResetSenhaRepositorio
    {
        DAO dao;

        public ResetSenhaRepositorio()
        {
            this.dao = DAO.Instance;
        }

        public ResetCompleto ObterTempoReset(string Login)
        {
            string SQL = " SELECT " +
            "    SUM(DATEDIFF (DD, DATA_SOLICITACAO,DATA_REJEITE))  AS TOTAL_REJEITE " +
            "   ,SUM(DATEDIFF (DD, DATA_SOLICITACAO,DATA_SENHA))  AS TOTAL_LIBERACAO " +
            "   ,COUNT(*) as TOTAL " +
            " FROM [Vivo_VPC].[dbo].[RESET_SENHA] INNER JOIN [Vivo_VPC].[dbo].[RESET_SENHA_SISTEMAS] " +
            " ON ([Vivo_VPC].[dbo].[RESET_SENHA].ID = [Vivo_VPC].[dbo].[RESET_SENHA_SISTEMAS].[ID_RESET]) " +
            " INNER JOIN [Vivo_VPC].[dbo].[ACESSO] " +
            " ON ([Vivo_VPC].[dbo].[RESET_SENHA].idAcesso = [Vivo_VPC].[dbo].[ACESSO].idAcesso) " +
            " WHERE [Vivo_VPC].[dbo].[ACESSO].Login = '" + Login + "' ";

            DataTable dsResetSenha = null;
            ResetCompleto retorno = new ResetCompleto();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    retorno.Total = Convert.ToDecimal(item[2]);
                    string Valor = (Convert.ToDecimal(item[1]) / Convert.ToDecimal(item[2])).ToString("N2") ;
                    retorno.TotalLiberado = Convert.ToDecimal(Valor);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public DataTable ResetSenhaMediaSLA()
        {
            string SQL = " SELECT [DDD] " +
                         "       ,CONVERT(DECIMAL(18,2),(SUM( CONVERT(DECIMAL(18,2),DATEDIFF(DD,[DATA_SOLICITACAO],[Data_Senha])))/CONVERT(DECIMAL(18,2),COUNT(*)))) AS SLA " +
                         " FROM [Vivo_VPC].[dbo].[RESET_SENHA_SISTEMAS] INNER JOIN [Vivo_VPC].[dbo].[RESET_SENHA] " +
                         " ON ([Vivo_VPC].[dbo].[RESET_SENHA_SISTEMAS].[ID_RESET] = [Vivo_VPC].[dbo].[RESET_SENHA].[ID]) " +
                         " WHERE [SENHA] IS NOT NULL " +
                         " GROUP BY [DDD] " +
                         " ORDER BY [DDD] ";

            return dao.returnaDataTable(SQL);
        }

        public DataTable ObterResetSenhaTotalSolicitado()
        {
            string SQL = " SELECT [DDD] " +
                         "        ,CASE WHEN MONTH(DATA_SOLICITACAO) = 1 THEN 'Jan' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 2 THEN 'Fev' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 3 THEN 'Mar' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 4 THEN 'Abr' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 5 THEN 'Mai' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 6 THEN 'Jun' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 7 THEN 'Jul' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 8 THEN 'Ago' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 9 THEN 'Set' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 10 THEN 'Out' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 11 THEN 'Nov' " +
                         "         WHEN MONTH(DATA_SOLICITACAO) = 12 THEN 'Dez' " +
	                     "         ELSE '' END AS MES" +
	                     "        ,COUNT(*) AS TOTAL " +
                         " FROM [Vivo_VPC].[dbo].[RESET_SENHA] INNER JOIN [Vivo_VPC].[dbo].[RESET_SENHA_SISTEMAS] " +
                         " ON ([Vivo_VPC].[dbo].[RESET_SENHA].ID = [Vivo_VPC].[dbo].[RESET_SENHA_SISTEMAS].[ID_RESET]) " +
                         " WHERE DATA_SOLICITACAO BETWEEN DATEADD(m,-2, GETDATE()) AND GETDATE() " +
                         " GROUP BY  " +
                         "     MONTH(DATA_SOLICITACAO)  " +
                         "    ,YEAR(DATA_SOLICITACAO)  " +
                         "    ,DDD " +
                        " ORDER BY Year(DATA_SOLICITACAO),month(DATA_SOLICITACAO) ASC ";

            return dao.returnaDataTable(SQL);
        }

        public ResetSenhaMovimentacao ObterResetSenhaMovimentacaoID(int id)
        {
            string SQL = " SELECT  [ID] " +
                         "        ,[IdReset] " +
                         "        ,[Sistema] " +
                         "        ,[Data] " +
                         "        ,[Senha] " +
                         "        ,[MotivoRejeite] " +
                         "        ,[DataRejeite] " +
                         "        ,[DataSenha] " +
                         "        ,[DataRejeiteGc] " +
                         "        ,[MotivoRejeiteGc] " +
                         "        ,[DataAceiteGc] " +
                         "        ,DATEDIFF(DD,[Data],GETDATE()) as Sla " +
                         "     FROM [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] " +
                         "     WHERE ID = " + id;

            DataTable dsResetSenha = null;
            ResetSenhaMovimentacao retorno = new ResetSenhaMovimentacao();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    retorno.Id = Convert.ToInt32(item[0]);
                    retorno.IdReset = Convert.ToInt32(item[1]);
                    retorno.Sistema = Convert.ToString(item[2]);
                    retorno.Data = Convert.ToDateTime(item[3]);
                    retorno.Senha = Convert.ToString(item[4]);
                    retorno.MotivoRejeite = Convert.ToString(item[5]);
                    retorno.DataRejeite = string.IsNullOrEmpty(item[6].ToString()) ? (DateTime?)null : Convert.ToDateTime(item[6]);
                    retorno.DataSenha = string.IsNullOrEmpty(item[7].ToString()) ? (DateTime?)null : Convert.ToDateTime(item[7]);
                    retorno.DataRejeiteGc = string.IsNullOrEmpty(item[8].ToString()) ? (DateTime?)null : Convert.ToDateTime(item[8]);
                    retorno.MotivoRejeiteGc = Convert.ToString(item[9]);
                    retorno.DataAceiteGc = string.IsNullOrEmpty(item[10].ToString()) ? (DateTime?)null : Convert.ToDateTime(item[10]);
                    retorno.Sla = Convert.ToInt32(item[11]);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public List<ResetCompleto> retornaResetSenhaAberto()
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS].[Id] " +
                         " , [IdReset] " +
                         " FROM [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] INNER JOIN [Vivo_SIM].[dbo].[RESET_SENHA] " +
                         " ON([Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS].[IdReset] = [Vivo_SIM].[dbo].[RESET_SENHA].[ID]) " +
                         " WHERE [Senha] IS NULL " +
                         " AND [MotivoRejeite] IS NULL";

            DataTable dsResetSenha = null;
            List<ResetCompleto> retorno = new List<ResetCompleto>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    ResetCompleto reset = new ResetCompleto();
                    reset.ResetMov = ObterResetSenhaMovimentacaoID(Convert.ToInt32(item[0]));
                    reset.Reset = ObterResetSenhaID(Convert.ToInt32(item[1]));
                    retorno.Add(reset);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
            
            //DateTime today = DateTime.Now;



            //var res = _context.ResetSenhaMovimentacao.Where(x => ((x.Senha == null && x.MotivoRejeite == null) || (x.MotivoRejeiteGc != null))).Select(p => new { idReset = p.IdReset, Id = p.Id, Data = p.Data, Sistema = p.Sistema }).ToList();
            //List<ResetSenhaMovimentacao> Resets = res.Select(t => new ResetSenhaMovimentacao() { IdReset = t.idReset, Sla = today.Subtract(t.Data).Days, Id = t.Id, Data = t.Data, Sistema = t.Sistema }).ToList();


            //List<ResetCompleto> Total = new List<ResetCompleto>();
            //foreach (ResetSenhaMovimentacao r in Resets)
            //{
            //    ResetCompleto t = new ResetCompleto();
            //    t.ResetMov = r;
            //    t.Reset = ObterColaboradorID(r.IdReset);
            //    Total.Add(t);
            //}
            //return Total;
        }

        public void InsereAceiteSenhaGc(string Id)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] " +
                         " SET [DataAceiteGc] = GetDate() " +
                         " WHERE [ID] = " + Id;

            this.dao.ExecutarSql(SQL);
        }

        public void AtribuirSenha(string Id, string Senha)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] " +
                         " SET [Senha] = '" + Senha.Replace("'", "''") + "'" +
                         ",[DataSenha] = GetDate() " +
                         " WHERE [ID] = " + Id;

            this.dao.ExecutarSql(SQL);
        }

        public void AtribuirMotivo(string Id, string Motivo)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] " +
                         " SET [MotivoRejeite] = '" + Motivo + "'" +
                         ",[DataRejeite] = GetDate() " +
                         " WHERE [ID] = " + Id;

            this.dao.ExecutarSql(SQL);
        }

        public List<ResetCompleto> retornaResetSenhaGc()
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS].[Id] " +
                         " , [IdReset] " +
                         " FROM [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] INNER JOIN [Vivo_SIM].[dbo].[RESET_SENHA] " +
                         " ON([Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS].[IdReset] = [Vivo_SIM].[dbo].[RESET_SENHA].[ID]) " +
                         " WHERE (([Senha] IS NOT NULL) OR ([MotivoRejeite] IS NOT NULL)) " +
                         " AND DataAceiteGc IS NULL " +
                         " AND MotivoRejeiteGc IS NULL ";

            DataTable dsResetSenha = null;
            List<ResetCompleto> retorno = new List<ResetCompleto>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    ResetCompleto reset = new ResetCompleto();
                    reset.ResetMov = ObterResetSenhaMovimentacaoID(Convert.ToInt32(item[0]));
                    reset.Reset = ObterResetSenhaID(Convert.ToInt32(item[1]));
                    retorno.Add(reset);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

            //var res = _context.ResetSenhaMovimentacao.Where(x => x.Senha != null || x.MotivoRejeite != null).Where(x => x.DataAceiteGc == null).Select(p => new { idReset = p.IdReset, MotivoRejeite = p.MotivoRejeite, Senha = p.Senha, DataAceiteGc = p.DataAceiteGc, Id = p.Id, Data = p.Data, Sistema = p.Sistema }).ToList();
            //List<ResetSenhaMovimentacao> Resets = res.Select(t => new ResetSenhaMovimentacao() { IdReset = t.idReset, MotivoRejeite = t.MotivoRejeite, Senha = t.Senha, DataAceiteGc = t.DataAceiteGc, Id = t.Id, Data = t.Data, Sistema = t.Sistema }).ToList();

            //List<ResetCompleto> Total = new List<ResetCompleto>();
            //foreach (ResetSenhaMovimentacao r in Resets)
            //{
            //    ResetCompleto t = new ResetCompleto();
            //    t.ResetMov = r;
            //    //t.Reset = ObterColaboradorID(r.IdReset);
            //    Total.Add(t);
            //}
            //return Total;
        }

        public List<ResetCompleto> retornaResetSenha(string Ddd, string Status, DateTime dtIni, DateTime dtFim)
        {
            string SQL = " SELECT [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS].[Id] " +
             " , [IdReset] " +
             " FROM [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] INNER JOIN [Vivo_SIM].[dbo].[RESET_SENHA] " +
             " ON([Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS].[IdReset] = [Vivo_SIM].[dbo].[RESET_SENHA].[ID]) " +
             " WHERE DataSolicitacao BETWEEN CONVERT(datetime,'" + dtIni.ToString("yyyy-dd-MM") + " 00:00:00',103)  AND CONVERT(datetime,'" + dtFim.ToString("yyyy-dd-MM") + " 23:59:59',103)";

            if (Ddd != "TODOS")
            {
                SQL = SQL + " AND [Vivo_SIM].[dbo].[RESET_SENHA].[DDD] = '" + Ddd + "' ";
            }

            if (Status == "CONCLUÍDOS")
            {
                SQL = SQL + " AND [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS].[DataSenha] IS NOT NULL ";
            }
            else if (Status == "REJEITADOS")
            {
                SQL = SQL + " AND [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS].[DataRejeite] IS NOT NULL ";
            }
             

            DataTable dsResetSenha = null;
            List<ResetCompleto> retorno = new List<ResetCompleto>();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    ResetCompleto reset = new ResetCompleto();
                    reset.ResetMov = ObterResetSenhaMovimentacaoID(Convert.ToInt32(item[0]));
                    reset.Reset = ObterResetSenhaID(Convert.ToInt32(item[1]));
                    retorno.Add(reset);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
       }

        public void RetornoAceitaReset(string Id, string Motivo)
        {
            string SQL = " UPDATE [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] " +
             " SET [DataRejeiteGc] = GetDate() " +
             " ,[MotivoRejeiteGc] = '" + Motivo + "' " +
             " WHERE [ID] = " + Id;

            this.dao.ExecutarSql(SQL);
        }

        public ResetSenha ObterColaborador(string matricula)
        {
            string SQL = " SELECT [LOGIN] " +
            " ,[MATRICULA] " +
            " ,[CPF] " +
            " ,[NOME]  " +
            " FROM [Vivo_SIM].[dbo].[RESET_SENHA] " +
            "  WHERE [MATRICULA] = '" + matricula + "'  "; 

            DataTable dsResetSenha = null;
            ResetSenha retorno = null;

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    retorno = new ResetSenha();

                    retorno.Login = Convert.ToString(item[0]);
                    retorno.Matricula = Convert.ToString(item[1]);
                    retorno.Cpf = Convert.ToString(item[2]);
                    retorno.Nome = Convert.ToString(item[3]);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public void InserirResetSenhaMovimentacao(List<ResetSenhaMovimentacao> rs, string login)
        {
            int xLogin = ProcuraId(login);

            foreach (var item in rs)
            {
                if (item.Checked)
                {
                    string SQL = " INSERT INTO [Vivo_SIM].[dbo].[RESET_SENHA_SISTEMAS] ( " +
                                 "  [IdReset] " +
                                 " ,[SISTEMA] " +
                                 " ,[DATA] " +
                                 "  ) " +
                               " VALUES (" +
                                +xLogin +
                               ",'" + item.Sistema + "'" +
                               ",GetDate() " +
                               " ) ";
                    dao.ExecutarSql(SQL);
                }
            }
        }

        public int ProcuraId(string login)
        {
            string SQL = " SELECT MAX(ID) FROM [Vivo_SIM].[dbo].[RESET_SENHA] ";

            DataTable dsResetSenha = null;
            int retorno = 0;

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    retorno = Convert.ToInt32(item[0]);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
        }

        public void InserirResetSenha(ResetSenha rs, int xIdAcesso)
        {
            string SQL = " INSERT INTO [Vivo_SIM].[dbo].[RESET_SENHA] " +
                  " VALUES (" +
                  "'" + rs.Login + "'" +
                  ",'" + rs.Matricula + "'" +
                  ",'" + rs.Nome + "'" +
                  ",'" + rs.Cpf + "'" +
                  ", GetDate() " +
                  ",'" + xIdAcesso + "'" +
                  ",'" + rs.Ddd + "'" +
                  ",'" + rs.Uf + "'" +
                  ") ";

            dao.ExecutarSql(SQL);
        }

        public ResetSenha ObterResetSenhaID(int id)
        {
            string SQL = " SELECT [ID] " +
                              "  ,[Login] " +
                              "  ,[Matricula] " +
                              "  ,[Nome] " +
                              "  ,[Cpf] " +
                              "  ,[DataSolicitacao] " +
                              "  ,[Ddd] " +
                              "  ,[Uf] " +
                          " FROM .[Vivo_SIM].[dbo].[RESET_SENHA] " +
                          " WHERE [ID] = " + id ;   

            DataTable dsResetSenha = null;
            ResetSenha retorno = new ResetSenha();

            dsResetSenha = this.dao.returnaDataTable(SQL);

            if (dsResetSenha.Rows.Count > 0)
            {
                foreach (DataRow item in dsResetSenha.Rows)
                {
                    retorno.Id = Convert.ToInt32(item[0]);
                    retorno.Login = Convert.ToString(item[1]);
                    retorno.Matricula = Convert.ToString(item[2]);
                    retorno.Nome = Convert.ToString(item[3]);
                    retorno.Cpf = Convert.ToString(item[4]);
                    retorno.DataSolicitacao = Convert.ToDateTime(item[5]);
                    retorno.Ddd = Convert.ToString(item[6]);
                    retorno.Uf = Convert.ToString(item[7]);
                }

                return retorno;
            }
            else
            {
                return retorno;
            }

            //var ctx = _context.ResetSenha.Where(c => c.Id == id).Select(c => new { Id = c.Id, Uf = c.Uf, Ddd = c.Ddd, Login = c.Login.ToUpper(), Matricula = c.Matricula.ToUpper(), Cpf = c.Cpf.ToUpper(), Nome = c.Nome.ToUpper() }).ToList();
            //ResetSenha rs = null;

            //foreach (var item in ctx)
            //{
            //    rs = new ResetSenha();
            //    rs.Id = item.Id;
            //    rs.Ddd = item.Ddd;
            //    rs.Uf = item.Uf;
            //    rs.Login = item.Login;
            //    rs.Matricula = item.Matricula;
            //    rs.Cpf = item.Cpf;
            //    rs.Nome = item.Nome;
            //}
            //return rs;
        }
    }
}
