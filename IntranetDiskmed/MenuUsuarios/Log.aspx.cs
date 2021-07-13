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
    public partial class Log : System.Web.UI.Page
    {
        public static string login = "";
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

            try
            {
                login = Criptografia.descriptografar(this.Request.QueryString["id"].ToString().Replace(" ", "+"));
                //pTitle.InnerHtml = "Log: ";
                //pTitle.InnerHtml += login;
            }
            catch
            {
                Response.Redirect("menu.aspx");
            }

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
                    string countQuery = " SELECT COUNT(*) FROM INTRANET_LOG ";
                    countQuery += " WHERE LOGIN = @LOGIN ";

                    string query = " SELECT TOP 50 [DATA]";
                    query += " ,HORA";
                    query += " ,IP ";

                    query += " ,LOGIN ";
                    query += " ,SUBSTRING(DESCRICAO,1,25) AS DESCRICAO";
                    query += " , SUBSTRING(PARAMETROS,1,25) AS PARAMETROS ";
                    query += " , CAMPO ";
                    query += " , REGISTRO ";
                    query += " , SUBSTRING(ISNULL(CONVERT(VARCHAR(MAX), CONVERT(VARBINARY(MAX), CONTEUDO)), ''),1,25) AS CONTEUDO ";
                    query += " FROM INTRANET_LOG ";
                    query += " WHERE LOGIN = @LOGIN ";
                    query += " ORDER BY DATA DESC, HORA DESC ";

                    #endregion

                    cx.CriarComando(countQuery);

                    #region PARAMETERS
                    cx.AdicionarParametro("LOGIN", login);
                    #endregion

                    DataTable dtCount = cx.ExecutarLeitura();

                    cx.CriarComando(query);

                    #region PARAMETERS
                    cx.AdicionarParametro("LOGIN", login);
                    #endregion

                    DataTable dtResults = cx.ExecutarLeitura();
                    cx.Desconectar();

                    if (dtResults.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtCount.Rows[0][0]) > 100)
                            btnMTodos.Visible = true;
                        else
                            btnMTodos.Visible = false;

                        gdvUserLog.DataSource = dtResults;
                        gdvUserLog.DataBind();
                        gdvUserLog.UseAccessibleHeader = true;
                        gdvUserLog.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                catch
                {
                    string msg = "Não foi possível carregar os logs do usuário! Tente novamente mais tarde!";
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

        protected void btnMTodos_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao cx = new Conexao();
                cx.Conectar();

                #region QUERY
                string query = " SELECT [DATA]";
                query += " ,HORA";
                query += " ,IP ";
                query += " ,LOGIN ";
                query += " ,SUBSTRING(DESCRICAO,1,25) AS DESCRICAO";
                query += " , SUBSTRING(PARAMETROS,1,25) AS PARAMETROS ";
                query += " , CAMPO ";
                query += " , REGISTRO ";
                query += " , SUBSTRING(ISNULL(CONVERT(VARCHAR(8000), CONVERT(VARBINARY(8000), CONTEUDO)), ''),1,25) AS CONTEUDO ";
                query += " FROM INTRANET_LOG ";
                query += " WHERE LOGIN = @LOGIN ";
                query += " ORDER BY DATA DESC, HORA DESC ";
                #endregion

                cx.CriarComando(query);

                #region PARAMETERS
                cx.AdicionarParametro("LOGIN", login);
                #endregion

                DataTable dtResults = cx.ExecutarLeitura();
                cx.Desconectar();

                if (dtResults.Rows.Count > 0)
                {
                    btnMTodos.Visible = false;


                    gdvUserLog.DataSource = dtResults;
                    gdvUserLog.DataBind();
                    gdvUserLog.UseAccessibleHeader = true;
                    gdvUserLog.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
            }
            catch
            {
                string msg = "Não foi possível carregar os logs do usuário! Tente novamente mais tarde!";
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("usuarios.aspx");
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

        protected void gdvUserLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer)
            {
                DateTime date;

                if (DateTime.TryParseExact(e.Row.Cells[0].Text, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    e.Row.Cells[0].Text = date.ToString("dd/MM/yyyy");
                }
            }
        }
    }
}