﻿using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace AtmoTrack_web_page.Controllers
{
    public class PadraoController<T> : Controller where T : PadraoViewModel
    {
        protected PadraoDAO<T> DAO { get; set; }
        protected bool GeraProximoId { get; set; }
        protected string NomeViewIndex { get; set; } = "index";
        protected string NomeViewForm { get; set; } = "Form";
        protected string NomeViewCadastro { get; set; } = "BuscaAvancada";
        protected string NomeServiceRegistro { get; set; }

        private UsuarioDAO _usuarioDAO = new UsuarioDAO();
        private EmpresaDAO _empresaDAO = new EmpresaDAO();
        private EquipamentoDAO _equipamentoDAO = new EquipamentoDAO();

        public virtual IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var lista = DAO.Listagem(); 
                    return View(NomeViewIndex, lista);
                }
                catch (Exception erro)
                {
                    return View("Error", new ErrorViewModel(erro.ToString()));
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public virtual Task<JsonResult> CriaServico(string empresa)
        {
            return Task.FromResult(Json("Base method not implemented."));
        }
        public virtual IActionResult Create()
        {
            if (User.Identity.IsAuthenticated || NomeServiceRegistro == "U")
            {
                try
                {

                    ViewBag.Operacao = "I";
                    T model = Activator.CreateInstance<T>();
                    PreencheDadosParaView("I", model);

                    if (NomeServiceRegistro == "Q")
                    {
                        var empresas = _equipamentoDAO.GetAllEmpresas().Select(e => new SelectListItem
                        {
                            Value = e.Id.ToString(),
                            Text = e.NomeService
                        }).ToList();

                        ViewBag.Empresas = empresas;

                    }

                    return View(NomeViewForm, model);
                }
                catch (Exception erro)
                {
                    return View("Error", new ErrorViewModel(erro.ToString()));
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        protected virtual void PreencheDadosParaView(string Operacao, T model)
        {
            if (GeraProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();

        }

        public virtual void ValidaDados(T model, string operacao, string statusId) { }

        public virtual async Task<IActionResult> Salvar(T model, string Operacao)
        {
            try
            {
                var statusId = DAO.Consulta(model.Id) != null ? "ok" : null;

                ValidaDados(model, Operacao, statusId);
                if (!ModelState.IsValid)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View(NomeViewForm, model);
                }
                else
                {
                    if (Operacao == "I")
                    {
                        DAO.InsertDinamico(model);
                        if (NomeServiceRegistro == "Q")
                        {
                            var empresaParaServico = _empresaDAO.Consulta(model.EmpresaId);

                            // Aguarda a conclusão de CriaServico antes de prosseguir
                            //var resultadoServico = await CriaServico(empresaParaServico.NomeService);

                            //if (resultadoServico?.Value?.ToString() != "ok")
                            //{
                            //    throw new Exception("Erro ao criar o serviço IoT.");
                            //}
                        }
                    }
                    else
                    {
                        if(NomeServiceRegistro == "U")
                        {
                           var dadosAExtrair =  _usuarioDAO.Consulta(model.Id);
                            model.DataRegistro = dadosAExtrair.DataRegistro;
                        }
                        DAO.AlterDinamico(model);
                    }

                    return RedirectToAction(NomeViewIndex);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }


        public virtual IActionResult Editar(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    ViewBag.Operacao = "A";
                    var model = DAO.Consulta(id);
                    if (model == null)
                        return RedirectToAction(NomeViewIndex);
                    else
                    {
                        if (NomeServiceRegistro == "Q")
                        {
                            var empresas = _equipamentoDAO.GetAllEmpresas().Select(e => new SelectListItem
                            {
                                Value = e.Id.ToString(),
                                Text = e.NomeFantasia
                            }).ToList();

                            ViewBag.Empresas = empresas;
                        }

                        PreencheDadosParaView("A", model);
                        return View(NomeViewForm, model);
                    }
                }
                catch (Exception erro)
                {
                    return View("Error", new ErrorViewModel(erro.ToString()));
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public virtual IActionResult Delete(int id)
        {
            try
            {
                DAO.Delete(id);
                return RedirectToAction(NomeViewIndex);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }

        public virtual IActionResult Exibir(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var viewRetorno = "";

                    var model = DAO.Consulta(id);
                    if (model == null)
                    {
                        return NotFound();
                    }

                    if (NomeServiceRegistro == "U")
                    {
                        viewRetorno = "VisualizarUsuario";
                    }
                    else if (NomeServiceRegistro == "E")
                    {
                        viewRetorno = "ExibirEmpresa";
                    }
                    else if (NomeServiceRegistro == "Q")
                    {
                        var empresaId = _equipamentoDAO.ConsultaEmpresa(model.EmpresaId);

                        ViewBag.ServiceNome = empresaId != null ? empresaId.NomeService : "Estado não encontrado";
                        viewRetorno = "ExibirEquipamento";
                    }

                    return View(viewRetorno, model);
                }
                catch (Exception erro)
                {
                    return View("Error", erro.ToString());
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public virtual IActionResult BuscaAvancada()
        {
            return View(NomeViewCadastro);
        }
    }
}
