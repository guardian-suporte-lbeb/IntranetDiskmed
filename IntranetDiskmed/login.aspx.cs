using IntranetDiskmed.Intranet;
using IntranetDiskmed.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetDiskmed
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuarios.Text.Trim()) || string.IsNullOrEmpty(txtSenha.Text.Trim()))
                return;

            string ip = Dns.GetHostName();
            object clientIPAddress = Dns.GetHostAddresses(ip);

            foreach (IPAddress ipAddress in Dns.GetHostAddresses(ip))
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = ipAddress.ToString();
                    break;
                }
            }

            Usuario usuario = new Usuario(ip, txtUsuarios.Text, txtSenha.Text);

            try
            {
                if (usuario.AutenticarUsuario())
                {
                    DateTime dtNow = DateTime.Now;
                    DateTime dtExpires = DateTime.Now.AddDays(5);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    usuario.Nome,
                    dtNow,
                    dtExpires,
                    true,
                    "",
                    FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    Session["USUARIO"] = usuario;
                    Session["DADOS_RELATORIO"] = "";
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(usuario.Nome, true));
                }
                else
                {
                    string msg = "Usuário ou senha incorretos!";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("alert('");
                    sb.Append(msg);
                    sb.Append("');");
                    sb.Append("</script>");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb.ToString(), false);
                }
            }
            catch
            {
                string msg = "Tentativa de Login sem sucesso! Tente novamente mais tarde!";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("alert('");
                sb.Append(msg);
                sb.Append("');");
                sb.Append("</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb.ToString(), false);
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                return;

            try
            {
                Conexao cx = new Conexao();
                cx.Conectar();

                string query = " SELECT NOME, SENHA FROM INTRANET_ACESSO ";
                query += " WHERE EMAIL = @EMAIL ";

                cx.CriarComando(query);
                cx.AdicionarParametro("@EMAIL", txtEmail.Text.ToUpper().Trim());
                DataTable dt = cx.ExecutarLeitura();

                cx.Desconectar();

                if (dt.Rows.Count == 0)
                {
                    string msg = "E-mail inválido!";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("alert('");
                    sb.Append(msg);
                    sb.Append("');");
                    sb.Append("</script>");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb.ToString(), false);
                }
                else
                {
                    string nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt.Rows[0]["NOME"].ToString().ToLower());
                    string senha = Criptografia.descriptografar(dt.Rows[0]["SENHA"].ToString());

                    string mensagem = nome + ",";
                    mensagem += "\n\n";
                    mensagem += "Foi solicitada a senha de acesso à Intranet.";
                    mensagem += "\n\n";
                    mensagem += "Senha: " + senha;
                    mensagem += "\n\n";
                    mensagem += "Atenciosamente,";
                    mensagem += "\n\n";
                    mensagem += "Intranet Térmica";

                    Email email = new Email("Recuperação de Senha | Intranet Térmica", mensagem, txtEmail.Text.ToLower().Trim());

                    if (email.EnviarEmail())
                    {
                        string msg = "E-mail enviado com sucesso!";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("alert('");
                        sb.Append(msg);
                        sb.Append("');");
                        sb.Append("</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb.ToString(), false);
                    }
                    else
                    {
                        string msg = "Não foi possível disparar a mensagem! Verifique se o e-mail informado está correto e tente novamente!";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("alert('");
                        sb.Append(msg);
                        sb.Append("');");
                        sb.Append("</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb.ToString(), false);
                    }
                }
            }
            catch
            {
                string msg = "Não foi possível disparar a mensagem para o e-mail informado! Tente novamente mais tarde!";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("alert('");
                sb.Append(msg);
                sb.Append("');");
                sb.Append("</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb.ToString(), false);
            }
        }
    }
}