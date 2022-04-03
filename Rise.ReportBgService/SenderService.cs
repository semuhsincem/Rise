using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Rise.ReportBgService
{
    public class SenderService
    {
        public static void SendMail(string filePath,string email)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;

            sc.Credentials = new NetworkCredential("riseasses@gmail.com", "123123Aa");

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("riseasses@gmail.com", "Muhsin Cem Kütükcü");

            mail.To.Add(email);
             

            mail.Subject = "Report"; mail.IsBodyHtml = true; mail.Body = "Rise Report Content";

            mail.Attachments.Add(new Attachment(filePath));

            sc.Send(mail);
        }
    }
}
