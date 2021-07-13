using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Xml;

namespace IntranetDiskmed.Intranet
{
    public class Email
    {
        public string EmailValidacao { get; set; } = "";
        private string assunto;
        private string mensagem;
        private string emailDestinatario;
        public string EmailRemetente { get; set; }
        public List<string> EmailsDestinatario { get; set; } = new List<string>();
        public Attachment Anexo { get; set; }
        public List<Attachment> Anexos { get; set; }
        public bool Anexar { get; set; } = false;

        public Email() { }

        public Email(string assunto, string mensagem, string emailDestinatario)
        {
            this.Assunto = assunto;
            this.Mensagem = mensagem;
            this.EmailDestinatario = emailDestinatario;
        }

        public string Assunto
        {
            get { return this.assunto; }
            set { this.assunto = value.Trim(); }
        }

        public string Mensagem
        {
            get { return this.mensagem; }
            set { this.mensagem = value.Trim(); }
        }

        public string EmailDestinatario
        {
            get { return this.emailDestinatario; }
            set { this.emailDestinatario = value.ToLower().Trim(); }
        }

        public bool EnviarEmail()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + Configuracoes.ArquivoConfig);

            try
            {
                string host = xmlDoc.GetElementsByTagName("smtpConnection")[0]["host"].InnerText.Trim();
                string user = xmlDoc.GetElementsByTagName("smtpConnection")[0]["login"].InnerText.Trim();
                string password = xmlDoc.GetElementsByTagName("smtpConnection")[0]["password"].InnerText.Trim();
                string email = xmlDoc.GetElementsByTagName("smtpConnection")[0]["email"].InnerText.Trim();
                int port = Convert.ToInt32(xmlDoc.GetElementsByTagName("smtpConnection")[0]["port"].InnerText.Trim());
                bool ssl = false;

                if (xmlDoc.GetElementsByTagName("smtpConnection")[0]["ssl"].InnerText.Trim() == "S")
                    ssl = true;

                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.EnableSsl = ssl;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(user, password);

                    using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                    {
                        mail.From = new System.Net.Mail.MailAddress(email);
                        mail.To.Add(new System.Net.Mail.MailAddress(this.EmailDestinatario));
                        mail.Subject = this.Assunto;
                        mail.Body = this.Mensagem;
                        mail.IsBodyHtml = false;
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}