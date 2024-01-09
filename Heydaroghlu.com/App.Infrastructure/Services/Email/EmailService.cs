using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace App.Infrastructure.Services.Email
{
	public interface IEmailService
	{
		void Send(string to, string subject, string html);
	}

	public class EmailService : IEmailService
	{
		public void Send(string to, string subject, string html)
		{
			// create message
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse("qaychiaz@gmail.com"));
			email.To.Add(MailboxAddress.Parse(to));
			email.Subject = subject;
			email.Body = new TextPart(TextFormat.Html) { Text = html };
			ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				return true;
			};

			// send email
			using var smtp = new SmtpClient();
			smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
			smtp.Authenticate("qaychiaz@gmail.com", "yqtk pdfo vogt offw");
			smtp.Send(email);
			smtp.Disconnect(true);
		}
	}
}
