using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivoMais.Repositorio.Entidades
{
    public class SaldoRede
    {
        public string Rede { get; set; }
        public decimal SaldoVpc { get; set; }
        public decimal SaldoVio { get; set; }

        [Required(ErrorMessage = "ADABAS: campo obrigatório")]
        public string Regional { get; set; }
        public decimal Saldo { get; set; }
        public decimal Usado { get; set; }
        public string Verba { get; set; }

        [Required(ErrorMessage = "Mês: campo obrigatório")]
        public string Mes { get; set; }
        
        [Required(ErrorMessage = "Ano: campo obrigatório")]
        public string Ano { get; set; }
    }
}
