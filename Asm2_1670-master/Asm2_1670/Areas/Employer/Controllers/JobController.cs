using Asm2_1670.Models;
using Asm2_1670.Models.ViewModels;
using Asm2_1670.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Asm2_1670.Areas.Employer.Controllers
{
	[Area("Employer")]
	[Authorize(Roles = "Employer")]
	public class JobController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUnitOfWork _unitOfWork;
		public JobController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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
		public IActionResult Postnew()
		{
			string userId = TakeIdUser();
			TakeUser(userId);
			JobVM jobVM = new JobVM()
			{
				Categories = _unitOfWork.CategoriesRepository.GetAll().Where(c => c.Status == "Active").Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = c.Name,
					Value = c.Id.ToString(),
				}),
				Job = new Job()
			};
			return View(jobVM);
		}
		[HttpPost]
		public IActionResult Postnew(JobVM jobVM)
		{
			string userId = TakeIdUser();
			TakeUser(userId);
			if (jobVM != null)
			{
				jobVM.Job.Email = ViewBag.Email;
				jobVM.Job.Name = ViewBag.Name;
				_unitOfWork.JobsRepository.Add(jobVM.Job);
				_unitOfWork.Save();
				return RedirectToAction("ManagerJob");
			}
			jobVM.Categories = _unitOfWork.CategoriesRepository.GetAll().Where(c => c.Status == "Active").Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = c.Name,
				Value = c.Id.ToString(),
			});
			return View(jobVM);
		}
		public IActionResult ManagerJob()
		{
			int count;
			string userId = TakeIdUser();
			TakeUser(userId);
			if (userId == null)
			{
				return NotFound();
			}
			List<Job> mylist = _unitOfWork.JobsRepository.GetAll().Where(j => j.UserId == userId).ToList();
			foreach(var job in mylist)
			{
				count = _unitOfWork.ApplicationsRepository.GetAll().Count(a => a.JobId == job.Id && a.Status == "Processing");
				job.Count = count;
			}
			return View(mylist);
		}
		public IActionResult EditJob(int? id)
		{
			string userId = TakeIdUser();
			TakeUser(userId);
			JobVM jobVM = new JobVM()
			{
				Categories = _unitOfWork.CategoriesRepository.GetAll().Where(c => c.Status == "Active").Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = c.Name,
					Value = c.Id.ToString(),
				}),
				Job = _unitOfWork.JobsRepository.Get(j => j.Id == id)
			};
			jobVM.Categories = _unitOfWork.CategoriesRepository.GetAll().Where(c => c.Status == "Active").Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = c.Name,
				Value = c.Id.ToString(),
			});
			return View(jobVM);
		}
		[HttpPost]
		public IActionResult EditJob(JobVM jobVM)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.JobsRepository.Update(jobVM.Job);
				_unitOfWork.Save();
				return RedirectToAction("ManagerJob");
			}
			jobVM.Categories = _unitOfWork.CategoriesRepository.GetAll().Where(c => c.Status == "Active").Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = c.Name,
				Value = c.Id.ToString(),
			});
			return View(jobVM);
		}
		public IActionResult DeleteJob(int? id)
		{
			Job? job = _unitOfWork.JobsRepository.Get(j => j.Id == id, "Categories");
			return View(job);
		}
		[HttpPost]
		public IActionResult DeleteJob(Job job)
		{
			_unitOfWork.JobsRepository.Delete(job);
			_unitOfWork.Save();
			return RedirectToAction("ManagerJob");
		}
	}
}
