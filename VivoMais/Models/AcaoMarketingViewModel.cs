using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class AcaoMarketingViewModel
    {

        public AcaoMarketing AcaoMarketing { get; set; }

        public List<AcaoMarketing> ListaAcoes { get; set; }

        [Required(ErrorMessage = "SALDO: campo obrigatório")]
        public string ValorVerba { get; set; }


        
    }
}