using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asm2_1670.Models
{
	public class Categories
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public string Proposer { get; set; }
		public string Status { get; set; }
	}
}
