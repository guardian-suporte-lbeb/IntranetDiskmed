using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntranetDiskmed.Intranet
{
    public class Interface
    {
        public enum Tipo { Informacao, Problema, Aviso }

        public static void FecharMensagemPortal(HtmlGenericControl modal, HtmlGenericControl background)
        {
            background.Attributes.Add("style", "display: none;");
            modal.Attributes.Add("style", "display: none;");
        }

        public static void MsgProblema(Label label, string mensagem = "")
        {
            label.Text = mensagem;
            label.ForeColor = ColorTranslator.FromHtml("#FF3300");
        }

        public static void ExibirModal(HtmlGenericControl modal, HtmlGenericControl background)
        {
            background.Attributes.Add("style", "display: block;");
            modal.Attributes.Add("style", "display: block;");
        }

        public static void FecharModal(HtmlGenericControl modal, HtmlGenericControl background)
        {
            background.Attributes.Add("style", "display: none;");
            modal.Attributes.Add("style", "display: none;");
        }

        public static void ExibirModalMensagem(Tipo tipoMensagem, HtmlGenericControl modal, HtmlGenericControl background, Label label, string mensagem = "")
        {
            // Definir a mensagem do modal
            label.Text = mensagem;

            if (String.IsNullOrWhiteSpace(mensagem))
                label.Text = "Não foi possível realizar a operação! Tente novamente ou entre em contato.";

            switch (tipoMensagem)
            {
                case Tipo.Informacao:
                    label.ForeColor = ColorTranslator.FromHtml("#027e02");
                    break;
                case Tipo.Problema:
                    label.ForeColor = ColorTranslator.FromHtml("#FF3300");
                    break;
                case Tipo.Aviso:
                    label.ForeColor = ColorTranslator.FromHtml("#ff8200");
                    break;
                default:
                    break;
            }

            background.Attributes.Add("style", "display: block;");
            modal.Attributes.Add("style", "display: block;");
        }

        public static void FecharModalMensagem(HtmlGenericControl modal, HtmlGenericControl background)
        {
            background.Attributes.Add("style", "display: none;");
            modal.Attributes.Add("style", "display: none;");
        }

        public static void ExibirMensagem(Tipo tipoMensagem, Label label, string mensagem = "")
        {
            label.Text = mensagem;

            switch (tipoMensagem)
            {
                case Tipo.Informacao:
                    label.ForeColor = ColorTranslator.FromHtml("#027e02");
                    break;
                case Tipo.Problema:
                    label.ForeColor = ColorTranslator.FromHtml("#FF3300");
                    break;
                case Tipo.Aviso:
                    label.ForeColor = ColorTranslator.FromHtml("#204d74");
                    break;
                default:
                    break;
            }
        }
    }
}