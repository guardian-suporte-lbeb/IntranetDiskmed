<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlteracaoCustoProduto.aspx.cs" Inherits="IntranetDiskmed.MenuCustoProduto.AlteracaoCustoProduto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Intranet - Alteração de Custo de Produto</title>
    <!-- core CSS -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <link href="../font-awesome/css/all.css" rel="stylesheet" />
    <link href="../Content/Interface-style.css" rel="stylesheet" />
    <link href="../Content/header-style.css" rel="stylesheet" />
    <link href="../Content/footer-style.css" rel="stylesheet" />
    <link href="../Content/modal-aplicacao.css" rel="stylesheet" />
    <script src="../Scripts/grid-view.js"></script>

    <!--/ Font Awesome /-->
    <link href="https://use.fontawesome.com/releases/v5.3.1/css/all.css" rel="stylesheet" />

    <!--/ Bootstrap /-->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <!--/ Font Awesome /-->
    <script src="https://kit.fontawesome.com/e187d646a0.js" crossorigin="anonymous"></script>

    <!--/ Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet" />

    <script src="https://igorescobar.github.io/jQuery-Mask-Plugin/js/jquery.mask.min.js"></script>

    <style>
        .tr, td {
            padding: 2px !important;
            padding-bottom: 4px !important;
            padding-top: 3px !important;
        }

        .espacoTop {
            margin-top: 7px !important;
            background-color: #438217 !important;
            text-align: center !important;
            color: white !important;
            padding-top: 1px !important;
            font-size: 15px !important;
            padding-bottom: 1px !important;
            margin-bottom: 7px;
            vertical-align: central !important;
            /*font-weight: bold !important;*/
        }

        .textoBarra {
            font-size: 15px;
            font-weight: bold;
            margin-top: 10px !important;
        }
    </style>
</head>
<body>
    <form id="formAlteracaoCusto" runat="server">
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
                            <asp:Label ID="TxtPaginaAtual" Text="Alteração de Custo de Produto" runat="server" />
                        </div>
                        <div class="col-sm-1 col-md-1 informacao-acesso">
                            <asp:Label ID="TxtUsuario" Text="" Font-Size="13px" runat="server" />
                        </div>
                    </div>
                </header>

                <!-- IMAGEM LOADING -->
                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
                    <ProgressTemplate>
                        <asp:Panel ID="PanelBackgroundLoading" CssClass="background-loading" runat="server">
                        </asp:Panel>
                        <asp:Panel ID="PanelMensagemLoading" CssClass="panel-loading" runat="server">
                            <i class="fas fa-spinner fa-spin"></i>
                            <asp:Label ID="LblInfoLoading" Text="Carregando..." runat="server"></asp:Label>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <div class="body-dados corpo">

                    <!-- Filtros de Consulta -->
                    <div style="display: flex; align-items: center;" class="row form-group">
                        <div class="col-md-2 col-sm-2">
                            <label>Data De:</label>
                            <asp:TextBox ID="TxtDataDe" CssClass="form-control input-sm" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="TxtDataDe_CalendarExtender" runat="server" Enabled="True"
                                PopupButtonID="TxtDataDe" TargetControlID="TxtDataDe" TodaysDateFormat="d MMMM, yyyy"
                                Format="dd/MM/yyyy" ClearTime="True"></asp:CalendarExtender>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <label>Data Até:</label>
                            <asp:TextBox ID="TxtDataAte" CssClass="form-control input-sm" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="TxtDataAte_CalendarExtender" runat="server" Enabled="True"
                                PopupButtonID="TxtDataAte" TargetControlID="TxtDataAte" TodaysDateFormat="d MMMM, yyyy"
                                Format="dd/MM/yyyy" ClearTime="True"></asp:CalendarExtender>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <label>Nome Usuário:</label>
                            <asp:TextBox ID="TxtNomeUsuario" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-6">
                            <label>Descrição do Produto:</label>
                            <asp:TextBox ID="TxtDescriProd" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <label>Origem:</label>
                            <asp:DropDownList ID="DdlOrigem" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <label>Marca:</label>
                            <asp:DropDownList ID="DdlMarca" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Todos" Value="Todos"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="margin-top: 2.5rem;" class="col-md-2 col-sm-2">
                            <asp:LinkButton ID="LbtnBuscarCustoProduto" CssClass="btn btn-sm btn-primary" OnClick="LbtnBuscarCustoProduto_Click" Style="float: right;" runat="server">
                                    Consultar <i class="fas fa-search"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:Panel ID="TituloGrid1" CssClass="espacoTop col-md-12 col-sm-12" Visible="false" runat="server">
                        <h1 class="textoBarra">ALTERAÇÃO DE CUSTO DE PRODUTO</h1>
                    </asp:Panel>
                    <div style="width: 100%; overflow-x: auto; height: 41rem;">
                        <asp:GridView ID="GdvCustoProduto" runat="server" PageSize="15" class="table" SelectedRowStyle-BorderStyle="Solid"
                            AlternatingRowStyle-BorderWidth="1px" Font-Size="small" CellPadding="15" AutoGenerateColumns="True" AllowSorting="true" OnSorting="GdvCustoProduto_Sorting">
                            <HeaderStyle CssClass="table-header" Wrap="true" BackColor="#438217" ForeColor="White" Font-Size="11px" />
                            <RowStyle CssClass="table-item" Wrap="False" Font-Size="11px" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>

                    </div>
                </div>

                <!-- RODAPE -->
                <footer class="rodape">
                    <div class="text-right" style="margin-top: 5px;">
                        <a id="btnHome" class="icone-rodape" title="Página Inicial" href="../index.aspx">
                            <i class="fa fa-home"></i>
                        </a>
                        <asp:LinkButton ID="btnSair" class="icone-rodape" ToolTip="Sair" OnClick="btnSair_Click" runat="server">
                           <i class="fas fa-sign-out-alt"></i>
                        </asp:LinkButton>
                    </div>
                </footer>

                <!----------------------------------------------------------- MODAIS -----------------------------------------------------------!>

                <!-- MODAL DE MENSAGEM -->
                <div id="ModalMensagem" class="modal-aplicacao modal-mensagem" style="display: none;" runat="server">
                    <!-- Corpo Modal -->
                    <div class="modal-conteudo">
                        <div class="row form-group">
                            <div class="col-sm-12 col-md-12 text-center">
                                <asp:Label ID="LblDescricaoModalMensagem" Font-Size="Medium" Font-Bold="true" runat="server" Text="aaaaaaa aaa aaa aaa "></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 col-md-12 text-center">
                                <br />
                                <a id="BtnFecharModalMensagem" onclick="FecharModalMensagemPortal()" class="btn btn-success">
                                    <i class="fas fa-check"></i>OK
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- BACKGROUND MODAL DE MENSAGEM PORTAL -->
                <div id="BackgroundModal" class="modal-mensagem-background" style="display: none;" runat="server"></div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
    <script src="../Scripts/jquery-1.12.4.js"></script>
    <script src="../Scripts/maskedinput.js"></script>
    <script src="../Scripts/mask.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../font-awesome/js/all.js"></script>
    <script src="../Scripts/Navigation.js"></script>
    <script src="../Scripts/modal-aplicacao.js"></script>
</body>
</html>
