// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Asm2_1670.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asm2_1670.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Asm2_1670.Models.User> _userManager;
        private readonly SignInManager<Asm2_1670.Models.User> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<Asm2_1670.Models.User> userManager,
            SignInManager<Asm2_1670.Models.User> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public string JobTitle { get; set; }
            public string SalaryOffer { get; set; }
            public string Expecrience { get; set; }
            public string Qualification { get; set; }
            public string CareerLevel { get; set; }
            public string Gender { get; set; }
            public string Since { get; set; }
            public string TeamSize { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string ImageUrl { get; set; }
        }
        
        private async Task LoadAsync(Asm2_1670.Models.User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                JobTitle = user.JobTitle,
                SalaryOffer = user.SalaryOffer,
                Expecrience = user.Expecrience,
                Qualification = user.Qualification,
                CareerLevel = user.CareerLevel,
                Gender = user.Gender,
                TeamSize = user.TeamSize,
                Name = user.Name,
                Description = user.Description,
                City = user.City,
                Country = user.Country,
                ImageUrl = user.ImageUrl
			};
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

			if (Input.JobTitle != user.JobTitle)
			{
				user.JobTitle = Input.JobTitle;
			}
			if (Input.SalaryOffer != user.SalaryOffer)
			{
				user.SalaryOffer = Input.SalaryOffer;
			}
			if (Input.Expecrience != user.Expecrience)
			{
				user.Expecrience = Input.Expecrience;
			}
			if (Input.Qualification != user.Qualification)
			{
				user.Qualification = Input.Qualification;
			}
			if (Input.CareerLevel != user.CareerLevel)
			{
				user.CareerLevel = Input.CareerLevel;
			}
			if (Input.Gender != user.Gender)
			{
				user.Gender = Input.Gender;
			}
			if (Input.TeamSize != user.TeamSize)
			{
				user.TeamSize = Input.TeamSize;
			}
			if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }
			if (Input.Description != user.Description)
			{
				user.Description = Input.Description;
			}
			if (Input.City != user.City)
			{
				user.City = Input.City;
			}
			if (Input.Country != user.Country)
			{
				user.Country = Input.Country;
			}
			if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string imagesPath = Path.Combine(wwwrootPath, @"images\User");
                if (!String.IsNullOrEmpty(user.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwrootPath, user.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(imagesPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                user.ImageUrl = @"\images\User\" + fileName;
            }
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
