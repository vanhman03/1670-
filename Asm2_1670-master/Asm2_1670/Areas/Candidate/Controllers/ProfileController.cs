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
    [Authorize(Roles = "Employer, Candidate")]
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        public ProfileController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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
			ViewBag.Email = user.Email;
			ViewBag.Name = user.Name;
			ViewBag.Phone = user.PhoneNumber;
            return user;
        }
        public IActionResult Profile(int? id, string? data)
        {
			TakeUser(TakeIdUser());
			ViewBag.JobId = id;
			ResumeVM ResumeVM = new ResumeVM()
			{
				ListAward = _unitOfWork.AwardsRepository.GetAll().Where(a => a.UserId == data).ToList(),
				ListEducation = _unitOfWork.EducationsRepository.GetAll().Where(e => e.UserId == data).ToList(),
				ListPortfolio = _unitOfWork.PortfoliosRepository.GetAll().Where(p => p.UserId == data).ToList(),
				ListWorkExp = _unitOfWork.WorkExperiencesRepository.GetAll().Where(w => w.UserId == data).ToList(),
				User = _unitOfWork.UsersRepository.Get(u => u.Id == data)
			};
			return View(ResumeVM);
		}
        public IActionResult Resume()
        {
            string UserId = TakeIdUser();
            ViewBag.Name = TakeUser(UserId).Name;
            ResumeVM ResumeVM = new ResumeVM()
            {
                ListAward = _unitOfWork.AwardsRepository.GetAll().Where(a => a.UserId == UserId).ToList(),
                ListEducation = _unitOfWork.EducationsRepository.GetAll().Where(e => e.UserId == UserId).ToList(),
                ListPortfolio = _unitOfWork.PortfoliosRepository.GetAll().Where(p => p.UserId == UserId).ToList(),
                ListWorkExp = _unitOfWork.WorkExperiencesRepository.GetAll().Where(w => w.UserId == UserId).ToList()
            };
            return View(ResumeVM);
        }
        public IActionResult AddEducation()
        {
            ViewBag.UserId = TakeIdUser();
			ViewBag.Name = TakeUser(TakeIdUser()).Name;
			return View();
        }
        [HttpPost]
        public IActionResult AddEducation(Education entity)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.EducationsRepository.Add(entity);
                _unitOfWork.Save();
            }
			return RedirectToAction("Resume");
		}
		public IActionResult AddWorkExperience()
        {
			ViewBag.UserId = TakeIdUser();
			ViewBag.Name = TakeUser(TakeIdUser()).Name;
			return View();
        }
		[HttpPost]
		public IActionResult AddWorkExperience(WorkExperience entity)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.WorkExperiencesRepository.Add(entity);
				_unitOfWork.Save();
			}
			return RedirectToAction("Resume");
		}
		public IActionResult AddPortfolio()
        {
			ViewBag.UserId = TakeIdUser();
			ViewBag.Name = TakeUser(TakeIdUser()).Name;
			return View();
        }
		[HttpPost]
		public IActionResult AddPortfolio(Portfolio entity, IFormFile? file)
		{
			string wwwrootPath = _webHostEnvironment.WebRootPath;
			if (file != null)
			{
				string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				string imagesPath = Path.Combine(wwwrootPath, @"images\User");
				if (!String.IsNullOrEmpty(entity.ImageUrl))
				{
					var oldImagePath = Path.Combine(wwwrootPath, entity.ImageUrl.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}
				using (var fileStream = new FileStream(Path.Combine(imagesPath, fileName), FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				entity.ImageUrl = @"\images\User\" + fileName;
			}
			if (ModelState.IsValid)
			{
				_unitOfWork.PortfoliosRepository.Add(entity);
				_unitOfWork.Save();
			}
			return RedirectToAction("Resume");
		}
		public IActionResult AddAward()
        {
			ViewBag.UserId = TakeIdUser();
			ViewBag.Name = TakeUser(TakeIdUser()).Name;
			return View();
        }
		[HttpPost]
		public IActionResult AddAward(Award entity)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.AwardsRepository.Add(entity);
				_unitOfWork.Save();
			}
			return RedirectToAction("Resume");
		}
	}
}
