using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class ResetSenhaTotalViewModel
    {
        public ResetSenha ResetSenha { get; set; }
        public List<ResetCompleto> ResetCompleto { get; set; }

        [Required(ErrorMessage = "Data Inicial: campo obrigatório")]
        public string DataIni { get; set; }

        [Required(ErrorMessage = "Data Final: campo obrigatório")]
        public string DataFim { get; set; }

        [Required(ErrorMessage = "Status: campo obrigatório")]
        public string Status { get; set; }
    }
}