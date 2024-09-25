using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm2_1670.Models.ViewModels
{
	public class JobVM
	{
		public Job Job { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> Categories { get; set; }
	}
}
