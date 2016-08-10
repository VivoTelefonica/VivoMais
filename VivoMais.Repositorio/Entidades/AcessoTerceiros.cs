using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivoMais.Repositorio.Entidades
{
    public class AcessoTerceiros
    {
        public int Id { get; set; }

        public Parceiro Parceiro { get; set; }

        //Extraído ou não
        public string Extraido { get; set; }

        public DateTime DataCadastro { get; set; }

        public int Sla { get; set; }

        [Required(ErrorMessage = "AÇÃO: campo obrigatório")]
        public string Acao { get; set; }

        [Required(ErrorMessage = "TIPO DE TERCEIRO: campo obrigatório")]
        public string TipoTerceiro { get; set; }

        [Required(ErrorMessage = "NOME: campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "SOBRENOME: campo obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "SEXO: campo obrigatório")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "CPF: campo obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "RG: campo obrigatório")]
        public string Rg { get; set; }

        [Required(ErrorMessage = "ÓRGÃO EMISSOR: campo obrigatório")]
        public string OrgaoEmissor { get; set; }

        [Required(ErrorMessage = "DATA DE NASCIMENTO: campo obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "ESTADO: campo obrigatório")]
        [Editable(false)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "SUB-GRUPO: campo obrigatório")]
        public string SubGrupo { get; set; }

        [Required(ErrorMessage = "DATA INICIO ATIVIDADE: campo obrigatório")]
        public string DtAtividadeIni { get; set; }

        [Required(ErrorMessage = "DATA FIM ATIVIDADE: campo obrigatório")]
        public string DtAtividadeFim { get; set; }

        [Required(ErrorMessage = "ÁREA: campo obrigatório")]
        public string Area { get; set; }

        [Required(ErrorMessage = "SUBÁREA: campo obrigatório")]
        public string SubArea { get; set; }

        [Required(ErrorMessage = "DDD: campo obrigatório")]
        public string Ddd { get; set; }

        [Required(ErrorMessage = "PERFIL: campo obrigatório")]
        public string Perfil { get; set; }

        [Required(ErrorMessage = "MATRICULA: campo obrigatório")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "FUNÇÃO: campo obrigatório")]
        public string Funcao { get; set; }

        public int Total { get; set; }

        public string Status { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string DataExtracao { get; set; }

        public string MobileToken { get; set; }

        
        //public string Aceite { get; set; }
        //public string Protocolo { get; set; }
        //public string Obs { get; set; }
    }
}
