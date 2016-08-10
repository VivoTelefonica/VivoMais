using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class LinhaTesteViewModel
    {
        public LinhaTeste LinhaTeste { get; set; }

        public List<LinhaTeste> ListaLinhaTeste { get; set; }
    }

}