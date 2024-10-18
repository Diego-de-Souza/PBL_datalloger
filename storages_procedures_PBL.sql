
/* Procedures de CRUD do site da AtmoTrack com o banco de dados*/
/*procedure para deletar dados no em uma determinada tabela do banco*/
create procedure spDelete(@id int, @tabela varchar(max))
as begin
	declare @sql varchar(max);
	set @sql = 'delete' + @tabela + 'where id = ' + cast(@id as varchar(max))
	exec(@sql)
end;
GO

/*procedure para consultar dados em uma determinada tabela no banco*/
create procedure spConsulta (@id int, @tabela varchar(max))
as begin
	declare @sql varchar(max);
	set @sql = 'select * from ' + @tabela + 'where id = ' + cast(@id as varchar(max))
	exec(@sql)
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

