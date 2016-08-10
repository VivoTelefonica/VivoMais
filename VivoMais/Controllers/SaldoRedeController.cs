using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class SaldoRedeController : Controller
    {
        private SaldoRedeRepositorio _repositorio;

        // GET: SaldoRede
        public ActionResult Index()
        {
            ViewData["ANO"] = ObterAno();
            ViewData["MES"] = ObterMeses();

            return View();
        }

        public ActionResult CadastrarSaldoRede(SaldoRede Saldo, FormCollection formCollection) 
        {
            _repositorio = new SaldoRedeRepositorio();

            ViewData["ANO"] = ObterAno();
            ViewData["MES"] = ObterMeses();

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
                        ModelState.AddModelError("File", "Formato não suportado");
                        return View("Index");
                    }

                    reader.IsFirstRowAsColumnNames = true;
                    DataSet result = reader.AsDataSet();


                    if (_repositorio.InsereParceiros()) 
                    {
                        while (reader.Read())
                        {
                            if(reader[0] == null)
                            {
                                break;
                            }
                            else
                            {
                                if (reader[3].ToString().ToUpper() != "ORIGEM DA VERBA")
                                {
                                    if (reader[3].ToString().ToUpper() == "VPC")
                                    {
                                        _repositorio.InserirSaldoVpc(reader[0].ToString().ToUpper(), Saldo.Regional, Saldo.Ano, Saldo.Mes, reader[1].ToString());
                                    }
                                    else if (reader[3].ToString().ToUpper() == "VIO")
                                    {
                                        _repositorio.InserirSaldoVio(reader[0].ToString().ToUpper(), Saldo.Regional, Saldo.Ano, Saldo.Mes, reader[1].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }

            TempData["MSG"] = "Verbas Atualizada com Sucesso!";

            return View("Index");
        }

        public string[] ObterAno() 
        {
            string[] ano = new string[2];

            ano[0] = Convert.ToString(DateTime.Now.Year);
            ano[1] = Convert.ToString(DateTime.Now.Year + 1);

            return ano;
        }

        public string[] ObterMeses()
        {
            string[] mes = new string[2];

            mes[0] = DateTime.Now.Month.ToString();
            mes[1] = Convert.ToString(DateTime.Now.AddMonths(1).Month);

            return mes;
        }
    }
}