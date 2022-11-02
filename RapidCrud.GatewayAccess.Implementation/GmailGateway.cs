using RapidCrud.GatewayAccess.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RapidCrud.GatewayAccess.Implementation
{
    public class GmailGateway : IGmailGateway
    {
        private void Send(string asunto, string mensaje)
        {
            try
            {
                var emailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                var emailPassword = ConfigurationManager.AppSettings["EmailFromPassword"].ToString();
                var emailVendedor = ConfigurationManager.AppSettings["EmailVendedor"].ToString();

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(emailFrom, emailPassword);
                    smtpClient.Port = 25;
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailVendedor);
                    mail.Subject = asunto;
                    mail.Body = mensaje;
                    mail.IsBodyHtml = true;

                    smtpClient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                //implemtar log
            }
        }

        public void SendMessage(string asunto, string mensaje)
        {
            Send(asunto, mensaje);
        }
    }
}
