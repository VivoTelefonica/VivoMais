using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;

namespace VivoMais.Models
{
    public class DescredenciamentoViewModel
    {
        public Descredenciamento Descredenciamento { get; set; }

        public List<Descredenciamento> ListaDescredenciamento { get; set; }

        public Parceiro Parceiro { get; set; }
        
        public string CartaSolicitacao { get; set; }

        public string TipoDescredenciamento { get; set; }

        public string Motivo { get; set; }
    }
}