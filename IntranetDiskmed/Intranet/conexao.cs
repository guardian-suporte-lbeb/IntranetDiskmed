using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace IntranetDiskmed.Intranet
{
    public class Conexao
    {
        public SqlConnection cn = new SqlConnection();
        public SqlCommand cd;

        public string ConexaoSql()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + Configuracoes.ArquivoConfig);

            string cnString = string.Format("Server={0};DataBase={1};UID={2};PWD={3};Connection Timeout=1440;",
                    xmlDoc.GetElementsByTagName("sqlConnection")[0]["host"].InnerText,
                    xmlDoc.GetElementsByTagName("sqlConnection")[0]["dataBase"].InnerText,
                    xmlDoc.GetElementsByTagName("sqlConnection")[0]["login"].InnerText,
                    xmlDoc.GetElementsByTagName("sqlConnection")[0]["password"].InnerText);

            return cnString;
        }

        public void Conectar()
        {
            cn.ConnectionString = ConexaoSql();

            cn.Open();
        }

        public void Desconectar()
        {
            cn.Close();
        }

        public void CriarComando(string query)
        {
            cd = new SqlCommand(query, cn);
        }

        public void AdicionarParametro(string parametro, object valor)
        {
            cd.Parameters.AddWithValue(parametro, valor);
        }

        public void ExecutarGravacao()
        {
            cd.ExecuteNonQuery();
        }

        public DataTable ExecutarLeitura()
        {
            SqlDataReader dr = cd.ExecuteReader();
            DataTable dt = new DataTable();

            if (dr.HasRows)
                dt.Load(dr);

            dr.Close();
            dr.Dispose();

            return dt;
        }
    }
}