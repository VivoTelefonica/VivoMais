using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivoMais.Repositorio.Entidades
{
    public class AcaoMarketing
    {
        public int Id { get; set; }
        public Parceiro Parceiro { get; set; }
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "TIPO DE MÍDIA: campo obrigatório")]
        public TipoMidia Midia { get; set; }

        [Required(ErrorMessage = "MÍDIA DETALHADA: campo obrigatório")]
        public TipoMidia MidiaDetalhada { get; set; }

        [Required(ErrorMessage = "VÉICULO: campo obrigatório")]
        public string Veiculo { get; set; }

        [Required(ErrorMessage = "DESCRIÇÃO DA AÇÃO: campo obrigatório")]
        public string DescricaoAcao { get; set; }

        [Required(ErrorMessage = "*")]
        public string Dia1 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia2 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia3 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia4 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia5 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia6 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia7 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia8 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia9 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia10 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia11 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia12 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia13 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia14 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia15 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia16 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia17 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia18 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia19 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia20 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia21 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia22 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia23 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia24 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia25 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia26 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia27 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia28 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia29 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia30 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Dia31 { get; set; }

        [Required(ErrorMessage = "CAMPANHA MACRO: campo obrigatório")]
        public string CampanhaMacro { get; set; }

        [Required(ErrorMessage = "MÊS DA AÇÃO: campo obrigatório")]
        public string MesAcao { get; set; }

        [Required(ErrorMessage = "MÍDIA EXCLUSIVA: campo obrigatório")]
        public string MidiaExclusivaVivo { get; set; }

        [Required(ErrorMessage = "VALOR TOTAL DA AÇÃO: campo obrigatório")]
        public string ValorTotalAcao { get; set; }

        [Required(ErrorMessage = "VALOR TOTAL DA VIVO: campo obrigatório")]
        public string ValorTotalVivo { get; set; }

        [Required(ErrorMessage = "PERCENTUAL DE PARTICIPAÇÃO: campo obrigatório")]
        public string PercentualParticipacaoVivo { get; set; }

        public string Justificativa { get; set; }

        [Required(ErrorMessage = "FOCO PRINCIPAL: campo obrigatório")]
        public string FocoPrincipal { get; set; }

        [Required(ErrorMessage = "QUANTIDADE: campo obrigatório")]
        public int QtdAcoes { get; set; }

        public int Insercoes { get; set; }

        [Required(ErrorMessage = "ORIGEM DE VERBA: campo obrigatório")]
        public string OrigemVerba { get; set; }

        public string Observacoes { get; set; }

        public int IdAcesso { get; set; }

        public int IdAcessoCadastro { get; set; }

        [Required(ErrorMessage = "FORMATO DO MATERIAL: campo obrigatório")]
        public string FormatoMaterial { get; set; }

        [Required(ErrorMessage = "CUSTO DO MATERIAL: campo obrigatório")]
        public string CustoUnitario { get; set; }

        public DateTime DataFinalizacao { get; set; }

        public string Protocolo { get; set; }

        [Required(ErrorMessage = "NOME DO CONTATO DA REDE: campo obrigatório")]
        public string NomeContatoRede { get; set; }

        [Required(ErrorMessage = "TELEFONE DO CONTATO DA REDE: campo obrigatório")]
        public string TelefoneContatoRede { get; set; }

        [Required(ErrorMessage = "E-MAIL DO CONTATO DA REDE: campo obrigatório")]
        public string EmailContatoRede { get; set; }
        public string Consideracao { get; set; }

        [Required(ErrorMessage = "*")]
        public string DtPreencherDiasIni { get; set; }

        [Required(ErrorMessage = "*")]
        public string DtPreencherDiasFim { get; set; }

        public StatusAcao Status { get; set; }

        public string PercentualRede { get; set; }

        public string ValorTotalRede { get; set; }

        public AcaoAcompanhamento Acompanhamento { get; set; }
    }
}