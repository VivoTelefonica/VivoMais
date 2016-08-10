using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Models;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class CarteiraController : Controller
    {
        private CarteiraRepositorio _repositorio;
        private ParceiroRepositorio _repositorioParceiro;

        public ActionResult Index()
        {
            ViewData["MES"] = MesAcao();
            return View();
        }

        public JsonResult AtualizarCarteira(string[][] ArrayCk)
        {
            CarteiraRepositorio _repositorio = new CarteiraRepositorio();
            bool retorno = false;

            for (int i = (ArrayCk.Length - 1); i <= (ArrayCk.Length - 1); i--)
            {
                if (i < 0)
                {
                    break;
                }
                else
                {
                    if (ArrayCk[i][4] == "RECARGA")
                    {
                        if (_repositorio.VerificaGcRevenda(ArrayCk[i][0]))
                        {
                            _repositorio.AtualizarCarteiraMesPDR(ArrayCk[i][0], ArrayCk[i][1], ArrayCk[i][2], ArrayCk[i][3]);
                        }
                        else
                        {
                            _repositorio.AtualizarCarteiraMes(ArrayCk[i][0], ArrayCk[i][1], ArrayCk[i][2], ArrayCk[i][3]);
                        }
                    }
                    else
                    {
                        retorno = _repositorio.AtualizarCarteiraMes(ArrayCk[i][0], ArrayCk[i][1], ArrayCk[i][2], ArrayCk[i][3]);
                    }
                }
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public string[] MesAcao()
        {
            string[] mes = new string[3];

            mes[0] = (CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month - 1) + "/" + DateTime.Now.Year).ToUpper();
            mes[1] = (CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + "/" + DateTime.Now.Year).ToUpper();
            mes[2] = (CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month + 1) + "/" + DateTime.Now.Year).ToUpper();

            return mes;
        }

        public ActionResult AbrirCarteira(CarteiraViewModel Carteira)
        {
            _repositorio = new CarteiraRepositorio();

            if (Carteira.TipoCarteira == "Abrir nova carteira")
            {
                if (_repositorio.AtualizaCarteira(Carteira.MesAbertura))
                {
                    _repositorio.AlterarIntervaloCarteira(Carteira.DataAberturaCarteira, Carteira.DataFechamentoCarteira, Carteira.MesAbertura);
                }
            }
            else
            {
                _repositorio.AlterarIntervaloCarteira(Carteira.DataAberturaCarteira, Carteira.DataFechamentoCarteira, Carteira.MesAbertura);
            }

            ViewData["MES"] = MesAcao();
            return View("Index");
        }

        public ActionResult ConsultarCarteira()
        {
            _repositorio = new CarteiraRepositorio();
            return View(_repositorio.ConsultarCarteira(Session["Login"].ToString()));
        }

        public ActionResult DefinirCarteiras()
        {
            _repositorio = new CarteiraRepositorio();
            _repositorioParceiro = new ParceiroRepositorio();

            ViewData["DDD"] = _repositorioParceiro.ObterDdd(Session["Login"].ToString());

            CarteiraViewModel carteira = new CarteiraViewModel();
            List<Parceiro> p = new List<Parceiro>();

            carteira.ListParceiro = p;

            return View(carteira);
        }

        public PartialViewResult BuscarCarteira(CarteiraViewModel CarteiraVM)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            _repositorio = new CarteiraRepositorio();


            ViewData["GESTOR"] = _repositorioParceiro.ObterGerenteContasDDD(CarteiraVM.Parceiro.Ddd);

            CarteiraViewModel xCarteira = new CarteiraViewModel();
            xCarteira.ListParceiro = _repositorio.RetornarCarteiras(CarteiraVM.Parceiro.Ddd, CarteiraVM.Parceiro.Canal, CarteiraVM.TipoCarteira, CarteiraVM.Parceiro.Gestor);
            return PartialView("PartialViewCarteira", xCarteira);

        }

        public List<int> retornaDDD()
        {
            List<int> Ddds = new List<int>();

            int Ddd = 71;
            Ddds.Add(Ddd);

            Ddd = 73;
            Ddds.Add(Ddd);

            Ddd = 74;
            Ddds.Add(Ddd);

            Ddd = 75;
            Ddds.Add(Ddd);

            Ddd = 77;
            Ddds.Add(Ddd);

            Ddd = 79;
            Ddds.Add(Ddd);

            Ddd = 81;
            Ddds.Add(Ddd);

            Ddd = 82;
            Ddds.Add(Ddd);

            Ddd = 83;
            Ddds.Add(Ddd);

            Ddd = 84;
            Ddds.Add(Ddd);

            Ddd = 85;
            Ddds.Add(Ddd);

            Ddd = 86;
            Ddds.Add(Ddd);

            Ddd = 87;
            Ddds.Add(Ddd);

            Ddd = 88;
            Ddds.Add(Ddd);

            Ddd = 89;
            Ddds.Add(Ddd);

            return Ddds;

        }

        public ActionResult GestaoCarteira()
        {
            return View();
        }

        public JsonResult GraficoCarteira()
        {
            _repositorio = new CarteiraRepositorio();

            List<CarteiraGraficoViewModel> rs = new List<CarteiraGraficoViewModel>();
            DataTable dt = _repositorio.ObterGraficoCarteira();

            List<double> xDdd = (from MyRow in dt.AsEnumerable()
                                 select MyRow.Field<double>("DDD")).Distinct().ToList();

            int cont = 0;
            for (int i = 0; i < xDdd.Count; i++)
            {
                List<DataRow> xRow = dt.Select("DDD='" + xDdd[i] + "'").ToList();
                List<int> Ddd = retornaDDD();

                CarteiraGraficoViewModel c = new CarteiraGraficoViewModel();
                c.Ddd = Ddd[cont].ToString();
                c.TotalFaltantes = 0;
                c.TotalPreenchido = 0;

                foreach (DataRow dtRow in xRow)
                {
                    if (Convert.ToInt32(dtRow["DDD"]) == Ddd[cont])
                    {
                        c.Ddd = dtRow["DDD"].ToString();
                        c.TotalFaltantes = c.TotalFaltantes + Convert.ToInt32(dtRow["FALTANTES"]);
                        c.TotalPreenchido = c.TotalPreenchido + Convert.ToInt32(dtRow["PREENCHIDOS"]);
                    }
                }
                cont++;
                rs.Add(c);

            }

            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}