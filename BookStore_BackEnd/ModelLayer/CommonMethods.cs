using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using Experimental.System.Messaging;

namespace ModelLayer
{
    public class CommonMethods
    {
        public static string Key = "ad@@54asdef@srgx";

        public static string ConvertToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string ConvertToDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }

        MessageQueue messageQueue = new MessageQueue();
        public void sendDatatoQueue(string Token)
        {
            messageQueue.Path = @".\private$\Token";
            if (!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(Token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString();
            string url = $"Fundoo Notes Reset Password: <a href=http://localhost:4200/reset/{Token}> Click Here</a>";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("nantha.test123@gmail.com");
            mail.To.Add("nantha.test123@gmail.com");
            mail.Subject = "subject";

            mail.IsBodyHtml = true;
            string htmlBody;
            mail.Subject = "FundooNote Reset Link";
            mail.Body = "<body><p>Dear Nantha,<br><br>" +
                "We have sent you a link for resetting your password.<br>" +
                "Please copy it and paste in your swagger authorization.</body>" + url;

           

            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("nantha.test123@gmail.com", "jgjhhtrjactpqxsw"),
                EnableSsl = true,
            };
            SMTP.Send(mail);
            messageQueue.BeginReceive();
        }

    }
}
