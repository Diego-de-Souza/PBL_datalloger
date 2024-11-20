function buscaCEP() {
    var cep = document.getElementById("Cep").value;
    cep = cep.replace('-', ''); // removemos o traço do CEP
    if (cep.length > 0) {
        var linkAPI = 'https://viacep.com.br/ws/' + cep + '/json/';
        $.ajax({
            url: linkAPI,
            beforeSend: function () {
                document.getElementById("Endereco").value = '...';
                document.getElementById("Estado").value = '...';
                document.getElementById("Cidade").value = '...';
                document.getElementById("Uf").value = '...';
                document.getElementById("Bairro").value = '...';
            },
            success: function (dados) {
                if (dados.erro != undefined) // quando o CEP não existe...
                {
                    alert('CEP não localizado...');
                    document.getElementById("Endereco").value = '';
                    document.getElementById("Estado").value = '';
                    document.getElementById("Cidade").value = '';
                    document.getElementById("Uf").value = '';
                    document.getElementById("Bairro").value = '';
                }
                else // quando o CEP existe
                {
                    document.getElementById("Endereco").value = dados.logradouro;
                    document.getElementById("Estado").value = dados.estado;
                    document.getElementById("Cidade").value = dados.localidade;
                    document.getElementById("Uf").value = dados.uf;
                    document.getElementById("Bairro").value = dados.bairro;
                    document.getElementById("Numero").focus();

                }
            }
        });
    }
}

const alertaTemperatura = document.getElementById('container-dados-reais-temperatura');
const alertaLuminosidade = document.getElementById('container-dados-reais-luminosidade');
const alertaUmidade = document.getElementById('container-dados-reais-umidade');

function mudaCorTemperatura() {
    alertaTemperatura.classList.add('alert-dados');
    alertaTemperatura.classList.remove('container-dados-reais');
    ativaAlerta("temperatura");
}

function mudaCorLuminosidade() {
    alertaLuminosidade.classList.add('alert-dados');
    alertaLuminosidade.classList.remove('container-dados-reais');
    ativaAlerta("luminosidade");
}

function mudaCorUmidade() {
    alertaUmidade.classList.add('alert-dados');
    alertaUmidade.classList.remove('container-dados-reais');
    ativaAlerta("umidade");
}

function fecharAlerta() {
    const alerta = document.getElementById("alerta");
    alerta.classList.add("d-none");

    $.ajax({
        type: "POST",
        url: "/Dashboard/ApagaLampada",
        success: function () {
            console.log("Lâmpada apagada com sucesso.");
        },
        error: function () {
            console.error("Erro ao tentar apagar a lâmpada.");
        }
    });
}

// Ajuste na função `ativaAlerta` para exibir a mensagem de forma mais clara
function ativaAlerta(unidade) {
    $.ajax({
        type: "POST",
        url: "/Dashboard/AtivaAlarme",
        data: { unidade: unidade },
        success: function (response) {
            const alerta = $("#alerta");
            $("#alerta-mensagem").text(response.mensagem);
            alerta.removeClass("d-none alert-danger alert-success")
                .addClass(response.sucesso ? "alert-warning" : "alert-danger");
            alerta.show();
        },
        error: function () {
            const alerta = $("#alerta");
            $("#alerta-mensagem").text("Erro ao processar a operação.");
            alerta.removeClass("d-none alert-success").addClass("alert-danger");
            alerta.show();
        }
    });
}

function aplicaFiltroConsultaAvancadaEquipamento() {
    var vNome = document.getElementById('Nome').value;
    var vEmpresaId = document.getElementById('EmpresaId').value;
    var vNomeFantasia = document.getElementById('NomeFantasia').value;
    var vLastUpdate = document.getElementById('LastUpdate').value;

    $.ajax({
        url: "/Equipamento/ObtemDadosConsultaAvancada",
        type: "POST",
        data: {Nome: vNome, EmpresaId: vEmpresaId, LastUpdate: vLastUpdate, NomeFantasia: vNomeFantasia},
        success: function (dados) {
            if (dados.erro !== undefined && dados.erro) {
                alert(dados.msg);
            } else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro na requisição AJAX:", error);
            alert("Erro ao obter dados da consulta avançada.");
        }
    });
}


function aplicaFiltroConsultaAvancada() {
    var vId = document.getElementById('id').value;
    var vNome = document.getElementById('NomeFantasia').value;
    var vEstados = document.getElementById('Estados').value;
    var vDataRegistro = document.getElementById('dataregistro').value;
    var vConnectionStatus = document.getElementById('connectionstatus').value;

    $.ajax({
        url: "/Empresa/ObtemDadosConsultaAvancada",
        type: "POST",
        data: { id: vId, nome: vNome, estados: vEstados, dataregistro: vDataRegistro, connectionstatus: vConnectionStatus },
        success: function (dados) {
            if (dados.erro !== undefined && dados.erro) {
                alert(dados.msg);
            } else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro na requisição AJAX:", error);
            alert("Erro ao obter dados da consulta avançada.");
        }
    });
}
