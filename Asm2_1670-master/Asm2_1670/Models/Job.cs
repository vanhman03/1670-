using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2_1670.Models
{
	public class Job
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		[ForeignKey("UserId")]
		public User? User { get; set; }
		public string JobTitle { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string JobType { get; set; }
		[ValidateNever]
		public int CategoriesId { get; set; }
		[ForeignKey("CategoriesId")]
		[ValidateNever]
		public Categories Categories { get; set; }
		public string OfferdSalary { get; set; }
		public string CareerLevel { get; set; }
		public string Experience { get; set; }
		public string Gender { get; set; }
		public string EndDay { get; set; }
		public string Qualification { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public int? Count { get; set; }
	}
}
