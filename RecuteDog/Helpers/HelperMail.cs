using RecuteDog.Models;
using System.Net;
using System.Net.Mail;

namespace RecuteDog.Helpers
{
    public class HelperMail
    {
        private IConfiguration configuration;
        public HelperMail(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private MailMessage ConfigureMailMessage(string para, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();
            string email = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            mail.From = new MailAddress(email);
            mail.To.Add(new MailAddress(para));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            return mail;
        }

        private SmtpClient ConfigureSmtpClient()
        {
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string hostName = this.configuration.GetValue<string>("MailSettings:Smtp:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Smtp:Port");
            bool enableSsl = this.configuration.GetValue<bool>("MailSettings:Smtp:EnableSSL");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Smtp:DefaultCredentials");

            SmtpClient client = new SmtpClient();
            client.Host = hostName;
            client.Port = port;
            client.EnableSsl = enableSsl;
            client.UseDefaultCredentials = defaultCredentials;
            NetworkCredential credentials = new NetworkCredential(user, password);
            client.Credentials = credentials;
            return client;
        }

        /**
         * Metodos invocadores del mail
         */

        public async Task SendMailAsync(string para, string asunto, string mensaje)
        {
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            //Asi no se tiene que estar configurando todo el rato el mensaje
            //y permite tener un modelo de correo para los clientes del correo
            /**
             * Por ultimo se manda el correo
             */
            SmtpClient client = this.ConfigureSmtpClient();
            await client.SendMailAsync(mail);
        }
    }
}
