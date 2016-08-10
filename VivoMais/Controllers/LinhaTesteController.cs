using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Models;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class LinhaTesteController : Controller
    {
        private LinhaTesteRepositorio _repositorio;

        // GET: LinhaTeste
        public ActionResult Index()
        {
            return View();
        }

        public FileContentResult Download(string Id)
        {
            _repositorio = new LinhaTesteRepositorio();

            SqlDataReader rdr;
            byte[] Arquivo = null;
            string Extensao = "";
            const string connect = @"Server=pesrvdrnd2;Database=Vivo_SIM;User id=sa;password=M@mc161187;";
            using (var con = new SqlConnection(connect))
            {
                var query = "SELECT DISTINCT [ARQUIVO],[EXTENSAO] FROM [Vivo_SIM].[dbo].[LINHA_TESTE_CADASTRO] WHERE ID = '" + Id + "'";
                var cmd = new SqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    Arquivo = (byte[])rdr["ARQUIVO"];
                    Extensao = rdr["EXTENSAO"].ToString();
                }
            }

            if (Extensao == ".doc")
            {
                return File(Arquivo, "vnd.ms-word", ("LinhaTeste" + Extensao));
            }
            if (Extensao == ".docx")
            {
                return File(Arquivo, "vnd.ms-word", ("LinhaTeste" + Extensao));
            }
            else if (Extensao == ".pdf")
            {
                return File(Arquivo, "application/pdf", ("LinhaTeste" + Extensao));
            }
            else
            {
                return File(Arquivo, "", "");
            }

            
        }

        public ActionResult Relatorio()
        {
            _repositorio = new LinhaTesteRepositorio();

            LinhaTesteViewModel Teste = new LinhaTesteViewModel();
            Teste.ListaLinhaTeste = _repositorio.ObterTodosLinhaTeste();

            return View(Teste);
        }

        [HttpPost]
        public ActionResult InserirLinhaTeste(LinhaTesteViewModel Linha, FormCollection formCollection)
        {
            LinhaTesteRepositorio _repositorio = new LinhaTesteRepositorio();

            HttpPostedFileBase file = Request.Files["UploadedFile"];

            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith(".pdf"))
                {
                    _repositorio.InserirLinhaTeste(file, Linha.LinhaTeste, System.IO.Path.GetExtension(file.FileName));
                    ViewBag.result = "Cadastro efetuado com sucesso!";
                }
                else if (file.FileName.EndsWith(".doc"))
                {
                    _repositorio.InserirLinhaTeste(file, Linha.LinhaTeste, System.IO.Path.GetExtension(file.FileName));
                    ViewBag.result = "Cadastro efetuado com sucesso!";
                }
                else if (file.FileName.EndsWith(".docx"))
                {
                    _repositorio.InserirLinhaTeste(file, Linha.LinhaTeste, System.IO.Path.GetExtension(file.FileName));
                    ViewBag.result = "Cadastro efetuado com sucesso!";
                }
                else
                {
                    ModelState.AddModelError("File", "Formato Não Suportado. Tenta outra vez.");
                    ViewBag.result = "Ocorreu um erro no formato do arquivo. Só é permitido arquivos de WORD e PDF!";
                }
            }

            return View("Index");
        }
    }
}