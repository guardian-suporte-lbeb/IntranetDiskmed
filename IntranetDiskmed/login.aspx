<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="IntranetDiskmed.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <script type="text/javascript">
        window.onload = function () {
            if (window.parent.frames.length != 0) {
                window.parent.location.href = "login.aspx";
            }
        }
    </script>

    <!-- core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/all.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/Interface-Style.css" rel="stylesheet" />
    <link href="Content/header-style.css" rel="stylesheet" />
    <link href="Content/footer-style.css" rel="stylesheet" />
</head>
<body>
    <form id="formLogin" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <header id="cabecalho" class="cabecalho-fixo">
            <div class="cabecalho-borda centralizar-cabecalho">
                <div class="cabecalho-img  col-sm-2 col-md-2">
                    <img src="images/logo_lbeb.png" alt="logo" class="logo-lbeb" />
                </div>
                <div class="centralizar-cabecalho col-sm-2 col-md-2">
                    <!-- Logo Guardian Portal -->
                    <img alt="Logo Guardian" src="images/logo_guardian.png" />
                </div>
                <div class="cabecalho-text col-sm-3 col-md-3">
                    <label class="negrito">Intranet</label>
                </div>
                <div class="centralizar-cabecalho col-sm-2 col-md-2">
                    <!-- Logo Cliente Guardian -->
                    <img alt="" src="images/logo-diskmed-sm.png" />
                </div>
                <div class="col-sm-2 col-md-2 informacao-sessao">
                    <asp:Label ID="TxtPaginaAtual" Text="Login" runat="server" />
                </div>
                <div class="col-sm-1 col-md-1 informacao-acesso">
                    <asp:Label ID="TxtUsuario" Text="" Font-Size="13px" runat="server" />
                </div>
            </div>
        </header>

        <section class="container-fluid corpo">
            <div class="login-controls">
                <div>
                    <div>
                        <div class="col-sm-6 login-left">
                            <img src="Images/logo-diskmed-lg.png" style="width: 23rem;border:3px solid #438217;border-radius: 1rem;" alt="logo" />
                        </div>
                        <div class="col-sm-2 login-right">
                            <div class="form-group">
                                <asp:TextBox placeholder="Usuário" ID="txtUsuarios" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="valUsuario" runat="server" ErrorMessage="Informe o Usuário" SetFocusOnError="True" ControlToValidate="txtUsuarios" ForeColor="Red" Display="Dynamic" Font-Size="12px" ValidationGroup="vGroup"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <asp:TextBox placeholder="Senha" ID="txtSenha" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="valSenha" runat="server" ErrorMessage="Informe a Senha" SetFocusOnError="True" ControlToValidate="txtSenha" ForeColor="Red" Display="Dynamic" Font-Size="12px" ValidationGroup="vGroup"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:Button ID="btnLogin" class="btn btn-success" runat="server" Text="Login" ValidationGroup="vGroup" OnClick="btnLogin_Click" CausesValidation="true" />
                                </div>

                                <div class="col-sm-8">
                                    <button id="btnRenovarSenha" type="button" class="btn btn-primary" data-toggle="modal" data-target="#mailModal">Esqueceu a senha?</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

         <!-- Mail Modal -->
        <div id="mailModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Recuperar Senha</h4>
                            </div>
                            <div class="modal-body">
                                <p>Informe seu e-mail</p>
                                <div>
                                    <asp:TextBox placeholder="E-mail" ID="txtEmail" class="form-control" runat="server" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="valEmail" runat="server" ErrorMessage="Informe o E-mail" SetFocusOnError="True" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" Font-Size="12px" ValidationGroup="vEmailGroup"></asp:RequiredFieldValidator>
                                </div>
                                <div class="text-right">
                                   
                                </div>
                            </div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
                                <ProgressTemplate>
                                    <div class="loadingModal">
                                    </div>
                                    <div class="loadingMessage">
                                        <h2>AGUARDE</h2>
                                        <asp:Image ID="Image1" runat="server" BorderStyle="None"
                                            ImageUrl="~/images/loading.gif" Width="300px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <div class="modal-footer">
                                <asp:Button ID="btnEnviar" class="btn btn-success" runat="server" OnClick="btnEnviar_Click" Text="Enviar" ValidationGroup="vEmailGroup" />
                                <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <!-- RODAPE -->
        <footer class="rodape">
            <div class="navegacao">
            </div>
        </footer>

    </form>

    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="font-awesome/js/all.js"></script>

</body>
</html>
