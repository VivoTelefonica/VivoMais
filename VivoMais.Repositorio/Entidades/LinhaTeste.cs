using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class LinhaTeste
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "COLABORADOR: campo obrigatório")]
        public string Colaborador { get; set; }

        [Required(ErrorMessage = "E-MAIL: campo obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "TIPO: campo obrigatório")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "MODALIDADE: campo obrigatório")]
        public string Modalidade { get; set; }

        [Required(ErrorMessage = "PLANO: campo obrigatório")]
        public string Plano { get; set; }

        [Required(ErrorMessage = "DIRETORIA: campo obrigatório")]
        public string Diretoria { get; set; }

        [Required(ErrorMessage = "USUARIO DA LINHA: campo obrigatório")]
        public string UsuarioLinha { get; set; }

        [Required(ErrorMessage = "STATUS: campo obrigatório")]
        public string StatusLinha { get; set; }

        [Required(ErrorMessage = "LINHA: campo obrigatório")]
        public string Linha { get; set; }

        [Required(ErrorMessage = "ICCID: campo obrigatório")]
        public string Iccid { get; set; }

        [Required(ErrorMessage = "CANAL: campo obrigatório")]
        public string Canal { get; set; }

        [Required(ErrorMessage = "DATA INICIO: campo obrigatório")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "DATA FIM: campo obrigatório")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "LOCALIDADE: campo obrigatório")]
        public string Localidade { get; set; }

        [Required(ErrorMessage = "NÚMERO DO FORMULARIO: campo obrigatório")]
        public string NumeroFormulario { get; set; }

        [Required(ErrorMessage = "REGIONAL: campo obrigatório")]
        public string Regional { get; set; }

        [Required(ErrorMessage = "STATUS: campo obrigatório")]
        public string Status { get; set; }
        public string Obs { get; set; }
        public string Extensao { get; set; }
    }
}