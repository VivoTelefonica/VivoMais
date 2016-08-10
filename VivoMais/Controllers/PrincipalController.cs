using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Models;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class PrincipalController : Controller
    {
        private AcessoTerceirosRepositorio _repositorioAcesso;

        public ActionResult Index()
        {
            _repositorioAcesso = new AcessoTerceirosRepositorio();

            PrincipalViewModel Principal = new PrincipalViewModel();
            Principal.Acesso = _repositorioAcesso.ObterEsperandoMobileToken(Session["Login"].ToString());
            Principal.SenhaLiberada = _repositorioAcesso.ObterLoginSenha(Session["Login"].ToString());

            return View(Principal);
        }

        public JsonResult AtualizarMobileToken(string[] ArrayCk)
        {
            _repositorioAcesso = new AcessoTerceirosRepositorio();

            if(ArrayCk == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                for (int i = (ArrayCk.Length - 1); i <= (ArrayCk.Length - 1); i--)
                {
                    if (i < 0)
                    {
                        break;
                    }
                    else
                    {
                        _repositorioAcesso.AtualizarMobileToken(ArrayCk[i]);
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult RecusarSenhaAcesso(string Id)
        {
            _repositorioAcesso = new AcessoTerceirosRepositorio();

            if (Id == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _repositorioAcesso.RecusarSenhaAcesso(Id);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AceitarSenhaAcesso(string Id)
        {
            _repositorioAcesso = new AcessoTerceirosRepositorio();

            if (Id == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _repositorioAcesso.AceitarSenhaAcesso(Id);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}