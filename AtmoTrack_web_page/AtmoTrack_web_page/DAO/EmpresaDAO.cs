using AtmoTrack_web_page.Models;
using System.Data.SqlClient;
using System.Data;

namespace AtmoTrack_web_page.DAO
{
    public class EmpresaDAO
    {
        public void Inserir(EmpresaViewModel empresa)
        {
            empresa.DataRegistro = DateTime.Now;
            empresa.DataAlteracao = DateTime.Now;

            string sql = @"INSERT INTO [dbo].[tbEmpresa](
                            [Id],
                            [RazaoSocial],
                            [NomeFantasia],
                            [CNPJ],
                            [InscricaoEstadual],
                            [WebSite],
                            [Telefone1],
                            [Telefone2],
                            [Endereco],
                            [Cep],
                            [EstadoId],
                            [CidadeId],
                            [Tipo],
                            [DataRegistro],
                            [DataAlteracao]
                        ) Values (
                            @Id,
                            @RazaoSocial,
                            @NomeFantasia,
                            @CNPJ,
                            @InscricaoEstadual,
                            @WebSite,
                            @Telefone1,
                            @Telefone2,
                            @Endereco,
                            @Cep,
                            @EstadoId,
                            @CidadeId,
                            @Tipo,
                            @DataRegistro,
                            @DataAlteracao
                        )";

            HelperDAO.ExecutaSQL(sql, CriaParamentros(empresa));
        }

        public void Alterar(EmpresaViewModel empresa)
        {
            empresa.DataAlteracao = DateTime.Now;

            string sql = @" UPDATE [dbo].[tbEmpresa] set
                            [RazaoSocial] = @RazaoSocial,
                            [NomeFantasia] = @NomeFantasia,
                            [CNPJ] = @CNPJ,
                            [InscricaoEstadual] = @InscricaoEstadual,
                            [WebSite] = @WebSite,
                            [Telefone1] = @Telefone1,
                            [Telefone2] = @Telefone2,
                            [Endereco] = @Endereco,
                            [Cep] = @Cep,
                            [EstadoId] = @EstadoId,
                            [CidadeId] = @CidadeId,
                            [Tipo] = @Tipo,
                            [DataRegistro] = @DataRegistro,
                            [DataAlteracao] = @DataAlteracao
                        WHERE Id = @Id";

            HelperDAO.ExecutaSQL(sql, CriaParamentros(empresa));
        }

        private SqlParameter[] CriaParamentros(EmpresaViewModel em)
        {
            SqlParameter[] parametros = new SqlParameter[15];

            parametros[0] = new SqlParameter("@Id", SqlDbType.Int) { Value = em.Id };
            parametros[1] = new SqlParameter("@RazaoSocial", SqlDbType.NVarChar, 100) { Value = (object)em.RazaoSocial };
            parametros[2] = new SqlParameter("@NomeFantasia", SqlDbType.NVarChar, 100) { Value = (object)em.NomeFantasia };
            parametros[3] = new SqlParameter("@CNPJ", SqlDbType.NVarChar, 15) { Value = (object)em.CNPJ };
            parametros[4] = new SqlParameter("@InscricaoEstadual", SqlDbType.NVarChar, 20) { Value = (object)em.InscricaoEstadual };
            parametros[5] = new SqlParameter("@WebSite", SqlDbType.NVarChar, 50) { Value = (object)em.WebSite };
            parametros[6] = new SqlParameter("@Telefone1", SqlDbType.NVarChar, 15) { Value = (object)em.Telefone1 };
            parametros[7] = new SqlParameter("@Telefone2", SqlDbType.NVarChar, 15) { Value = (object)em.Telefone2 ?? DBNull.Value };
            parametros[8] = new SqlParameter("@Endereco", SqlDbType.NVarChar, 100) { Value = (object)em.Endereco };
            parametros[9] = new SqlParameter("@Cep", SqlDbType.NVarChar, 15) { Value = (object)em.Cep };
            parametros[10] = new SqlParameter("@EstadoId", SqlDbType.SmallInt) { Value = (object)em.EstadoId };
            parametros[11] = new SqlParameter("@CidadeId", SqlDbType.Int) { Value = (object)em.CidadeId ?? DBNull.Value };
            parametros[12] = new SqlParameter("@Tipo", SqlDbType.NVarChar, 50) { Value = (object)em.Tipo };
            parametros[13] = new SqlParameter("@DataRegistro", SqlDbType.DateTime) { Value = (object)em.DataRegistro ?? DBNull.Value };
            parametros[14] = new SqlParameter("@DataAlteracao", SqlDbType.DateTime) { Value = (object)em.DataAlteracao ?? DBNull.Value };

            return parametros;
        }

        private EmpresaViewModel MontaViewModelEmpresa(DataRow registro)
        {
            var em = new EmpresaViewModel();


            em.Id = Convert.ToInt32(registro["Id"]);
            em.RazaoSocial = registro["RazaoSocial"].ToString();
            em.NomeFantasia= registro["NomeFantasia"].ToString();
            em.CNPJ = registro["CNPJ"].ToString();
            em.InscricaoEstadual = registro["InscricaoEstadual"].ToString();
            em.WebSite = registro["WebSite"].ToString();
            em.Telefone1 = registro["Telefone1"].ToString();
            if (registro["Telefone2"] != DBNull.Value)
                em.Telefone2 = registro["Telefone2"].ToString();

            em.Endereco = registro["Endereco"].ToString();
            em.Cep = registro["Cep"].ToString();
            if (registro["EstadoId"] != DBNull.Value)
                em.EstadoId = Convert.ToInt16(registro["EstadoId"]);

            if (registro["CidadeId"] != DBNull.Value)
                em.CidadeId = Convert.ToInt32(registro["CidadeId"]);

            em.Tipo = registro["Tipo"].ToString();

            if (registro["DataRegistro"] != DBNull.Value)
                em.DataRegistro = Convert.ToDateTime(registro["DataRegistro"]);

            if (registro["DataAlteracao"] != DBNull.Value)
                em.DataAlteracao = Convert.ToDateTime(registro["DataAlteracao"]);


            return em;
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
                Cidade = registro["Cidaded"].ToString(),
            };

            return cidade;
        }

        public List<EmpresaViewModel> Listagem()
        {
            var Lista = new List<EmpresaViewModel>();
            string sql = "Select * from [dbo].[tbEmpresa] order by RazaoSocial";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);

            Console.WriteLine($"Número de linhas retornadas: {tabela.Rows.Count}");

            foreach (DataRow row in tabela.Rows)
            {
                Lista.Add(MontaViewModelEmpresa(row));
            }

            return Lista; ;
        }

        public int LastId()
        {
            string sql = "select isnull(max(id) +1, 1) as 'MAIOR' from [dbo].[tbEmpresa]";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
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
                Console.WriteLine("Erro: " + ex.Message);
                throw;
            }

            return ListaEstados;
        }

        public EmpresaViewModel Consulta(int id)
        {
            string sql = "Select * from [dbo].[tbEmpresa] where id = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return MontaViewModelEmpresa(tabela.Rows[0]);
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
            string sql = "Select * from [dbo].[tbCidade] where estadoId = " + id;
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
        private EmpresaViewModel MontaViewModelParaExibir(DataRow row)
        {
            return new EmpresaViewModel
            {
                Id = Convert.ToInt32(row["Id"]),
                RazaoSocial = row["RazaoSocial"].ToString(),
                NomeFantasia = row["NomeFantasia"].ToString(),
                CNPJ = row["Endereco"].ToString(),
                InscricaoEstadual = row["InscricaoEstadual"].ToString(),
                WebSite = row["WebSite"].ToString(),
                Telefone1 = row["Telefone1"].ToString(),
                Telefone2 = row["Telefone2"].ToString(),
                Endereco = row["Endereco"].ToString(),
                Cep = row["Cep"].ToString(),
                EstadoId = Convert.ToInt16(row["EstadoId"]),
                CidadeId = Convert.ToInt32(row["CidadeId"]),
                Tipo = row["Tipo"].ToString(),
                DataRegistro = Convert.ToDateTime(row["DataRegistro"]),
                DataAlteracao = Convert.ToDateTime(row["DataAlteracao"])
            };
        }
        public void Excluir(int id)
        {
            string sql = "delete [dbo].[tbEmpresa] where id =" + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
    }
}
