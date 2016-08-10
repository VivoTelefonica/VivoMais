using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VivoMais.Repositorio.Entidades
{
    public class ResetSenha
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "LOGIN: campo obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "MATRÍCULA: campo obrigatório")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "NOME: campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF: campo obrigatório")]
        public string Cpf { get; set; }
        public DateTime DataSolicitacao { get; set; }

        [Required(ErrorMessage = "DDD: campo obrigatório")]
        public string Ddd { get; set; }

        [Required(ErrorMessage = "UF: campo obrigatório")]
        public string Uf { get; set; }


    }
}
