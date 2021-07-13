<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="IntranetDiskmed.MenuUsuarios.Log" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log</title>

    <!-- core CSS -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Navigation.css" rel="stylesheet" />
    <link href="../font-awesome/css/all.css" rel="stylesheet" />
    <link href="../Content/Interface-Style.css" rel="stylesheet" />
    <link href="../Content/header-style.css" rel="stylesheet" />
    <link href="../Content/footer-style.css" rel="stylesheet" />
</head>
<body>
    <form id="frmDadosCliente" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                    <img alt="" src="../images/logo-termica-sm.png" />
                </div>
                <div class="col-sm-2 col-md-2 informacao-sessao">
                    <asp:Label ID="TxtPaginaAtual" Text="Usuários" runat="server" />
                </div>
                <div class="col-sm-1 col-md-1 informacao-acesso">
                    <asp:Label ID="TxtUsuario" Text="" Font-Size="13px" runat="server" />
                </div>
            </div>
        </header>

        <div class="container-fluid corpo">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="col-md-6 col-sm-6">
                        <h3>LOG</h3>
                    </div>
                    <div class="col-md-6 col-sm-6 text-right">
                        <asp:Button ID="btnMTodos" class="btn btn-padrao btn-danger" runat="server" Text="Mostrar Todos os Registros" OnClick="btnMTodos_Click" />
                    </div>
                    <div class="content-dados limitSize content-grid">
                        <asp:GridView ID="gdvUserLog" runat="server" class="table table-hover estilo-gridview" AlternatingRowStyle-BackColor="#E1EAFB" OnRowDataBound="gdvUserLog_RowDataBound">

                            <HeaderStyle CssClass="table-header" Wrap="False" />
                            <RowStyle CssClass="table-item" Wrap="False" />
                        </asp:GridView>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="buttons">
            <div>
            </div>
        </div>

        <!-- RODAPE -->
        <footer class="rodape">
            <div class="informacoes">
                <asp:Button ID="btnVoltar" class="btn btn-warning btn-padrao" runat="server" Text="Voltar" OnClick="btnVoltar_Click" Width="100px" />
            </div>
            <div class="navegacao  navegacao-sm">
                <a class="icone-rodape" title="Página Inicial" href="../index.aspx">
                    <i class="fa fa-home"></i>
                </a>
                <asp:LinkButton ID="btnSair" class="icone-rodape" ToolTip="Sair" OnClick="btnSair_Click" runat="server">
                    <i class="fas fa-sign-out-alt"></i>
                </asp:LinkButton>
            </div>
        </footer>

    </form>

    <script src="../Scripts/jquery-3.1.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../font-awesome/js/all.js"></script>
    <script src="../Scripts/Navigation.js"></script>
</body>
</html>
