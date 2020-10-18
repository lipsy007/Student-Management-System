using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Ishant_Goyal.services
{
    public interface IEmailServices
    {
        void SendEmail(string to, string body, string subject);
    }
    public class Emailservices : IEmailServices
    {
        private readonly EmailConfig _email;

        public Emailservices(EmailConfig email)
        {
            _email = email;
        }


        public void SendEmail(string to, string body, string subject)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(to));
                message.From = new MailAddress(_email.From);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = _email.Port;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.Username, _email.Password);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
        }


    }
    public class EmailConfig
        {
            public string From { get; set; }
            public string SmtpServer { get; set; }
            public int Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
  

}
