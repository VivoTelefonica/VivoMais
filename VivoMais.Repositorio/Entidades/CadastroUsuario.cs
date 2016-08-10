using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class CadastroUsuario
    {

        [Required(ErrorMessage = "Perfil: campo obrigatório")]
        public String Perfil { get; set; }

        [Required(ErrorMessage = "Tipo de Acesso: campo obrigatório")]
        public String TipoAcesso { get; set; }

        [Required(ErrorMessage = "Login: campo obrigatório")]
        public String Login { get; set; }

        public List<CadastroUsuario> Redes { get; set; }

        public String DDDs { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha: campo obrigatório")]
        public String Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar a senha")]
        [Required, Compare("Senha", ErrorMessage = "As senhas informadas não conferem. Verifique as senhas informadas e tente novamente.")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Nome: campo obrigatório")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Email: campo obrigatório")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$", ErrorMessage = "E-mail em formato inválido.")]
        public String Email { get; set; }

    }
}