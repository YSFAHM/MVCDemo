using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(MailMessage message)
        {

            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("yoyo1236744@gmail.com", "");
            message.From = new MailAddress("yoyo1236744@gmail.com", "CompanyMVC");
            client.Send(message);

        }
    }
}
