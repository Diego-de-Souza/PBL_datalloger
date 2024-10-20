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
            CamposInsert = "Id, Nome, Email, Senha, Endereco, Cep, Telefone, TelefoneComercial, Empresa, Cargo, EstadoId, CidadeId, DataRegistro, DataAlteracao";
            ValoresInsert = "@Id, @Nome, @Email, @Senha, @Endereco, @Cep, @Telefone, @TelefoneComercial, @Empresa, @Cargo, @EstadoId, @CidadeId, @DataRegistro, @DataAlteracao";
            SetCampos = "Nome = @Nome, Email = @Email, Senha = @Senha, Endereco = @Endereco, Cep = @Cep, Telefone = @Telefone, TelefoneComercial = @TelefoneComercial, Empresa = @Empresa, Cargo = @Cargo, EstadoId = @EstadoId, CidadeId = @CidadeId, DataRegistro = @DataRegistro, DataAlteracao = @DataAlteracao";
            Condicoes = "WHERE Id = @Id";

        }

        protected override SqlParameter[] CriaParametros(UsuarioViewModel us)
        {
            SqlParameter[] parametros = new SqlParameter[14];
            
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
            parametros[10] = new SqlParameter("@EstadoId", SqlDbType.SmallInt) { Value = (object)us.EstadoId };
            parametros[11] = new SqlParameter("@CidadeId", SqlDbType.Int) { Value = (object)us.CidadeId ?? DBNull.Value };
            parametros[12] = new SqlParameter("@DataRegistro", SqlDbType.DateTime) { Value = (object)us.DataRegistro ?? DBNull.Value };
            parametros[13] = new SqlParameter("@DataAlteracao", SqlDbType.DateTime) { Value = (object)us.DataAlteracao ?? DBNull.Value };
 
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
            if (registro["EstadoId"] != DBNull.Value)
                us.EstadoId = Convert.ToInt16(registro["EstadoId"]);

            if (registro["CidadeId"] != DBNull.Value)
                us.CidadeId = Convert.ToInt32(registro["CidadeId"]);

            if (registro["DataRegistro"] != DBNull.Value)
                us.DataRegistro = Convert.ToDateTime(registro["DataRegistro"]);

            if (registro["DataAlteracao"] != DBNull.Value)
                us.DataAlteracao = Convert.ToDateTime(registro["DataAlteracao"]);
            

            return us;
        }

        private EstadoViewModel MontaViewModelEstado(DataRow registro)
        {
            var estado = new EstadoViewModel()
            {
                Id = Convert.ToInt16(registro["Id"]),
                Estado = registro["Estado"].ToString(),
            };

            return estado;
        }

        private CidadeViewModel MontaViewModelCidade(DataRow registro)
        {
            var cidade = new CidadeViewModel()
            {
                Id = Convert.ToInt32(registro["Id"]),
                Cidade = registro["Cidade"].ToString(),
                EstadoId = Convert.ToInt16(registro["EstadoId"])
            };

            return cidade;
        }

        public List<EstadoViewModel> GetAllStates()
        {
            var ListaEstados = new List<EstadoViewModel>();
            string sql = "Select * from [dbo].[tbEstado] order by Estado";
            try
            {
                DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
                foreach (DataRow row in tabela.Rows)
                {
                    ListaEstados.Add(MontaViewModelEstado(row));
                }
            }
            catch (Exception ex)
            {
                // Registrar o erro ou exibir em uma view apropriada
                Console.WriteLine("Erro: " + ex.Message);
                throw;
            }

            return ListaEstados;
        }

        public List<CidadeViewModel> GetAllCitiesEstadoId(int id)
        {
            var ListaCidades = new List<CidadeViewModel>();
            string sql = "Select * from [dbo].[tbCidade] where estadoId = " + id + "order by Cidade";
            try
            {
                DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
                foreach (DataRow row in tabela.Rows)
                {
                    ListaCidades.Add(MontaViewModelCidade(row));
                }
                return ListaCidades;
            }
            catch (Exception ex)
            {
                // Registrar o erro ou exibir em uma view apropriada
                Console.WriteLine("Erro: " + ex.Message);
                throw;
            }

        }

        public EstadoViewModel ConsultaEstado(int id)
        {
            string sql = "Select * from [dbo].[tbEstado] where id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return MontaViewModelEstado(tabela.Rows[0]);
            }
        }

        public CidadeViewModel ConsultaCidade(int id)
        {
            string sql = "Select * from [dbo].[tbCidade] where Id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return MontaViewModelCidade(tabela.Rows[0]);
            }
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
                EstadoId = Convert.ToInt16(row["EstadoId"]),
                CidadeId = Convert.ToInt32(row["CidadeId"]),
                DataRegistro = Convert.ToDateTime(row["DataRegistro"]),
                DataAlteracao = Convert.ToDateTime(row["DataAlteracao"])
            };
        }
    }
}
