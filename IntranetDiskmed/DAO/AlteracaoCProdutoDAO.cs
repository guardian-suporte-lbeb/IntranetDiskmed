using IntranetDiskmed.Intranet;
using IntranetDiskmed.Models;
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
        public DataTable DadosAlteracaoCusto(string filtro)
        {
            DataTable dtalteracaoCusto = new DataTable();

            string query =
                "SELECT " +
                "ZZP_FILIAL AS 'FILIAL', " +
                "SUBSTRING(ZZP_DATA, 7, 2 ) + '/' + SUBSTRING(ZZP_DATA, 5, 2 ) + '/' + SUBSTRING(ZZP_DATA, 1, 4 ) AS 'DATA', " +
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
                "WHERE D_E_L_E_T_ = '' " + filtro + " " +
                "ORDER BY DATA DESC, HORA DESC ";

            try
            {
                Conexao conexao = new Conexao();
                using (SqlConnection connection = new SqlConnection(conexao.ConexaoSql()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandTimeout = 1000;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dtalteracaoCusto.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return dtalteracaoCusto;
        }

        public List<AlteracaoCProduto> BuscarOrigemLista()
        {
            List<AlteracaoCProduto> alteracaoCProdutos = new List<AlteracaoCProduto>();

            string query = "SELECT DISTINCT ZZP_ORIGEM FROM " + TabelasERP.ZZP + " ORDER BY ZZP_ORIGEM ";

            Conexao conexao = new Conexao();
            using (SqlConnection connection = new SqlConnection(conexao.ConexaoSql()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 1000;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AlteracaoCProduto alteracaoCProduto = new AlteracaoCProduto();
                            alteracaoCProduto.Origem = reader["ZZP_ORIGEM"].ToString();

                            alteracaoCProdutos.Add(alteracaoCProduto);
                        }

                    }
                }
            }
            return alteracaoCProdutos;
        }

        public List<AlteracaoCProduto> BuscarMarcaLista()
        {
            List<AlteracaoCProduto> alteracaoCProdutos = new List<AlteracaoCProduto>();

            string query = "SELECT DISTINCT ZZP_MARCA FROM " + TabelasERP.ZZP + " ORDER BY ZZP_MARCA ";

            Conexao conexao = new Conexao();
            using (SqlConnection connection = new SqlConnection(conexao.ConexaoSql()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 1000;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AlteracaoCProduto alteracaoCProduto = new AlteracaoCProduto();
                            alteracaoCProduto.Marca = reader["ZZP_MARCA"].ToString();

                            alteracaoCProdutos.Add(alteracaoCProduto);
                        }

                    }
                }
            }
            return alteracaoCProdutos;
        }
    }
}