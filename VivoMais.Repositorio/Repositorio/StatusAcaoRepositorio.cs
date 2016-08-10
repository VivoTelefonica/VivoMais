using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Repositorio.Repositorio
{
    public class StatusAcaoRepositorio
    {
        DAO dao;

        public StatusAcaoRepositorio()
        {
            this.dao = DAO.Instance;
        }


        public StatusAcao ObterTipoMidiaId(int id)
        {
            string SQL =  " SELECT [id] " +
                          "    ,[descricao] " +
                          " FROM [Vivo_SIM].[dbo].[ACAO_STATUS] " +
                          " WHERE id = " + id;

            DataTable dsStatus = null;
            StatusAcao retorno = null;

            dsStatus = this.dao.returnaDataTable(SQL);

            if (dsStatus.Rows.Count > 0)
            {
                foreach (DataRow item in dsStatus.Rows)
                {
                    retorno = new StatusAcao();

                    retorno.Id = Convert.ToInt32(item[0]);
                    retorno.Descricao = Convert.ToString(item[1]);
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
