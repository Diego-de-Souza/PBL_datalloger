using AtmoTrack_web_page.Models;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;

namespace AtmoTrack_web_page.DAO
{
    public class UsuarioDAO: PadraoDAO<UsuarioViewModel>
    {
        protected override void SetTabela()
        {
            Tabela = "Usuario";
            CamposInsert = "Id, Nome, Email, Senha, Endereco, Cep, Telefone, TelefoneComercial, Empresa, Cargo, Estado, Cidade, Bairro, Uf, Numero, DataRegistro, DataAlteracao";
            ValoresInsert = "@Id, @Nome, @Email, @Senha, @Endereco, @Cep, @Telefone, @TelefoneComercial, @Empresa, @Cargo, @Estado, @Cidade, @Bairro, @Uf, @Numero, @DataRegistro, @DataAlteracao";
            SetCampos = "Nome = @Nome, Email = @Email, Senha = @Senha, Endereco = @Endereco, Cep = @Cep, Telefone = @Telefone, TelefoneComercial = @TelefoneComercial, Empresa = @Empresa, Cargo = @Cargo, Estado = @Estado, Cidade = @Cidade, Birro = @Bairro, Uf = @Uf, Numero = @Numero, DataRegistro = @DataRegistro, DataAlteracao = @DataAlteracao";
            Condicoes = "WHERE Id = @Id";

        }

        protected override SqlParameter[] CriaParametros(UsuarioViewModel us)
        {
            SqlParameter[] parametros = new SqlParameter[17];
            
            parametros[0] = new SqlParameter("@Id", SqlDbType.Int) { Value = us.Id };
            parametros[1] = new SqlParameter("@Nome", SqlDbType.NVarChar, 100) { Value = (object)us.Nome ?? DBNull.Value };
            parametros[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = (object)us.Email };
            parametros[3] = new SqlParameter("@Senha", SqlDbType.NVarChar, 50) { Value = (object)us.Senha };
            parametros[4] = new SqlParameter("@Endereco", SqlDbType.NVarChar, 255) { Value = (object)us.Endereco };
            parametros[5] = new SqlParameter("@Cep", SqlDbType.NVarChar, 10) { Value = (object)us.Cep };
            parametros[6] = new SqlParameter("@Telefone", SqlDbType.NVarChar, 15) { Value = (object)us.Telefone };
            parametros[7] = new SqlParameter("@TelefoneComercial", SqlDbType.NVarChar, 15) { Value = (object)us.TelefoneComercial ?? DBNull.Value };
            parametros[8] = new SqlParameter("@Empresa", SqlDbType.NVarChar, 100) { Value = (object)us.Empresa };
            parametros[9] = new SqlParameter("@Cargo", SqlDbType.NVarChar, 50) { Value = (object)us.Cargo };
            parametros[10] = new SqlParameter("@Estado", SqlDbType.NVarChar, 50) { Value = (object)us.Estado };
            parametros[11] = new SqlParameter("@Cidade", SqlDbType.NVarChar, 50) { Value = (object)us.Cidade};
            parametros[12] = new SqlParameter("@Bairro", SqlDbType.NVarChar, 50) { Value = (object)us.Bairro };
            parametros[13] = new SqlParameter("@Uf", SqlDbType.NVarChar, 2) { Value = (object)us.Uf };
            parametros[14] = new SqlParameter("@Numero", SqlDbType.NVarChar, 50) { Value = (object)us.Numero };
            parametros[15] = new SqlParameter("@DataRegistro", SqlDbType.DateTime) { Value = (object)us.DataRegistro ?? DBNull.Value };
            parametros[16] = new SqlParameter("@DataAlteracao", SqlDbType.DateTime) { Value = (object)us.DataAlteracao ?? DBNull.Value };
 
            return parametros;
        }

        protected override UsuarioViewModel MontaModel(DataRow registro)
        {
            var us = new UsuarioViewModel();


            us.Id = Convert.ToInt32(registro["Id"]);
            us.Nome = registro["Nome"].ToString();
            us.Email = registro["Email"].ToString();
            us.Endereco = registro["Endereco"].ToString();
            us.Cep = registro["Cep"].ToString();
            us.Telefone = registro["Telefone"].ToString();
            if (registro["Telefone"] != DBNull.Value)
                us.TelefoneComercial = registro["TelefoneComercial"].ToString();

            us.Empresa = registro["Empresa"].ToString();
            us.Cargo = registro["Cargo"].ToString();
            us.Estado = registro["Estado"].ToString();
            us.Cidade = registro["Cidade"].ToString();
            us.Bairro = registro["Bairro"].ToString();
            us.Uf = registro["Uf"].ToString();
            us.Numero = registro["Numero"].ToString();
            if (registro["DataRegistro"] != DBNull.Value)
                us.DataRegistro = Convert.ToDateTime(registro["DataRegistro"]);

            if (registro["DataAlteracao"] != DBNull.Value)
                us.DataAlteracao = Convert.ToDateTime(registro["DataAlteracao"]);
            

            return us;
        }

        private UsuarioViewModel MontaViewModelParaExibir(DataRow row)
        {
            return new UsuarioViewModel
            {
                Id = Convert.ToInt32(row["Id"]),
                Nome = row["Nome"].ToString(),
                Email = row["Email"].ToString(),
                Endereco = row["Endereco"].ToString(),
                Cep = row["Cep"].ToString(),
                Telefone = row["Telefone"].ToString(),
                TelefoneComercial = row["TelefoneComercial"].ToString(),
                Empresa = row["Empresa"].ToString(),
                Cargo = row["Cargo"].ToString(),
                Estado = row["Estado"].ToString(),
                Cidade = row["Cidade"].ToString(),
                Bairro = row["Bairro"].ToString(),
                Uf = row["Uf"].ToString(),
                Numero = row["Numero"].ToString(),
                DataRegistro = Convert.ToDateTime(row["DataRegistro"]),
                DataAlteracao = Convert.ToDateTime(row["DataAlteracao"])
            };
        }
    }
}
