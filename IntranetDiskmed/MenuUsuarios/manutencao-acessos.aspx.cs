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
    public partial class manuntencao_acessos : System.Web.UI.Page
    {
        static string opt = "";
        static string id = "";

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
                opt = Criptografia.descriptografar(this.Request.QueryString["opt"].ToString().Replace(" ", "+"));

                if (opt == "incluir")
                {
                    //pTitle.InnerHtml = "Incluir";
                }
                else
                {
                    id = Criptografia.descriptografar(this.Request.QueryString["id"].ToString().Replace(" ", "+"));

                    //if (opt == "editar")
                    //    //pTitle.InnerHtml = "Editar";
                    //else if (opt == "excluir")
                    //    //pTitle.InnerHtml = "Excluir";
                }
            }
            catch
            {
                Response.Redirect("menu.aspx");
            }

            if (!IsPostBack)
            {
                LblTituloAcao.Text = opt.ToUpper() + " USUÁRIO";
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

                if (opt != "incluir")
                {
                    try
                    {
                        Conexao cx = new Conexao();
                        cx.Conectar();

                        #region QUERY
                        string query = " SELECT * FROM INTRANET_ACESSO ";
                        query += " WHERE LOGIN = @LOGIN ";
                        #endregion

                        cx.CriarComando(query);

                        #region PARAMETERS
                        cx.AdicionarParametro("@LOGIN", id.ToUpper());
                        #endregion

                        DataTable dtResults = cx.ExecutarLeitura();
                        cx.Desconectar();

                        if (dtResults.Rows.Count > 0)
                        {
                            txtNome.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtResults.Rows[0]["NOME"].ToString().ToLower().Trim());
                            txtLogin.Text = id.ToUpper();
                            txtEmail.Text = dtResults.Rows[0]["EMAIL"].ToString().ToLower().Trim();
                            txtSenhaUsuario.Text = Criptografia.descriptografar(dtResults.Rows[0]["SENHA"].ToString());

                            if (usuario.Login == id)
                            {
                                if (opt == "excluir")
                                    btnConfirmar.Visible = false;

                                txtLogin.ReadOnly = true;
                                rdAdm.Enabled = false;
                                rdUsuario.Enabled = false;
                            }

                            if (dtResults.Rows[0]["TIPO"].ToString() == "A")
                            {
                                rdAdm.Checked = true;
                                rdUsuario.Checked = false;
                            }
                            else
                            {
                                rdAdm.Checked = false;
                                rdUsuario.Checked = true;
                            }

                            if (dtResults.Rows[0]["EMPENHO"].ToString() == "S")
                            {
                                chkEmpenho.Checked = true;
                            }
                            else
                            {
                                chkEmpenho.Checked = false;
                            }

                            if (dtResults.Rows[0]["CUSTO"].ToString() == "S")
                            {
                                chkCustos.Checked = true;
                            }
                            else
                            {
                                chkCustos.Checked = false;
                            }

                            if (dtResults.Rows[0]["ANVISA"].ToString() == "S")
                            {
                                chkAnvisa.Checked = true;
                            }
                            else
                            {
                                chkAnvisa.Checked = false;
                            }

                            if (opt == "excluir")
                            {
                                txtNome.ReadOnly = true;
                                txtEmail.ReadOnly = true;
                                txtLogin.ReadOnly = true;
                                txtSenhaUsuario.ReadOnly = true;
                                rdUsuario.Enabled = false;
                                rdAdm.Enabled = false;
                            }
                        }
                        else
                        {
                            btnConfirmar.Enabled = false;

                            string msg = "Usuário não encontrado!";
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
                    catch
                    {
                        btnConfirmar.Enabled = false;

                        string msg = "Não foi possível carregar os dados do usuário! Tente novamente mais tarde!";
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
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text.Trim()) &&
                string.IsNullOrEmpty(txtEmail.Text.Trim()) &&
                string.IsNullOrEmpty(txtLogin.Text.Trim()) &&
                string.IsNullOrEmpty(txtSenhaUsuario.Text.Trim()))
                return;

            if (rdUsuario.Checked)
            {
                bool formValido = false;
                List<bool> permissoes = new List<bool>();
                permissoes.Add(chkEmpenho.Checked);
                permissoes.Add(chkCustos.Checked);
                permissoes.Add(chkAnvisa.Checked);

                foreach (bool permissao in permissoes)
                {
                    if (permissao)
                    {
                        formValido = true;
                        break;
                    }
                }

                if (!formValido)
                    return;
            }

            try
            {
                Usuario usuario = (Usuario)Session["USUARIO"];

                string query = "";
                Conexao cx = new Conexao();
                cx.Conectar();

                if (opt == "incluir")
                {
                    #region QUERY
                    query = " SELECT * FROM INTRANET_ACESSO ";
                    query += " WHERE LOGIN = @LOGIN ";
                    query += " OR EMAIL = @EMAIL ";
                    query += " OR NOME = @NOME ";
                    #endregion

                    cx.CriarComando(query);

                    #region PARAMETERS
                    cx.AdicionarParametro("@LOGIN", txtLogin.Text.ToUpper().Trim());
                    cx.AdicionarParametro("@EMAIL", txtEmail.Text.ToUpper().Trim());
                    cx.AdicionarParametro("@NOME", txtNome.Text.ToUpper().Trim());
                    #endregion

                    DataTable dtResults = cx.ExecutarLeitura();

                    if (dtResults.Rows.Count > 0)
                    {
                        txtLogin.Focus();

                        string msg2 = "Nome, Login ou E-mail já existentes!";
                        System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                        sb2.Append("<script type = 'text/javascript'>");
                        sb2.Append("alert('");
                        sb2.Append(msg2);
                        sb2.Append("');");
                        sb2.Append("</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb2.ToString(), false);

                        return;
                    }

                    #region QUERY
                    query = " INSERT INTO INTRANET_ACESSO ";
                    query += " (NOME, LOGIN, EMAIL, SENHA, TIPO, BLOQ, ";
                    query += " EMPENHO, CUSTO, ANVISA) ";
                    query += " VALUES ";
                    query += " (@NOME, @LOGIN, @EMAIL, @SENHA, @TIPO, @BLOQ, ";
                    query += " @EMPENHO, @CUSTO, @ANVISA) ";
                    #endregion
                }
                else if (opt == "editar")
                {
                    if (txtLogin.Text.ToUpper().Trim() != id)
                    {
                        #region QUERY
                        query = " SELECT * FROM INTRANET_ACESSO ";
                        query += " WHERE LOGIN = @LOGIN ";
                        query += " OR EMAIL = @EMAIL ";
                        query += " OR NOME = @NOME ";
                        #endregion

                        cx.CriarComando(query);

                        #region PARAMETERS
                        cx.AdicionarParametro("@LOGIN", txtLogin.Text.ToUpper().Trim());
                        cx.AdicionarParametro("@EMAIL", txtEmail.Text.ToUpper().Trim());
                        cx.AdicionarParametro("@NOME", txtNome.Text.ToUpper().Trim());
                        #endregion

                        DataTable dtResults = cx.ExecutarLeitura();

                        if (dtResults.Rows.Count > 0)
                        {
                            txtLogin.Focus();

                            string msg2 = "Nome, Login ou E-mail já existentes!";
                            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                            sb2.Append("<script type = 'text/javascript'>");
                            sb2.Append("alert('");
                            sb2.Append(msg2);
                            sb2.Append("');");
                            sb2.Append("</script>");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb2.ToString(), false);

                            return;
                        }
                    }

                    #region QUERY
                    query = " UPDATE INTRANET_ACESSO ";
                    query += " SET ";
                    query += " NOME = @NOME, LOGIN = @LOGIN, EMAIL = @EMAIL, ";
                    query += " SENHA = @SENHA, TIPO = @TIPO, BLOQ = @BLOQ, ";
                    query += " EMPENHO = @EMPENHO, CUSTO = @CUSTO, ANVISA = @ANVISA ";
                    query += " WHERE LOGIN = @LOGIN ";
                    #endregion
                }
                else if (opt == "excluir")
                {
                    #region QUERY
                    query = " DELETE FROM INTRANET_ACESSO ";
                    query += " WHERE LOGIN = @LOGIN ";
                    #endregion
                }

                cx.CriarComando(query);

                #region PARAMETERS
                cx.AdicionarParametro("@NOME", txtNome.Text.ToUpper().Trim());
                cx.AdicionarParametro("@LOGIN", txtLogin.Text.ToUpper().Trim());
                cx.AdicionarParametro("@EMAIL", txtEmail.Text.ToUpper().Trim());
                cx.AdicionarParametro("@SENHA", Criptografia.criptografar(txtSenhaUsuario.Text.Trim()));

                if (rdAdm.Checked)
                    cx.AdicionarParametro("@TIPO", "A");
                else
                    cx.AdicionarParametro("@TIPO", "U");

                cx.AdicionarParametro("@BLOQ", "");

                if (rdUsuario.Checked)
                {
                    if (chkEmpenho.Checked)
                        cx.AdicionarParametro("@EMPENHO", "S");
                    else
                        cx.AdicionarParametro("@EMPENHO", "N");

                    if (chkCustos.Checked)
                        cx.AdicionarParametro("@CUSTO", "S");
                    else
                        cx.AdicionarParametro("@CUSTO", "N");

                    if (chkAnvisa.Checked)
                        cx.AdicionarParametro("@ANVISA", "S");
                    else
                        cx.AdicionarParametro("@ANVISA", "N");
                }
                else
                {
                    cx.AdicionarParametro("@EMPENHO", "S");
                    cx.AdicionarParametro("@CUSTO", "S");
                    cx.AdicionarParametro("@ANVISA", "S");
                }

                #endregion

                cx.ExecutarGravacao();
                cx.Desconectar();

                if (opt == "editar" && usuario.Login == id)
                {
                    usuario.Nome = txtNome.Text.Trim().ToUpper();
                    usuario.Email = txtEmail.Text.Trim().ToUpper();
                    usuario.Senha = Criptografia.criptografar(txtSenhaUsuario.Text.Trim());

                    if (chkEmpenho.Checked)
                        usuario.Empenho = true;
                    else
                        usuario.Empenho = false;

                    if (chkCustos.Checked)
                        usuario.Custo = true;
                    else
                        usuario.Custo = false;

                    if (chkAnvisa.Checked)
                        usuario.Anvisa = true;
                    else
                        usuario.Anvisa = false;
                    
                    Session["USUARIO"] = usuario;
                }

                string msg = "Confirmação efetuada com sucesso!";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("alert('");
                sb.Append(msg);
                sb.Append("');");
                sb.Append("window.location = 'usuarios.aspx';");
                sb.Append("</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", sb.ToString(), false);
            }
            catch
            {
                string msg = "Não foi possível confirmar a operação para o usuário! Tente novamente mais tarde!";
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

        protected void valPermissoes_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (rdUsuario.Checked)
            {
                args.IsValid = false;

                List<bool> permissoes = new List<bool>();
                permissoes.Add(chkEmpenho.Checked);
                permissoes.Add(chkCustos.Checked);
                permissoes.Add(chkAnvisa.Checked);

                foreach (bool permissao in permissoes)
                {
                    if (permissao)
                    {
                        args.IsValid = true;
                        break;
                    }
                }
            }
        }

        protected void LbtnSair_Click(object sender, EventArgs e)
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
            Session.Clear();
            Session.Abandon();

            Response.Redirect(FormsAuthentication.LoginUrl);
        }
    }
}