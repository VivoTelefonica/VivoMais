using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class ParceiroController : Controller
    {
        private ParceiroRepositorio _repositorioParceiro;

        // GET: Parceiro
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObterUf()
        {
            _repositorioParceiro = new ParceiroRepositorio();
            return Json(_repositorioParceiro.ObterUf(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterParceirosRedeLLPP(string Ddd)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            return Json(_repositorioParceiro.ObterParceirosRedeLLPP(Ddd), JsonRequestBehavior.AllowGet);
        }

        public List<string> ObterDdd(string Login)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            return _repositorioParceiro.ObterDdd(Login);
        }

        public JsonResult ObterParceirosRedeVarejo(string Ddd)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            return Json(_repositorioParceiro.ObterParceirosRedeVarejo(Ddd), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterDddPorUf(string uf)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            return Json(_repositorioParceiro.ObterDddPorUf(uf), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterDddPorUfLogin(string uf)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            return Json(_repositorioParceiro.ObterDddPorUfLogin(uf, Session["Login"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterGerenteContasUf(string Uf) 
        {
            _repositorioParceiro = new ParceiroRepositorio();
            return Json(_repositorioParceiro.ObterGerenteContas(Uf, Session["Login"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterGerenteContas(string Ddd, string Canal, string TipoCarteira)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            var GerenteContas = _repositorioParceiro.ObterGerenteContas(Ddd, Canal, TipoCarteira).Distinct();
            return Json(GerenteContas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterGerenteContasDDD(string Ddd)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            var GerenteContas = _repositorioParceiro.ObterGerenteContasDDD(Ddd).Distinct();
            return Json(GerenteContas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterParceirosRede(string Ddd)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            var retorno = _repositorioParceiro.ObterParceirosRedeLogin(Ddd, Session["LOGIN"].ToString());

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterAdabasDDD(string Rede, string Ddd, string TipoVerba)
        {
            _repositorioParceiro = new ParceiroRepositorio();

            return Json(_repositorioParceiro.ObterAdabasDDD(Rede, Ddd, TipoVerba), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterSaldoVpcParceiros(string Rede, string Ddd)
        {
            _repositorioParceiro = new ParceiroRepositorio();

            SaldoRede SaldoParceiro = _repositorioParceiro.ObterValorParceiros(Rede);
            AcaoMarketing ValorInserido = _repositorioParceiro.ObterSaldoParceiros(Rede, "Verba Cooperada");

            SaldoParceiro.Saldo = Convert.ToDecimal(SaldoParceiro.SaldoVpc) + Convert.ToDecimal(ValorInserido.ValorTotalVivo);
            string Valor = SaldoParceiro.Saldo.ToString();
            Valor = Valor.Replace(".", "").Replace(",", "");

            return Json(Valor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterInformacoesParceiros(string Adabas)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            Parceiro retorno = _repositorioParceiro.ObterInformacoesParceiros(Adabas);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}