using Asm2_1670.Models;
using Asm2_1670.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Asm2_1670.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class AccountController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<Asm2_1670.Models.User> _userManager;
		public AccountController(IUnitOfWork unitOfWork, UserManager<Asm2_1670.Models.User> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}
		public string TakeIdUser()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			string userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			return userId;
		}
		public Asm2_1670.Models.User TakeUser(string userId)
		{
			Asm2_1670.Models.User? user = _unitOfWork.UsersRepository.Get(u => u.Id == userId);

			ViewBag.UserId = userId;
			ViewBag.Email = user.Email;
			ViewBag.Name = user.Name;
			return user;
		}
		public IActionResult Index()
		{
			List<Asm2_1670.Models.User>? myList = _unitOfWork.UsersRepository.GetAll().Where(u => u.Id != TakeIdUser()).ToList();
			return View(myList);
		}
		public IActionResult Delete(string? data)
		{
			Asm2_1670.Models.User user = _unitOfWork.UsersRepository.Get(u => u.Id == data);
			_unitOfWork.UsersRepository.Delete(user);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
		public IActionResult ResetPass(string? data)
		{
			return View(TakeUser(data));
		}
		[HttpPost]
		public async Task<IActionResult> ResetPass(string id, string pass)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

			var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, pass);

			if (resetResult.Succeeded)
			{
				return RedirectToAction("Index");
			}

			foreach (var error in resetResult.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return BadRequest(ModelState);
		}
	}
}
