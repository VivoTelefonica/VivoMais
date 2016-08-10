using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class Saving
    {
        public int Id { get; set; }

        public string MesAcao { get; set; }

        public Parceiro Parceiro { get; set; }

        public string SaldoFaturado { get; set; }

        public string TipoAcao { get; set; }

        public string OpcaoAcao { get; set; }

        public string Oferta { get; set; }

        public DateTime? PeriodoInicial { get; set; }

        public DateTime? PeriodoFinal { get; set; }

        public string ValorAcao { get; set; }

        public string Descricao { get; set; }

        //public byte[] ComprovanteAcao { get; set; }

        public string ExtensaoArquivo { get; set; }

        public string TipoArquivo { get; set; }

    }
}