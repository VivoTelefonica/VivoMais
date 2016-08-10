using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class ResetSenhaViewModel
    {
        public Acesso Acesso { get; set; }
        public ResetSenha ResetSenha { get; set; }
        public List<ResetSenhaMovimentacao> ResetSenhaMovimentacao { get; set; }
    }
}