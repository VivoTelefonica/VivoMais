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
    public class VivoVisitaController : Controller
    {
        private ParceiroRepositorio _repositorioParceiro;
        private VivoVisitaRepositorio _repositorio;

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult AbrirCheckList()
        {
            _repositorioParceiro = new ParceiroRepositorio();

            ViewData["DDD"] = _repositorioParceiro.ObterDdd(Session["Login"].ToString());
            return View();
        }

        public ActionResult VisitaCanalTerritorio()
        {
            return View();
        }


        public ActionResult AbrirVisita(VivoVisitaViewModel Visita)
        {
            _repositorio = new VivoVisitaRepositorio();
            _repositorioParceiro = new ParceiroRepositorio();

            ViewData["DDD"] = _repositorioParceiro.ObterDdd(Session["Login"].ToString());

            if (Visita.Passagem.TipoAbertura.Equals("PASSAGEM DE LOJA"))
            {
                if (_repositorio.CadastrarPassagemAberturaVisita(Visita.Passagem, Session["Id"].ToString()))
                {
                    return View("Index");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (_repositorio.CadastrarCanalTerritorioAberturaVisita(Visita.Passagem, Session["Id"].ToString()))
                {
                    return View("VisitaCanalTerritorio");
                }
                else
                {
                    return null;
                }
            }


        }

        public ActionResult InsereVisitaPassagem(VivoVisitaViewModel Visita) 
        {
            _repositorio = new VivoVisitaRepositorio();
            _repositorioParceiro = new ParceiroRepositorio();

            VivoVisitaPassagemAbertura Abertura = _repositorio.ObterPassagemVivoVisita(Session["Id"].ToString());

            if (_repositorio.AtualizarPassagemLojaEstoque(Visita.Passagem.Estoque, Abertura.Id))
            {
                if (_repositorio.AtualizaPassagemLojaCaixa(Visita.Passagem.Caixa, Abertura.Id))
                {
                    if (_repositorio.AtualizaPassagemLojaEstruturaProcessos(Visita.Passagem.EstruturaProcessos, Abertura.Id))
                    {
                        if (_repositorio.AtualizaPassagemLojaPositivacao(Visita.Passagem.Positivacao, Abertura.Id))
                        {
                            if (_repositorio.AtualizaPassagemLojaPessoas(Visita.Passagem.Pessoas, Abertura.Id))
                            {
                                if (_repositorio.AtualizaPassagemLojaVagas(Visita.Passagem.Vagas, Abertura.Id))
                                {
                                    return View("Index");
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public ActionResult InsereVisitaCanalTerritorial(VivoVisitaViewModel Visita)
        {
            _repositorio = new VivoVisitaRepositorio();
            _repositorioParceiro = new ParceiroRepositorio();

            VivoVisitaCanalTerritorioAbertura Abertura = _repositorio.ObterCanalTerritorioVivoVisita(Session["Id"].ToString());

            if (_repositorio.AtualizaCanalTerritorioPessoas(Visita.CanalTerritorio.Pessoas, Abertura.Id))
            {
                if (_repositorio.AtualizaCanalTerritorioEstruturasOperacao(Visita.CanalTerritorio.EstruturaOperacoes, Abertura.Id))
                {
                    if (_repositorio.AtualizaCanalTerritorioProcessos(Visita.CanalTerritorio.Processos, Abertura.Id))
                    {
                        if (_repositorio.AtualizaCanalTerritorioIndicadores(Visita.CanalTerritorio.Indicadores, Abertura.Id))
                        {
                            return View("VisitaCanalTerritorio");
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}