<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DadosAnvisa.aspx.cs" Inherits="IntranetDiskmed.MenuAnvisa.DadosAnvisa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Intranet - Dados Anvisa </title>
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

        th {
            text-align: center;
            vertical-align: middle;
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
    <form id="formAnvisa" runat="server">
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
                            <asp:Label ID="TxtPaginaAtual" Text="Dados Anvisa" runat="server" />
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
                        <div class="col-md-6 col-sm-6">
                            <label>Texto Livre:</label>
                            <asp:TextBox ID="TxtTextoLike" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <label>Valor De:</label>
                            <asp:TextBox ID="TxtValorDe" CssClass="form-control input-sm" runat="server" placeholder="0" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-2">
                            <label>Valor até:</label>
                            <asp:TextBox ID="TxtValorAte" CssClass="form-control input-sm" runat="server" placeholder="999" TextMode="Number"></asp:TextBox>
                        </div>
                        <div style="margin-top: 2.5rem;" class="col-md-2 col-sm-2">
                            <asp:LinkButton ID="LbtnConsultarDadosAnvisa" CssClass="btn btn-sm btn-primary" OnClick="LbtnConsultarDadosAnvisa_Click" Style="float: right;" runat="server">
                                    Consultar <i class="fas fa-search"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:Panel ID="TituloGrid1" CssClass="espacoTop col-md-12 col-sm-12" Visible="false" runat="server">
                        <h1 class="textoBarra">DADOS ANVISA</h1>
                    </asp:Panel>
                    <div style="width: 100%; overflow-x: auto; height: 41rem;">
                        <asp:GridView ID="GdvAnvisa" runat="server" PageSize="15" class="table" SelectedRowStyle-BorderStyle="Solid"
                            AlternatingRowStyle-BorderWidth="1px" Font-Size="small" CellPadding="15" AutoGenerateColumns="False" AllowSorting="true" OnSorting="GdvAnvisa_Sorting">
                            <Columns>
                                <asp:BoundField DataField="Filial" HeaderText="Filial ↓↑" SortExpression="Filial" />
                                <asp:BoundField DataField="Substancia" HeaderText="SUBSTÂNCIA ↓↑" SortExpression="Substancia" />
                                <asp:BoundField DataField="Substancia2" HeaderText="SUBSTÂNCIA LINHA 2 ↓↑" SortExpression="Substancia2" />
                                <asp:BoundField DataField="CNPJ" HeaderText="CNPJ ↓↑" SortExpression="CNPJ" />
                                <asp:BoundField DataField="Laboratorio" HeaderText="LABORATÓRIO ↓↑" SortExpression="Laboratorio" />
                                <asp:BoundField DataField="CodGgrem" HeaderText="CÓDIGO GGREM ↓↑" SortExpression="CodGgrem" />
                                <asp:BoundField DataField="Registro" HeaderText="REGISTRO ↓↑" SortExpression="Registro" />
                                <asp:BoundField DataField="Ean1" HeaderText="EAN 1 ↓↑" SortExpression="Ean1" />
                                <asp:BoundField DataField="Ean2" HeaderText="EAN 2 ↓↑" SortExpression="Ean2" />
                                <asp:BoundField DataField="Ean3" HeaderText="EAN 3 ↓↑" SortExpression="Ean3" />
                                <asp:BoundField DataField="Produto" HeaderText="PRODUTO ↓↑" SortExpression="Produto" />
                                <asp:BoundField DataField="Apresentacao" HeaderText="APRESENTAÇÃO ↓↑" SortExpression="Apresentacao" />
                                <asp:BoundField DataField="ClasseTerapeutica" HeaderText="CLASSE TERAPÊUTICA ↓↑" SortExpression="ClasseTerapeutica" />
                                <asp:BoundField DataField="TipoProduto" HeaderText="TIPO DE PRODUTO (STATUS DO PRODUTO) ↓↑" SortExpression="TipoProduto" />
                                <asp:BoundField DataField="RegimePreco" HeaderText="REGIME DE PRECO ↓↑" SortExpression="RegimePreco" />
                                <asp:BoundField DataField="PFSemImpostos" HeaderText="PF SEM IMPOSTOS ↓↑" SortExpression="PFSemImpostos" />
                                <asp:BoundField DataField="PF0" HeaderText="PF 0% ↓↑" SortExpression="PF0" />
                                <asp:BoundField DataField="PF12" HeaderText="PF 12% ↓↑" SortExpression="PF12" />
                                <asp:BoundField DataField="PF17" HeaderText="PF 17% ↓↑" SortExpression="PF17" />
                                <asp:BoundField DataField="PF17AL" HeaderText="PF 17% ALC ↓↑" SortExpression="PF17AL" />
                                <asp:BoundField DataField="PF175" HeaderText="PF 17,5% ↓↑" SortExpression="PF175" />
                                <asp:BoundField DataField="PF175AL" HeaderText="PF 17,5% ALC ↓↑" SortExpression="PF175AL" />
                                <asp:BoundField DataField="PF18" HeaderText="PF 18% ↓↑" SortExpression="PF18" />
                                <asp:BoundField DataField="PF18AL" HeaderText="PF 18% ALC ↓↑" SortExpression="PF18AL" />
                                <asp:BoundField DataField="PF20" HeaderText="PF 20% ↓↑" SortExpression="PF20" />
                                <asp:BoundField DataField="PMC0" HeaderText="PMC 0% ↓↑" SortExpression="PMC0" />
                                <asp:BoundField DataField="PMC12" HeaderText="PMC 12% ↓↑" SortExpression="PMC12" />
                                <asp:BoundField DataField="PMC17" HeaderText="PMC 17% ↓↑" SortExpression="PMC17" />
                                <asp:BoundField DataField="PMC17AL" HeaderText="PMC 17% ALC ↓↑" SortExpression="PMC17AL" />
                                <asp:BoundField DataField="PMC175" HeaderText="PMC 17,5% ↓↑" SortExpression="PMC175" />
                                <asp:BoundField DataField="PMC175AL" HeaderText="PMC 17,5% ALC ↓↑" SortExpression="PMC175AL" />
                                <asp:BoundField DataField="PMC18" HeaderText="PMC 18% ↓↑" SortExpression="PMC18" />
                                <asp:BoundField DataField="PMC18AL" HeaderText="PMC 18% ALC ↓↑" SortExpression="PMC18AL" />
                                <asp:BoundField DataField="PMC20" HeaderText="PMC 20% ↓↑" SortExpression="PMC20" />
                                <asp:BoundField DataField="RestHosp" HeaderText="RESTRIÇÃO HOSPITALAR ↓↑" SortExpression="RestHosp" />
                                <asp:BoundField DataField="Cap" HeaderText="CAP ↓↑" SortExpression="Cap" />
                                <asp:BoundField DataField="Confaz87" HeaderText="CONFAZ 87 ↓↑" SortExpression="Confaz87" />
                                <asp:BoundField DataField="ICMS0" HeaderText="ICMS 0% ↓↑" SortExpression="ICMS0" />
                                <asp:BoundField DataField="AnaliseRec" HeaderText="ANÁLISE RECURSAL ↓↑" SortExpression="AnaliseRec" />
                                <asp:BoundField DataField="Listpc" HeaderText="LISTA DE CONCESSÃO DE CRÉDITO TRIBUTÁRIO (PIS/COFINS) ↓↑" SortExpression="Listpc" />
                                <asp:BoundField DataField="Comerc19" HeaderText="COMERCIALIZAÇÃO 2019 ↓↑" SortExpression="Comerc19" />
                                <asp:BoundField DataField="Tarja" HeaderText="TARJA ↓↑" SortExpression="Tarja" />
                                <asp:BoundField DataField="Idproc" HeaderText="ID. PROCESSAMENTO ↓↑" SortExpression="Idproc" />
                            </Columns>
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
