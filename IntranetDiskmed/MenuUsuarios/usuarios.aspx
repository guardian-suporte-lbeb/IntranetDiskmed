<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="IntranetDiskmed.MenuUsuarios.usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Usuários</title>

    <!-- core CSS -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Navigation.css" rel="stylesheet" />
    <link href="../font-awesome/css/all.css" rel="stylesheet" />
    <link href="../Content/Interface-Style.css" rel="stylesheet" />
    <link href="../Content/header-style.css" rel="stylesheet" />
    <link href="../Content/footer-style.css" rel="stylesheet" />
    <link href="../Content/GridView-Style.css" rel="stylesheet" />
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

                <section class="container-fluid corpo">
                    <div class="padrao-grid">
                        <asp:GridView ID="gdvUsuarios" runat="server" class="table estilo-gridview table-hover" AlternatingRowStyle-BackColor="#E1EAFB" OnRowDataBound="gdvUsuarios_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" class="actionBtn btn-action" OnClick="btnEditar_Click" runat="server" ToolTip="Editar"><i  class="fas fa-edit" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnLog" class="actionBtn btn-action grid-icone-success" Style="padding-left: 5px;" OnClick="btnLog_Click" runat="server" ToolTip="Log"><i class="far fa-file-alt" aria-hidden="true"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnExcluir" class="actionBtn btn-action grid-icone-danger" Style="padding-left: 15px;" OnClick="btnExcluir_Click" runat="server" ToolTip="Excluir"><i class="far fa-trash-alt" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="table-header" Wrap="False" />
                            <RowStyle CssClass="table-item" Wrap="False" />
                        </asp:GridView>
                    </div>
                </section>

                <footer class="rodape add-fixed">
                    <div class="informacoes">
                        <asp:Button ID="btnIncluir" class="btn btn-warning btn-padrao" runat="server" Text="Incluir" OnClick="btnIncluir_Click" Width="100px" />
                    </div>
                    <div class="navegacao navegacao-sm">
                        <a id="btnHome" class="icone-rodape" title="Página Inicial" href="../index.aspx">
                            <i class="fa fa-home"></i>
                        </a>
                        <asp:LinkButton ID="btnSair" class="icone-rodape" ToolTip="Sair" OnClick="btnSair_Click" runat="server">
                           <i class="fas fa-sign-out-alt"></i>
                        </asp:LinkButton>
                    </div>
                </footer>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>

<script src="../Scripts/jquery-3.1.1.min.js"></script>
<script src="../Scripts/bootstrap.min.js"></script>
<script src="../font-awesome/js/all.js"></script>
<script src="../Scripts/Navigation.js"></script>

</html>
