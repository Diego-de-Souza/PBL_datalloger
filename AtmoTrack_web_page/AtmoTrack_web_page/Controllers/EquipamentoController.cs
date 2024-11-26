using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace AtmoTrack_web_page.Controllers
{
    public class EquipamentoController : PadraoController<EquipamentoViewModel>
    {
        public EquipamentoController()
        {
            DAO = new EquipamentoDAO();
            GeraProximoId = true;
            NomeServiceRegistro = "Q";
        }

        private DashboardDAO _dashboardDAO = new DashboardDAO();

        public override void ValidaDados(EquipamentoViewModel equipamento, string operacao, string statusId)
        {
            ModelState.Clear(); // Limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês) 
            EquipamentoDAO dao = new EquipamentoDAO();


            if (equipamento.Id <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            else
            {
                if (operacao == "I" && statusId != null)
                    ModelState.AddModelError("Id", "Código já está em uso.");
                if (operacao == "A" && statusId == null)
                    ModelState.AddModelError("Id", "Usuário não existe.");
            }

            if (string.IsNullOrEmpty(equipamento.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");

            if (string.IsNullOrEmpty(equipamento.MacAddress))
                ModelState.AddModelError("MacAddress", "Preencha o MacAddress.");

            if (string.IsNullOrEmpty(equipamento.IpAddress))
                ModelState.AddModelError("IpAddress", "Preencha o IpAdress.");

            if (equipamento.EmpresaId <= 0)
                ModelState.AddModelError("EmpresaId", "Selecione uma empresa.");

            if (string.IsNullOrEmpty(equipamento.SSID))
                ModelState.AddModelError("SSID", "Preencha o SSID.");

            if (equipamento.SignalStrength <= 0)
                ModelState.AddModelError("SignalStrength", "Selecione força do sinal.");

            if (string.IsNullOrEmpty(equipamento.ConnectionStatus))
                ModelState.AddModelError("ConnectionStatus", "Preencha o status da conexão.");

            if (string.IsNullOrEmpty(equipamento.SensorData))
                ModelState.AddModelError("SensorData", "Preencha os dados do sensor.");

            if (string.IsNullOrEmpty(equipamento.StatusEquipamento))
                ModelState.AddModelError("StatusEquipamento", "Preencha o status do equipamento.");

            if (string.IsNullOrEmpty(equipamento.AuthToken))
                ModelState.AddModelError("AuthToken", "Preencha o token de autenticação.");

            if (string.IsNullOrEmpty(equipamento.FirmwareVersion))
                ModelState.AddModelError("FirmwareVersion", "Preencha a versão do firmware.");

            if (equipamento.LastUpdate == DateTime.MinValue)
            {
                ModelState.AddModelError("LastUpdate", "Data de atualização inválida.");
            }
            else
            {
                DateTime minDate = new DateTime(1900, 1, 1); // Exemplo de data mínima aceita pelo banco de dados
                DateTime maxDate = DateTime.Now;

                if (equipamento.LastUpdate < minDate || equipamento.LastUpdate > maxDate)
                {
                    ModelState.AddModelError("LastUpdate", "Data de atualização fora do intervalo permitido.");
                }
            }
        }

        //[HttpPost]
        //public override async Task<JsonResult> CriaServico(string empresa)
        //{
        //    try
        //    {
        //        // Criar serviço IoT
        //        var criaServico = await _dashboardDAO.CreateIoTServiceAsync(empresa);

        //        if (criaServico == "{}")
        //        {
        //            // Registrar dispositivo "Lamp"
        //            var criaDispositivoLamp = await _dashboardDAO.RegisterDeviceAsync(empresa);
        //            if (criaDispositivoLamp == "{}")
        //            {
        //                // Registrar comandos para a lâmpada
        //                var criaComandosLamp = await _dashboardDAO.RegisterLampCommandsAsync(empresa);
        //                if (criaComandosLamp != "")
        //                {
        //                    throw new Exception("Erro ao registrar comandos para a lâmpada.");
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("Erro ao registrar dispositivo Lamp.");
        //            }

        //            // Adicionar assinatura de notificações de temperatura
        //            var criaServicoNotificacao = await _dashboardDAO.AddTemperatureSubscriptionAsync(empresa);
        //            if (criaServicoNotificacao != "")
        //            {
        //                throw new Exception("Erro ao adicionar assinatura de notificações de temperatura.");
        //            }

        //            // Criar entidade no Orion
        //            var criaOrion = await _dashboardDAO.CreateOrionAsync(empresa);
        //            if (criaOrion != "")
        //            {
        //                throw new Exception("Erro ao criar entidade no Orion.");
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("Erro ao criar serviço IoT.");
        //        }

        //        // Retornar "ok" como resposta final após todas as operações bem-sucedidas
        //        return Json("ok");
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        // Retornar erro em caso de falha na requisição HTTP
        //        return Json($"Erro ao criar os serviços: {e.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Tratar erros gerais
        //        return Json($"Erro inesperado: {ex.Message}");
        //    }
        //}
        public IActionResult ExibeConsultaAvancada()
        {
            try
            {
                EquipamentoBuscaAvancadaViewModel equipamentoBusca = new EquipamentoBuscaAvancadaViewModel();

                EquipamentoDAO EquipamentoDAO = new EquipamentoDAO();
                var listaequipamentos = EquipamentoDAO.Listagem();
                EmpresaDAO empresaDAO = new EmpresaDAO();
                var listaEmpresas = empresaDAO.Listagem();
                var listaBusca = listaequipamentos.Select(equipamento => new EquipamentoBuscaAvancadaViewModel
                {
                    Nome = equipamento.Nome,
                    NomeFantasia = listaEmpresas
                        .Where(empresa => empresa.Id == equipamento.EmpresaId)
                        .Select(empresa => empresa.NomeFantasia)
                        .FirstOrDefault(),
                    EmpresaId = listaEmpresas
                        .Where(empresa => empresa.Id == equipamento.EmpresaId)
                        .Select(empresa => empresa.EmpresaId)
                        .FirstOrDefault(),
                    LastUpdate = equipamento.LastUpdate
                }).ToList();

                return View("BuscaAvancada", listaBusca);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.Message));
            }
        }
        public IActionResult ObtemDadosConsultaAvancada(string nome, string empresaId, string nomefantasia, DateTime lastupdate)
        {
            try
            {
                EquipamentoDAO dao = new EquipamentoDAO();
                if (string.IsNullOrEmpty(nome))
                    nome = "";
                if (string.IsNullOrEmpty(empresaId))
                    empresaId = "";
                if (string.IsNullOrEmpty(nomefantasia))
                    nomefantasia = "";
                if (lastupdate.Date == Convert.ToDateTime(lastupdate))
                    lastupdate = SqlDateTime.MinValue.Value;
                var lista = dao.ConsultaAvancadaEquipamento(nome, empresaId, nomefantasia, lastupdate);
                return PartialView("pvGridEq", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }
    }
}
