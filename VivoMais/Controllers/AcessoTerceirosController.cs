using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Models;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;
using Microsoft.Office.Interop.Excel;

namespace VivoMais.Controllers
{
    public class AcessoTerceirosController : Controller
    {
        private AcessoTerceirosRepositorio _repositorio;
        private ParceiroRepositorio _repositorioParceiro;

        

        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                _repositorioParceiro = new ParceiroRepositorio();
                ViewData["REDE"] = _repositorioParceiro.ObterRedes(Session["Login"].ToString());
            }
            return View();
        }

        public PartialViewResult ObterLoginSenha()
        {
            _repositorio = new AcessoTerceirosRepositorio();

            PrincipalViewModel Principal = new PrincipalViewModel();
            //Principal.Acesso = _repositorio.ObterEsperandoMobileToken(Session["Login"].ToString());
            Principal.SenhaLiberada = _repositorio.ObterLoginSenha(Session["Login"].ToString());

            return PartialView("DivConsultarAcesso", Principal);
        }

        public PartialViewResult ObterEsperandoMobileToken()
        {
            _repositorio = new AcessoTerceirosRepositorio();

            PrincipalViewModel Principal = new PrincipalViewModel();
            //Principal.Acesso = _repositorio.ObterEsperandoMobileToken(Session["Login"].ToString());
            Principal.Acesso = _repositorio.ObterEsperandoMobileToken(Session["Login"].ToString());

            return PartialView("DivMobileToken", Principal);
        }

        public PartialViewResult RetornarAcessosEmAbertoGC(AcessoTerceiroViewModel Acessos)
        {

            _repositorio = new AcessoTerceirosRepositorio();

            _repositorioParceiro = new ParceiroRepositorio();
            ViewData["UF"] = _repositorioParceiro.ObterUf(Session["Login"].ToString());

            System.Data.DataTable dt = _repositorio.ObterAcessosEmAbertoGC(Convert.ToDateTime(Acessos.DataIni), Convert.ToDateTime(Acessos.DataFim), Acessos.Parceiro.Gestor, Acessos.Status);
            AcessoTerceiroViewModel acessoTerceiroTotal = new AcessoTerceiroViewModel();

            List<AcessoTerceiros> acessoTotal = new List<AcessoTerceiros>();

            foreach (DataRow dtRow in dt.Rows)
            {
                AcessoTerceiros at = new AcessoTerceiros();
                at.Nome = Convert.ToString(dtRow["NOME"]);
                at.Matricula = Convert.ToString(dtRow["MATRICULA"]);
                at.Sla = Convert.ToInt32(dtRow["SLA"]);
                at.Login = Convert.ToString(dtRow["LOGIN"]);
                at.Senha = Convert.ToString(dtRow["SENHA"]);
                at.Status = Convert.ToString(dtRow["STATUS"]);

                acessoTotal.Add(at);
            }

            acessoTerceiroTotal.TerceirosAberto = acessoTotal;

            return PartialView("PartialViewInformaGC", acessoTerceiroTotal);
        }

        public PartialViewResult AtualizarAcessoTerceirosConsulta(AcessoTerceiroViewModel Acessos)
        {
            //RetornaAtualizacaoAcessos
            _repositorio = new AcessoTerceirosRepositorio();

            _repositorioParceiro = new ParceiroRepositorio();
            ViewData["UF"] = _repositorioParceiro.ObterUf(Session["Login"].ToString());

            System.Data.DataTable dt = _repositorio.RetornaAtualizacaoAcessos(Acessos.Parceiro.Uf, Acessos.Parceiro.Gestor, Acessos.DataIni, Acessos.DataFim);
            AcessoTerceiroViewModel acessoTerceiroTotal = new AcessoTerceiroViewModel();
            List<AcessoTerceiros> acessoTotal = new List<AcessoTerceiros>();

            foreach (DataRow dtRow in dt.Rows)
            {
                AcessoTerceiros at = new AcessoTerceiros();
                at.Id = Convert.ToInt32(dtRow["ID"]);
                at.Acao = Convert.ToString(dtRow["ACAO"]);
                at.Nome = Convert.ToString(dtRow["NOME"]);
                at.Cpf = Convert.ToString(dtRow["CPF"]);
                at.Perfil = Convert.ToString(dtRow["PERFIL"]);
                at.Matricula = Convert.ToString(dtRow["MATRICULA"]);
                at.DataCadastro = Convert.ToDateTime(dtRow["DataCadastro"]);
                at.DataExtracao = Convert.ToString(dtRow["DataExtracao"]);
                at.MobileToken = Convert.ToString(dtRow["MobileToken"]);
                at.Login = Convert.ToString(dtRow["LOGIN"]);
                at.Senha = Convert.ToString(dtRow["SENHA"]);

                acessoTotal.Add(at);
            }
            acessoTerceiroTotal.TerceirosAberto = acessoTotal;

            return PartialView("PartialViewInformaGC", acessoTerceiroTotal);
        }

        public ActionResult AtualizarAcessoTerceiros()
        {
            _repositorio = new AcessoTerceirosRepositorio();

            _repositorioParceiro = new ParceiroRepositorio();
            ViewData["UF"] = _repositorioParceiro.ObterUf(Session["Login"].ToString());

            System.Data.DataTable dt = _repositorio.RetornaAtualizacaoAcessos();
            AcessoTerceiroViewModel acessoTerceiroTotal = new AcessoTerceiroViewModel();
            List<AcessoTerceiros> acessoTotal = new List<AcessoTerceiros>();

            foreach (DataRow dtRow in dt.Rows)
            {
                AcessoTerceiros at = new AcessoTerceiros();
                at.Id = Convert.ToInt32(dtRow["ID"]);
                at.Acao = Convert.ToString(dtRow["ACAO"]);
                at.Nome = Convert.ToString(dtRow["NOME"]);
                at.Cpf = Convert.ToString(dtRow["CPF"]);
                at.Perfil = Convert.ToString(dtRow["PERFIL"]);
                at.Matricula = Convert.ToString(dtRow["MATRICULA"]);
                at.DataCadastro = Convert.ToDateTime(dtRow["DataCadastro"]);
                at.DataExtracao = Convert.ToString(dtRow["DataExtracao"]);
                at.MobileToken = Convert.ToString(dtRow["MobileToken"]);
                at.Login = Convert.ToString(dtRow["LOGIN"]);
                at.Senha = Convert.ToString(dtRow["SENHA"]);

                acessoTotal.Add(at);
            }
            acessoTerceiroTotal.TerceirosAberto = acessoTotal;

            return View(acessoTerceiroTotal);
        }

        public ActionResult ConsultarAcessosTerceiros()
        {
            _repositorio = new AcessoTerceirosRepositorio();

            _repositorioParceiro = new ParceiroRepositorio();
            ViewData["UF"] = _repositorioParceiro.ObterUf(Session["Login"].ToString());

            System.Data.DataTable dt = _repositorio.ObterAcessosEmAbertoGC("TODOS");
            AcessoTerceiroViewModel acessoTerceiroTotal = new AcessoTerceiroViewModel();

            List<AcessoTerceiros> acessoTotal = new List<AcessoTerceiros>();

            foreach (DataRow dtRow in dt.Rows)
            {
                AcessoTerceiros at = new AcessoTerceiros();
                at.Id = Convert.ToInt32(dtRow["ID"]);
                at.Nome = Convert.ToString(dtRow["NOME"]);
                at.Matricula = Convert.ToString(dtRow["MATRICULA"]);
                at.Sla = Convert.ToInt32(dtRow["SLA"]);
                at.Login = Convert.ToString(dtRow["LOGIN"]);
                at.Senha = Convert.ToString(dtRow["SENHA"]);
                at.Status = Convert.ToString(dtRow["STATUS"]);

                acessoTotal.Add(at);
            }

            acessoTerceiroTotal.TerceirosAberto = acessoTotal;

            return View(acessoTerceiroTotal);
        }

        public JsonResult AceitarSenhaAcesso(string Id)
        {
            _repositorio = new AcessoTerceirosRepositorio();
            var retorno = _repositorio.AceitarSenhaAcesso(Id);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RejeiteSenhaAcesso(string Id)
        {
            _repositorio = new AcessoTerceirosRepositorio();
            var retorno = _repositorio.RejeiteSenhaAcesso(Id);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsereMotivoAcesso(string Id, string Obs)
        {
            _repositorio = new AcessoTerceirosRepositorio();
            var retorno = _repositorio.MotivoRecusaAcesso(Id, Obs);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsereLoginSenhaAcesso(string Id, string Login, string Senha)
        {
            _repositorio = new AcessoTerceirosRepositorio();
            var retorno = _repositorio.InsereLoginSenhaAcesso(Id, Login, Senha);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterGerenteContas(string Uf)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            var GC = _repositorioParceiro.ObterGerenteContas(Uf, Session["Login"].ToString());

            return Json(GC, JsonRequestBehavior.AllowGet);
        }

        private void liberarObjetos(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
                //Response.Write("Ocorreu um erro durante a liberação do objeto " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public JsonResult CarregaAdabas(string Rede)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            var Adabas = _repositorioParceiro.ObterAdabas(Rede, Session["LOGIN"].ToString());
            return Json(Adabas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CarregaInformacoesParceiros(string Adabas)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            Parceiro parceiro = _repositorioParceiro.ObterInformacoesParceiros(Adabas);

            return Json(parceiro, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult RetornarAcessosEmAberto(AcessoTerceiroViewModel Acessos)
        {

            _repositorio = new AcessoTerceirosRepositorio();
            _repositorioParceiro = new ParceiroRepositorio();

            List<Parceiro> GerenteContas = _repositorioParceiro.ObterGerenteContas().ToList();
            List<string> GContas = GerenteContas.OrderBy(x => x.Gestor).Select(x => x.Gestor).ToList();

            ViewData["GC"] = GContas;

            System.Data.DataTable dt = _repositorio.ObterAcessosEmAberto(Convert.ToDateTime(Acessos.DataIni), Convert.ToDateTime(Acessos.DataFim), Acessos.Parceiro.Gestor);

            AcessoTerceiroViewModel acessoTerceiroTotal = new AcessoTerceiroViewModel();

            List<AcessoTerceiros> acessoTotal = new List<AcessoTerceiros>();

            foreach (DataRow dtRow in dt.Rows)
            {
                Parceiro p = new Parceiro();
                p.Uf = dtRow["UF"].ToString();
                p.Gestor = dtRow["GESTOR"].ToString();

                AcessoTerceiros at = new AcessoTerceiros();
                at.Total = Convert.ToInt32(dtRow["TOTAL"]);
                at.Extraido = Convert.ToString(dtRow["EXTRAIDO"]);
                at.Parceiro = p;

                acessoTotal.Add(at);
            }

            acessoTerceiroTotal.TerceirosAberto = acessoTotal;
            return PartialView("PartialViewExtrairHumanos", acessoTerceiroTotal);
        }

        public FilePathResult ExtraiPlanilhaTerceiros(string Uf, string Gc, string DataIni, string DataFim)
        {
            _repositorio = new AcessoTerceirosRepositorio();

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
            {
                process.Kill();
            }

            Application xlApp = new Application();
            Workbook xlWorkBook;
            Worksheet xlWorkSheet = new Worksheet();
            object misValue = System.Reflection.Missing.Value;
            System.Data.DataTable dt = null;

            if (Uf != null)
            {
                dt = _repositorio.ExtrairSolicitacoesAcessos(Uf, Gc, DataIni, DataFim);
            }

            int i = 4;

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    xlWorkBook = xlApp.Workbooks.Open(@"C:\VivoSimArquivos\Layout-Externo.xlsx");
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(2);


                    foreach (DataRow item in dt.Rows)
                    {
                        xlWorkSheet.Cells[i, 4] = "T4 Dealers";
                        xlWorkSheet.Cells[i, 8] = "-";
                        xlWorkSheet.Cells[i, 10] = "0 Solt";
                        xlWorkSheet.Cells[i, 14] = "BR Brasileiro";
                        xlWorkSheet.Cells[i, 22] = "BR Brasil";
                        xlWorkSheet.Cells[i, 27] = "NÃO INFORMADO";
                        xlWorkSheet.Cells[i, 28] = "NÃO INFORMADO";
                        xlWorkSheet.Cells[i, 29] = "NÃO INFORMADO";
                        xlWorkSheet.Cells[i, 30] = "NÃO INFORMADO";
                        xlWorkSheet.Cells[i, 31] = "2 Ens Méd/Profissional";
                        xlWorkSheet.Cells[i, 32] = "129   Outro(a)";
                        xlWorkSheet.Cells[i, 33] = "1 Completo";
                        xlWorkSheet.Cells[i, 43] = "TOD";
                        xlWorkSheet.Cells[i, 44] = "10000911";


                        if (Convert.ToString(Session["id"]) == "a0029088")
                        {
                            xlWorkSheet.Cells[i, 45] = "29088";
                        }
                        else if (Convert.ToString(Session["id"]) == "a0043723")
                        {
                            xlWorkSheet.Cells[i, 45] = "43723";
                        }
                        else
                        {
                            xlWorkSheet.Cells[i, 45] = "29088";
                        }

                        xlWorkSheet.Cells[i, 48] = "A Ativo";
                        xlWorkSheet.Cells[i, 49] = "80000027    VENDEDOR";
                        xlWorkSheet.Cells[i, 50] = "80000064    SERVIÇOS - VENDAS";
                        xlWorkSheet.Cells[i, 51] = "TBRA";
                        xlWorkSheet.Cells[i, 54] = "8.00";
                        xlWorkSheet.Cells[i, 58] = "2 Não";
                        xlWorkSheet.Cells[i, 59] = "1 Sim";
                        xlWorkSheet.Cells[i, 60] = "2 Não";
                        xlWorkSheet.Cells[i, 68] = "-";
                        xlWorkSheet.Cells[i, 70] = "2 Contratada";
                        xlWorkSheet.Cells[i, 71] = "1 Sim";
                        xlWorkSheet.Cells[i, 72] = "2 Não";

                        if (Convert.ToString(item[1]) != "1   Inclusão")
                        {
                            xlWorkSheet.Cells[i, 3] = Convert.ToString(item[39]).ToUpper(); //"T4 Dealers";
                        }


                        xlWorkSheet.Cells[i, 2] = Convert.ToString(item[1]);
                        xlWorkSheet.Cells[i, 5] = Convert.ToString(item[4]).ToUpper();
                        xlWorkSheet.Cells[i, 6] = Convert.ToString(item[5]).ToUpper();
                        xlWorkSheet.Cells[i, 7] = Convert.ToString(item[6]);

                        string xx = Convert.ToString(item[9]).ToUpper();
                        xx = xx.Replace("-", "");
                        xx = xx.Replace(".", "");
                        xlWorkSheet.Cells[i, 12] = xx;

                        string rg = Convert.ToString(item[7]).ToUpper();
                        rg = rg.Replace("-", "");
                        rg = rg.Replace(".", "");
                        xlWorkSheet.Cells[i, 9] = rg;

                        string cpf = Convert.ToString(item[8]).ToUpper();
                        cpf = cpf.Replace("-", "");
                        cpf = cpf.Replace(".", "");
                        xlWorkSheet.Cells[i, 11] = cpf;

                        DateTime data = Convert.ToDateTime(item[10]);
                        xlWorkSheet.Cells[i, 13] = data.ToString("yyyyMMdd");

                        xlWorkSheet.Cells[i, 15] = Convert.ToString(item[11]).ToUpper();
                        xlWorkSheet.Cells[i, 16] = Convert.ToString(item[12]).ToUpper();
                        xlWorkSheet.Cells[i, 17] = Convert.ToString(item[13]).ToUpper();
                        xlWorkSheet.Cells[i, 18] = Convert.ToString(item[14]).ToUpper();
                        xlWorkSheet.Cells[i, 19] = Convert.ToString(item[15]).ToUpper();

                        string cep = Convert.ToString(item[16]).ToUpper();
                        if (!cep.Contains("-"))
                        {
                            cep = cep.Substring(0, cep.Length - 3) + "-" + cep.Substring(cep.Length - 3, 3);
                        }

                        xlWorkSheet.Cells[i, 20] = cep;

                        xlWorkSheet.Cells[i, 21] = Convert.ToString(item[17]).ToUpper();
                        xlWorkSheet.Cells[i, 23] = Convert.ToString(item[18]).ToUpper();

                        string tel = Convert.ToString(item[19]).ToUpper();
                        tel = tel.Replace("-", "");
                        xlWorkSheet.Cells[i, 24] = tel;

                        string cel = Convert.ToString(item[20]).ToUpper();
                        cel = cel.Replace("-", "");
                        xlWorkSheet.Cells[i, 25] = cel;


                        xlWorkSheet.Cells[i, 40] = Convert.ToString(item[21]).ToUpper();
                        xlWorkSheet.Cells[i, 41] = Convert.ToString(item[22]).ToUpper();
                        xlWorkSheet.Cells[i, 42] = Convert.ToString(item[23]).ToUpper();

                        long Data = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
                        xlWorkSheet.Cells[i, 46] = Data.ToString();
                        xlWorkSheet.Cells[i, 47] = Convert.ToString(Data + 20000);

                        xlWorkSheet.Cells[i, 52] = Convert.ToString(item[26]).ToUpper();
                        xlWorkSheet.Cells[i, 53] = Convert.ToString(item[27]).ToUpper();
                        xlWorkSheet.Cells[i, 61] = Convert.ToString(item[28]).ToUpper() + " NE";
                        i = i + 1;
                    }

                    if (System.IO.File.Exists(@"C:\VivoSimArquivos\SolicitacaoAcessosExterno.xls"))
                    {
                        System.IO.File.Delete(@"C:\VivoSimArquivos\SolicitacaoAcessosExterno.xls");
                    }

                    xlWorkBook.SaveAs(@"C:\VivoSimArquivos\SolicitacaoAcessosExterno.xls", XlFileFormat.xlExcel12, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    liberarObjetos(xlWorkSheet);
                    liberarObjetos(xlWorkBook);
                    liberarObjetos(xlApp);
                }
            }
            return File(@"C:\VivoSimArquivos\SolicitacaoAcessosExterno.xls", "application/vnd.ms-excel", "SolicitacaoAcessosExterno.xls");
        }

        public ActionResult ExtrairHumanos()
        {
            _repositorioParceiro = new ParceiroRepositorio();
            _repositorio = new AcessoTerceirosRepositorio();

            List<Parceiro> GerenteContas = _repositorioParceiro.ObterGerenteContas().ToList();
            List<string> GContas = GerenteContas.OrderBy(x => x.Gestor).Select(x => x.Gestor).ToList();

            ViewData["GC"] = GContas;
            System.Data.DataTable dt = _repositorio.ObterAcessosEmAberto();

            AcessoTerceiroViewModel acessoTerceiroTotal = new AcessoTerceiroViewModel();
            List<AcessoTerceiros> acessoTotal = new List<AcessoTerceiros>();

            foreach (DataRow dtRow in dt.Rows)
            {
                Parceiro p = new Parceiro();
                p.Uf = dtRow["UF"].ToString();
                p.Gestor = dtRow["GESTOR"].ToString();

                AcessoTerceiros at = new AcessoTerceiros();
                at.Total = Convert.ToInt32(dtRow["TOTAL"]);
                at.Extraido = Convert.ToString(dtRow["EXTRAIDO"]);
                at.Sla = Convert.ToInt32(dtRow["SLA"]);
                at.DataCadastro = Convert.ToDateTime(dtRow["DATACADASTRO"]);
                at.Parceiro = p;

                acessoTotal.Add(at);
            }

            acessoTerceiroTotal.TerceirosAberto = acessoTotal;

            return View(acessoTerceiroTotal);
        }

        public ActionResult CadastraTerceiros(AcessoTerceiros acesso)
        {
            _repositorioParceiro = new ParceiroRepositorio();
            _repositorio = new AcessoTerceirosRepositorio();
            _repositorio.cadastrarAcessosTerceiros(acesso);

            ViewData["REDE"] = _repositorioParceiro.ObterRedes(Session["Login"].ToString());

            return View("Index");

        }

        [HttpGet]
        public virtual ActionResult Download(string Tipo, string Caminho)
        {
            return File(Caminho, "application/vnd.ms-excel", Tipo);
        }

    }
}