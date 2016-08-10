using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class SavingViewModel
    {
        public Saving saving { get; set; }

        public List<Saving> listaSaving { get; set; }
    }
}