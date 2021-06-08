using IntranetDiskmed.Intranet;
using IntranetDiskmed.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetDiskmed.MenuUsuarios
{
    public partial class usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["USUARIO"];

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

            TxtUsuario.Text = usuario.Nome;


            if (!IsPostBack)
            {
                if (usuario.Tipo != "A")
                    Response.Redirect("menu.aspx");

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now);

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetNoServerCaching();
                HttpContext.Current.Response.Cache.SetNoStore();
                Response.Cache.AppendCacheExtension("no-cache");
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                Response.Cache.SetNoStore();
                Response.AppendHeader("Pragma", "no-cache");

                try
                {
                    Conexao cx = new Conexao();
                    cx.Conectar();

                    #region QUERY
                    string query = " SELECT NOME, LOGIN, EMAIL, TIPO FROM INTRANET_ACESSO ";
                    #endregion

                    cx.CriarComando(query);

                    DataTable dtResults = cx.ExecutarLeitura();
                    cx.Desconectar();

                    if (dtResults.Rows.Count > 0)
                    {
                        gdvUsuarios.DataSource = dtResults;
                        gdvUsuarios.DataBind();
                        gdvUsuarios.UseAccessibleHeader = true;
                        gdvUsuarios.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                catch
                {
                    string msg = "Não foi possível carregar os usuários! Tente novamente mais tarde!";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("alert('");
                    sb.Append(msg);
                    sb.Append("');");
                    sb.Append("window.history.back();");
                    sb.Append("</script>");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb.ToString(), false);
                }
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            Response.Redirect("manutencao-acessos.aspx?opt=" + Criptografia.criptografar("editar") + "&id=" + Criptografia.criptografar(row.Cells[2].Text));
        }

        protected void btnLog_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            Response.Redirect("log.aspx?id=" + Criptografia.criptografar(row.Cells[2].Text));
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;

            Response.Redirect("manutencao-acessos.aspx?opt=" + Criptografia.criptografar("excluir") + "&id=" + Criptografia.criptografar(row.Cells[2].Text));
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("manutencao-acessos.aspx?opt=" + Criptografia.criptografar("incluir"));
        }

        protected void gdvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(e.Row.Cells[1].Text.ToLower());
                e.Row.Cells[3].Text = e.Row.Cells[3].Text.ToLower();

                if (e.Row.Cells[4].Text == "A")
                    e.Row.Cells[4].Text = "Administrador";
                else
                    e.Row.Cells[4].Text = "Usuário";
            }
        }
    }
}