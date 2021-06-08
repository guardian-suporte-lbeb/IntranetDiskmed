<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manutencao-acessos.aspx.cs" Inherits="IntranetDiskmed.MenuUsuarios.manuntencao_acessos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acessos</title>

    <!-- core CSS -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../font-awesome/css/all.css" rel="stylesheet" />
    <link href="../Content/Navigation.css" rel="stylesheet" />
    <link href="../Content/Interface-Style.css" rel="stylesheet" />
    <link href="../Content/header-style.css" rel="stylesheet" />
    <link href="../Content/footer-style.css" rel="stylesheet" />
</head>
<body>
    <form id="frmDadosCliente" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <header id="cabecalho" class="cabecalho-fixo">
                    <div class="cabecalho-borda centralizar-cabecalho">
                        <div class="cabecalho-img  col-sm-2 col-md-2">
                            <img src="../images/logo_lbeb.png" alt="logo" class="logo-lbeb" />
                        </div>
                        <div class="centralizar-cabecalho col-sm-2 col-md-2">
                            <!-- Logo Guardian Portal -->
                            <img alt="Logo Guardian" src="../images/logo_guardian.png" />
                        </div>
                        <div class="cabecalho-text col-sm-3 col-md-3">
                            <label class="negrito">Intranet</label>
                        </div>
                        <div class="centralizar-cabecalho col-sm-2 col-md-2">
                            <!-- Logo Cliente Guardian -->
                            <img alt="" src="../Images/logo-diskmed-sm.png" />
                        </div>
                        <div class="col-sm-2 col-md-2 informacao-sessao">
                            <asp:Label ID="TxtPaginaAtual" Text="Usuários" runat="server" />
                        </div>
                        <div class="col-sm-1 col-md-1 informacao-acesso">
                            <asp:Label ID="TxtUsuario" Text="" Font-Size="13px" runat="server" />
                        </div>
                    </div>
                </header>

                <!--/header-->
                <div class="container-fluid corpo">

                    <div class="col-md-12 col-sm-12">
                        <h3>
                            <asp:Label ID="LblTituloAcao" runat="server" Text="USUÁRIO"></asp:Label></h3>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:Label ID="lblNome" runat="server" Text="Nome:"></asp:Label>
                            <asp:TextBox ID="txtNome" class="form-control panel-primary" runat="server" ReadOnly="False" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valNome" runat="server" ErrorMessage="Informe o Nome" SetFocusOnError="True" ControlToValidate="txtNome" ForeColor="Red" Display="Dynamic" Font-Size="12px" ValidationGroup="vGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                            <asp:TextBox ID="txtEmail" class="form-control panel-primary" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o Email" SetFocusOnError="True" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" Font-Size="12px" ValidationGroup="vGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:Label ID="lblLogin" runat="server" Text="Login:"></asp:Label>
                            <asp:TextBox ID="txtLogin" class="form-control panel-primary" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Informe o Login" SetFocusOnError="True" ControlToValidate="txtLogin" ForeColor="Red" Display="Dynamic" Font-Size="12px" ValidationGroup="vGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <asp:Label ID="lblSenha" runat="server" Text="Senha:"></asp:Label>
                            <asp:TextBox ID="txtSenhaUsuario" class="form-control panel-primary" runat="server" MaxLength="15"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe a Senha" SetFocusOnError="True" ControlToValidate="txtSenhaUsuario" ForeColor="Red" Display="Dynamic" Font-Size="12px" ValidationGroup="vGroup"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-sm-12" style="float: left;">
                        <div class="form-group">
                            <asp:Label ID="lblTipo1" runat="server" Text="Tipo: "></asp:Label>
                            <div runat="server" class="panel panel-primary">
                                <div class="form-group">
                                    <asp:RadioButton ID="rdUsuario" Style="padding-left: 10px;" runat="server" Checked="True" GroupName="tipo" />
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuário"></asp:Label>
                                    <asp:RadioButton ID="rdAdm" runat="server" GroupName="tipo" />
                                    <asp:Label ID="lblAdm" runat="server" Text="Administrador"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12" style="float: left;">
                        <%-- Permissões de Cliente --%>
                        <div class="form-group">
                            <asp:Label ID="lblPermissoesCli" runat="server" Text="Permissões: "></asp:Label>
                            <div runat="server" class="panel panel-primary">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkEmpenho" Style="padding-left: 10px;" runat="server" />
                                    <asp:Label ID="lblConsultar" runat="server" Text="Empenhos"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox ID="chkCustos" Style="padding-left: 10px;" runat="server" />
                                    <asp:Label ID="lblLiberacoes" runat="server" Text="Custos"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox ID="chkAnvisa" Style="padding-left: 10px;" runat="server" />
                                    <asp:Label ID="lblEditarCliente" runat="server" Text="Anvisa"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="form-group">
                            <asp:CustomValidator ID="valPermissoes" runat="server" ErrorMessage="Informe pelo menos uma permissão de acesso" ForeColor="Red" Display="Dynamic" Font-Size="12px" ValidationGroup="vGroup" OnServerValidate="valPermissoes_ServerValidate"></asp:CustomValidator>
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
                </div>

                <div class="buttons">
                    <div>
                    </div>
                </div>

                <!-- RODAPE -->
                <footer class="rodape">
                    <div class="informacoes">
                        <asp:Button ID="btnConfirmar" class="btn btn-success  btn-padrao" type="submit" runat="server" ValidationGroup="vGroup" OnClick="btnConfirmar_Click" Text="Confirmar" Width="100px" />
                        <asp:Button ID="btnVoltar" class="btn btn-warning  btn-padrao" type="submit" runat="server" OnClick="btnVoltar_Click" Text="Voltar" Width="100px" />
                    </div>
                    <div class="navegacao navegacao-sm">
                        <a id="btnHome" class="icone-rodape" title="Página Inicial" href="../index.aspx">
                            <i class="fa fa-home"></i>
                        </a>
                        <asp:LinkButton ID="LbtnSair" class="icone-rodape " ToolTip="Sair" OnClick="LbtnSair_Click" runat="server">
                           <i class="fas fa-sign-out-alt"></i>
                        </asp:LinkButton>
                    </div>
                </footer>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script src="../Scripts/jquery-3.1.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../font-awesome/js/all.js"></script>
    <script src="../Scripts/Navigation.js"></script>

</body>
</html>
