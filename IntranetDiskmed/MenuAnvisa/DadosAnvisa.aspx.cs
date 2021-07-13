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

namespace IntranetDiskmed.MenuAnvisa
{
    public partial class DadosAnvisa : System.Web.UI.Page
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

        protected void GdvAnvisa_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtNotas = Session["GridAnvisa"] as DataTable;

            if (dtNotas != null)
            {
                dtNotas.DefaultView.Sort = e.SortExpression + " " + OrdenarColuna(e);
                GdvAnvisa.DataSource = dtNotas;
                GdvAnvisa.DataBind();
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

        protected void LbtnConsultarDadosAnvisa_Click(object sender, EventArgs e)
        {
            string filtroQuery = "";

            if (!String.IsNullOrWhiteSpace(TxtTextoLike.Text.Trim()))
            {
                filtroQuery += " AND ( UPPER(RTRIM(LTRIM((ZZA_SUBS1))) LIKE UPPER(RTRIM(LTRIM(('%" + TxtTextoLike.Text + "%'))) " +
                    "OR UPPER(RTRIM(LTRIM((ZZA_SUBS2))) LIKE UPPER(RTRIM(LTRIM(('%" + TxtTextoLike.Text + "%'))) " +
                    "OR UPPER(RTRIM(LTRIM((ZZA_LABORA))) LIKE UPPER(RTRIM(LTRIM(('%" + TxtTextoLike.Text + "%'))) " +
                    "OR UPPER(RTRIM(LTRIM((ZZA_PRODUT))) LIKE UPPER(RTRIM(LTRIM(('%" + TxtTextoLike.Text + "%'))) " +
                    "OR UPPER(RTRIM(LTRIM((ZZA_CLASSE))) LIKE UPPER(RTRIM(LTRIM(('%" + TxtTextoLike.Text + "%'))) " +
                    "OR UPPER(RTRIM(LTRIM((ZZA_REGIME))) LIKE UPPER(RTRIM(LTRIM(('%" + TxtTextoLike.Text + "%'))) " +
                    "OR UPPER(RTRIM(LTRIM((ZZA_LISTPC))) LIKE UPPER(RTRIM(LTRIM(('%" + TxtTextoLike.Text + "%'))) " +
                    "OR UPPER(RTRIM(LTRIM((ZZA_TARJA))) LIKE UPPER(RTRIM(LTRIM(('%" + TxtTextoLike.Text + "%'))) " +
                    " ) ";
            }

            if (!String.IsNullOrWhiteSpace(TxtValorDe.Text) || !String.IsNullOrWhiteSpace(TxtValorAte.Text))
            {
                filtroQuery += " AND ( ZZA_PFSIMP BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF0 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF12 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF17 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF17AL BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF175 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF175A BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF18 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF18AL BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PF20 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PMC0 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PMC12 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PMC17 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PMC17A BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PMC175 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PM175A BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PMC18 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PMC18A BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    "OR ZZA_PMC20 BETWEEN '" + TxtValorDe.Text.Trim() + "' AND '" + TxtValorAte.Text.Trim() + "' " +
                    ") ";
            }

            try
            {
                 DataTable dtanvisa = new AnvisaDAO().BuscarDadosAnvisa(filtroQuery);

                if (dtanvisa.Rows.Count > 0)
                {
                    Session["GridAnvisa"] = dtanvisa;
                    TituloGrid1.Visible = true;
                    GdvAnvisa.DataSource = dtanvisa;
                    GdvAnvisa.DataBind();
                    GdvAnvisa.UseAccessibleHeader = true;
                    GdvAnvisa.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    TituloGrid1.Visible = false;
                    Interface.ExibirModalMensagem(Interface.Tipo.Problema, ModalMensagem, BackgroundModal, LblDescricaoModalMensagem, "Nenhum dado encontrado!");
                    GdvAnvisa.DataSource = null;
                    GdvAnvisa.DataBind();
                }
            }
            catch (Exception ex)
            {
                Interface.ExibirModalMensagem(Interface.Tipo.Problema, ModalMensagem, BackgroundModal, LblDescricaoModalMensagem, ex.ToString());
                throw;
            }
        }
    }
}