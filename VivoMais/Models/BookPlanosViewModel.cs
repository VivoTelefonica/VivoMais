using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class BookPlanosViewModel
    {
        public List<BookPlanos> Book { get; set; }
        public string Ddd { get; set; }
        public string Plano { get; set; }
    }
}