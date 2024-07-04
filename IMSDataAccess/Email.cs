using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IMSDataAccess
{
    public class Email : IEmail
    {
        public async Task<object> Emailmet(string ToEmail, string subject, string msg)
        {
            var fromAddress = new System.Net.Mail.MailAddress("***", "***");
            const string fromPassword = "***";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            return smtp.SendMailAsync(new MailMessage(from: fromAddress.Address, to: ToEmail, subject, msg));
        }
    }
}
