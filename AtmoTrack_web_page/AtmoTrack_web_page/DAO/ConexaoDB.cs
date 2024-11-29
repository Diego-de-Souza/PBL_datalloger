﻿using System.Data.SqlClient;

namespace AtmoTrack_web_page.DAO
{
    public class ConexaoDB
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source = DESKTOP-2C0F335\\SQLEXPRESS ;Initial Catalog=PBL_EC5;user id=sa; password=123456";
            try
            {
                SqlConnection conexao = new SqlConnection(strCon);
                conexao.Open();
                return conexao;
            }
            catch (SqlException ex)
            {
                // Lidar com a exceção, talvez logando o erro
                throw new Exception("Erro ao conectar ao banco de dados", ex);
            }
        }

        public HttpClient GetHttpClient()
        {
            
            return new HttpClient();
        }
    }
}
