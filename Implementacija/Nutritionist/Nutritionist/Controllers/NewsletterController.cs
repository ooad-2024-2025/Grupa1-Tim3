using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nutritionist.Data;
using Nutritionist.Models;
using Nutritionist.ViewModels;

namespace Nutritionist.Controllers
{
    [Authorize]
    public class NewsletterController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public NewsletterController(
            UserManager<User> userManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new NewsletterViewModel
            {
                IsSubscribed = user.IsSubscribedToNewsletter
            };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle()
        {
            var user = await _userManager.GetUserAsync(User);
            user.IsSubscribedToNewsletter = !user.IsSubscribedToNewsletter;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
}
}
