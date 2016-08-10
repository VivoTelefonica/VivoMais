using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivoMais.Repositorio.Entidades
{
    public class ResetSenhaMovimentacao
    {
        public int Id { get; set; }
        public int IdReset { get; set; }
        public string Sistema { get; set; }
        public bool Checked { get; set; }
        public DateTime Data { get; set; }
        public string Senha { get; set; }
        public string MotivoRejeite { get; set; }
        public DateTime? DataRejeite { get; set; }
        public DateTime? DataSenha { get; set; }
        public DateTime? DataRejeiteGc { get; set; }
        public string MotivoRejeiteGc { get; set; }
        public DateTime? DataAceiteGc { get; set; }
        public int Sla { get; set; }


    }
}
