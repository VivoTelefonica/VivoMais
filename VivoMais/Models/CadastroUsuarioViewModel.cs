using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class CadastroUsuarioViewModel
    {
        public CadastroUsuario CadastroUsuario { get; set; }

        public List<string> Redes { get; set; }

        public List<string> DDDs { get; set; }
    }
}