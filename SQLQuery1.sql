CREATE DATABASE PBL_EC5;

USE PBL_EC5;

CREATE TABLE tbEstado (
	Id SMALLINT PRIMARY KEY NOT NULL,
	Estado NVARCHAR(100) NOT NULL
);
GO 
CREATE TABLE tbCidade (
	Id INT PRIMARY KEY NOT NULL,
	Cidade NVARCHAR(100) NOT NULL,
	EstadoId SMALLINT NOT NULL,

	CONSTRAINT FK_Estado_FOR_CIDADE FOREIGN KEY (EstadoId) REFERENCES tbEstado(Id)
);
GO
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
    EstadoId SMALLINT,
    CidadeId INT,
    DataRegistro DATETIME,
    DataAlteracao DATETIME,

    CONSTRAINT FK_Estado FOREIGN KEY (EstadoId) REFERENCES tbEstado(Id),
    CONSTRAINT FK_Cidade FOREIGN KEY (CidadeId) REFERENCES tbCidade(Id)
);
GO

CREATE TABLE tbEmpresa (
	Id INT PRIMARY KEY NOT NULL, 
    RazaoSocial NVARCHAR(100) NOT NULL,
    NomeFantasia NVARCHAR(100) NOT NULL,
    CNPJ NVARCHAR(15) NOT NULL,
    InscricaoEstadual NVARCHAR(20),
    WebSite NVARCHAR(50),
    Telefone1 NVARCHAR(15),
    Telefone2 NVARCHAR(15),
    Endereco NVARCHAR(100),
    Cep NVARCHAR(15),
    EstadoId SMALLINT,
    CidadeId INT,
	Tipo NVARCHAR(50),
    DataRegistro DATETIME,
    DataAlteracao DATETIME,

    CONSTRAINT FK_Estado_empresa FOREIGN KEY (EstadoId) REFERENCES tbEstado(Id),
    CONSTRAINT FK_Cidade_empresa FOREIGN KEY (CidadeId) REFERENCES tbCidade(Id)
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

INSERT INTO estado VALUES(1, 'Acre')
INSERT INTO estado VALUES(2, 'Alagoas')
INSERT INTO estado VALUES(3, 'Amazonas')
INSERT INTO estado VALUES(4, 'Amap�')
INSERT INTO estado VALUES(5, 'Bahia')
INSERT INTO estado VALUES(6, 'Cear�')
INSERT INTO estado VALUES(7, 'Distrito Federal')
INSERT INTO estado VALUES(8, 'Esp�rito Santo')
INSERT INTO estado VALUES(9, 'Goi�s')
INSERT INTO estado VALUES(10, 'Maranh�o')
INSERT INTO estado VALUES(11, 'Minas Gerais')
INSERT INTO estado VALUES(12, 'Mato Grosso do Sul')
INSERT INTO estado VALUES(13, 'Mato Grosso')
INSERT INTO estado VALUES(14, 'Par�')
INSERT INTO estado VALUES(15, 'Para�ba')
INSERT INTO estado VALUES(16, 'Pernambuco')
INSERT INTO estado VALUES(17, 'Piau�')
INSERT INTO estado VALUES(18, 'Paran�')
INSERT INTO estado VALUES(19, 'Rio de Janeiro')
INSERT INTO estado VALUES(20, 'Rio Grande do Norte')
INSERT INTO estado VALUES(21, 'Rond�nia')
INSERT INTO estado VALUES(22, 'Roraima')
INSERT INTO estado VALUES(23, 'Rio Grande do Sul')
INSERT INTO estado VALUES(24, 'Santa Catarina')
INSERT INTO estado VALUES(25, 'Sergipe')
INSERT INTO estado VALUES(26, 'S�o Paulo')
INSERT INTO estado VALUES(27, 'Tocantins')

INSERT INTO tbCidade (id, nome, estadoId) VALUES (1, 'Rio Branco', 1);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (2, 'Cruzeiro do Sul', 1);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (3, 'Senador Guiomard', 1);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (4, 'Tarauac�', 1);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (5, 'Feij�', 1);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (6, 'Macei�', 2);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (7, 'Arapiraca', 2);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (8, 'Palmeira dos �ndios', 2);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (9, 'Rio Largo', 2);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (10, 'Penedo', 2);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (11, 'Manaus', 3);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (12, 'Parintins', 3);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (13, 'Itacoatiara', 3);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (14, 'Manacapuru', 3);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (15, 'Tabatinga', 3);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (16, 'Macap�', 4);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (17, 'Santana', 4);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (18, 'Laranjal do Jari', 4);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (19, 'Tartarugalzinho', 4);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (20, 'Mazag�o', 4);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (21, 'Salvador', 5);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (22, 'Feira de Santana', 5);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (23, 'Vit�ria da Conquista', 5);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (24, 'Cama�ari', 5);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (25, 'Ilh�us', 5);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (26, 'Fortaleza', 6);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (27, 'Caucaia', 6);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (28, 'Juazeiro do Norte', 6);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (29, 'Maracana�', 6);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (30, 'Sobral', 6);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (31, 'Bras�lia', 7);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (32, 'Gama', 7);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (33, 'Taguatinga', 7);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (34, 'Ceil�ndia', 7);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (35, 'Planaltina', 7);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (36, 'Vit�ria', 8);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (37, 'Vila Velha', 8);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (38, 'Serra', 8);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (39, 'Cariacica', 8);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (40, 'Linhares', 8);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (41, 'Goi�nia', 9);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (42, 'Aparecida de Goi�nia', 9);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (43, 'An�polis', 9);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (44, 'Rio Verde', 9);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (45, 'Luzi�nia', 9);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (46, 'S�o Lu�s', 10);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (47, 'Imperatriz', 10);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (48, 'S�o Jos� de Ribamar', 10);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (49, 'Timon', 10);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (50, 'Caxias', 10);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (51, 'Belo Horizonte', 11);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (52, 'Uberl�ndia', 11);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (53, 'Contagem', 11);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (54, 'Juiz de Fora', 11);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (55, 'Montes Claros', 11);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (56, 'Campo Grande', 12);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (57, 'Dourados', 12);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (58, 'Tr�s Lagoas', 12);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (59, 'Corumb�', 12);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (60, 'Ponta Por�', 12);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (61, 'Cuiab�', 13);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (62, 'V�rzea Grande', 13);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (63, 'Rondon�polis', 13);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (64, 'Sinop', 13);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (65, 'Tangar� da Serra', 13);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (66, 'Bel�m', 14);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (67, 'Ananindeua', 14);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (68, 'Santar�m', 14);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (69, 'Marab�', 14);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (70, 'Altamira', 14);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (71, 'Jo�o Pessoa', 15);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (72, 'Campina Grande', 15);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (73, 'Santa Rita', 15);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (74, 'Patos', 15);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (75, 'Bayeux', 15);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (76, 'Recife', 16);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (77, 'Olinda', 16);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (78, 'Jaboat�o dos Guararapes', 16);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (79, 'Caruaru', 16);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (80, 'Petrolina', 16);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (81, 'Teresina', 17);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (82, 'Parna�ba', 17);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (83, 'Picos', 17);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (84, 'Campo Maior', 17);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (85, 'Floriano', 17);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (86, 'Curitiba', 18);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (87, 'Londrina', 18);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (88, 'Maring�', 18);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (89, 'Ponta Grossa', 18);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (90, 'Foz do Igua�u', 18);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (91, 'Rio de Janeiro', 19);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (92, 'Niter�i', 19);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (93, 'Duque de Caxias', 19);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (94, 'Nova Igua�u', 19);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (95, 'Volta Redonda', 19);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (96, 'Natal', 20);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (97, 'Mossor�', 20);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (98, 'Parnamirim', 20);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (99, 'S�o Gon�alo do Amarante', 20);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (100, 'Caic�', 20);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (101, 'Porto Velho', 21);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (102, 'Ji-Paran�', 21);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (103, 'Ariquemes', 21);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (104, 'Cacoal', 21);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (105, 'Rolim de Moura', 21);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (106, 'Boa Vista', 22);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (107, 'Rorain�polis', 22);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (108, 'Caracara�', 22);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (109, 'Cant�', 22);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (110, 'Iracema', 22);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (111, 'Porto Alegre', 23);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (112, 'Canoas', 23);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (113, 'Gravata�', 23);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (114, 'Pelotas', 23);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (115, 'Santa Maria', 23);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (116, 'Florian�polis', 24);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (117, 'Joinville', 24);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (118, 'Blumenau', 24);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (119, 'Chapec�', 24);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (120, 'Crici�ma', 24);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (121, 'Aracaju', 25);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (122, 'Lagarto', 25);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (123, 'Itabaiana', 25);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (124, 'Nossa Senhora do Socorro', 25);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (125, 'Est�ncia', 25);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (126, 'S�o Paulo', 26);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (127, 'Campinas', 26);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (128, 'S�o Bernardo do Campo', 26);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (129, 'Santos', 26);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (130, 'S�o Jos� dos Campos', 26);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (131, 'Palmas', 27);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (132, 'Aragua�na', 27);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (133, 'Gurupi', 27);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (134, 'Tocantin�polis', 27);
INSERT INTO tbCidade (id, nome, estadoId) VALUES (135, 'Para�so do Tocantins', 27);

drop Table Usuario;
drop Table tbCidade;
drop Table tbEstado;