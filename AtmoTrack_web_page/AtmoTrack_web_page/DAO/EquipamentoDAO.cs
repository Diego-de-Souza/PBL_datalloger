using AtmoTrack_web_page.Models;
using System.Data.SqlClient;
using System.Data;

namespace AtmoTrack_web_page.DAO
{
    public class EquipamentoDAO : PadraoDAO<EquipamentoViewModel>
    {
        protected override void SetTabela()
        {
            Tabela = "tbEquipamento";
            CamposInsert = "Id, Nome, EmpresaId, MacAddress, IpAddress, SSID, SignalStrength, ConnectionStatus, DataRegistro, SensorData, StatusEquipamento, AuthToken, FirmwareVersion, LastUpdate, DataAlteracao";
            ValoresInsert = "@Id, @Nome, @EmpresaId, @MacAddress, @IpAddress, @SSID, @SignalStrength, @ConnectionStatus, @DataRegistro, @SensorData, @StatusEquipamento, @AuthToken, @FirmwareVersion, @LastUpdate, @DataAlteracao";
            SetCampos = "Nome = @Nome, EmpresaId = @EmpresaId, MacAddress = @MacAddress, IpAddress = @IpAddress, SSID = @SSID, SignalStrength = @SignalStrength, ConnectionStatus = @ConnectionStatus, DataRegistro = @DataRegistro, SensorData = @SensorData, StatusEquipamento = @StatusEquipamento, AuthToken = @AuthToken, FirmwareVersion = @FirmwareVersion, LastUpdate = @LastUpdate, DataAlteracao = @DataAlteracao";
            Condicoes = "WHERE Id = @Id";
        }

        protected override SqlParameter[] CriaParametros(EquipamentoViewModel eq)
        {
            SqlParameter[] parametros = new SqlParameter[15];

            parametros[0] = new SqlParameter("@Id", SqlDbType.Int) { Value = eq.Id };
            parametros[1] = new SqlParameter("@Nome", SqlDbType.NVarChar, 100) { Value = (object)eq.Nome };
            parametros[2] = new SqlParameter("@EmpresaId", SqlDbType.Int) { Value = eq.EmpresaId };
            parametros[3] = new SqlParameter("@MacAddress", SqlDbType.NVarChar, 17) { Value = (object)eq.MacAddress };
            parametros[4] = new SqlParameter("@IpAddress", SqlDbType.NVarChar, 15) { Value = (object)eq.IpAddress };
            parametros[5] = new SqlParameter("@SSID", SqlDbType.NVarChar, 32) { Value = (object)eq.SSID };
            parametros[6] = new SqlParameter("@SignalStrength", SqlDbType.Int) { Value = eq.SignalStrength };
            parametros[7] = new SqlParameter("@ConnectionStatus", SqlDbType.NVarChar, 50) { Value = (object)eq.ConnectionStatus };
            parametros[8] = new SqlParameter("@DataRegistro", SqlDbType.DateTime) { Value = eq.DataRegistro };
            parametros[9] = new SqlParameter("@SensorData", SqlDbType.NVarChar, 255) { Value = (object)eq.SensorData ?? DBNull.Value };
            parametros[10] = new SqlParameter("@StatusEquipamento", SqlDbType.NVarChar, 50) { Value = (object)eq.StatusEquipamento };
            parametros[11] = new SqlParameter("@AuthToken", SqlDbType.NVarChar, 255) { Value = (object)eq.AuthToken };
            parametros[12] = new SqlParameter("@FirmwareVersion", SqlDbType.NVarChar, 50) { Value = (object)eq.FirmwareVersion };
            parametros[13] = new SqlParameter("@LastUpdate", SqlDbType.DateTime) { Value = eq.LastUpdate };
            parametros[14] = new SqlParameter("@DataAlteracao", SqlDbType.DateTime) { Value = eq.DataAlteracao };

            return parametros;
        }

        protected override EquipamentoViewModel MontaModel(DataRow registro)
        {
            var eq = new EquipamentoViewModel();

            eq.Id = Convert.ToInt32(registro["Id"]);
            eq.Nome = registro["Nome"].ToString();
            eq.EmpresaId = Convert.ToInt32(registro["EmpresaId"]);
            eq.MacAddress = registro["MacAddress"].ToString();
            eq.IpAddress = registro["IpAddress"].ToString();
            eq.SSID = registro["SSID"].ToString();
            eq.SignalStrength = Convert.ToInt32(registro["SignalStrength"]);
            eq.ConnectionStatus = registro["ConnectionStatus"].ToString();

            if (registro["DataRegistro"] != DBNull.Value)
                eq.DataRegistro = Convert.ToDateTime(registro["DataRegistro"]);

            eq.SensorData = registro["SensorData"].ToString();
            eq.StatusEquipamento = registro["StatusEquipamento"].ToString();
            eq.AuthToken = registro["AuthToken"].ToString();
            eq.FirmwareVersion = registro["FirmwareVersion"].ToString();

            if (registro["LastUpdate"] != DBNull.Value)
                eq.LastUpdate = Convert.ToDateTime(registro["LastUpdate"]);

            if (registro["DataAlteracao"] != DBNull.Value)
                eq.DataAlteracao = Convert.ToDateTime(registro["DataAlteracao"]);

            return eq;
        }

        private EmpresaViewModel MontaViewModelEmpresa(DataRow registro)
        {
            var empresa = new EmpresaViewModel();
            empresa.Id = Convert.ToInt32(registro["id"]);
            empresa.NomeFantasia = registro["NomeFantasia"].ToString();

            return empresa;
        }

        public List<EmpresaViewModel> GetAllEmpresas()
        {
            var ListaEmpresas = new List<EmpresaViewModel>();
            string sql = "Select Id, NomeFantasia from [dbo].[tbEmpresa] order by NomeFantasia";
            try
            {
                DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
                foreach (DataRow row in tabela.Rows)
                {
                    ListaEmpresas.Add(MontaViewModelEmpresa(row));
                }

                return ListaEmpresas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                throw;
            }
        }

        public EmpresaViewModel ConsultaEmpresa(int id)
        {
            string sql = "Select Id, NomeFantasia from [dbo].[tbEmpresa] where id = " + id;
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

        private EquipamentoViewModel MontaViewModelParaExibir(DataRow row)
        {
            return new EquipamentoViewModel
            {
                Id = Convert.ToInt32(row["Id"]),
                Nome = row["Nome"].ToString(),
                EmpresaId = Convert.ToInt32(row["EmpresaId"]),
                MacAddress = row["MacAddress"].ToString(),
                IpAddress = row["IpAddress"].ToString(),
                SSID = row["SSID"].ToString(),
                SignalStrength = Convert.ToInt32(row["SignalStrength"]),
                ConnectionStatus = row["ConnectionStatus"].ToString(),
                DataRegistro = Convert.ToDateTime(row["DataRegistro"]),
                SensorData = row["SensorData"].ToString(),
                StatusEquipamento = row["StatusEquipamento"].ToString(),
                AuthToken = row["AuthToken"].ToString(),
                FirmwareVersion = row["FirmwareVersion"].ToString(),
                LastUpdate = Convert.ToDateTime(row["LastUpdate"]),
                DataAlteracao = Convert.ToDateTime(row["DataAlteracao"])
            };
        }
    }
}
