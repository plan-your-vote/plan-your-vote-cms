using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlanYourVoteLibrary2;

namespace Web.Controllers
{
    [EnableCors("EmailPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IOptions<EmailConfiguration> emailConfiguration;

        public EmailController(IOptions<EmailConfiguration> emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Email email)
        {
            bool success = SendEmail(email.EmailAddress, email.Subject, email.Message);

            if (success)
            {
                return Ok();
            }

            return BadRequest("Failed to send email");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool SendEmail(string emailAddress, string subject, string message)
        {
            MailMessage mm = new MailMessage
            {
                From = new MailAddress("test@gmail.com", emailConfiguration.Value.NetworkCredName),
                Subject = subject,
                Body = message,
                IsBodyHtml = false
            };

            mm.To.Add(new MailAddress(emailAddress));
            
            NetworkCredential networkCred = new NetworkCredential
            {
                UserName = emailConfiguration.Value.NetworkCredUserName,
                Password = emailConfiguration.Value.NetworkCredPassword,
            };

            SmtpClient smtp = new SmtpClient
            {
                Host = emailConfiguration.Value.SmtpHost,
                EnableSsl = emailConfiguration.Value.SmtpEnableSSL,
                UseDefaultCredentials = emailConfiguration.Value.SmptUseDefaultCredentials,
                Credentials = networkCred,
                Port = emailConfiguration.Value.SmtpPort
            };

            try
            {
                smtp.Send(mm);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}