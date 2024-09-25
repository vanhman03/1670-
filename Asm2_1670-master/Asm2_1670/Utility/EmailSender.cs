using Microsoft.AspNetCore.Identity.UI.Services;
namespace Asm2_1670.Utility
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			//Add email sending logic
			return Task.CompletedTask;
		}
	}
}
