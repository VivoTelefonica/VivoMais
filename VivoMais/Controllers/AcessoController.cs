using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class AcessoController : Controller
    {
        private AcessoRepositorio _repositorio;

        // GET: Acesso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Logar(Acesso acesso)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new AcessoRepositorio();
                Acesso xAcesso = _repositorio.ObterColaborador(acesso.Login, acesso.Senha);
                if (xAcesso != null)
                {
                    Session.Timeout = 900;
                    Session["Id"] = xAcesso.IdAcesso;
                    Session["Login"] = xAcesso.Login;
                    Session["Nome"] = xAcesso.Nome;
                    Session["Email"] = xAcesso.Email;
                    Session["Regional"] = xAcesso.Regional;
                    Session["Funcao"] = xAcesso.Funcao;
                    Session["Perfil"] = xAcesso.Perfil;
                    Session["Imagem"] = "Erinaldo.jpg";
                    Session["TipoMenu"] = "~/_LayoutPage.cshtml";

                    return RedirectToAction("Index", "Principal");
                }
                else
                {
                    return RedirectToAction("Index", "Acesso");
                }
            }
            else
            {
                return RedirectToAction("Index", "Acesso");
            }
        }
    }
}