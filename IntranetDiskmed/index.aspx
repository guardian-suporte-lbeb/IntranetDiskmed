<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="IntranetDiskmed.index" %>

<%@ OutputCache Location="None" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página Inicial</title>

    <!-- core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/all.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link href="Content/Interface-Style.css" rel="stylesheet" />
    <link href="Content/header-style.css" rel="stylesheet" />
    <link href="Content/footer-style.css" rel="stylesheet" />

</head>
<body>
    <form id="frmMenu" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                            <asp:Label ID="TxtPaginaAtual" Text="Página Inicial" runat="server" />
                        </div>
                        <div class="col-sm-1 col-md-1 informacao-acesso">
                            <asp:Label ID="TxtUsuario" Text="" Font-Size="13px" runat="server" />
                        </div>
                    </div>
                </header>

                <!-- Imagem Loading -->
                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
                    <ProgressTemplate>
                        <asp:Panel ID="PanelBackgroundLoading" CssClass="background-loading" runat="server">
                        </asp:Panel>
                        <asp:Panel ID="PanelMensagemLoading" CssClass="panel-loading" runat="server">
                            <i class="fas fa-spinner fa-spin"></i>
                            <asp:Label ID="LblInfoLoading" Text=" Carregando..." runat="server"></asp:Label>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <section class="container-fluid corpo">

                    <div class="container">
                        <div class="row">

                            <div class="col-md-2 col-sm-2">

                            </div>

                            <div id="menuEmpenho" runat="server" class="col-md-5 col-sm-5">
                                <div class="feature-wrap">
                                    <a href="MenuComercial/menu-comercial.aspx">
                                        <i class="fas fa-people-carry"></i>
                                        <h2>Empenho</h2>
                                    </a>
                                </div>
                            </div>

                            <div id="menuCusto" runat="server" class="col-md-5 col-sm-5">
                                <div class="feature-wrap">
                                    <a href="MenuFaturamento/menu-faturamento.aspx">
                                        <i class="fas fa-money-check-alt"></i>
                                        <h2>Custos</h2>
                                    </a>
                                </div>
                            </div>

                            <div class="col-md-2 col-sm-2">

                            </div>

                            <div id="menuAnvisa" runat="server" class="col-md-5 col-sm-5">
                                <div class="feature-wrap">
                                    <a href="MenuRH/menu-rh.aspx">
                                        <i class="fas fa-file-signature"></i>
                                        <h2>Anvisa</h2>
                                    </a>
                                </div>
                            </div>

                            <div id="menuAcessos" runat="server" class="col-md-5 col-sm-5">
                                <div class="feature-wrap">
                                    <a href="MenuUsuarios/usuarios.aspx">
                                        <i class="fas fa-user"></i>
                                        <h2>Usuários</h2>
                                    </a>
                                </div>
                            </div>

                        </div>
                    </div>
                </section>

                <!-- RODAPE -->
                <footer class="rodape">
                    <div class="navegacao">
                        <asp:LinkButton ID="LinkButton1" class="icone-rodape" ToolTip="Sair" OnClick="btnSair_Click" runat="server">
                            <i class="fas fa-sign-out-alt"></i>
                        </asp:LinkButton>
                    </div>
                </footer>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="font-awesome/js/all.js"></script>

</body>
</html>
