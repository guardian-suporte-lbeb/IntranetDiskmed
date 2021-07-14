function ExibirModalMensagemPortal() {
    document.getElementById("ModalMensagem").style.display = "block";
}

function FecharModalMensagemPortal()
{
    document.getElementById("ModalMensagem").style.display = "none";
    document.getElementById("BackgroundModal").style.display = "none";
}

function FecharModal(modal, background)
{
    document.getElementById(modal).style.display = "none";
    document.getElementById(background).style.display = "none";
}