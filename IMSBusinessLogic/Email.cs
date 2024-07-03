using System.Net;
using System.Net.Mail;

namespace IMSBusinessLogic
{
    public class Email:IEmail
    {
        public async Task<object> SendLowStockEmail(string ToEmail, string subject, string msg)
        {
            var fromAddress = new System.Net.Mail.MailAddress("aaa@gmail.com", "aaa");
            const string fromPassword = "*******";

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
        public async Task<object> SendReportEmail(string ToEmail, string subject, string msg)
        {
            var fromAddress = new System.Net.Mail.MailAddress("aaa@gmail.com", "aaa");
            const string fromPassword = "*****";

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
