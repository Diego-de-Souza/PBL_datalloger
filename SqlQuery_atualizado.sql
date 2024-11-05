CREATE DATABASE PBL_EC5;

USE PBL_EC5;

CREATE TABLE Usuario (
    Id INT PRIMARY KEY NOT NULL, 
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Senha NVARCHAR(50) NOT NULL,
    Endereco NVARCHAR(255),
    Cep NVARCHAR(10),
    Telefone NVARCHAR(15),
    TelefoneComercial NVARCHAR(15),
    Empresa NVARCHAR(100),
    Cargo NVARCHAR(50),
    Estado NVARCHAR(50),
    Cidade NVARCHAR(50),
	Bairro NVARCHAR(50),
	UF NVARCHAR(2),
	Numero NVARCHAR(50),
    DataRegistro DATETIME,
    DataAlteracao DATETIME,
);
GO

CREATE TABLE tbEmpresa (
	Id INT PRIMARY KEY NOT NULL, 
    RazaoSocial NVARCHAR(100) NOT NULL,
    NomeFantasia NVARCHAR(100) NOT NULL,
    CNPJ NVARCHAR(20) NOT NULL,
    InscricaoEstadual NVARCHAR(20),
    WebSite NVARCHAR(50),
    Telefone1 NVARCHAR(15),
    Telefone2 NVARCHAR(15),
    Endereco NVARCHAR(100),
    Cep NVARCHAR(15),
    Estado NVARCHAR(50),
    Cidade NVARCHAR(50),
	Bairro NVARCHAR(50),
	UF NVARCHAR(2),
	Numero NVARCHAR(50),
	Tipo NVARCHAR(50),
    DataRegistro DATETIME,
    DataAlteracao DATETIME,
);
GO

CREATE TABLE tbEquipamento (
    Id INT PRIMARY KEY NOT NULL,                    
    Nome NVARCHAR(100) NOT NULL, 
    EmpresaId INT NOT NULL, 
    MacAddress NVARCHAR(17) NOT NULL, 
    IpAddress NVARCHAR(45), 
    SSID NVARCHAR(100), 
    SignalStrength INT, 
    ConnectionStatus NVARCHAR(50), 
    DataRegistro DATETIME NOT NULL,
    SensorData TEXT, 
    StatusEquipamento NVARCHAR(50),
    AuthToken NVARCHAR(100),
    FirmwareVersion NVARCHAR(50), 
    LastUpdate DATETIME NOT NULL, 
    DataAlteracao DATETIME  
);

