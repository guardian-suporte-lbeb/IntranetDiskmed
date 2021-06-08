using IntranetDiskmed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetDiskmed
{
    public partial class index : System.Web.UI.Page
    {
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

            bool acessoClientes = false;

            List<bool> rotinasClientes = new List<bool>();
            rotinasClientes.Add(usuario.Anvisa);
            rotinasClientes.Add(usuario.Custo);
            rotinasClientes.Add(usuario.Empenho);

            foreach (bool rotina in rotinasClientes)
            {
                if (rotina)
                {
                    acessoClientes = true;
                    break;
                }
            }

            if (usuario.Tipo != "A")
                menuAcessos.Visible = false;
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
    }
}