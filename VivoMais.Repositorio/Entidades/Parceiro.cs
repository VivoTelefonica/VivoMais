using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VivoMais.Repositorio.Entidades
{
    public class Parceiro
    {
        [Key]
        [Required(ErrorMessage = "ADABAS: campo obrigatório")]
        public string Vendedor { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DataInicioContrato { get; set; }
        public DateTime? DataFimContrato { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string InscricaoEstadual { get; set; }
        public string OptanteSimples { get; set; }
        public string Regional { get; set; }
        public string Territorial { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Bairro  { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Contato { get; set; }
        public string Celular { get; set; }
        public string Telefone { get; set; }
        public string Fax { get; set; }
        public string EmailFinanceiro { get; set; }
        public string EmailComercial { get; set; }
        public string EmailDivulgacao { get; set; }
        public string HomePage { get; set; }
        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public string DigitoVerificador { get; set; }
        public string RegiaoGeografica { get; set; }
        public string SetorAtividade { get; set; }
        public string AreaCredito { get; set; }
        public string OrganizacaoCompras { get; set; }
        public string OrganizacaoVendas { get; set; }
        public string GrupoVendedor { get; set; }
        public string PagadorFatura { get; set; }
        public string RecebedorMercadoria { get; set; }
        public string RecebedorFatura { get; set; }
        public string CodigoVendedor { get; set; }
        public string Regional2 { get; set; }
        public string Territorial2 { get; set; }

        [Required(ErrorMessage = "UF: campo obrigatório")]
        public string Uf { get; set; }
        public string RecebedorComissionamento { get; set; }
        public string PontoVenda { get; set; }

        [Required(ErrorMessage = "GERENTE DE CONTAS: campo obrigatório")]
        public string Gestor { get; set; }
        public DateTime? DataCadastroGestor { get; set; }
        public string SellIn { get; set; }
        public DateTime? DataCadastroSellIn { get; set; }
        public string StatusCallidus { get; set; }
        public string Instancia { get; set; }
        public string Atividade { get; set; }
        public string Limite { get; set; }
        public string PrazoLimite { get; set; }
        public string MargemAparelho { get; set; }
        public string MargemChip { get; set; }
        public string MargemRecarga { get; set; }
        public string CondicoesPagamento { get; set; }
        public string Vpc { get; set; }
        public string AtualEstrelagem { get; set; }
        public DateTime? DataCartSellIn { get; set; }
        public DateTime? DataCartGestor { get; set; }
        public string CanalDistribuicao { get; set; }

        [Required(ErrorMessage = "REDE: campo obrigatório")]
        public string Rede { get; set; }

        [Required(ErrorMessage = "CANAL: campo obrigatório")]
        public string Canal { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoFornecedor { get; set; }
        public string MatriculaGerenteConta { get; set; }
        public string CodIbge { get; set; }

        [Required(ErrorMessage = "DDD: campo obrigatório")]
        public string Ddd { get; set; }

       public string UfLocalidadePdv { get; set; }
       public string Ixos { get; set; }
       public string DddGestor { get; set; }
       public string AreaAtuacaoDddGestor { get; set; }
       public string GerenteTerritorial { get; set; }
       public string NovoGestor { get; set; }
       public string Observacao { get; set; }
       public string Saldo { get; set; }
       public string CentroCusto { get; set; }

    }
}
