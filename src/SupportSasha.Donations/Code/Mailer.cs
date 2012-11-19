using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Management;

namespace SupportSasha.Donations.Code
{
    public class Mailer
    {
        public void SendThankyouEmail(string to)
        {
            try
            {
                MailMessage message = new MailMessage("donations@supportsasha.com", to);
                message.Subject = "Thank you for your donation";
                message.IsBodyHtml = true;
                message.Body = GetEmailBody();

                SendMessage(message);
            }
            catch (Exception ex)
            {
                new LogEvent(ex).Raise();
            }

        }

        private static void SendMessage(MailMessage message)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(message);
            }
        }
        public void SendNotificationEmail(string name, decimal amount)
        {
            try
            {
                MailMessage message = new MailMessage("donations@supportsasha.com", "marto83@gmail.com");
                message.Subject = String.Format("New donation from {0} for {1}", name, amount);
                message.IsBodyHtml = false;
                message.Body = "Yay there is a new donation... http://donations.supportsasha.com";

                SendMessage(message);
            }
            catch (Exception ex)
            {
                new LogEvent(ex).Raise();
            }
        }

        private string GetEmailBody()
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(Settings.Website.ThankyouEmailUrl);
            }
        }
    }

    public class LogEvent : WebRequestErrorEvent
    {
        public LogEvent(string message)
            : base(null, null, 100001, new Exception(message))
        {
        }

        public LogEvent(Exception ex)
            : base(null, null, 100001, ex)
        {
        }
    }
}