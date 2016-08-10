using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using VivoMais.Models;
using VivoMais.Repositorio.Entidades;
using VivoMais.Repositorio.Repositorio;

namespace VivoMais.Controllers
{
    public class CadastroUsuarioController : Controller
    {
        private ParceiroRepositorio _repositorioParceiro;
        private AcessoRepositorio _repositorioAcesso;
        private CadastroUsuarioRepositorio _repositorio;

        public ActionResult Index()
        {
            _repositorioParceiro = new ParceiroRepositorio();

            ViewData["UF"] = _repositorioParceiro.ObterUf().Distinct();
            return View();
        }

        public RedirectToRouteResult Redireciona(string Login, string Senha) 
        {
            _repositorioAcesso = new AcessoRepositorio();

            Acesso xAcesso = _repositorioAcesso.ObterColaborador(Login, Senha);
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
                Session["Imagem"] = xAcesso.Imagem;
                Session["TipoMenu"] = "~/_LayoutPage.cshtml";

                return RedirectToAction("Index", "Principal");
            }
            else
            {
                return RedirectToAction("Index", "Acesso");
            }
        }

        public RedirectToRouteResult transacaoUsuario(RetornoAcessoUsuarioViewModel cu, FormCollection formCollection)
        {
            AcessoController Actrl = new AcessoController();
            CadastroUsuarioRepositorio cadUsuario = new CadastroUsuarioRepositorio();

            //HttpPostedFileBase file = Request.Files["UploadedFile"];

            //if (!Directory.Exists("C:\\ImagemVivoSim\\" + cu.Usuario.Login))
            //{
            //    Directory.CreateDirectory("C:\\ImagemVivoSim\\" + cu.Usuario.Login);

            //    FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, "C:\\ImagemVivoSim\\" + cu.Usuario.Login);
            //    f2.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, "C:\\ImagemVivoSim\\" + cu.Usuario.Login);
            //}
            //else
            //{
            //    Directory.Delete("C:\\ImagemVivoSim\\" + cu.Usuario.Login, true);
            //    Directory.CreateDirectory("C:\\ImagemVivoSim\\" + cu.Usuario.Login);
            //}

            //string pic = System.IO.Path.GetFileName(file.FileName);
            //string path = System.IO.Path.Combine("C:\\ImagemVivoSim\\" + cu.Usuario.Login + "\\" , pic);
            //// file is uploaded
            //file.SaveAs(path);







            try
            {
                if (cadUsuario.obterDadosUsuario(cu.Usuario.Login) == null)
                {
                    if (cu.Usuario.TipoAcesso == "LOJA")
                    {
                        cu.Parceiro.Canal = "Loja Própria";

                        if (cadUsuario.CadastrarTabelaAcesso(cu.Usuario, cu.Parceiro))
                        {
                            if (cadUsuario.CadastrarTabelaAcessoPermissaoMenu(cu.Usuario))
                            {
                                if (cadUsuario.CadastrarTabelaCadastroVivoSIM(cu.Usuario, cu.Parceiro))
                                {
                                    if (cadUsuario.CadastrarTabelaCadastroVivoSIMCanal(cu.Usuario, cu.Parceiro))
                                    {
                                        cadUsuario.CadastrarTabelaCadastroSELLIN(cu.Usuario, cu.Parceiro.Rede, cu.Parceiro);
                                        {
                                            return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
                                        }
                                    }
                                }
                            }
                        }
                        //else
                        //{
                        //    return View("Index");
                        //}

                    }
                    else if (cu.Usuario.TipoAcesso == "VENDAS" && cu.Usuario.Perfil != "SELL IN")
                    {
                        if (cadUsuario.CadastrarTabelaAcesso(cu.Usuario, cu.Parceiro))
                        {
                            if (cadUsuario.CadastrarTabelaAcessoPermissaoMenu(cu.Usuario))
                            {
                                return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
                            }
                        }
                        //else
                        //{
                        //    return View("Index");
                        //}

                    }
                    else if (cu.Usuario.TipoAcesso == "VENDAS" && cu.Usuario.Perfil == "SELL IN")
                    {
                        cu.ParceiroSellIn.Canal = "Varejo";

                        if (cadUsuario.CadastrarTabelaAcesso(cu.Usuario, cu.Parceiro))
                        {
                            if (cadUsuario.CadastrarTabelaAcessoPermissaoMenu(cu.Usuario))
                            {
                                foreach (string item in cu.RedeSellIn)
                                {
                                    cadUsuario.CadastrarTabelaCadastroSELLIN(cu.Usuario, item, cu.ParceiroSellIn);
                                }

                                return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);

                                //if (cadUsuario.CadastrarTabelaCadastroVivoSIMSellIn(cu.Usuario, cu.ParceiroSellIn.Ddd, cu.Parceiro.Uf))
                                //{
                                //    if (cadUsuario.CadastrarTabelaCadastroVivoSIMCanal(cu.Usuario, cu.ParceiroSellIn))
                                //    {
                                        
                                //    }
                                //}
                            }
                        }


                    }
                    else if (cu.Usuario.TipoAcesso == "ADMINISTRATIVO")
                    {

                        _repositorio = new CadastroUsuarioRepositorio();

                        cu.ListaCanalUsuario = _repositorio.buscarCanais();

                        if (cadUsuario.CadastrarTabelaAcesso(cu.Usuario, cu.Parceiro))
                        {
                            if (cadUsuario.CadastrarTabelaAcessoPermissaoMenu(cu.Usuario))
                            {
                                if (cu.Usuario.Perfil == "SUPORTE" || cu.Usuario.Perfil == "MARKETING REGIONAL")
                                {
                                    if(cadUsuario.CadastrarTabelaCadastroVivoSIMCanalTodosCanais(cu.Usuario))
                                    {
                                        if(cadUsuario.CadastraTodosDddUfs(cu.Usuario))
                                        {
                                            return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
                                        }
                                    }
                                    else 
                                    {
                                        View("Index");
                                    }
                                    //foreach (string item in cu.ListaDDDUsuario)
                                    //{
                                    //    cadUsuario.CadastrarTabelaCadastroVivoSIM(cu.Usuario, item);
                                    //}
                                }
                                else
                                {
                                    if (cadUsuario.CadastrarTabelaCadastroVivoSIMCanalTodosCanais(cu.Usuario))
                                    {
                                        if (cadUsuario.CadastrarTabelaCadastroVivoSIM(cu.Usuario, cu.Parceiro))
                                        {
                                            return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
                                        }
                                    }
                                    else
                                    {
                                        View("Index");
                                    }
                                }


                                //cadUsuario.CadastrarTabelaCadastroVivoSIMCanalTodosCanais(cu.Usuario);
                                //{
                                //    return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
                                //}

                            }
                        }
                    }
                    //else
                    //{
                    //    return View("Index");
                    //}
                }

                else
                {
                    if (cu.Usuario.TipoAcesso == "LOJA")
                    {
                        cu.Parceiro.Canal = "Loja Própria";

                        if (cadUsuario.AlteraTabelaAcesso(cu.Usuario, cu.Parceiro))
                        {
                            if (cadUsuario.AlteraTabelaAcessoPermissaoMenu(cu.Usuario))
                            {
                                if (cadUsuario.AlteraTabelaCadastroVivoSIM(cu.Usuario, cu.Parceiro))
                                {
                                    if (cadUsuario.AlteraTabelaCadastroVivoSIMCanal(cu.Usuario, cu.Parceiro))
                                    {
                                        cadUsuario.DeletarTabelaCadastroSELLIN(cu.Usuario);
                                        cadUsuario.CadastrarTabelaCadastroSELLIN(cu.Usuario, cu.Parceiro.Rede, cu.Parceiro);
                                        {
                                            return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
                                        }
                                    }
                                }
                            }
                        }
                        //else
                        //{
                        //    return View("Index");
                        //}

                    }

                    else if (cu.Usuario.TipoAcesso == "VENDAS" && cu.Usuario.Perfil != "SELL IN")
                    {
                        if (cadUsuario.AlteraTabelaAcesso(cu.Usuario, cu.Parceiro))
                        {
                            if (cadUsuario.AlteraTabelaAcessoPermissaoMenu(cu.Usuario))
                            {
                                return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
                            }
                        }
                        //else
                        //{
                        //    return View("Index");
                        //}

                    }
                    else if (cu.Usuario.TipoAcesso == "VENDAS" && cu.Usuario.Perfil == "SELL IN")
                    {

                        cu.Parceiro.Canal = "Varejo";

                        if (cadUsuario.AlteraTabelaAcesso(cu.Usuario, cu.Parceiro))
                        {
                            if (cadUsuario.AlteraTabelaAcessoPermissaoMenu(cu.Usuario))
                            {
                                if (cadUsuario.AlteraTabelaCadastroVivoSIM(cu.Usuario, cu.Parceiro))
                                {
                                    cadUsuario.DeletarTabelaCadastroSELLIN(cu.Usuario);

                                    foreach (string item in cu.RedeSellIn)
                                    {
                                        cadUsuario.CadastrarTabelaCadastroSELLIN(cu.Usuario, item, cu.Parceiro);
                                    }

                                    return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);

                                }
                            }
                        }

                        //else
                        //{
                        //    return View("Index");
                        //}
                    }
                    else if (cu.Usuario.TipoAcesso == "ADMINISTRATIVO")
                    {
                        if (cadUsuario.AlteraTabelaAcesso(cu.Usuario, cu.Parceiro))
                        {
                            if (cu.Usuario.Perfil != "SUPORTE")
                            {
                                cadUsuario.AlteraTabelaCadastroVivoSIM(cu.Usuario, cu.Parceiro);
                            }
                            else if (cu.Usuario.Perfil == "SUPORTE")
                            {
                                cadUsuario.DeletaTabelaCadastroVivoSIM(cu.Usuario);

                                foreach (string item in cu.ListaDDDUsuario)
                                {
                                    cadUsuario.CadastrarTabelaCadastroVivoSIM(cu.Usuario, item);
                                }

                                cadUsuario.DeletaTabelaCadastroVivoSIMCanal(cu.Usuario);

                                cadUsuario.CadastrarTabelaCadastroVivoSIMCanalTodosCanais(cu.Usuario);

                                return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
                            }
                        }
                    }
                    //else
                    //{
                    //    return View("Index");
                    //}
                }
            }
            catch
            {
                //return View("Index");
            }

            return Redireciona(cu.Usuario.Login, cu.Usuario.Senha);
        }

        public JsonResult ObterPerfil(string TipoDeAcesso)
        {
            ArrayList retorno = new ArrayList();
            if (TipoDeAcesso == "LOJA")
            {
                retorno.Add("GERENTE GERAL - LLPP");
                retorno.Add("GERENTE DE NEGÓCIO - LLPP");

            }
            if (TipoDeAcesso == "VENDAS")
            {
                retorno.Add("GERENTE DE CONTAS");
                retorno.Add("SELL IN");
            }
            if (TipoDeAcesso == "ADMINISTRATIVO")
            {
                retorno.Add("SUPORTE");
                retorno.Add("MARKETING REGIONAL");
                retorno.Add("MARKETING TERRITORIAL");
                retorno.Add("TERRITORIAL");
                retorno.Add("GERENTE DDD");
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult obterDadosUsuario(string Login)
        {
            CadastroUsuario cu = new CadastroUsuario();
            RetornoAcessoUsuarioViewModel retornoUsuario = new RetornoAcessoUsuarioViewModel();

            _repositorio = new CadastroUsuarioRepositorio();
            _repositorioParceiro = new ParceiroRepositorio();

            retornoUsuario.Usuario = _repositorio.obterDadosUsuario(Login);

            retornoUsuario.ListaDDDUsuario = _repositorio.buscarListaDDDUsuario(Login);

            retornoUsuario.DddUsuario = _repositorio.buscarDDDPorUsuario(Login);
            retornoUsuario.RedeSellIn = _repositorio.buscarRedePorUsuario(Login);

            if (retornoUsuario.Usuario != null)
            {
                retornoUsuario.RedePorDDD = _repositorio.ObterParceirosRedePorDDD(Login, retornoUsuario.Parceiro.Ddd);

                if (retornoUsuario.Usuario.TipoAcesso == "VENDAS" && retornoUsuario.Usuario.Perfil != "SELL IN")
                {
                    _repositorio.obterDadosUsuario(Login);
                }
                else if (retornoUsuario.Usuario.TipoAcesso == "LOJA")
                {
                    _repositorio.obterDadosUsuario(Login);
                    _repositorio.buscarListaDDDUsuario(Login);
                }
                else if (retornoUsuario.Usuario.TipoAcesso == "VENDAS" && retornoUsuario.Usuario.Perfil == "SELL IN")
                {
                    _repositorio.obterDadosUsuario(Login);
                    _repositorio.buscarListaDDDUsuario(Login);
                    _repositorio.buscarRedePorUsuario(Login);
                    _repositorioParceiro.ObterParceirosRede(retornoUsuario.Parceiro.Ddd);
                }
                else if (retornoUsuario.Usuario.TipoAcesso == "ADMINISTRATIVO")
                {
                    _repositorio.obterDadosUsuario(Login);
                    _repositorio.buscarListaDDDUsuario(Login);
                }
            }

            return Json(retornoUsuario, JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscarDDDPorUsuario(string Login)
        {
            _repositorio = new CadastroUsuarioRepositorio();
            var rede = _repositorio.buscarDDDPorUsuario(Login);
            return Json(rede, JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscarRedePorUsuario(string Login, string DDD)
        {
            _repositorio = new CadastroUsuarioRepositorio();
            _repositorioParceiro = new ParceiroRepositorio();

            var rede = _repositorioParceiro.ObterParceirosRede(DDD);
            return Json(rede, JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscarRede(string DDD)
        {
            RetornoAcessoUsuarioViewModel retornoUsuario = new RetornoAcessoUsuarioViewModel();
            _repositorio = new CadastroUsuarioRepositorio();
            _repositorioParceiro = new ParceiroRepositorio();

            retornoUsuario.RedePorDDD = _repositorioParceiro.ObterParceirosRede(DDD);
            return Json(retornoUsuario, JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscarLoja(string DDD)
        {
            _repositorio = new CadastroUsuarioRepositorio();
            var canal = _repositorio.buscarLoja(DDD);
            return Json(canal, JsonRequestBehavior.AllowGet);
        }

    }
    
}