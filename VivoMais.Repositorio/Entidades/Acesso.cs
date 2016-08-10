using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivoMais.Repositorio.Entidades
{
    public class Acesso
    {
        [Key]
        public int IdAcesso { get; set; }

        [Required(ErrorMessage = "LOGIN: campo obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "SENHA: campo obrigatório")]
        public string Senha { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Regional { get; set; }

        public string Perfil { get; set; }

        public string Funcao { get; set; }

        public string Imagem { get; set; }
    }
}
