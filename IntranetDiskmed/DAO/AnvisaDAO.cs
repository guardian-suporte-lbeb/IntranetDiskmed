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
    public class AnvisaDAO
    {
        public DataTable BuscarDadosAnvisa(string filtroQuery)
        {
            DataTable dtdadosAnvisa = new DataTable();

            string query = "";

            try
            {
                query += 
                    "SELECT " +
                    "ZZA_FILIAL AS 'Filial', " +
                    "ZZA_SUBS1 AS 'Substancia', " +
                    "ZZA_SUBS2 AS 'Substancia2', " +
                    "ZZA_CNPJ AS 'CNPJ', " +
                    "ZZA_LABORA AS 'Laboratorio', " +
                    "ZZA_GGREM AS 'CodGgrem', " +
                    "ZZA_REGIST AS 'Registro', " +
                    "ZZA_EAN1 AS 'Ean1', " +
                    "ZZA_EAN2 AS 'Ean2', " +
                    "ZZA_EAN3 AS 'Ean3', " +
                    "ZZA_PRODUT AS 'Produto', " +
                    "ZZA_APRESE AS 'Apresentacao', " +
                    "D_E_L_E_T_, " +
                    "R_E_C_N_O_, " +
                    "R_E_C_D_E_L_, " +
                    "ZZA_CLASSE AS 'ClasseTerapeutica', " +
                    "ZZA_TIPO AS 'TipoProduto', " +
                    "ZZA_REGIME AS 'RegimePreco', " +
                    "ZZA_PFSIMP AS 'PFSemImpostos', " +
                    "ZZA_PF0 AS 'PF0', " +
                    "ZZA_PF12 AS 'PF12', " +
                    "ZZA_PF17 AS 'PF17', " +
                    "ZZA_PF17AL AS 'PF17AL', " +
                    "ZZA_PF175 AS 'PF175', " +
                    "ZZA_PF175A AS 'PF175AL', " +
                    "ZZA_PF18 AS 'PF18', " +
                    "ZZA_PF18AL AS 'PF18AL', " +
                    "ZZA_PF20 AS 'PF20', " +
                    "ZZA_PMC0 AS 'PMC0', " +
                    "ZZA_PMC12 AS 'PMC12', " +
                    "ZZA_PMC17 AS 'PMC17', " +
                    "ZZA_PMC17A AS 'PMC17AL', " +
                    "ZZA_PMC175 AS 'PMC175', " +
                    "ZZA_PM175A AS 'PMC175AL', " +
                    "ZZA_PMC18 AS 'PMC18', " +
                    "ZZA_PMC18A AS 'PMC18AL', " +
                    "ZZA_PMC20 AS 'PMC20', " +
                    "ZZA_RESTR AS 'RestHosp', " +
                    "ZZA_CAP AS 'Cap', " +
                    "ZZA_CONF87 AS 'Confaz87', " +
                    "ZZA_ICM0 AS 'ICMS0', " +
                    "ZZA_ANREC AS 'AnaliseRec', " +
                    "ZZA_LISTPC AS 'Listpc', " +
                    "ZZA_COM19 AS 'Comerc19', " +
                    "ZZA_TARJA AS 'Tarja', " +
                    "ZZA_IDPROC AS 'Idproc' " +
                    "FROM " + TabelasERP.ZZA + " " +
                    "WHERE D_E_L_E_T_ = '' " + filtroQuery + " ";

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
                                dtdadosAnvisa.Load(reader);
                            }
                        }
                    }
                }
                return dtdadosAnvisa;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}