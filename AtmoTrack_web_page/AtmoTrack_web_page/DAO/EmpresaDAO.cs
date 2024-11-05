using AtmoTrack_web_page.Models;
using System.Data.SqlClient;
using System.Data;

namespace AtmoTrack_web_page.DAO
{
    public class EmpresaDAO: PadraoDAO<EmpresaViewModel>
    {
        protected override void SetTabela()
        {
            Tabela = "tbEmpresa";
            CamposInsert = "Id, RazaoSocial, NomeFantasia, CNPJ, InscricaoEstadual, WebSite, Telefone1, Telefone2, Endereco, Cep, Estado, Cidade, Bairro, Uf, Numero, Tipo, DataRegistro, DataAlteracao";
            ValoresInsert = "@Id, @RazaoSocial, @NomeFantasia, @CNPJ, @InscricaoEstadual, @WebSite, @Telefone1, @Telefone2, @Endereco, @Cep, @Estado, @Cidade, @Bairro, @Uf, @Numero, @Tipo, @DataRegistro, @DataAlteracao";
            SetCampos = "RazaoSocial = @RazaoSocial, NomeFantasia = @NomeFantasia, CNPJ = @CNPJ, InscricaoEstadual = @InscricaoEstadual, WebSite = @WebSite, Telefone1 = @Telefone1, Telefone2 = @Telefone2, Endereco = @Endereco, Cep = @Cep, Estado = @Estado, Cidade = @Cidade, Bairro = @Bairro, Uf = @Uf, Numero = @Numero, Tipo = @Tipo, DataRegistro = @DataRegistro, DataAlteracao = @DataAlteracao";
            Condicoes = "WHERE Id = @Id";
        }

        protected override SqlParameter[] CriaParametros(EmpresaViewModel em)
        {
            SqlParameter[] parametros = new SqlParameter[18];

            parametros[0] = new SqlParameter("@Id", SqlDbType.Int) { Value = em.Id };
            parametros[1] = new SqlParameter("@RazaoSocial", SqlDbType.NVarChar, 100) { Value = (object)em.RazaoSocial };
            parametros[2] = new SqlParameter("@NomeFantasia", SqlDbType.NVarChar, 100) { Value = (object)em.NomeFantasia };
            parametros[3] = new SqlParameter("@CNPJ", SqlDbType.NVarChar, 20) { Value = (object)em.CNPJ };
            parametros[4] = new SqlParameter("@InscricaoEstadual", SqlDbType.NVarChar, 20) { Value = (object)em.InscricaoEstadual };
            parametros[5] = new SqlParameter("@WebSite", SqlDbType.NVarChar, 50) { Value = (object)em.WebSite };
            parametros[6] = new SqlParameter("@Telefone1", SqlDbType.NVarChar, 15) { Value = (object)em.Telefone1 };
            parametros[7] = new SqlParameter("@Telefone2", SqlDbType.NVarChar, 15) { Value = (object)em.Telefone2 ?? DBNull.Value };
            parametros[8] = new SqlParameter("@Endereco", SqlDbType.NVarChar, 100) { Value = (object)em.Endereco };
            parametros[9] = new SqlParameter("@Cep", SqlDbType.NVarChar, 15) { Value = (object)em.Cep };
            parametros[10] = new SqlParameter("@Estado", SqlDbType.NVarChar, 50) { Value = (object)em.Estado };
            parametros[11] = new SqlParameter("@Cidade", SqlDbType.NVarChar, 50) { Value = (object)em.Cidade };
            parametros[12] = new SqlParameter("@Bairro", SqlDbType.NVarChar, 50) { Value = (object)em.Bairro };
            parametros[13] = new SqlParameter("@Uf", SqlDbType.NVarChar, 2) { Value = (object)em.Uf };
            parametros[14] = new SqlParameter("Numero", SqlDbType.NVarChar, 50) { Value = em.Numero};
            parametros[15] = new SqlParameter("@Tipo", SqlDbType.NVarChar, 50) { Value = (object)em.Tipo };
            parametros[16] = new SqlParameter("@DataRegistro", SqlDbType.DateTime) { Value = (object)em.DataRegistro ?? DBNull.Value };
            parametros[17] = new SqlParameter("@DataAlteracao", SqlDbType.DateTime) { Value = (object)em.DataAlteracao ?? DBNull.Value };

            return parametros;
        }

        protected override EmpresaViewModel MontaModel(DataRow registro)
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
            em.Estado = registro["Estado"].ToString();
            em.Cidade = registro["Cidade"].ToString();
            em.Bairro = registro["Bairro"].ToString();
            em.Uf = registro["Uf"].ToString();
            em.Numero = registro["Numero"].ToString();
            em.Tipo = registro["Tipo"].ToString();

            if (registro["DataRegistro"] != DBNull.Value)
                em.DataRegistro = Convert.ToDateTime(registro["DataRegistro"]);

            if (registro["DataAlteracao"] != DBNull.Value)
                em.DataAlteracao = Convert.ToDateTime(registro["DataAlteracao"]);


            return em;
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
                Estado = row["Estado"].ToString(),
                Cidade = row["Cidade"].ToString(),
                Bairro = row["Bairro"].ToString(),
                Uf = row["Uf"].ToString(),
                Numero = row["Numero"].ToString(),
                Tipo = row["Tipo"].ToString(),
                DataRegistro = Convert.ToDateTime(row["DataRegistro"]),
                DataAlteracao = Convert.ToDateTime(row["DataAlteracao"])
            };
        }
    }
}
