
/* Procedures de CRUD do site da AtmoTrack com o banco de dados*/
	/*procedure para deletar dados no em uma determinada tabela do banco*/
	create procedure spDelete(@id int, @tabela varchar(max))
	as begin
		declare @sql varchar(max);
		set @sql = 'delete from ' + @tabela + ' where id = ' + cast(@id as varchar(max))
		exec(@sql)
	end;
	GO

/*procedure para consultar dados em uma determinada tabela no banco*/
create procedure spConsulta (@id int, @tabela varchar(max))
as begin
	declare @sql varchar(max);
	set @sql = 'select * from ' + @tabela + ' where id = ' + cast(@id as varchar(max))
	exec(@sql)
end;
GO

/*procedure para consultar dados em uma determinada tabela no banco*/
create procedure [dbo].[spConsultaAvancadaJogos]
(
 @descricao varchar(max),
 @categoria int,
 @dataInicial datetime,
 @dataFinal datetime)
as
begin
declare @categIni int
declare @categFim int

set @categIni = case @categoria when 0 then 0 else @categoria end
set @categFim = case @categoria when 0 then 999999 else @categoria end
 select jogos.*, Categorias.descricao as 'DescricaoCategoria'
from Jogos
inner join Categorias on jogos.categoriaId = categorias.id
where jogos.descricao like '%' + @descricao + '%' and
 jogos.data_aquisicao between @dataInicial and @dataFinal and
 jogos.categoriaID between @categIni and @categFim;
end;
GO

/*procedure para consultar dados em uma determinada tabela no banco e retornar uma listagem*/
create procedure spListagem (@tabela varchar(max), @ordem varchar(max))
as begin
	exec('select * from '+@tabela+' order by '+@ordem)
end
GO

/*Procedure de busca do próximo Id*/
create procedure spProximoId(@tabela varchar(max))
as begin
	exec('select isnull(max(id) +1, 1) as MAIOR from '+@tabela)
end
GO

/*procedure de busca avançada equipamento*/

CREATE OR ALTER PROCEDURE spConsultaAvancada_tbEquipamento
(
	@Id INT = NULL,
    @Nome NVARCHAR(100) = NULL,
    @EmpresaId INT = NULL,
    @NomeFantasia NVARCHAR(100) = NULL,
    @LastUpdate DATETIME = NULL
)
AS
BEGIN
    SELECT 
        tbEquipamento.*,
		tbEmpresa.Id,
        tbEmpresa.NomeFantasia
    FROM
        tbEquipamento
    INNER JOIN 
        tbEmpresa ON tbEquipamento.EmpresaId = tbEmpresa.Id
    WHERE 
		(tbEquipamento.Id = @Id OR @Id IS NULL) AND
        (tbEquipamento.Nome LIKE '%' + ISNULL(@Nome, '') + '%' OR @Nome IS NULL) AND
        (tbEmpresa.NomeFantasia LIKE '%' + ISNULL(@NomeFantasia, '') + '%' OR @NomeFantasia IS NULL) AND
        (tbEmpresa.Id = @EmpresaId OR @EmpresaId IS NULL) AND
        (tbEquipamento.LastUpdate >= @LastUpdate OR @LastUpdate IS NULL)
END;
GO

/*procedure de busca avançada empresa*/
CREATE OR ALTER PROCEDURE spConsultaAvancada_tbEmpresa
(
    @Id INT = Null,
    @NomeFantasia NVARCHAR(100) = Null,
    @Estado NVARCHAR(50) = Null,
    @DataRegistro DATETIME = Null,
    @ConnectionStatus NVARCHAR(100) = Null
)
AS
BEGIN
    SELECT 
        tbEmpresa.*,
        tbEquipamento.ConnectionStatus
    FROM 
        tbEmpresa
    INNER JOIN 
        tbEquipamento ON tbEmpresa.Id = tbEquipamento.EmpresaId
    WHERE 
        (tbEmpresa.Id = @Id OR @Id IS NULL) AND
        (tbEmpresa.NomeFantasia LIKE '%' + ISNULL(@NomeFantasia, '') + '%' OR @NomeFantasia IS NULL) AND
        (tbEmpresa.Estado LIKE '%' + ISNULL(@Estado, '') + '%' OR @Estado IS NULL) AND
        (tbEmpresa.DataRegistro >= @DataRegistro OR @DataRegistro IS NULL) AND
        (tbEquipamento.ConnectionStatus LIKE '%' + ISNULL(@ConnectionStatus, '') + '%' OR @ConnectionStatus IS NULL);
END;
GO

use PBL_EC5;

