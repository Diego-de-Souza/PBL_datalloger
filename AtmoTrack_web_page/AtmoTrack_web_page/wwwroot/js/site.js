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

function aplicaFiltroConsultaAvancadaEquipamento() {
    var vNome = document.getElementById('Nome').value;
    var vEmpresaId = document.getElementById('EmpresaId').value;
    var vNomeFantasia = document.getElementById('NomeFantasia').value;
    var vLastUpdate = document.getElementById('LastUpdate').value;

    $.ajax({
        url: "/Equipamento/ObtemDadosConsultaAvancada",
        type: "POST",
        data: { Nome: vNome, EmpresaId: vEmpresaId, LastUpdate: vLastUpdate, NomeFantasia: vNomeFantasia },
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

function aplicaFiltroConsultaAvancadaEmpresa() {
    var vId = document.getElementById('id').value;
    var vNome = document.getElementById('NomeFantasia').value;
    var vEstados = document.getElementById('Estado').value;
    var vDataRegistro = document.getElementById('dataregistro').value;
    var vConnectionStatus = document.getElementById('connectionstatus').value;

    $.ajax({
        url: "/Empresa/ObtemDadosConsultaAvancada",
        type: "POST",
        data: { id: vId, nome: vNome, estado: vEstados, dataregistro: vDataRegistro, connectionstatus: vConnectionStatus },
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


