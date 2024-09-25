using Microsoft.AspNetCore.Identity;

namespace Asm2_1670.Models
{
	public class User : IdentityUser
	{
		public string? JobTitle { get; set; }
		public string? SalaryOffer { get; set; }
		public string? Expecrience { get; set; }
		public string? Qualification { get; set; }
		public string? CareerLevel { get; set; }
		public string? Gender { get; set; }
		public string? Since { get; set; }
		public string? TeamSize { get; set; }
		public string Name{ get; set; }
		public string? Description { get; set; }
		public string? City { get; set; }
		public string? Country { get; set; }
		public string? ImageUrl { get; set; }
	}
}
