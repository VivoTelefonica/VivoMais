using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivoMais.Repositorio.Entidades
{
    public class ResetCompleto
    {
        public ResetSenha Reset { get; set; }
        public ResetSenhaMovimentacao ResetMov { get; set; }

        public decimal TotalRejeitado { get; set; }
        public decimal TotalLiberado { get; set; }
        public decimal Total { get; set; }
    }
}
