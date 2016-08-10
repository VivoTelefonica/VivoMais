using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;
using VivoMais.Models;

namespace VivoMais.Controllers
{
    public class DescredenciamentoController : Controller
    {
        private ParceiroRepositorio _repositorio;
        private DescredenciamentoRepositorio dr;
        private DescredenciamentoViewModel descViewModel;

        public ActionResult Index()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
            return View();
        }

        public ActionResult AtualizarDescredenciamento()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
            return View();
        }

        public ActionResult ConsultarDescredenciamento()
        {
            _repositorio = new ParceiroRepositorio();
            ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
            return View();
        }

        [HttpPost]
        public ActionResult SolicitarDescredenciamento(Descredenciamento d, HttpPostedFileBase CartaSolicitacao)
        {
            _repositorio = new ParceiroRepositorio();

            HttpPostedFileBase file = Request.Files["CartaSolicitacao"];

            d.ExtensaoArquivo = System.IO.Path.GetExtension(file.FileName).ToLower();

            if (d.ExtensaoArquivo.Equals(".doc"))
            {
                d.TipoArquivo = "application/vnd.ms-word";
            }
            else if (d.ExtensaoArquivo.Equals(".docx"))
            {
                d.TipoArquivo = "application/vnd.ms-word";
            }
            else if (d.ExtensaoArquivo.Equals(".msg"))
            {
                d.TipoArquivo = "application/vnd.ms-outlook";
            }
            else
            {
                ViewBag.result = "Formato de arquivo não suportado. Utilize um arquivo word ou um anexo de email.";
                ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
                return View("Index");
            }

            DescredenciamentoRepositorio dr = new DescredenciamentoRepositorio();
            bool VerificarAdabas = dr.VerificarAdabas(d);

            if (VerificarAdabas == false)
            {
                dr.SolicitarDescredenciamento(file, d);
                ViewBag.result = "Solicitação de Descredenciamento efetuada com sucesso!";
                ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
                return View("Index");
            }
            else
            {
                ViewBag.result = "O parceiro já está em processo de descredenciamento.";
                ViewData["UF"] = _repositorio.ObterUf(Session["Login"].ToString()).Distinct();
                return View("Index");
            }

        }

        public PartialViewResult buscarDescredenciamento(DescredenciamentoViewModel Descredenciamentos)
        {
            DescredenciamentoRepositorio _repositorioDescredenciamento = new DescredenciamentoRepositorio();

            DescredenciamentoViewModel descredenciamento = new DescredenciamentoViewModel();
            descredenciamento.ListaDescredenciamento = _repositorioDescredenciamento.buscarDescredenciamento(Descredenciamentos.Descredenciamento.Parceiro.Rede, Descredenciamentos.Descredenciamento.Parceiro.Vendedor, Descredenciamentos.Descredenciamento.TipoDescredenciamento, Descredenciamentos.Descredenciamento.Status);

            return PartialView("PartialViewConsultarDescredenciamento", descredenciamento);
        }

        public PartialViewResult buscarDescredenciamentoAlteracao(DescredenciamentoViewModel Descredenciamentos)
        {
            DescredenciamentoRepositorio _repositorioDescredenciamento = new DescredenciamentoRepositorio();

            DescredenciamentoViewModel descredenciamento = new DescredenciamentoViewModel();
            descredenciamento.ListaDescredenciamento = _repositorioDescredenciamento.buscarDescredenciamento(Descredenciamentos.Descredenciamento.Parceiro.Rede, Descredenciamentos.Descredenciamento.Parceiro.Vendedor, Descredenciamentos.Descredenciamento.TipoDescredenciamento, Descredenciamentos.Descredenciamento.Status);

            return PartialView("PartialViewAlterarDescredenciamento", descredenciamento);

        }

        [HttpPost]
        public ActionResult AtualizarDescredenciamento(Descredenciamento descredenciamento)
        {
            dr.AlterarDescredenciamentoPorVigencia(descredenciamento);

            return View("PartialViewPopUpAtualizarDescredenciamento");
        }

        [HttpPost]
        public ActionResult AtualizarDescredenciamentoPorInatividade(Descredenciamento descredenciamento)
        {
            dr.AlterarDescredenciamentoPorInatividade(descredenciamento);

            return View("PartialViewPopUpAtualizarDescredenciamento");
        }

        [HttpPost]
        public ActionResult AtualizarDescredenciamentoAmigavel(Descredenciamento descredenciamento)
        {
            dr.AlterarDescredenciamentoAmigavel(descredenciamento);

            return View("PartialViewPopUpAtualizarDescredenciamento");
        }

        [HttpPost]
        public ActionResult AtualizarDescredenciamentoPorVigencia(Descredenciamento descredenciamento)
        {
            dr.AlterarDescredenciamentoPorVigencia(descredenciamento);

            return View("PartialViewPopUpAtualizarDescredenciamento");
        }

        public ActionResult PartialViewAtualizacaoAmigavel(int Id)
        {
            DescredenciamentoRepositorio _repositorioDescredenciamento = new DescredenciamentoRepositorio();

            DescredenciamentoViewModel descredenciamento = new DescredenciamentoViewModel();
            descredenciamento.Descredenciamento = _repositorioDescredenciamento.ObterDescredenciamentoID(Id);

            return PartialView("PartialViewPopUpAtualizarDescredenciamentoAmigavel", descredenciamento);

        }

        public ActionResult PartialViewAtualizacaoInatividade(int Id)
        {

            DescredenciamentoRepositorio _repositorioDescredenciamento = new DescredenciamentoRepositorio();

            DescredenciamentoViewModel descredenciamento = new DescredenciamentoViewModel();
            descredenciamento.Descredenciamento = _repositorioDescredenciamento.ObterDescredenciamentoID(Id);

            return PartialView("PartialViewPopUpAtualizarDescredenciamento", descredenciamento);

        }

        public ActionResult PartialViewAtualizacaoVigencia(int Id)
        {

            DescredenciamentoRepositorio _repositorioDescredenciamento = new DescredenciamentoRepositorio();

            DescredenciamentoViewModel descredenciamento = new DescredenciamentoViewModel();
            descredenciamento.Descredenciamento = _repositorioDescredenciamento.ObterDescredenciamentoID(Id);

            return PartialView("PartialViewPopUpAtualizarDescredenciamentoVigencia", descredenciamento);

        }

        public JsonResult ObterCNPJ(string Adabas)
        {

            _repositorio = new ParceiroRepositorio();
            return Json(_repositorio.ObterGerenteContas(Adabas, Session["Login"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public FileContentResult GetFile(int id)
        {
            SqlDataReader rdr;
            byte[] fileContent = null;
            string fileType = "";
            string file_Name = "";
            const string connect = @"Server=pesrvdrnd2;Database=Vivo_SIM;User id=sa;password=M@mc161187;";
            using (var con = new SqlConnection(connect))
            {
                var query = "SELECT [ARQUIVO_DESCREDENCIAMENTO] , [TIPO_ARQUIVO] , [EXTENSAO_ARQUIVO] FROM Vivo_SIM.dbo.DESCREDENCIAMENTO_CADASTRO WHERE ID = '" + id + "'";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    fileContent = (byte[])rdr["ARQUIVO_DESCREDENCIAMENTO"];
                    fileType = rdr["EXTENSAO_ARQUIVO"].ToString();
                    file_Name = rdr["TIPO_ARQUIVO"].ToString();
                }
            }
            return File(fileContent, fileType, ("Descredenciamento" + fileType));
        }


        [HttpGet]
        public virtual ActionResult Download(string Tipo, string Caminho)
        {
            return File(Caminho, "application/application/vnd.ms-word", Tipo);
        }
    }
}