using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VivoMais.Repositorio.Entidades;

namespace VivoMais.Models
{
    public class AcessoTerceiroViewModel
    {
        public List<AcessoTerceiros> TerceirosAberto { get; set; }

        [Required(ErrorMessage = "Gerente de Contas: campo obrigatório")]
        public string Gestor { get; set; }

        public Parceiro Parceiro { get; set; }

        [Required(ErrorMessage = "Data Inicial: campo obrigatório")]
        public string DataIni { get; set; }

        [Required(ErrorMessage = "Data Final: campo obrigatório")]
        public string DataFim { get; set; }

        public string Status { get; set; }
        
        public string Observacao { get; set; }
        
    }
}