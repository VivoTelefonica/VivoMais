using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;
using VivoMais.Models;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace VivoMais.Controllers
{
    public class SavingController : Controller
    {
        string connectioString = ConfigurationManager.ConnectionStrings["ConexaoSQL"].ConnectionString;
        private ParceiroRepositorio _repositorio;
        private SavingRepositorio _repositorioSaving;


        // GET: Saving
        public ActionResult Index()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());
            ViewData["MES"] = ObterMeses();

            return View();
        }

        public ActionResult ConsultarSaving()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());
            ViewData["MES"] = ObterMeses();

            return View();
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

        public JsonResult ObterParceirosRedePorDDDeCanalRecarga(string Ddd)
        {
            SavingRepositorio _repositorioSaving = new SavingRepositorio();

            var retorno = _repositorioSaving.ObterParceirosRedePorDDDeCanalRecarga(Ddd);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult Download(string Tipo, string Caminho)
        {
            return File(Caminho, "application/vnd.ms-excel", Tipo);
        }

        public ActionResult RetrieveImage(string id)
        {
            SavingRepositorio _repositorio = new SavingRepositorio();


            byte[] cover = _repositorio.GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }

            else
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult CadastrarSaving(SavingViewModel s, FormCollection formCollection)
        {
            _repositorio = new ParceiroRepositorio();

            HttpPostedFileBase upload = Request.Files["ComprovanteAcao"];

            if (upload != null && upload.ContentLength > 0)
            {
                s.saving.ExtensaoArquivo = System.IO.Path.GetExtension(upload.FileName).ToLower();

                if (s.saving.ExtensaoArquivo.Equals(".jpg"))
                {
                    s.saving.TipoArquivo = "imagem/jpg";
                }
                else if (s.saving.ExtensaoArquivo.Equals(".jpeg"))
                {
                    s.saving.TipoArquivo = "imagem/jpeg";
                }
                else if (s.saving.ExtensaoArquivo.Equals(".png"))
                {
                    s.saving.TipoArquivo = "imagem/png";
                }
                else
                {
                    ViewBag.result = "Formato de arquivo nao suportado. Utilize uma imagem com o formato JPG, JPEG ou PNG.";
                    ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());
                    return View("Index");
                }

                if (CadastrarArquivo(upload,s.saving))
                {
                    ViewBag.result = "Solicitação de Saving efetuada com sucesso!";
                    ViewData["DDD"] = _repositorio.ObterDdd(Session["Login"].ToString());
                    return View("Index");
                }
                else
                {
                    ViewBag.result = "Ocorreu um erro ao tentar processar sua solicitação!";
                    return View("Index");
                }
                
            }
            else
            {
                ViewBag.result = "Formato de arquivo inexistente. Utilize uma imagem com o formato JPG, JPEG ou PNG.";
                return View("Index");
            }
        }

        public bool CadastrarArquivo(HttpPostedFileBase upload, Saving saving) 
        {
            SqlConnection connection = new SqlConnection(connectioString);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("INSERT INTO [VIVO_SIM].[dbo].[SAVING]" +
                         " ([mesAcao] " +
                         " ,[ddd] " +
                         " ,[rede] " +
                         " ,[tipoAcao] " +
                         " ,[opcaoAcao] " +
                         " ,[oferta] " +
                         " ,[periodoInicial] " +
                         " ,[periodoFinal] " +
                         " ,[valorAcao] " +
                         " ,[descricao] " +
                         " ,[comprovanteAcao] " +
                         " ) VALUES " +
                         "('" + saving.MesAcao + "'" +
                         ",'" + saving.Parceiro.Ddd + "'" +
                         ",'" + saving.Parceiro.Rede + "'" +
                         ",'" + saving.TipoAcao + "'" +
                         ",'" + saving.OpcaoAcao + "'" +
                         ",'" + saving.Oferta + "'" +
                         ",'" + saving.PeriodoInicial + "'" +
                         ",'" + saving.PeriodoFinal + "'" +
                         ",'" + saving.ValorAcao + "'" +
                         ",'" + saving.Descricao + "'" +
                         " ," + "(@binaryValue))", connection))
            {
                cmd.Parameters.Add("@binaryValue", SqlDbType.Image).Value = ConvertToBytes(upload); ;
                cmd.ExecuteNonQuery();
            }

            try
            {
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase arquivo)
        {
            byte[] arquivoBytes = null;
            BinaryReader reader = new BinaryReader(arquivo.InputStream);

            arquivoBytes = reader.ReadBytes((int)arquivo.ContentLength);
            return arquivoBytes;
        }

        public JsonResult ExtrairSaving(string MesAcao)
        {
            _repositorio = new ParceiroRepositorio();
            SavingRepositorio _repositorioSaving = new SavingRepositorio();

            if (System.IO.File.Exists(@"C:\PPC\Saving.xls"))
            {
                System.IO.File.Delete(@"C:\PPC\Saving.xls");
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

            List<Saving> ListSaving = _repositorioSaving.ObterSaving(MesAcao);

            if (ListSaving.Count > 0)
            {
                xlWorkSheet.Cells[1, 1] = "AÇÃO";
                xlWorkSheet.Cells[1, 2] = "DDD";
                xlWorkSheet.Cells[1, 3] = "REDE";
                xlWorkSheet.Cells[1, 4] = "VALOR";
                xlWorkSheet.Cells[1, 5] = "TIPO DE AÇÃO";
                xlWorkSheet.Cells[1, 6] = "OPÇÃO DE AÇÃO";
                xlWorkSheet.Cells[1, 7] = "MÊS";
                xlWorkSheet.Cells[1, 8] = "OFERTA";
                
                int i = 2;
                foreach (Saving saving in ListSaving)
                {
                    xlWorkSheet.Cells[i, 1] = saving.Descricao;
                    xlWorkSheet.Cells[i, 2] = saving.Parceiro.Ddd;
                    xlWorkSheet.Cells[i, 3] = saving.Parceiro.Rede;
                    xlWorkSheet.Cells[i, 4] = saving.ValorAcao;
                    xlWorkSheet.Cells[i, 5] = saving.TipoAcao;
                    xlWorkSheet.Cells[i, 6] = saving.OpcaoAcao;
                    xlWorkSheet.Cells[i, 7] = saving.MesAcao;
                    xlWorkSheet.Cells[i, 8] = saving.Oferta;
                    i++;
                }

                xlWorkBook.SaveAs("C:\\PPC\\Saving.xls", XlFileFormat.xlExcel8, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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

        public PartialViewResult ObterListaSaving(SavingViewModel Save)
        {
            _repositorioSaving = new SavingRepositorio();

            SavingViewModel Reset = new SavingViewModel();
            Reset.listaSaving = _repositorioSaving.ObterSaving(Save.saving.MesAcao);

            return PartialView("PartialViewConsultarSaving", Reset);
        }

        public JsonResult retornaFaturamentoChipSaving(string Rede)
        {
            _repositorioSaving = new SavingRepositorio();

            Saving s = _repositorioSaving.retornaFaturamentoChipSaving(Rede);


            return Json(s.SaldoFaturado, JsonRequestBehavior.AllowGet);
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
    }
}