using IntranetDiskmed.DAO;
using IntranetDiskmed.Intranet;
using IntranetDiskmed.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetDiskmed.MenuCustoProduto
{
    public partial class AlteracaoCustoProduto : System.Web.UI.Page
    {
        private Usuario usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoServerCaching();
                HttpContext.Current.Response.Cache.SetNoStore();
                Response.Cache.AppendCacheExtension("no-cache");
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                Response.Cache.SetNoStore();
                Response.AppendHeader("Pragma", "no-cache");
            }

            usuario = (Usuario)Session["USUARIO"];
            Interface.FecharModalMensagem(ModalMensagem, BackgroundModal);

            if (usuario == null)
            {
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);
                HttpContext.Current.Response.Cache.SetNoServerCaching();
                HttpContext.Current.Response.Cache.SetNoStore();
                Response.Cache.AppendCacheExtension("no-cache");
                Response.Expires = 0;
                Session.Abandon();

                Response.Redirect(FormsAuthentication.LoginUrl);
            }

            if (!IsPostBack)
            {
                PreencherOrigem();
                PreencherMarca();
            }
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["USUARIO"];
            usuario.DeslogarUsuario();

            FormsAuthentication.SignOut();

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();
            Response.Cache.AppendCacheExtension("no-cache");
            Response.Expires = 0;
            Session.Abandon();

            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        protected void GdvCustoProduto_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtNotas = Session["GdvCustoProduto"] as DataTable;

            if (dtNotas != null)
            {
                dtNotas.DefaultView.Sort = e.SortExpression + " " + OrdenarColuna(e);
                GdvCustoProduto.DataSource = dtNotas;
                GdvCustoProduto.DataBind();
            }
        }

        private string OrdenarColuna(GridViewSortEventArgs e)
        {
            // Por padrão, defina a direção de classificação como ascendente.
            string ordem = "ASC";

            //Recupere a última coluna que foi classificada.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Verifique se a mesma coluna está sendo classificada.
                // Caso contrário, o valor padrão pode ser retornado.
                if (sortExpression == e.SortExpression)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        ordem = "DESC";
                    }
                }
            }

            // Salve novos valores no ViewState.
            ViewState["SortDirection"] = ordem;
            ViewState["SortExpression"] = e.SortExpression;

            return ordem;
        }

        public void BuscarDados()
        {
            AlteracaoCProdutoDAO CProdutoDAO = new AlteracaoCProdutoDAO();

            DataTable dtDados = new DataTable();

            string filtroQuery = "";

            // Filtro por Data De
            if (!string.IsNullOrEmpty(TxtDataDe.Text.Replace("'", " ")))
            {
                try
                {
                    filtroQuery += " AND ZZP_DATA >= '" + Convert.ToDateTime(TxtDataDe.Text.Trim()).ToString("yyyy/MM/dd").Replace("/", "") + "' ";
                }
                catch (Exception)
                {
                    Interface.ExibirModalMensagem(Interface.Tipo.Problema, ModalMensagem, BackgroundModal, LblDescricaoModalMensagem);
                    return;
                }
            }

            // Filtro por Data Ate
            if (!string.IsNullOrEmpty(TxtDataAte.Text.Replace("'", " ")))
            {
                try
                {
                    filtroQuery += " AND ZZP_DATA <= '" + Convert.ToDateTime(TxtDataAte.Text.Trim()).ToString("yyyy/MM/dd").Replace("/", "") + "' ";
                }
                catch (Exception)
                {
                    Interface.ExibirModalMensagem(Interface.Tipo.Problema, ModalMensagem, BackgroundModal, LblDescricaoModalMensagem);
                    return;
                }
            }

            // Filtro por Like
            if (!String.IsNullOrWhiteSpace(TxtNomeUsuario.Text.Replace("'", " ")))
            {
                filtroQuery += " AND ZZP_NOMEUS LIKE '%" + TxtNomeUsuario.Text.Replace("'", " ").TrimStart().TrimEnd() + "%' ";
            }

            if (!String.IsNullOrWhiteSpace(TxtDescriProd.Text.Replace("'", " ")))
            {
                filtroQuery += " AND ZZP_DESCRI LIKE '%" + TxtDescriProd.Text.Replace("'", " ").TrimStart().TrimEnd() + "%' ";
            }

            // Filtro Lista
            if (DdlMarca.SelectedValue != "Todos")
            {
                filtroQuery += " AND ZZP_MARCA LIKE '%" + DdlMarca.SelectedValue.TrimStart().TrimEnd() + "%' ";
            }

            if (DdlOrigem.SelectedValue != "Todos")
            {
                filtroQuery += " AND ZZP_ORIGEM LIKE '%" + DdlOrigem.SelectedValue.TrimStart().TrimEnd() + "%' ";
            }

            dtDados = CProdutoDAO.DadosAlteracaoCusto(filtroQuery);

            if (dtDados.Rows.Count > 0)
            {
                GdvCustoProduto.DataSource = dtDados;
                GdvCustoProduto.DataBind();
                GdvCustoProduto.UseAccessibleHeader = true;
                GdvCustoProduto.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                TituloGrid1.Visible = false;
                Interface.ExibirModalMensagem(Interface.Tipo.Problema, ModalMensagem, BackgroundModal, LblDescricaoModalMensagem, "Nenhum dado encontrado!");
                GdvCustoProduto.DataSource = null;
                GdvCustoProduto.DataBind();
            }
        }

        protected void LbtnBuscarCustoProduto_Click(object sender, EventArgs e)
        {
            BuscarDados();
        }

        public void PreencherOrigem()
        {
            List<AlteracaoCProduto> listAlteracaoCProduto = new AlteracaoCProdutoDAO().BuscarOrigemLista();

            if (listAlteracaoCProduto.Count > 0)
            {
                DdlOrigem.Items.Clear();
                DdlOrigem.Items.Add(new ListItem { Value = "Todos", Text = "Todos" });
                foreach (AlteracaoCProduto alteracaoCProduto in listAlteracaoCProduto)
                {
                    ListItem listItemFiltro = new ListItem
                    {
                        Value = alteracaoCProduto.Origem,
                        Text = alteracaoCProduto.Origem,
                    };
                    DdlOrigem.Items.Add(listItemFiltro);
                }
            }
        }

        public void PreencherMarca()
        {
            List<AlteracaoCProduto> listAlteracaoCProduto = new AlteracaoCProdutoDAO().BuscarMarcaLista();

            if (listAlteracaoCProduto.Count > 0)
            {
                DdlMarca.Items.Clear();
                DdlMarca.Items.Add(new ListItem { Value = "Todos", Text = "Todos" });
                foreach (AlteracaoCProduto alteracaoCProduto in listAlteracaoCProduto)
                {
                    ListItem listItemFiltro = new ListItem
                    {
                        Value = alteracaoCProduto.Marca,
                        Text = alteracaoCProduto.Marca,
                    };
                    DdlMarca.Items.Add(listItemFiltro);
                }
            }
        }
    }
}