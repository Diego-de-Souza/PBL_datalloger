using AtmoTrack_web_page.Models;
using System.Data;
using System.Data.SqlClient;

namespace AtmoTrack_web_page.DAO
{
    public class LogServiceDAO
    {
        private string Tabela = "Logs";
        private string CamposInsert = "NomeServiceEvento, Temperatura, DataHora";

        public void InsertDinamicoLog(LogEventoViewModel model)
        {

            model.DataHora = DateTime.Now;

            SqlParameter[] parametros = CriaParametros(model);

            string query = $"INSERT INTO {Tabela} ({CamposInsert}) VALUES ({string.Join(", ", parametros.Select(p => $"{p.ParameterName}"))})";

            HelperDAO.ExecutaSQL(query, parametros);
        }

        private SqlParameter[] CriaParametros(LogEventoViewModel eq)
        {
            SqlParameter[] parametros = new SqlParameter[4];

            parametros[0] = new SqlParameter("@Id", SqlDbType.Int) { Value = eq.Id };
            parametros[1] = new SqlParameter("@NomeServiceEvento", SqlDbType.NVarChar, 100) { Value = (object)eq.NomeServiceEvento };
            parametros[2] = new SqlParameter("@Temperatura", SqlDbType.Int) { Value = eq.Temperatura };
            parametros[3] = new SqlParameter("@DataHora", SqlDbType.DateTime) { Value = eq.DataHora };

            return parametros;
        }
    }
}
