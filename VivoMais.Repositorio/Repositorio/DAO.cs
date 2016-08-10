using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VivoMais.Repositorio
{
    public class DAO
    {
        SqlConnection conn;
        private static DAO instance;

        public DAO()
        {
            string connectioString = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
            this.conn = new SqlConnection(connectioString);
        }

        public static DAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAO();
                }
                return instance;
            }
        }

        public bool ExecutarSql(string sql)
        {
            // verifica se a conexao esta aberta
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            //Iniciando uma transação.
            SqlTransaction trans = conn.BeginTransaction("myTransaction");
            try
            {
                SqlCommand comando = new SqlCommand(sql, this.conn, trans);
                comando.ExecuteNonQuery();

                //Confirmando a transação.
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //Reverte a transação (Rollback) se acontecer algum erro na transação
                trans.Rollback("myTransaction");
                throw ex;
            }
            finally
            {
                GC.Collect();
                this.conn.Close();
            }
        }

        public SqlDataReader retornaQuery(string sql)
        {
            // verifica se a conexao esta aberta
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();


            //Iniciando uma transação.
            SqlTransaction trans = conn.BeginTransaction("myTransaction");
            try
            {
                SqlCommand comando = new SqlCommand(sql, this.conn, trans);
                SqlDataReader retorno = comando.ExecuteReader();

                //Confirmando a transação.
                return retorno;
            }
            catch (Exception ex)
            {
                //Reverte a transação (Rollback) se acontecer algum erro na transação
                trans.Rollback("myTransaction");
                throw ex;
            }
            finally
            {
                this.conn.Close();
                GC.Collect();
            }
        }

        public void fecharConexao()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public DataTable returnaDataTable(string sql)
        {
            DataTable dTable = null;
            try
            {
                dTable = new DataTable();

                if (this.conn.State == ConnectionState.Open)
                {
                    fecharConexao();
                }

                this.conn.Open();

                SqlCommand build = new SqlCommand();
                SqlDataAdapter adap = new SqlDataAdapter(sql, conn);

                adap.Fill(dTable);

            }
            catch (Exception ex)
            {
                throw new Exception("Não é possível estabelecer a comunicação com o banco de dados." + ex.Message);

            }
            finally
            {
                GC.Collect();
            }

            return dTable;

        }
    }
}
