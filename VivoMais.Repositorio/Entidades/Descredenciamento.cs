using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.IO;
using System.ComponentModel.DataAnnotations;  

namespace VivoMais.Repositorio.Entidades
{
    public class Descredenciamento
    {
        public int Id { get; set; }

        public Parceiro Parceiro { get; set; }

        public DescredenciamentoAmigavel Amigavel { get; set; }

        public DescredenciamentoInatividade Inatividade { get; set; }

        public DescredenciamentoVigencia Vigencia { get; set; }

        [Required(ErrorMessage = "TIPO DE DESCREDENCIAMENTO: campo obrigatório")]
        public string TipoDescredenciamento { get; set; }

        public string Motivo { get; set; }

        public string Adabas { get; set; }

        [Required(ErrorMessage = "STATUS: campo obrigatório")]
        public string Status { get; set; }

        public string PeriodoInicial { get; set; }

        public string PeriodoFinal { get; set; }

        public string TipoArquivo { get; set; }

        public string ExtensaoArquivo { get; set; }

        [Required(ErrorMessage = "CARTA DE SOLICITAÇÃO: campo obrigatório")]
        public byte[] CartaSolicitacao { get; set; }

    }
}


/*
 TESTE GIT
 */