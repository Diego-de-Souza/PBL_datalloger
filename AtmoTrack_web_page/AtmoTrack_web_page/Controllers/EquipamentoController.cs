using AtmoTrack_web_page.DAO;
using AtmoTrack_web_page.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace AtmoTrack_web_page.Controllers
{
    public class EquipamentoController : PadraoController<EquipamentoViewModel>
    {
        public EquipamentoController()
        {
            DAO = new EquipamentoDAO();
            GeraProximoId = true;
            TipoRegistro = "Q";
        }

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
    }
}
