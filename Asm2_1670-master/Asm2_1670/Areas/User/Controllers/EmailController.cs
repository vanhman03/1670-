using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace Asm2_1670.Areas.User.Controllers
{
	[Area("User")]
	public class EmailController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult SendEmail(string name, string from, string to, string phone, string content)
        {
            // Tạo đối tượng MailMessage để chứa thông tin về email
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = "Email From " + name;
            mail.Body = content + " Contact: " + phone;

            // Tạo đối tượng SmtpClient để gửi email
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from, "h@i1912003"),
                EnableSsl = true
            };

            try
            {
                // Gửi email
                smtpClient.Send(mail);
                // Trả về thông báo thành công
                return RedirectToAction("Profile", "Profile", new { area = "Candidate" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi
                return Content("Error sending email: " + ex.Message);
            }
        }
    }

}
