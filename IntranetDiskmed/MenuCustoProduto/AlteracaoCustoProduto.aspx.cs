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

        protected void LbtnBuscarCustoProduto_Click(object sender, EventArgs e)
        {

        }
    }
}