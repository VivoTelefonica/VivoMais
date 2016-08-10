using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivoMais.Models;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class BookPlanosController : Controller
    {
        private BookPlanosRepositorio _repositorio;

        // GET: BookPlanos
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ObterBookPlanos(BookPlanosViewModel BookPlanos)
        {
            _repositorio = new BookPlanosRepositorio();
            BookPlanosViewModel Book = null;


            if (BookPlanos.Ddd == "7x")
            {
                Book = new BookPlanosViewModel();
                Book.Book = _repositorio.ObterBookPlanos7x(BookPlanos.Plano);
            }
            else
            {
                Book = new BookPlanosViewModel();
                Book.Book = _repositorio.ObterBookPlanos8x(BookPlanos.Plano);
            }


            return PartialView("PartialViewBookPlanos", Book);
        }
    }
}