using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class RetornoAcessoUsuarioViewModel
    {
        public List<string> RedeSellIn { get; set; }

        public List<string> DddUsuario { get; set; }

        public List<string> RedePorDDD { get; set; }

        public CadastroUsuario Usuario { get; set; }

        public List<string> ListaDDDUsuario { get; set; }

        public List<string> ListaCanalUsuario { get; set; }

        public Parceiro Parceiro { get; set; }

        public Parceiro ParceiroSellIn { get; set; }
    }
}