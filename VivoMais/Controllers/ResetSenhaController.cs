using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;
using VivoMais.Models;
using System.Data;


namespace VivoMais.Controllers
{
    public class ResetSenhaController : Controller
    {
        private ParceiroRepositorio _repositorio;
        private ResetSenhaRepositorio _repositorioResetSenha;
        private AcessoRepositorio _repositorioAcesso;

        // GET: ResetSenha
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                _repositorio = new ParceiroRepositorio();
                ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
            }
            return View(carregaCheckList());
        }

        public ActionResult Movimentacao()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio = new ParceiroRepositorio();
                    _repositorioResetSenha = new ResetSenhaRepositorio();

                    ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();

                    ResetSenhaTotalViewModel Reset = new ResetSenhaTotalViewModel();
                    Reset.ResetCompleto = _repositorioResetSenha.retornaResetSenhaAberto();

                    _repositorio = null;
                    _repositorioResetSenha = null;

                    return View(Reset);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult ResetSenhaRetorno()
        {
            _repositorio = new ParceiroRepositorio();
            _repositorioResetSenha = new ResetSenhaRepositorio();

            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();

            ResetSenhaTotalViewModel Reset = new ResetSenhaTotalViewModel();
            Reset.ResetCompleto = _repositorioResetSenha.retornaResetSenhaGc();

            _repositorio = null;
            _repositorioResetSenha = null;

            return View(Reset);
        }

        public ActionResult ResetSenhaGestao()
        {

            return View();
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

        public JsonResult ResetSenhaGestaoTotal()
        {
            _repositorioResetSenha = new ResetSenhaRepositorio();

            List<ResetSenhaGraficoViewModel> rsG = new List<ResetSenhaGraficoViewModel>();
            DataTable dt = _repositorioResetSenha.ObterResetSenhaTotalSolicitado();

            List<string> xMes = (from MyRow in dt.AsEnumerable()
                                 select MyRow.Field<string>("MES")).Distinct().ToList();


            for (int i = 0; i < xMes.Count; i++)
            {
                List<DataRow> xRow = dt.Select("MES='" + xMes[i] + "'").ToList();
                List<int> Ddd = retornaDDD();
                int cont = 0;

                while (cont < 15)
                {
                    ResetSenhaGraficoViewModel rs = new ResetSenhaGraficoViewModel();
                    rs.Ddd = Ddd[cont].ToString();
                    rs.Mes = xMes[i];
                    rs.Valor = 0;

                    foreach (DataRow dtRow in xRow)
                    {
                        if (Convert.ToInt32(dtRow["DDD"]) == Ddd[cont])
                        {
                            rs.Ddd = dtRow["DDD"].ToString();
                            rs.Mes = dtRow["MES"].ToString();
                            rs.Valor = decimal.Parse(dtRow["TOTAL"].ToString());
                            break;
                        }
                    }
                    cont++;
                    rsG.Add(rs);
                }
            }

            return Json(rsG, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult RejeiteSenhaGc(string Id, string Motivo)
        {
            _repositorioResetSenha = new ResetSenhaRepositorio();
            _repositorioResetSenha.RetornoAceitaReset(Id, Motivo);

            ResetSenhaTotalViewModel Reset = new ResetSenhaTotalViewModel();
            Reset.ResetCompleto = _repositorioResetSenha.retornaResetSenhaGc();

            _repositorioResetSenha = null;

            return PartialView("PartialViewRetornoReset", Reset);
        }

        public PartialViewResult InsereAceiteSenhaGc(string Id)
        {
            _repositorioResetSenha = new ResetSenhaRepositorio();
            _repositorioResetSenha.InsereAceiteSenhaGc(Id);

            ResetSenhaTotalViewModel Reset = new ResetSenhaTotalViewModel();
            Reset.ResetCompleto = _repositorioResetSenha.retornaResetSenhaGc();

            _repositorio = null;
            _repositorioResetSenha = null;

            return PartialView("PartialViewRetornoReset", Reset);
        }

        public PartialViewResult AtribuirSenha(string Id, string Senha)
        {
            _repositorioResetSenha = new ResetSenhaRepositorio();
            _repositorioResetSenha.AtribuirSenha(Id, Senha);

            ResetSenhaTotalViewModel Reset = new ResetSenhaTotalViewModel();
            Reset.ResetCompleto = _repositorioResetSenha.retornaResetSenhaAberto();

            return PartialView("PartialViewSolicitacaoReset", Reset);
        }

        public PartialViewResult AtribuirMotivo(string Id, string Motivo)
        {
            _repositorioResetSenha = new ResetSenhaRepositorio();
            _repositorioResetSenha.AtribuirMotivo(Id, Motivo);

            ResetSenhaTotalViewModel Reset = new ResetSenhaTotalViewModel();
            Reset.ResetCompleto = _repositorioResetSenha.retornaResetSenhaAberto();


            return PartialView("PartialViewSolicitacaoReset", Reset);
        }

        private ResetSenhaViewModel carregaCheckList()
        {
            ResetSenhaViewModel Reset = new ResetSenhaViewModel();
            Reset.ResetSenhaMovimentacao = new List<ResetSenhaMovimentacao>()
            {
                new ResetSenhaMovimentacao {Sistema = "Atlys", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "Gedoc", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "IDM", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "Ngin Care", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "Platon", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "Portal de Vendas (DW Microstrategy)", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "Programa de Pontos", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "SICS", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "SRE", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "SPN", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "Vivo 360", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "Vivo Net", Checked = false},
                new ResetSenhaMovimentacao {Sistema = "WebVendas", Checked = false}
            };

            return Reset;
        }

        public JsonResult ObterDdd(string uf)
        {
            _repositorio = new ParceiroRepositorio();
            var ddd = _repositorio.ObterDddPorUfLogin(uf, Session["Login"].ToString()).Distinct();
            return Json(ddd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcuraColaboradorMatricula(string matricula)
        {
            _repositorioResetSenha = new ResetSenhaRepositorio();
            ResetSenha colaborador = _repositorioResetSenha.ObterColaborador(matricula);

            return Json(colaborador, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult BuscaReset(ResetSenhaTotalViewModel rSenha)
        {
            _repositorioResetSenha = new ResetSenhaRepositorio();

            ResetSenhaTotalViewModel Reset = new ResetSenhaTotalViewModel();
            Reset.ResetCompleto = _repositorioResetSenha.retornaResetSenha(rSenha.ResetSenha.Ddd, rSenha.Status, Convert.ToDateTime(rSenha.DataIni), Convert.ToDateTime(rSenha.DataFim));

            return PartialView("PartialViewSolicitacaoReset", Reset);
        }

        public PartialViewResult BuscaRetornoReset(ResetSenhaTotalViewModel rSenha)
        {
            _repositorioResetSenha = new ResetSenhaRepositorio();

            ResetSenhaTotalViewModel Reset = new ResetSenhaTotalViewModel();
            Reset.ResetCompleto = _repositorioResetSenha.retornaResetSenha(rSenha.ResetSenha.Ddd, rSenha.Status, Convert.ToDateTime(rSenha.DataIni), Convert.ToDateTime(rSenha.DataFim));

            return PartialView("PartialViewRetornoReset", Reset);
        }

        public ActionResult SolicitaReset(ResetSenhaViewModel rsMovimentacao)
        {
            _repositorio = new ParceiroRepositorio();

            if (ModelState.IsValid)
            {
                _repositorioAcesso = new AcessoRepositorio();
                _repositorioResetSenha = new ResetSenhaRepositorio();

                rsMovimentacao.Acesso = _repositorioAcesso.ObterColaborador(Session["Login"].ToString());

                _repositorioResetSenha.InserirResetSenha(rsMovimentacao.ResetSenha, rsMovimentacao.Acesso.IdAcesso);
                _repositorioResetSenha.InserirResetSenhaMovimentacao(rsMovimentacao.ResetSenhaMovimentacao, rsMovimentacao.ResetSenha.Login);

                
            }

            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();

            return View("Index", carregaCheckList());

        }

        public JsonResult ResetSenhaMediaSLA()
        {

            _repositorioResetSenha = new ResetSenhaRepositorio();

            List<ResetSenhaGraficoViewModel> rsG = new List<ResetSenhaGraficoViewModel>();


            DataTable dt = _repositorioResetSenha.ResetSenhaMediaSLA();

            foreach (DataRow dtRow in dt.Rows)
            {
                ResetSenhaGraficoViewModel rs = new ResetSenhaGraficoViewModel();
                rs.Ddd = dtRow["DDD"].ToString();
                rs.Valor = decimal.Parse(dtRow["SLA"].ToString());
                rsG.Add(rs);
            }

            return Json(rsG, JsonRequestBehavior.AllowGet);
        }
        
    }
}