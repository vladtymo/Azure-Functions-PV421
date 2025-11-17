using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

namespace FuncAzEx1
{
    public class EmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        [Function("SendConfirmationEmail")]
        public IActionResult Run(
    [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            //var data = await req.ReadFromJsonAsync<EmailRequest>();

            //var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API_KEY"));
            //var msg = new SendGridMessage()
            //{
            //    From = new EmailAddress("noreply@yourapp.com", "Your App"),
            //    Subject = "Confirm your email",
            //    HtmlContent = $"<a href='{data.ConfirmationUrl}'>Click to confirm</a>"
            //};
            //msg.AddTo(data.Email);

            //await client.SendEmailAsync(msg);

            //var res = req.CreateResponse(HttpStatusCode.OK);
            //await res.WriteStringAsync("Email sent");
            //return res;

            return new OkObjectResult("Email sent (mock)");
        }

        public record EmailRequest(string Email, string ConfirmationUrl);

    }
}
