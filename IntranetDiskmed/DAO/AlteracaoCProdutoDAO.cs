using IntranetDiskmed.Intranet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.DAO
{
    public class AlteracaoCProdutoDAO
    {
        public DataTable DadosAlteracaoCusto()
        {
            DataTable dtalteracaoCusto = new DataTable();

            string query =
                "SELECT " +
                "ZZP_FILIAL AS 'FILIAL', " +
                "ZZP_DATA AS 'DATA', " +
                "ZZP_HORA AS 'HORA', " +
                "ZZP_USER AS 'COD USUARIO', " +
                "ZZP_NOMEUS AS 'NOME USUARIO', " +
                "ZZP_ORIGEM AS 'ROTINA', " +
                "ZZP_CODIGO AS 'COD PRODUTO', " +
                "ZZP_DESCRI AS 'DESCRIÇÃO DO PRODUTO', " +
                "ZZP_MARCA AS 'MARCA', " +
                "ZZP_OLDUPC AS 'CUSTO ANTERIOR', " +
                "ZZP_OLDSTD AS 'STANDART ANTERIOR', " +
                "ZZP_NEWUPC AS 'CUSTO NOVO', " +
                "ZZP_NEWSTD AS 'STANDART NOVO' " +
                "FROM " + TabelasERP.ZZP + " " +
                "WHERE D_E_L_E_T_ = '' " +
                "ORDER BY DATA DESC, HORA DESC ";

            Conexao conexao = new Conexao();
            using (SqlConnection connection = new SqlConnection(conexao.ConexaoSql()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 1000;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dtalteracaoCusto.Load(reader);
                        }
                    }
                }
            }
            return dtalteracaoCusto;

        }
    }
}