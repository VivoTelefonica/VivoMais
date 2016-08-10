using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using Microsoft.Office.Interop.Excel;
using VivoMais.Models;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class AcaoMarketingController : Controller
    {
        private ParceiroRepositorio _repositorio;
        private TipoMidiaRepositorio _repositorioTipoMidia;
        private AcaoMarketingRepositorio _repositorioAcaoMarketing;
        private AcaoAcompanhamentoRepositorio _repositorioAcompanhamentoRepositorio;

        // GET: AcaoMarketing
        public ActionResult Index()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
            return View();
        }

        public ActionResult AcompanhamentoAcao()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());
            ViewData["MESES"] = ObterMeses();
            return View();
        }

        public JsonResult ObterTimeLine(string Id)
        {

            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            List<AcaoMarketing> Acao = _repositorioAcaoMarketing.ObterTimeline("20160615101427");

            return Json(Acao, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BtnConsultarAcompanhamentoAcao(AcaoMarketingViewModel Acao)
        {

            _repositorio = new ParceiroRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());
            ViewData["MESES"] = ObterMeses();

            AcaoMarketingViewModel AcaoMKT = null;

            List<AcaoMarketing> xAcao = _repositorioAcaoMarketing.ObterAcompanhamentoAcoes(Acao.AcaoMarketing, Session["Login"].ToString());
            if (xAcao != null)
            {
                AcaoMKT = new AcaoMarketingViewModel();
                AcaoMKT.ListaAcoes = xAcao;
            }

            return PartialView("PartialViewAcompanhamentoConsultarBuscar", AcaoMKT);
        }

        public ActionResult ConsultarAcompanhamentoAcao()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());
            ViewData["MESES"] = ObterMeses();

            return View();
        }

        public ActionResult ObterAcaoAcompanhamentoProtocolo(string Id)
        {
            _repositorioAcompanhamentoRepositorio = new AcaoAcompanhamentoRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            AcaoMarketingViewModel AcaoMKT = new AcaoMarketingViewModel();
            AcaoMKT.AcaoMarketing = _repositorioAcaoMarketing.RetornaAcaoProtocolo(Id);
            AcaoMKT.AcaoMarketing.Acompanhamento = _repositorioAcompanhamentoRepositorio.ObterAcaoAcompanhamentoProtocolo(Id);


            return PartialView("PartialViewAtualizarAcompanhamento", AcaoMKT);
        }

        public ActionResult ObterAcaoAcompanhamentoConsultarPorProtocolo(string Id)
        {
            _repositorioAcompanhamentoRepositorio = new AcaoAcompanhamentoRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            AcaoMarketingViewModel AcaoMKT = new AcaoMarketingViewModel();
            AcaoMKT.AcaoMarketing = _repositorioAcaoMarketing.RetornaAcaoProtocolo(Id);
            AcaoMKT.AcaoMarketing.Acompanhamento = _repositorioAcompanhamentoRepositorio.ObterAcaoAcompanhamentoProtocolo(Id);


            return PartialView("PartialViewAcompanhamentoConsultarAcao", AcaoMKT);
        }

        public PartialViewResult BuscarAcompanhamento(AcaoMarketingViewModel Acao)
        {
            _repositorio = new ParceiroRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());
            ViewData["MESES"] = ObterMeses();

            AcaoMarketingViewModel AcaoMKT = null;

            List<AcaoMarketing> xAcao = _repositorioAcaoMarketing.ObterAcompanhamentoAcoes(Acao.AcaoMarketing, Session["Login"].ToString());
            if (xAcao != null)
            {
                AcaoMKT = new AcaoMarketingViewModel();
                AcaoMKT.ListaAcoes = xAcao;
            }

            return PartialView("PartialViewBuscarAcompanhamento", AcaoMKT);
        }

        public ActionResult ExtrairRel()
        {
            ViewData["MESES"] = ObterMeses();

            return View();
        }

        public ActionResult ImportarProtocolo()
        {
            return View();
        }

        public ActionResult ImportarREL()
        {
            return View();
        }

        public ActionResult SelecionarREL()
        {
            ViewData["MESES"] = ObterMeses();

            return View();
        }

        public JsonResult EscolherAcoesRel(string[] ArrayCk)
        {
            _repositorioAcompanhamentoRepositorio = new AcaoAcompanhamentoRepositorio();

            bool retorno = false;

            for (int i = (ArrayCk.Length - 1); i <= (ArrayCk.Length - 1); i--)
            {
                if (i < 0)
                {
                    break;
                }
                else
                {
                    _repositorioAcompanhamentoRepositorio.SelecionaAcoesParaRel(ArrayCk[i]);
                    retorno = true;
                }
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult SelecionarRelBuscar(AcaoMarketingViewModel Acao)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            AcaoMarketingViewModel ListaAcao = new AcaoMarketingViewModel();
            ListaAcao.ListaAcoes = _repositorioAcaoMarketing.SelecionarRel(Acao.AcaoMarketing.MesAcao, Session["Login"].ToString());

            return PartialView("PartialViewSelecionarREL", ListaAcao);
        }
                       
        public ActionResult ImportarArquivoProtocolo(FormCollection formCollection)
        {
            _repositorioAcompanhamentoRepositorio = new AcaoAcompanhamentoRepositorio();

            if (Request != null)
            {
                HttpPostedFileBase upload = Request.Files["UploadedFile"];

                if (upload != null && upload.ContentLength > 0)
                {
                    System.IO.Stream stream = upload.InputStream;

                    // We return the interface, so that
                    IExcelDataReader reader = null;


                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View("ImportarProtocolo");
                    }

                    reader.IsFirstRowAsColumnNames = true;
                    DataSet result = reader.AsDataSet();

                    while (reader.Read())
                    {
                        if (reader[12].ToString() != "IDENTIFICADOR")
                        {
                            _repositorioAcompanhamentoRepositorio.AtualizarAcompanhamentoProtocolo(reader[12].ToString(), reader[13].ToString(), Convert.ToDateTime(reader[14].ToString()), Convert.ToDateTime(reader[15].ToString()));
                        }
                    }
                        
                    reader.Close();

                }
            }

            TempData["MSG"] = "Protocolo Atualizado com Sucesso!";
            return View("ImportarProtocolo");
        }

        public ActionResult ImportarArquivoREL(FormCollection formCollection)
        {
            _repositorioAcompanhamentoRepositorio = new AcaoAcompanhamentoRepositorio();

            if (Request != null)
            {
                HttpPostedFileBase upload = Request.Files["UploadedFile"];

                if (upload != null && upload.ContentLength > 0)
                {
                    System.IO.Stream stream = upload.InputStream;

                    // We return the interface, so that
                    IExcelDataReader reader = null;


                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View("ImportarREL");
                    }

                    reader.IsFirstRowAsColumnNames = true;
                    DataSet result = reader.AsDataSet();

                    while (reader.Read())
                    {
                        if (reader[19].ToString() != "NÚMERO PROTOCOLO")
                        {
                            _repositorioAcompanhamentoRepositorio.InserirNumeroREL(reader[19].ToString(), reader[16].ToString(), reader[17].ToString(), reader[20].ToString());
                        }
                    }

                    reader.Close();

                }
            }

            TempData["MSG"] = "REL Atualizada com Sucesso!";
            return View("ImportarREL");
        }

        public JsonResult ExtrairProtocoloDownload(string MesAcao)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

                if (System.IO.File.Exists(@"C:\PPC\ProtocoloREL.xlsx"))
                {
                    System.IO.File.Delete(@"C:\PPC\ProtocoloREL.xlsx");
                }

                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    process.Kill();
                }

                AcaoMarketingViewModel ListaAcao = new AcaoMarketingViewModel();
                ListaAcao.ListaAcoes = _repositorioAcaoMarketing.ExtrairProtocolo(MesAcao, Session["Login"].ToString());

                Application xlApp = new Application();
                Workbook xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
                Worksheet xlWorkSheet = new Worksheet();
                object misValue = System.Reflection.Missing.Value;
                int i = 2;

                if (ListaAcao.ListaAcoes != null)
                {
                    if (ListaAcao.ListaAcoes.Count > 0)
                    {
                        xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        xlWorkSheet.Cells[1, 1] = "UF";
                        xlWorkSheet.Cells[1, 2] = "CENTRO CUSTO";
                        xlWorkSheet.Cells[1, 3] = "CNPJ";
                        xlWorkSheet.Cells[1, 4] = "COD DEALER";
                        xlWorkSheet.Cells[1, 5] = "RAZÃO SOCIAL";
                        xlWorkSheet.Cells[1, 6] = "FORNECEDOR";
                        xlWorkSheet.Cells[1, 7] = "TOTAL PPC";
                        xlWorkSheet.Cells[1, 8] = "TOTAL VIVO";
                        xlWorkSheet.Cells[1, 9] = "MÊS AÇÃO";
                        xlWorkSheet.Cells[1, 10] = "RESPONSAVEL";
                        xlWorkSheet.Cells[1, 11] = "CANAL";
                        xlWorkSheet.Cells[1, 12] = "VIO/VPC";
                        xlWorkSheet.Cells[1, 13] = "IDENTIFICADOR";
                        xlWorkSheet.Cells[1, 14] = "PROTOCOLO";
                        xlWorkSheet.Cells[1, 15] = "SOLICITAÇÃO PROTOCOLO";
                        xlWorkSheet.Cells[1, 16] = "VENCIMENTO PROTOCOLO";

                        foreach (AcaoMarketing item in ListaAcao.ListaAcoes)
                        {
                            xlWorkSheet.Cells[i, 1] = item.Parceiro.Uf;
                            xlWorkSheet.Cells[i, 2] = item.Parceiro.CentroCusto;
                            xlWorkSheet.Cells[i, 3] = item.Parceiro.Cnpj;
                            xlWorkSheet.Cells[i, 4] = item.Parceiro.Vendedor;
                            xlWorkSheet.Cells[i, 5] = item.Parceiro.RazaoSocial;
                            xlWorkSheet.Cells[i, 6] = item.Parceiro.CodigoFornecedor;
                            xlWorkSheet.Cells[i, 7] = item.ValorTotalAcao;
                            xlWorkSheet.Cells[i, 8] = item.ValorTotalVivo;
                            xlWorkSheet.Cells[i, 9] = item.MesAcao;
                            xlWorkSheet.Cells[i, 10] = "Mariana De Almeida Holanda";
                            xlWorkSheet.Cells[i, 11] = item.Parceiro.Canal;
                            xlWorkSheet.Cells[i, 12] = item.OrigemVerba;
                            xlWorkSheet.Cells[i, 13] = item.Protocolo;
                            xlWorkSheet.Cells[i, 14] = item.Acompanhamento.NumeroProtocolo;

                            if(item.Acompanhamento.DataSolicitacaoProtocolo != DateTime.MinValue)
                            {
                                xlWorkSheet.Cells[i, 15] = item.Acompanhamento.DataSolicitacaoProtocolo;
                            }

                            if (item.Acompanhamento.DataVencimentoProtocolo != DateTime.MinValue)
                            {
                                xlWorkSheet.Cells[i, 16] = item.Acompanhamento.DataVencimentoProtocolo;
                            }


                            i++;
                        }

                        xlWorkBook.SaveAs("C:\\PPC\\ProtocoloREL.xlsx", XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                    }
                }


                liberarObjetos(xlWorkSheet);
                liberarObjetos(xlWorkBook);
                liberarObjetos(xlApp);

                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    process.Kill();
                }

                return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ExtrairRELDownload(string MesAcao)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            if (System.IO.File.Exists(@"C:\PPC\REL.xlsx"))
                {
                    System.IO.File.Delete(@"C:\PPC\REL.xlsx");
                }

                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    process.Kill();
                }

                AcaoMarketingViewModel ListaAcao = new AcaoMarketingViewModel();
                ListaAcao.ListaAcoes = _repositorioAcaoMarketing.ExtrairRel(MesAcao, Session["Login"].ToString());

                Application xlApp = new Application();
                Workbook xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
                Worksheet xlWorkSheet = new Worksheet();
                object misValue = System.Reflection.Missing.Value;
                int i = 2;

                if (ListaAcao.ListaAcoes != null)
                {
                    if (ListaAcao.ListaAcoes.Count > 0)
                    {
                        xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                        xlWorkSheet.Cells[1, 1] = "UF";
                        xlWorkSheet.Cells[1, 2] = "CENTRO CUSTO";
                        xlWorkSheet.Cells[1, 3] = "CNPJ";
                        xlWorkSheet.Cells[1, 4] = "COD DEALER";
                        xlWorkSheet.Cells[1, 5] = "RAZÃO SOCIAL";
                        xlWorkSheet.Cells[1, 6] = "CÓDIGO FORNECEDOR";
                        xlWorkSheet.Cells[1, 7] = "TOTAL PPC";
                        xlWorkSheet.Cells[1, 8] = "REEMBOLSO A REC";
                        xlWorkSheet.Cells[1, 9] = "CONTRATO";
                        xlWorkSheet.Cells[1, 10] = "APROVADO";
                        xlWorkSheet.Cells[1, 11] = "NOTA DE DÉBITO";
                        xlWorkSheet.Cells[1, 12] = "COMPETÊNCIA";
                        xlWorkSheet.Cells[1, 13] = "DATA SOLICITAÇÃO";
                        xlWorkSheet.Cells[1, 14] = "RESPONSÁVEL";
                        xlWorkSheet.Cells[1, 15] = "CANAL";
                        xlWorkSheet.Cells[1, 16] = "NÚMERO AT/PROCESSO";
                        xlWorkSheet.Cells[1, 17] = "REL";
                        xlWorkSheet.Cells[1, 18] = "DOCUMENTO";
                        xlWorkSheet.Cells[1, 19] = "FLAG SALDO";
                        xlWorkSheet.Cells[1, 20] = "NÚMERO PROTOCOLO";
                        xlWorkSheet.Cells[1, 21] = "DATA SOLICITAÇÃO REL";



                        foreach (AcaoMarketing item in ListaAcao.ListaAcoes)
                        {
                            xlWorkSheet.Cells[i, 1] = item.Parceiro.Uf;
                            xlWorkSheet.Cells[i, 2] = item.Parceiro.CentroCusto;
                            xlWorkSheet.Cells[i, 3] = item.Parceiro.Cnpj;
                            xlWorkSheet.Cells[i, 4] = item.Parceiro.Vendedor;
                            xlWorkSheet.Cells[i, 5] = item.Parceiro.RazaoSocial;
                            xlWorkSheet.Cells[i, 6] = item.Parceiro.CodigoFornecedor;
                            xlWorkSheet.Cells[i, 7] = item.ValorTotalAcao;
                            xlWorkSheet.Cells[i, 8] = item.ValorTotalVivo;
                            xlWorkSheet.Cells[i, 9] = "";
                            xlWorkSheet.Cells[i, 10] = "";
                            xlWorkSheet.Cells[i, 11] = item.ValorTotalVivo;
                            xlWorkSheet.Cells[i, 12] = MesAcao;
                            xlWorkSheet.Cells[i, 13] = DateTime.Now.ToString("dd/MM/yyyy");
                            xlWorkSheet.Cells[i, 14] = "Mariana De Almeida Holanda";
                            xlWorkSheet.Cells[i, 15] = item.Parceiro.Canal;
                            xlWorkSheet.Cells[i, 16] = "";
                            xlWorkSheet.Cells[i, 17] = "";
                            xlWorkSheet.Cells[i, 18] = "";
                            xlWorkSheet.Cells[i, 19] = "";
                            xlWorkSheet.Cells[i, 20] = item.Acompanhamento.NumeroProtocolo;
                            xlWorkSheet.Cells[i, 21] = "";

                            i++;
                        }

                        xlWorkBook.SaveAs("C:\\PPC\\REL.xlsx", XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                    }
                }


                liberarObjetos(xlWorkSheet);
                liberarObjetos(xlWorkBook);
                liberarObjetos(xlApp);

                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    process.Kill();
                }

                return Json(true, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult ExtrairProtocolo() 
        {
            ViewData["MESES"] = ObterMeses();

            return View();
        }

        public PartialViewResult ExtrairProtocoloBuscar(AcaoMarketingViewModel Acao)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            AcaoMarketingViewModel ListaAcao = new AcaoMarketingViewModel();
            ListaAcao.ListaAcoes = _repositorioAcaoMarketing.ExtrairProtocolo(Acao.AcaoMarketing.MesAcao, Session["Login"].ToString());

            return PartialView("PartialViewExtrairProtocolo", ListaAcao);
        }

        public PartialViewResult ExtrairRelBuscar(AcaoMarketingViewModel Acao)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            AcaoMarketingViewModel ListaAcao = new AcaoMarketingViewModel();
            ListaAcao.ListaAcoes = _repositorioAcaoMarketing.ExtrairRel(Acao.AcaoMarketing.MesAcao, Session["Login"].ToString());

            return PartialView("PartialViewExtrairRel", ListaAcao);
        }
        
        public ActionResult ConsultarAcao()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["REDE"] = _repositorio.ObterRedes(Session["Login"].ToString());
            ViewData["MES"] = ObterMeses();

            return View();
        }

        public PartialViewResult ConsultarAcaoPartView(AcaoMarketingViewModel Acao)
        {
            _repositorio = new ParceiroRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            ViewData["REDE"] = _repositorio.ObterRedes(Session["Login"].ToString());
            ViewData["MES"] = ObterMeses();

            AcaoMarketingViewModel AcaoMKT = null;

            List<AcaoMarketing> xAcao = _repositorioAcaoMarketing.ConsultarStatusAcoes(Acao.AcaoMarketing.Parceiro.Rede, Acao.AcaoMarketing.Status.Descricao, Acao.AcaoMarketing.MesAcao);
            if (xAcao != null)
            {
                AcaoMKT = new AcaoMarketingViewModel();
                AcaoMKT.ListaAcoes = xAcao;
            }

            return PartialView("PartialViewConsultarAcao", AcaoMKT);
        }

        public ActionResult AlterarAcao()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["REDE"] = _repositorio.ObterRedes(Session["Login"].ToString());
            ViewData["MES"] = ObterMeses();
            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();

            return View();
        }

        public PartialViewResult ObterAcao(AcaoMarketingViewModel Acao)
        {
            _repositorio = new ParceiroRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            ViewData["REDE"] = _repositorio.ObterRedes(Session["Login"].ToString());
            ViewData["MES"] = ObterMeses();

            AcaoMarketingViewModel AcaoMKT = null;


            List<AcaoMarketing> xAcao = _repositorioAcaoMarketing.RetornaAcaoRedeMes(Acao.AcaoMarketing.Parceiro.Rede, Acao.AcaoMarketing.MesAcao);
            if (xAcao != null)
            {
                AcaoMKT = new AcaoMarketingViewModel();
                AcaoMKT.ListaAcoes = xAcao;
            }

            return PartialView("PartialViewAlterarBuscar", AcaoMKT);
        }

        public ActionResult ConsultarSaldo()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["REDE"] = _repositorio.ObterRedes(Session["Login"].ToString());

            return View();
        }

        public PartialViewResult ObterSaldo(AcaoMarketingViewModel Acao)
        {
            _repositorio = new ParceiroRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            ViewData["REDE"] = _repositorio.ObterRedes(Session["Login"].ToString());
            SaldoRede sr = _repositorioAcaoMarketing.ObterSaldoRede(Acao.AcaoMarketing.Parceiro.Rede, Acao.AcaoMarketing.OrigemVerba, "NE");

            sr.Verba = Acao.AcaoMarketing.OrigemVerba;
            if (Acao.AcaoMarketing.OrigemVerba == "VERBA COOPERADA")
            {
                sr.Saldo = sr.SaldoVpc + sr.Usado;
            }
            else
            {
                sr.Saldo = sr.SaldoVio + sr.Usado;
            }





            return PartialView("PartialViewConsultarSaldo", sr);
        }

        public JsonResult ArrayExtrairPPC(string[] Id)
        {
            if (Id != null)
            {
                _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

                if (System.IO.File.Exists(@"C:\PPC\ppc.xls"))
                {
                    System.IO.File.Delete(@"C:\PPC\ppc.xls");
                }

                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    process.Kill();
                }

                decimal CustoUnitario = 0;
                decimal Quantidade = 0;
                decimal CustoVivo = 0;
                decimal CustoTotal = 0;

                Range celulas;
                Application xlApp;
                Workbook xlWorkBook;
                Worksheet xlWorkSheet = new Worksheet();
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Application();
                xlWorkBook = xlApp.Workbooks.Add(Type.Missing);

                System.Data.DataTable ds = _repositorioAcaoMarketing.extrairPPC(Id);
                int i = 10;

                if (ds != null)
                {
                    if (ds.Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Rows)
                        {

                            #region Tamanho da Coluna
                            celulas = xlApp.get_Range("A1");
                            celulas.ColumnWidth = 35.71;

                            celulas = xlApp.get_Range("B1");
                            celulas.ColumnWidth = 28;

                            celulas = xlApp.get_Range("C1");
                            celulas.ColumnWidth = 24.14;

                            celulas = xlApp.get_Range("D1");
                            celulas.ColumnWidth = 24.14;

                            celulas = xlApp.get_Range("E1");
                            celulas.ColumnWidth = 18;

                            celulas = xlApp.get_Range("F1");
                            celulas.ColumnWidth = 13.71;

                            celulas = xlApp.get_Range("G1", "AK1");
                            celulas.ColumnWidth = 3.57;

                            celulas = xlApp.get_Range("AM1");
                            celulas.ColumnWidth = 15.57;

                            celulas = xlApp.get_Range("AL1");
                            celulas.ColumnWidth = 15.57;

                            celulas = xlApp.get_Range("AN1");
                            celulas.ColumnWidth = 15.57;

                            //celulas = xlApp.get_Range("AO1");
                            //celulas.ColumnWidth = 8.43;

                            celulas = xlApp.get_Range("AP1");
                            celulas.ColumnWidth = 11.14;



                            #endregion

                            //#region Terceira Linha

                            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                            xlWorkSheet.PageSetup.PrintArea = "$A$1:$AP$44";
                            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                            xlWorkSheet.PageSetup.FitToPagesWide = 1;
                            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                            xlWorkSheet.PageSetup.Zoom = false;


                            xlWorkSheet.get_Range("A1").RowHeight = 24.75;
                            xlWorkSheet.get_Range("A2", "A45").RowHeight = 12.75;
                            xlWorkSheet.get_Range("A1", "AP45").Cells.Font.Size = 10;
                            xlWorkSheet.get_Range("A1", "AP45").Cells.Font.Name = "Arial";
                            xlWorkSheet.get_Range("A1", "AP45").Cells.Borders.Color = ColorTranslator.ToWin32(Color.White);
                            xlWorkSheet.get_Range("A10", "AN28").Cells.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            if (Convert.ToString(item[52]) == "Verba Cooperada")
                            {
                                xlWorkSheet.Cells[1, 1] = "VIVO VPC";
                            }
                            else
                            {
                                xlWorkSheet.Cells[1, 1] = "VIVO VIO";
                            }
                            celulas = xlApp.get_Range("A1");
                            celulas.Font.Size = 20;
                            celulas.Font.Name = "Arial Black";
                            celulas.Font.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;

                            //Adabas
                            xlWorkSheet.Cells[3, 1] = "Código do Agente: ";
                            celulas = xlApp.get_Range("A3");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("B3", "AC3");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[3, 2] = Convert.ToString(item[0]);


                            //Nome Fantasia
                            xlWorkSheet.Cells[3, 30] = "Nome Fantasia:";
                            celulas = xlApp.get_Range("AD3", "AK3");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("AL3", "AN3");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            xlWorkSheet.Cells[3, 38] = Convert.ToString(item[2]);


                            //Tipo de Solicitação
                            xlWorkSheet.Cells[1, 9] = "Tipo de Solicitação: ";
                            celulas = xlApp.get_Range("I1", "O1");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("I2", "O2");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[2, 9] = "PROPOSTA DE PUBLICIDADE COOPERADA";

                            //Canal
                            xlWorkSheet.Cells[1, 16] = "Canal: ";
                            celulas = xlApp.get_Range("P1", "W1");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;

                            celulas = xlApp.get_Range("P2", "W2");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[2, 16] = Convert.ToString(item[6]);

                            //Numero Processo
                            xlWorkSheet.Cells[1, 24] = "Nº do Processo: " + item[60].ToString();
                            celulas = xlApp.get_Range("X1", "AK1");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;


                            //Dia Mes Ano
                            xlWorkSheet.Cells[1, 38] = "Dia/ Mês / Ano da proposta:";
                            celulas = xlApp.get_Range("AL1", "AN1");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("AL2", "AN2");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            string dataAcao = Convert.ToString(item[49]);
                            string ano = dataAcao.Substring(dataAcao.Length - 4, 4);
                            string mes = dataAcao.Substring(0, 2);

                            //if (dataAcao.Substring(0, 2) == "01")
                            //{
                            //    mes = "01";
                            //}
                            //else if (dataAcao.Substring(0, 2) == "02")
                            //{
                            //    mes = "02";
                            //}
                            //else if (dataAcao.Substring(0, 2) == "03")
                            //{
                            //    mes = "03";
                            //}
                            //else if (dataAcao.Substring(0, 2) == "04")
                            //{
                            //    mes = "04";
                            //}
                            //else if (dataAcao.Substring(0, 2) == "05")
                            //{
                            //    mes = "05";
                            //}
                            //else if (dataAcao.Substring(0, 2) == "06")
                            //{
                            //    mes = "06";
                            //}
                            //else if (dataAcao.Substring(0, 2) == "07")
                            //{
                            //    mes = "07";
                            //}
                            //else if (dataAcao.Substring(0, 2) == "08")
                            //{
                            //    mes = "08";
                            //}
                            //else if (dataAcao.Substring(0, 2) == "09")
                            //{
                            //    mes = "09";
                            //}
                            //else if (dataAcao.Substring(0, 3) == "Out")
                            //{
                            //    mes = "10";
                            //}
                            //else if (dataAcao.Substring(0, 3) == "Nov")
                            //{
                            //    mes = "11";
                            //}
                            //else
                            //{
                            //    mes = "12";
                            //}

                            int cont = 0;
                            for (int x = 12; x < 44; x++)
                            {
                                cont = cont + 1;
                                if (Convert.ToString(item[x]) != "0")
                                {
                                    break;
                                }
                            }

                            string xCont = cont.ToString();
                            if (xCont.Count() == 1)
                            {
                                xlWorkSheet.Cells[2, 38] = "'0" + cont + "/" + mes + "/" + ano;
                            }
                            else
                            {
                                xlWorkSheet.Cells[2, 38] = "'" + cont + "/" + mes + "/" + ano;
                            }

                            //Razão Social
                            xlWorkSheet.Cells[4, 1] = "Razão Social:";
                            celulas = xlApp.get_Range("A4");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("B4", "AC4");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            xlWorkSheet.Cells[4, 2] = Convert.ToString(item[1]);


                            //Pessoa para Contato:
                            xlWorkSheet.Cells[4, 30] = "Pessoa para Contato:";
                            celulas = xlApp.get_Range("AD4", "AK4");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("AL4", "AN4");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            xlWorkSheet.Cells[4, 38] = Convert.ToString(item[54]);


                            //Cidades atingidas com as ações
                            xlWorkSheet.Cells[5, 1] = "Cidades atingidas com as ações:";
                            celulas = xlApp.get_Range("A5");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;

                            celulas = xlApp.get_Range("B5", "AC5");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            xlWorkSheet.Cells[5, 2] = Convert.ToString(item[4]);


                            //Telefones de Contato
                            xlWorkSheet.Cells[5, 30] = "Telefones de Contato:";
                            celulas = xlApp.get_Range("AD5", "AK5");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;

                            celulas = xlApp.get_Range("AL5", "AN5");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            xlWorkSheet.Cells[5, 38] = Convert.ToString(item[55]);


                            //Nome do Gerente de Contas Vivo
                            xlWorkSheet.Cells[6, 1] = "Nome do Gerente de Contas Vivo:";
                            celulas = xlApp.get_Range("A6");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;

                            celulas = xlApp.get_Range("B6", "AC6");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            xlWorkSheet.Cells[6, 2] = Convert.ToString(item[5]);


                            //E-mail para  Contato
                            xlWorkSheet.Cells[6, 30] = "E-mail para  Contato:";
                            celulas = xlApp.get_Range("AD6", "AK6");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;

                            celulas = xlApp.get_Range("AL6", "AN6");
                            celulas.Merge(Type.Missing);
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            xlWorkSheet.Cells[6, 38] = Convert.ToString(item[56]);

                            //Tipo de ação 
                            xlWorkSheet.Cells[7, 1] = "Tipo de ação";
                            celulas = xlApp.get_Range("A7", "A9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 1] = Convert.ToString(item[3]);




                            // Nome do Veículo com telefone
                            xlWorkSheet.Cells[7, 2] = "Nome do Veículo com telefone";
                            celulas = xlApp.get_Range("B7", "B9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 2] = Convert.ToString(item[8]);


                            // Ação Detalhada
                            xlWorkSheet.Cells[7, 3] = "Ação Detalhada";
                            celulas = xlApp.get_Range("C7", "C9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 3] = Convert.ToString(item[57]);

                            //Cidade
                            xlWorkSheet.Cells[7, 4] = "Cidade";
                            celulas = xlApp.get_Range("D7", "D9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 4] = Convert.ToString(item[4]);


                            //Formato do Material
                            xlWorkSheet.Cells[7, 5] = "Formato do Material";
                            celulas = xlApp.get_Range("E7", "E9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 5] = Convert.ToString(item[10]);


                            //Foco da Ação
                            xlWorkSheet.Cells[7, 6] = "Foco da Ação";
                            celulas = xlApp.get_Range("F7", "F9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 6] = Convert.ToString(item[11]);

                            //Período
                            xlWorkSheet.Cells[7, 7] = "Período de Veiculação desta ação:";
                            celulas = xlApp.get_Range("G7", "AK8");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            if (Convert.ToString(item[58]) != "" && Convert.ToString(item[59]) != "")
                            {
                                DateTime DtIni = _repositorioAcaoMarketing.retornaDataInicialAcao(Id);

                                DateTime DtFim = Convert.ToDateTime(item[59]);
                                int diferencaDias = (DtFim.Subtract(DtIni)).Days;

                                xlWorkSheet.Cells[9, 7] = "'" + DtIni.ToString("dd") + "/" + DtIni.ToString("MM");
                                celulas = xlApp.get_Range("G9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 7] = Convert.ToString(item[12]);

                                DateTime data = DtIni.AddDays(1);
                                xlWorkSheet.Cells[9, 8] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("H9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 8] = Convert.ToString(item[13]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 9] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("I9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 9] = Convert.ToString(item[14]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 10] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("J9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 10] = Convert.ToString(item[15]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 11] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("K9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 11] = Convert.ToString(item[16]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 12] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("L9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 12] = Convert.ToString(item[17]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 13] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("M9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 13] = Convert.ToString(item[18]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 14] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("N9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 14] = Convert.ToString(item[19]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 15] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("O9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 15] = Convert.ToString(item[20]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 16] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("P9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 16] = Convert.ToString(item[21]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 17] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("Q9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 17] = Convert.ToString(item[22]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 18] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("R9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 18] = Convert.ToString(item[23]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 19] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("S9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 19] = Convert.ToString(item[24]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 20] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("T9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 20] = Convert.ToString(item[25]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 21] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("U9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 21] = Convert.ToString(item[26]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 22] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("V9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 22] = Convert.ToString(item[27]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 23] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("W9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 23] = Convert.ToString(item[28]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 24] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("X9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 24] = Convert.ToString(item[29]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 25] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("Y9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 25] = Convert.ToString(item[30]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 26] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("Z9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 26] = Convert.ToString(item[31]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 27] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AA9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 27] = Convert.ToString(item[32]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 28] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AB9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 28] = Convert.ToString(item[33]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 29] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AC9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 29] = Convert.ToString(item[34]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 30] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AD9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 30] = Convert.ToString(item[35]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 31] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AE9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 31] = Convert.ToString(item[36]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 32] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AF9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 32] = Convert.ToString(item[37]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 33] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AG9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 33] = Convert.ToString(item[38]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 34] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AH9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 34] = Convert.ToString(item[39]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 35] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AI9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 35] = Convert.ToString(item[40]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 36] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AJ9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 36] = Convert.ToString(item[41]);

                                data = data.AddDays(1);
                                xlWorkSheet.Cells[9, 37] = "'" + data.ToString("dd") + "/" + data.ToString("MM");
                                celulas = xlApp.get_Range("AK9");
                                celulas.ColumnWidth = 4.57;
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 37] = Convert.ToString(item[42]);
                            }
                            else
                            {
                                //Dias Veículação
                                xlWorkSheet.Cells[9, 7] = "1";
                                celulas = xlApp.get_Range("G9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 7] = Convert.ToString(item[12]);

                                xlWorkSheet.Cells[9, 8] = "2";
                                celulas = xlApp.get_Range("H9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 8] = Convert.ToString(item[13]);

                                xlWorkSheet.Cells[9, 9] = "3";
                                celulas = xlApp.get_Range("I9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 9] = Convert.ToString(item[14]);

                                xlWorkSheet.Cells[9, 10] = "4";
                                celulas = xlApp.get_Range("J9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 10] = Convert.ToString(item[15]);

                                xlWorkSheet.Cells[9, 11] = "5";
                                celulas = xlApp.get_Range("K9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 11] = Convert.ToString(item[16]);

                                xlWorkSheet.Cells[9, 12] = "6";
                                celulas = xlApp.get_Range("L9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 12] = Convert.ToString(item[17]);

                                xlWorkSheet.Cells[9, 13] = "7";
                                celulas = xlApp.get_Range("M9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 13] = Convert.ToString(item[18]);

                                xlWorkSheet.Cells[9, 14] = "8";
                                celulas = xlApp.get_Range("N9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 14] = Convert.ToString(item[19]);

                                xlWorkSheet.Cells[9, 15] = "9";
                                celulas = xlApp.get_Range("O9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 15] = Convert.ToString(item[20]);

                                xlWorkSheet.Cells[9, 16] = "10";
                                celulas = xlApp.get_Range("P9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 16] = Convert.ToString(item[21]);

                                xlWorkSheet.Cells[9, 17] = "11";
                                celulas = xlApp.get_Range("Q9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 17] = Convert.ToString(item[22]);

                                xlWorkSheet.Cells[9, 18] = "12";
                                celulas = xlApp.get_Range("R9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 18] = Convert.ToString(item[23]);

                                xlWorkSheet.Cells[9, 19] = "13";
                                celulas = xlApp.get_Range("S9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 19] = Convert.ToString(item[24]);

                                xlWorkSheet.Cells[9, 20] = "14";
                                celulas = xlApp.get_Range("T9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 20] = Convert.ToString(item[25]);

                                xlWorkSheet.Cells[9, 21] = "15";
                                celulas = xlApp.get_Range("U9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 21] = Convert.ToString(item[26]);

                                xlWorkSheet.Cells[9, 22] = "16";
                                celulas = xlApp.get_Range("V9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 22] = Convert.ToString(item[27]);

                                xlWorkSheet.Cells[9, 23] = "17";
                                celulas = xlApp.get_Range("W9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 23] = Convert.ToString(item[28]);

                                xlWorkSheet.Cells[9, 24] = "18";
                                celulas = xlApp.get_Range("X9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 24] = Convert.ToString(item[29]);

                                xlWorkSheet.Cells[9, 25] = "19";
                                celulas = xlApp.get_Range("Y9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 25] = Convert.ToString(item[30]);

                                xlWorkSheet.Cells[9, 26] = "20";
                                celulas = xlApp.get_Range("Z9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 26] = Convert.ToString(item[31]);

                                xlWorkSheet.Cells[9, 27] = "21";
                                celulas = xlApp.get_Range("AA9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 27] = Convert.ToString(item[32]);

                                xlWorkSheet.Cells[9, 28] = "22";
                                celulas = xlApp.get_Range("AB9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 28] = Convert.ToString(item[33]);

                                xlWorkSheet.Cells[9, 29] = "23";
                                celulas = xlApp.get_Range("AC9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 29] = Convert.ToString(item[34]);

                                xlWorkSheet.Cells[9, 30] = "24";
                                celulas = xlApp.get_Range("AD9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 30] = Convert.ToString(item[35]);

                                xlWorkSheet.Cells[9, 31] = "25";
                                celulas = xlApp.get_Range("AE9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 31] = Convert.ToString(item[36]);

                                xlWorkSheet.Cells[9, 32] = "26";
                                celulas = xlApp.get_Range("AF9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 32] = Convert.ToString(item[37]);

                                xlWorkSheet.Cells[9, 33] = "27";
                                celulas = xlApp.get_Range("AG9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 33] = Convert.ToString(item[38]);

                                xlWorkSheet.Cells[9, 34] = "28";
                                celulas = xlApp.get_Range("AH9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 34] = Convert.ToString(item[39]);

                                xlWorkSheet.Cells[9, 35] = "29";
                                celulas = xlApp.get_Range("AI9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 35] = Convert.ToString(item[40]);

                                xlWorkSheet.Cells[9, 36] = "30";
                                celulas = xlApp.get_Range("AJ9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 36] = Convert.ToString(item[41]);

                                xlWorkSheet.Cells[9, 37] = "31";
                                celulas = xlApp.get_Range("AK9");
                                celulas.Font.Bold = true;
                                celulas.Merge(Type.Missing);
                                celulas.HorizontalAlignment = Constants.xlCenter;
                                celulas.VerticalAlignment = Constants.xlCenter;
                                celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                                xlWorkSheet.Cells[i, 37] = Convert.ToString(item[42]);

                            }

                            //Quantidade
                            xlWorkSheet.Cells[7, 38] = "Quantidade";
                            celulas = xlApp.get_Range("AL7", "AL9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 38] = Convert.ToString(item[43]);
                            Quantidade = Quantidade + Convert.ToDecimal(item[43]);

                            celulas = xlApp.get_Range("AL10", "AL28");
                            celulas.HorizontalAlignment = Constants.xlCenter;

                            celulas = xlApp.get_Range("AM10", "AM28");
                            celulas.HorizontalAlignment = Constants.xlCenter;

                            celulas = xlApp.get_Range("AN10", "AN28");
                            celulas.HorizontalAlignment = Constants.xlCenter;

                            //Custo Unitário
                            xlWorkSheet.Cells[7, 39] = "Custo Unitário";
                            celulas = xlApp.get_Range("AM7", "AM9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 39] = Convert.ToString(item[44]);
                            CustoUnitario = CustoUnitario + Convert.ToDecimal(item[44]);

                            //Custo Total
                            xlWorkSheet.Cells[7, 40] = "Custo Total";
                            celulas = xlApp.get_Range("AN7", "AN9");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[i, 40] = Convert.ToString(item[47]);
                            CustoTotal = CustoTotal + Convert.ToDecimal(item[47]);
                            CustoVivo = CustoVivo + Convert.ToDecimal(item[46]);

                            //TOTAL:
                            xlWorkSheet.Cells[29, 32] = "TOTAL";
                            celulas = xlApp.get_Range("AF29", "AK29");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[29, 38] = Convert.ToString(Quantidade);



                            //Total Custo Unitario
                            xlWorkSheet.Cells[29, 39] = Convert.ToString(CustoUnitario);
                            celulas = xlApp.get_Range("AL29");
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            //Total Custo Total
                            xlWorkSheet.Cells[29, 40] = Convert.ToString(CustoTotal);
                            celulas = xlApp.get_Range("AL29");
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("AM29");
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("AN29");
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            //Observações:
                            xlWorkSheet.Cells[30, 1] = "Observações:" + Convert.ToString(item[48]);
                            celulas = xlApp.get_Range("A30", "X30");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            //Nº do IXOS:
                            xlWorkSheet.Cells[31, 1] = "Nº do IXOS:" + Convert.ToString(item[50]);
                            celulas = xlApp.get_Range("A31", "X34");
                            celulas.Font.Bold = true;
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlLeft;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[31, 29] = "Custo da Mídia:";
                            celulas = xlApp.get_Range("AC31", "AK31");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);



                            xlWorkSheet.Cells[31, 38] = "(R$)";
                            celulas = xlApp.get_Range("AL31", "AM31");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            xlWorkSheet.Cells[31, 40] = "(%)";
                            celulas = xlApp.get_Range("AN31");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            xlWorkSheet.Cells[32, 38] = CustoTotal;
                            celulas = xlApp.get_Range("AL32", "AM32");
                            celulas.NumberFormat = "#,##.00";
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("AL33", "AM33");
                            celulas.NumberFormat = "#,##.00";
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("AL34", "AM34");
                            celulas.NumberFormat = "#,##.00";
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            xlWorkSheet.Cells[32, 29] = "Valor Total da Mídia";
                            celulas = xlApp.get_Range("AC32", "AK32");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);
                            xlWorkSheet.Cells[32, 40] = "100%";


                            xlWorkSheet.Cells[33, 29] = "Participação do Dealer";
                            celulas = xlApp.get_Range("AC33", "AK33");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            string xx = Convert.ToString(item[45]);
                            xx = xx.Replace('.', ',');
                            xx = xx.Replace('%', ' ');

                            double PercentDealer = (Convert.ToDouble(xx) - Convert.ToDouble(100.00));
                            PercentDealer = Math.Abs(PercentDealer);
                            //((CustoTotal - CustoVivo)/CustoTotal);
                            //PercentDealer = Math.Round(PercentDealer, 3);
                            //PercentDealer = PercentDealer * 100;

                            xlWorkSheet.Cells[33, 40] = PercentDealer.ToString("N2") + "%";
                            celulas = xlApp.get_Range("AN33");
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);



                            xlWorkSheet.Cells[33, 38] = (CustoTotal - CustoVivo);
                            celulas = xlApp.get_Range("AN34");
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            celulas = xlApp.get_Range("AN32");
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[34, 29] = "Colaboração da VIVO";
                            celulas = xlApp.get_Range("AC34", "AK34");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            xlWorkSheet.Cells[34, 38] = CustoVivo;
                            celulas = xlApp.get_Range("AL34", "AM35");
                            celulas.NumberFormat = "#,##.00";


                            decimal PercentTotalVivo = ((CustoTotal - ((CustoTotal - CustoVivo))) / CustoTotal);
                            PercentTotalVivo = Math.Round(PercentTotalVivo, 3);
                            PercentTotalVivo = PercentTotalVivo * 100;
                            xlWorkSheet.Cells[34, 40] = Convert.ToString(item[45]) + "%";

                            //48
                            xlWorkSheet.Cells[35, 1] = "Assumo total responsabilidade, perante a Vivo, pela veracidade das informações aqui descritas, valores e descontos aqui declarados, bem como percentual de ressarcimento. ";
                            celulas = xlApp.get_Range("A35", "AN35");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[36, 1] = "ATENÇÃO: Respeitar a política de alçadas - Item 6.7 da Instrução de Trabalho";
                            celulas = xlApp.get_Range("A36", "AN36");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.VerticalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[38, 1] = "Parceiro";
                            celulas = xlApp.get_Range("A38", "B38");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[39, 1] = "Assinatura do Parceiro - Revendedor Vivo";
                            celulas = xlApp.get_Range("A39", "B39");
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            xlWorkSheet.Cells[38, 4] = "Gerente de Contas ou KAM";
                            celulas = xlApp.get_Range("D38", "M38");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[39, 4] = "Carimbo e Assinatura - Responsável FundoCoop";
                            celulas = xlApp.get_Range("D39", "M39");
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            xlWorkSheet.Cells[42, 4] = "Responsável pela ação + Gerente do Canal";
                            celulas = xlApp.get_Range("D42", "M42");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[43, 4] = "Carimbo e Assinatura Responsável FundoCoop";
                            celulas = xlApp.get_Range("D43", "M43");
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);




                            xlWorkSheet.Cells[38, 20] = "Responsável pela ação + Gerente do Mkt";
                            celulas = xlApp.get_Range("T38", "AL38");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[39, 20] = "Carimbo e Assinatura Responsável FundoCoop";
                            celulas = xlApp.get_Range("T39", "AL39");
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[42, 20] = "Diretor Regional";
                            celulas = xlApp.get_Range("T42", "AL42");
                            celulas.Merge(Type.Missing);
                            celulas.Font.Bold = true;
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);


                            xlWorkSheet.Cells[43, 20] = "Carimbo e Assinatura Responsável";
                            celulas = xlApp.get_Range("T43", "AL43");
                            celulas.Merge(Type.Missing);
                            celulas.HorizontalAlignment = Constants.xlCenter;
                            celulas.Borders.Color = ColorTranslator.ToWin32(Color.Black);

                            i = i + 1;
                        }


                        xlWorkSheet.Shapes.AddPicture("C:\\VivoSimArquivos\\Nova imagem.PNG", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1600, 20, 100, 100);

                        celulas = xlApp.get_Range("A45", "A65536");
                        celulas.EntireRow.Hidden = true;

                        xlWorkSheet.Protect("N@utico", true, true, true, true, true, true, true, true, true, true, true, true, true, true, true);

                        xlWorkBook.SaveAs("C:\\PPC\\ppc.xls", XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                    }
                }

                liberarObjetos(xlWorkSheet);
                liberarObjetos(xlWorkBook);
                liberarObjetos(xlApp);

                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    process.Kill();
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExtrairAcoes()
        {
            _repositorio = new ParceiroRepositorio();

            ViewData["MESES"] = ObterMeses();
            List<string> Ddd = _repositorio.ObterDdd(Session["Login"].ToString());
            Ddd.Add("TODOS");

            ViewData["DDD"] = Ddd;

            AcaoMarketingViewModel AcaoMKT = new AcaoMarketingViewModel();
            List<AcaoMarketing> Acoes = new List<AcaoMarketing>();
            AcaoMKT.ListaAcoes = Acoes;

            return View(AcaoMKT);
        }

        public PartialViewResult ListarAcoesPartialView(AcaoMarketingViewModel Acao)
        {
            _repositorio = new ParceiroRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            ViewData["MESES"] = ObterMeses();
            ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());


            AcaoMarketingViewModel AcaoViewModel = new AcaoMarketingViewModel();
            List<AcaoMarketing> ListMKT = _repositorioAcaoMarketing.ObterAcoesCadastradas(Acao.AcaoMarketing.MesAcao, Acao.AcaoMarketing.Parceiro.Ddd);
            AcaoViewModel.ListaAcoes = ListMKT;

            return PartialView("PartialViewExtrairAcoes", AcaoViewModel);
        }

        public JsonResult ListarAcoes(string MesAcao, string DDD)
        {
            _repositorio = new ParceiroRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            if (System.IO.File.Exists(@"C:\PPC\ExtrairAcoes.xls"))
            {
                System.IO.File.Delete(@"C:\PPC\ExtrairAcoes.xls");
            }

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
            {
                process.Kill();
            }

            Application xlApp = new Application();
            Worksheet xlWorkSheet = new Worksheet();
            object misValue = System.Reflection.Missing.Value;

            Workbook xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            List<AcaoMarketing> ListMKT = _repositorioAcaoMarketing.ObterAcoesCadastradas(MesAcao, DDD);

            if (ListMKT.Count > 0)
            {
                xlWorkSheet.Cells[1, 1] = "Regional";
                xlWorkSheet.Cells[1, 2] = "Praça";
                xlWorkSheet.Cells[1, 3] = "DDD's Envolvidos";
                xlWorkSheet.Cells[1, 4] = "Canal";
                xlWorkSheet.Cells[1, 5] = "Rede";
                xlWorkSheet.Cells[1, 6] = "Razão Social";
                xlWorkSheet.Cells[1, 7] = "Tipo de Mídia";
                xlWorkSheet.Cells[1, 8] = "Mídia Detalhada";
                xlWorkSheet.Cells[1, 9] = "Veículo";
                xlWorkSheet.Cells[1, 10] = "Descrição da ação";
                xlWorkSheet.Cells[1, 11] = "Campanha Macro";
                xlWorkSheet.Cells[1, 12] = "Mês da ação";
                xlWorkSheet.Cells[1, 13] = "Mídia Exclusiva para Vivo";
                xlWorkSheet.Cells[1, 14] = "Valor Total da Ação";
                xlWorkSheet.Cells[1, 15] = "Valor de Participação da Vivo";
                xlWorkSheet.Cells[1, 16] = "Valor Reembolsado";
                xlWorkSheet.Cells[1, 17] = "% de Participação da Vivo";
                xlWorkSheet.Cells[1, 18] = "Foco Principal Campanha";
                xlWorkSheet.Cells[1, 19] = "Quantidade de Ações";
                xlWorkSheet.Cells[1, 20] = "Inserções";
                xlWorkSheet.Cells[1, 21] = "Status da ação";
                xlWorkSheet.Cells[1, 22] = "Nível de registro";
                xlWorkSheet.Cells[1, 23] = "Origem da Verba";
                xlWorkSheet.Cells[1, 24] = "Código Adabas Dealer";
                xlWorkSheet.Cells[1, 25] = "Protocolo";
                xlWorkSheet.Cells[1, 26] = "Mes lançamento da Rel";
                xlWorkSheet.Cells[1, 27] = "Número da Rel";
                xlWorkSheet.Cells[1, 28] = "Nº Doc Sap";
                xlWorkSheet.Cells[1, 29] = "Observações";

                int i = 2;
                foreach (AcaoMarketing Acao in ListMKT)
                {
                    xlWorkSheet.Cells[i, 1] = Acao.Parceiro.Regional;
                    xlWorkSheet.Cells[i, 2] = Acao.Parceiro.Uf;
                    xlWorkSheet.Cells[i, 3] = Acao.Parceiro.Ddd;
                    xlWorkSheet.Cells[i, 4] = Acao.Parceiro.Canal;
                    xlWorkSheet.Cells[i, 5] = Acao.Parceiro.Rede;
                    xlWorkSheet.Cells[i, 6] = Acao.Parceiro.RazaoSocial;
                    xlWorkSheet.Cells[i, 7] = Acao.Midia.Midia;
                    xlWorkSheet.Cells[i, 8] = Acao.MidiaDetalhada.MidiaDetalhada;
                    xlWorkSheet.Cells[i, 9] = Acao.Veiculo;
                    xlWorkSheet.Cells[i, 10] = Acao.DescricaoAcao;
                    xlWorkSheet.Cells[i, 11] = Acao.CampanhaMacro;
                    xlWorkSheet.Cells[i, 12] = Acao.MesAcao;
                    xlWorkSheet.Cells[i, 13] = Acao.MidiaExclusivaVivo;
                    xlWorkSheet.Cells[i, 14] = Acao.ValorTotalAcao;
                    xlWorkSheet.Cells[i, 15] = Acao.ValorTotalVivo;
                    xlWorkSheet.Cells[i, 16] = "";
                    xlWorkSheet.Cells[i, 17] = Acao.PercentualParticipacaoVivo + "%";
                    xlWorkSheet.Cells[i, 18] = Acao.FocoPrincipal;
                    xlWorkSheet.Cells[i, 19] = Acao.QtdAcoes;
                    xlWorkSheet.Cells[i, 20] = Acao.Insercoes;
                    xlWorkSheet.Cells[i, 21] = Acao.Justificativa;
                    xlWorkSheet.Cells[i, 22] = "";
                    xlWorkSheet.Cells[i, 23] = Acao.OrigemVerba;
                    xlWorkSheet.Cells[i, 24] = Acao.Parceiro.Vendedor;
                    xlWorkSheet.Cells[i, 25] = "";
                    xlWorkSheet.Cells[i, 26] = "";
                    xlWorkSheet.Cells[i, 27] = "";
                    xlWorkSheet.Cells[i, 28] = "";
                    xlWorkSheet.Cells[i, 29] = "";
                    i++;
                }

                xlWorkBook.SaveAs("C:\\PPC\\ExtrairAcoes.xls", XlFileFormat.xlExcel8, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                liberarObjetos(xlWorkSheet);
                liberarObjetos(xlWorkBook);
                liberarObjetos(xlApp);

                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    process.Kill();
                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public virtual ActionResult Download(string Tipo, string Caminho)
        {
            return File(Caminho, "application/vnd.ms-excel", Tipo);
        }

        public PartialViewResult ObterPPC(AcaoMarketingViewModel Acao)
        {
            _repositorio = new ParceiroRepositorio();
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            List<AcaoMarketing> Acoes = _repositorioAcaoMarketing.ObterAcoesParaExtracao(Acao.AcaoMarketing.MesAcao, Acao.AcaoMarketing.Parceiro.Vendedor, Acao.AcaoMarketing.OrigemVerba);


            AcaoMarketingViewModel AcaoMKT = new AcaoMarketingViewModel();
            AcaoMKT.ListaAcoes = Acoes;

            ViewData["MESES"] = ObterMeses();

            List<string> Parceiro = _repositorio.ObterAdabasLogin(Session["Login"].ToString());
            ViewData["ADABAS"] = Parceiro;

            return PartialView("PartialViewExtrairPPC", AcaoMKT);
        }

        public string[] ObterMeses()
        {
            int Mes = DateTime.Now.Month;

            string[] mes = new string[12 + Mes];

            mes[0] = "01/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[1] = "02/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[2] = "03/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[3] = "04/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[4] = "05/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[5] = "06/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[6] = "07/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[7] = "08/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[8] = "09/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[9] = "10/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[10] = "11/" + Convert.ToString(DateTime.Now.Year - 1);
            mes[11] = "12/" + Convert.ToString(DateTime.Now.Year - 1);


            if (Mes >= 1)
            {
                mes[12] = "01/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 2)
            {
                mes[13] = "02/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 3)
            {
                mes[14] = "03/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 4)
            {
                mes[15] = "04/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 5)
            {
                mes[16] = "05/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 6)
            {
                mes[17] = "06/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 7)
            {
                mes[18] = "07/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 8)
            {
                mes[19] = "08/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 9)
            {
                mes[20] = "09/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 10)
            {
                mes[21] = "10/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 11)
            {
                mes[22] = "11/" + Convert.ToString(DateTime.Now.Year);
            }

            if (Mes >= 12)
            {
                mes[23] = "12/" + Convert.ToString(DateTime.Now.Year);
            }



            return mes;
        }

        public ActionResult ExtrairPPC()
        {
            AcaoMarketingViewModel AcaoMKT = new AcaoMarketingViewModel();
            List<AcaoMarketing> Acoes = new List<AcaoMarketing>();
            AcaoMKT.ListaAcoes = Acoes;

            _repositorio = new ParceiroRepositorio();

            ViewData["MESES"] = ObterMeses();

            List<string> Parceiro = _repositorio.ObterAdabasLogin(Session["Login"].ToString());
            ViewData["ADABAS"] = Parceiro;
            return View(AcaoMKT);
        }

        public ActionResult AprovarAcao()
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            List<AcaoMarketing> Acao = _repositorioAcaoMarketing.ListarAprovacaoAcao(Session["Login"].ToString(), Session["Perfil"].ToString());

            return View(Acao);
        }

        public ActionResult AcaoVio()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
            return View();
        }

        public ActionResult ValidaMarketingEspecifico(string Protocolo)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            List<AcaoMarketing> Acao = _repositorioAcaoMarketing.ListarValidacaoMarketingRegionalEspecifico(Protocolo);
            return View("ValidaMarketingEspecifico", Acao);
        }

        public ActionResult ValidaMarketing()
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            AcaoMarketing a = new AcaoMarketing();
            AcaoMarketingViewModel Acao = new AcaoMarketingViewModel();
                
            Acao.AcaoMarketing = a;
            Acao.ListaAcoes = _repositorioAcaoMarketing.ListarValidacaoMarketingRegional(Session["Login"].ToString(), Session["Perfil"].ToString());

            return View(Acao);
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

        public JsonResult RetornaAcaoID(string Id)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            AcaoMarketing Acao = _repositorioAcaoMarketing.RetornaAcaoID(Convert.ToInt32(Id));

            return Json(Acao, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AtualizarStatusAcao(string Status, string Id)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            if (Status.Equals("8"))
            {
                if (_repositorioAcaoMarketing.ApagarAcao(Id))
                {
                    return Json(_repositorioAcaoMarketing.AtualizarStatusAcao(Status, Id), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if(Status == "6")
                {
                    if (_repositorioAcaoMarketing.AtualizaHistoricoGerenteContas(Convert.ToInt32(Id)))
                    {
                        return Json(_repositorioAcaoMarketing.AtualizarStatusAcao(Status, Id), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (Status == "3")
                {
                    if (_repositorioAcaoMarketing.AtualizaHistoricoMKTTerritorial(Convert.ToInt32(Id)))
                    {
                        return Json(_repositorioAcaoMarketing.AtualizarStatusAcao(Status, Id), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (_repositorioAcaoMarketing.AtualizaHistoricoMKTRegional(Convert.ToInt32(Id)))
                    {
                        return Json(_repositorioAcaoMarketing.AtualizarStatusAcao(Status, Id), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                


                //return Json(_repositorioAcaoMarketing.AtualizarStatusAcao(Status, Id), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AtualizarStatusAcaoProtocolo(string Status, string Id)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();

            if (_repositorioAcaoMarketing.AtualizaHistoricoAprovar(Convert.ToInt32(Id)))
            {
                return Json(_repositorioAcaoMarketing.AtualizarStatusAcaoProtocolo(Status, Id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ObterMidiaDetalhada(int Midia)
        {
            _repositorioTipoMidia = new TipoMidiaRepositorio();
            List<TipoMidia> retorno = _repositorioTipoMidia.ObterMidiaDetalhada(Midia.ToString());

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalculoPercentual(string ValorTotalAcao, string ValorVerba, string PercentualVivo, string ValorTotalVivo, string Id)
        {
            decimal xValorTotalAcao = Convert.ToDecimal(ValorTotalAcao);
            decimal xValorVerba = Convert.ToDecimal(ValorVerba);
            decimal xPercentualVivo = 0;
            decimal xValorTotalVivo = 0;

            if (ValorTotalVivo != "")
            {
                xValorTotalVivo = Convert.ToDecimal(ValorTotalVivo);
            }


            if (PercentualVivo != "")
            {
                xPercentualVivo = Convert.ToDecimal(PercentualVivo);
            }

            decimal ValorVivo = (xValorTotalAcao * (xPercentualVivo) / 100);

            string[] retorno = new string[7];

            if ((xValorVerba + xValorTotalVivo) >= ValorVivo)
            {
                decimal ValorRede = xValorTotalAcao * Math.Abs((xPercentualVivo / 100) - 1);
                decimal PercentualRede = 100 - xPercentualVivo;

                retorno[0] = xValorTotalAcao.ToString("N2");
                retorno[1] = ValorVivo.ToString("N2");

                if (xPercentualVivo == 0)
                {
                    retorno[2] = "";
                }
                else
                {
                    retorno[2] = xPercentualVivo.ToString();
                }

                retorno[3] = ValorRede.ToString("N2");
                retorno[4] = PercentualRede.ToString();

                if (xValorTotalVivo > ValorVivo)
                {
                    retorno[5] = Convert.ToString(xValorVerba + Math.Abs(xValorTotalVivo - ValorVivo));
                }
                else
                {
                    retorno[5] = Convert.ToString(xValorVerba - Math.Abs(xValorTotalVivo - ValorVivo));
                }

                retorno[6] = "1"; // Flag para retorno OK
            }
            else
            {
                _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
                AcaoMarketing Acao = _repositorioAcaoMarketing.RetornaAcaoID(Convert.ToInt32(Id));

                retorno[0] = Acao.ValorTotalAcao;
                retorno[1] = Acao.ValorTotalVivo;
                retorno[2] = Acao.PercentualParticipacaoVivo;
                retorno[3] = Acao.ValorTotalRede;
                retorno[4] = Acao.PercentualRede;
                retorno[5] = ValorVerba;
                retorno[6] = "0"; // Flag para Valor da Vivo menor que o Saldo da Rede
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CadastrarAcaoVpc(AcaoMarketingViewModel Acao)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            _repositorio = new ParceiroRepositorio();

            string protocolo = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            protocolo = protocolo.Replace("/", "");
            protocolo = protocolo.Replace(":", "");
            protocolo = protocolo.Replace(" ", "");

            Acao.AcaoMarketing.Protocolo = protocolo;
            Acao.AcaoMarketing.OrigemVerba = "Verba Cooperada";

            StatusAcao Status = new StatusAcao();
            Status.Id = 2;

            Acao.AcaoMarketing.Status = Status;
            Acao.AcaoMarketing.IdAcesso = Convert.ToInt32(Session["Id"]); ;

            _repositorioAcaoMarketing.InserirAcao(Acao.AcaoMarketing);

            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();

            return View("Index");
        }

        public ActionResult CadastrarAcaoVio(AcaoMarketingViewModel Acao)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            _repositorio = new ParceiroRepositorio();

            string protocolo = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            protocolo = protocolo.Replace("/", "");
            protocolo = protocolo.Replace(":", "");
            protocolo = protocolo.Replace(" ", "");

            Acao.AcaoMarketing.Protocolo = protocolo;
            Acao.AcaoMarketing.OrigemVerba = "Verba de Incentivo a Open";

            StatusAcao Status = new StatusAcao();
            Status.Id = 3;

            Acao.AcaoMarketing.Status = Status;
            Acao.AcaoMarketing.IdAcesso = Convert.ToInt32(Session["Id"]); ;

            _repositorioAcaoMarketing.InserirAcao(Acao.AcaoMarketing);

            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();

            return View("AcaoVio");
        }

        public ActionResult AtualizarAcao(AcaoMarketingViewModel Acao)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            _repositorio = new ParceiroRepositorio();

            if (_repositorioAcaoMarketing.VerificaHistorico(Acao.AcaoMarketing.Id))
            {
                _repositorioAcaoMarketing.AtualizarAcao(Acao.AcaoMarketing);
            }
            else
            {
                if (_repositorioAcaoMarketing.CadastroHistorico(Acao.AcaoMarketing.Id))
                {
                    _repositorioAcaoMarketing.AtualizarAcao(Acao.AcaoMarketing);
                }
            }

            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();

            return View("PartialViewAtualizarAcao");
        }

        public ActionResult PartialViewAtualizacao(string Id)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            _repositorio = new ParceiroRepositorio();
            _repositorioTipoMidia = new TipoMidiaRepositorio();

            AcaoMarketingViewModel AcaoMKT = new AcaoMarketingViewModel();
            AcaoMKT.AcaoMarketing = _repositorioAcaoMarketing.RetornaAcaoID(Convert.ToInt32(Id));


            List<Parceiro> ListParceiro = _repositorio.ObterAdabasDDD(AcaoMKT.AcaoMarketing.Parceiro.Rede, AcaoMKT.AcaoMarketing.Parceiro.Ddd, AcaoMKT.AcaoMarketing.OrigemVerba);
            AcaoMKT.ValorVerba = ListParceiro[0].Saldo;


            //ViewData["UF"] = _repositorio.ObterUf().Distinct();
            //ViewData["DDD"] = _repositorio.ObterDdd(AcaoMKT.AcaoMarketing.Parceiro.Uf);
            //ViewData["REDE"] = _repositorio.ObterParceirosRede(AcaoMKT.AcaoMarketing.Parceiro.Ddd).Select(x => x.Rede).ToList();
            ViewData["VENDEDOR"] = ListParceiro.Select(x => x.Vendedor).ToList();
            ViewData["MIDIADETALHADA"] = _repositorioTipoMidia.ObterMidiaDetalhada(AcaoMKT.AcaoMarketing.Midia.IdTipoMidia).Select(x => x.MidiaDetalhada).ToList();

            return PartialView("PartialViewAtualizarAcao", AcaoMKT);
        }

        public ActionResult RetornaAcaoIDPartialView(string Id)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            AcaoMarketingViewModel Acao = new AcaoMarketingViewModel();
            Acao.AcaoMarketing = _repositorioAcaoMarketing.RetornaAcaoID(Convert.ToInt32(Id));

            return PartialView("PartialViewValidaMarketing", Acao);
        }

        public ActionResult RetornaAcaoPartialView(string Id)
        {
            _repositorioAcaoMarketing = new AcaoMarketingRepositorio();
            AcaoMarketing Acao = _repositorioAcaoMarketing.RetornaAcaoID(Convert.ToInt32(Id));
            List<AcaoMarketing> Lacao = new List<AcaoMarketing>();
            Lacao.Add(Acao);

            return PartialView("PartialViewAprovacao", Lacao);
        }

        public ActionResult AtualizarAcompanhamento(AcaoMarketingViewModel Acao)
        {
            _repositorioAcompanhamentoRepositorio = new AcaoAcompanhamentoRepositorio();

            _repositorioAcompanhamentoRepositorio.AtualizarAcompanhamento(Acao.AcaoMarketing.Acompanhamento);

            return PartialView("PartialViewAtualizarAcompanhamento", true);
        }

        public JsonResult GeraGraficoAcompanhamento(string Id)
        {
            _repositorioAcompanhamentoRepositorio = new AcaoAcompanhamentoRepositorio();


            //List<CarteiraGraficoViewModel> rs = new List<CarteiraGraficoViewModel>();

            System.Data.DataTable dt = _repositorioAcompanhamentoRepositorio.ObterGraficoAcompanhamento(Id);

            //List<double> xDdd = (from MyRow in dt.AsEnumerable()
            //                     select MyRow.Field<double>("DDD")).Distinct().ToList();

            //int cont = 0;
            //for (int i = 0; i < xDdd.Count; i++)
            //{
            //    List<DataRow> xRow = dt.Select("DDD='" + xDdd[i] + "'").ToList();
            //    List<int> Ddd = retornaDDD();

            //    CarteiraGraficoViewModel c = new CarteiraGraficoViewModel();
            //    c.Ddd = Ddd[cont].ToString();
            //    c.TotalFaltantes = 0;
            //    c.TotalPreenchido = 0;

            //    foreach (DataRow dtRow in xRow)
            //    {
            //        if (Convert.ToInt32(dtRow["DDD"]) == Ddd[cont])
            //        {
            //            c.Ddd = dtRow["DDD"].ToString();
            //            c.TotalFaltantes = c.TotalFaltantes + Convert.ToInt32(dtRow["FALTANTES"]);
            //            c.TotalPreenchido = c.TotalPreenchido + Convert.ToInt32(dtRow["PREENCHIDOS"]);
            //        }
            //    }
            //    cont++;
            //    rs.Add(c);

            //}

            return Json(dt, JsonRequestBehavior.AllowGet);
        }
    }
    
}