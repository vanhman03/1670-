using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2_1670.Models
{
	public class Application
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		[ValidateNever]
		public int JobId { get; set; }
		[ValidateNever]
		[ForeignKey("JobId")]
		public Job Job { get; set; }
		public string AppliedTime { get; set; }
		public string Status { get; set; }
	}
}
