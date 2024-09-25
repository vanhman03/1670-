using Asm2_1670.Models;
using Asm2_1670.Models.ViewModels;
using Asm2_1670.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Asm2_1670.Areas.Candidate.Controllers
{
	[Area("Candidate")]
	[Authorize(Roles = "Candidate")]
	public class ApplyController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUnitOfWork _unitOfWork;
		public ApplyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
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
			string userId = TakeIdUser();
			List<Application> listApply = _unitOfWork.ApplicationsRepository.GetAll().Where(Application => Application.UserId == userId).ToList();
			foreach(var obj in listApply)
			{
				var job = _unitOfWork.JobsRepository.Get(j => j.Id == obj.JobId);
				obj.Job = job;
			}
			return View(listApply);
		}
		public IActionResult ApplyJob(int? id, string? data)
		{
			string currentTime = DateTime.Now.ToShortDateString();
			Application application = new Application
			{
				JobId = (int)id,
				UserId = data,
				AppliedTime = currentTime,
				Status = "Processing"
			};
			_unitOfWork.ApplicationsRepository.Add(application);
			_unitOfWork.Save();
			
			return RedirectToAction("Index", "Job", new { area = "User" });
		}
		public IActionResult Delete(int? id)
		{
			if(id == null || id == 0)
			{
				return NotFound();
			}
			Application? apply = _unitOfWork.ApplicationsRepository.Get(a => a.Id == id);
			_unitOfWork.ApplicationsRepository.Delete(apply);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
	}
}
