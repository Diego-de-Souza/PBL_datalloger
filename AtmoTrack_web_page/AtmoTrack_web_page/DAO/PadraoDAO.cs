using AtmoTrack_web_page.Models;
using System.Data;
using System.Data.SqlClient;

namespace AtmoTrack_web_page.DAO
{
    public abstract class PadraoDAO<T> where T: PadraoViewModel
    {
        public PadraoDAO()
        {
            SetTabela();
        }

        protected string Tabela { get; set; }
        protected string NomeSpListagem { get; set; } = "SpListagem";
        protected abstract SqlParameter[] CriaParametros(T model);
        protected abstract T MontaModel(DataRow registro);
        protected abstract void SetTabela();
        protected string CamposInsert { get; set; }
        protected string ValoresInsert { get; set; }
        protected string SetCampos { get; set; }
        protected string Condicoes { get; set; }

        public virtual void InsertDinamico(T model)
        {

            model.DataRegistro = DateTime.Now;
            model.DataAlteracao = DateTime.Now;

            SqlParameter[] parametros = CriaParametros(model);

            string query = $"INSERT INTO {Tabela} ({CamposInsert}) VALUES ({string.Join(", ", parametros.Select(p => $"{p.ParameterName}"))})";

            HelperDAO.ExecutaSQL(query, parametros);
        }

        public virtual void AlterDinamico(T model)
        {
            model.DataAlteracao = DateTime.Now;

            SqlParameter[] parametros = CriaParametros(model);

            string setClause = SetCampos;
            string whereClause = Condicoes ?? "WHERE Id = @Id";

            string query = $"UPDATE {Tabela} SET {setClause} {whereClause}";

            HelperDAO.ExecutaSQL(query, parametros);
        }

        public virtual void Delete(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            HelperDAO.ExecutaProc("spDelete", p);
        }

        public virtual T Consulta(int id)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela",Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsulta", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public virtual int ProximoId()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
            return Convert.ToInt32(tabela.Rows[0][0]);
        }

        public virtual List<T> Listagem()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela",Tabela),
                new SqlParameter("Ordem", "1") // primeiro campo d atabela
            };
            var tabela = HelperDAO.ExecutaProcSelect("SpListagem", p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

    }
}
