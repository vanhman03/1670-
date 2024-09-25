using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2_1670.Models
{
	public class Award
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		[ForeignKey("UserId")]
		public User? User { get; set; }
		public string Title { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string Description { get; set; }
	}
}
