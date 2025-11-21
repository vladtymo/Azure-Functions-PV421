using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FuncAzEx1
{
    public class EmailSender
    {
        private readonly IEmailSender emailSender;

        public EmailSender(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [Function("SendConfirmationEmail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var data = await req.ReadFromJsonAsync<SendEmailData>();

            string subject = "Please confirm your email";
            string content = $"Click the link to confirm your email: <a href='www.apple.com'>Confirm</a>";

            await emailSender.SendEmailAsync(data.Email, subject, content);

            return new OkObjectResult("Email was sent!");
        }
    }
}
