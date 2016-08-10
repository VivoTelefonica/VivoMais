using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoMais.Models
{
    public class CarteiraGraficoViewModel
    {
        public string Ddd { get; set; }
        public int TotalPreenchido { get; set; }
        public int TotalFaltantes { get; set; }
    }
}