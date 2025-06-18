using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutritionist.Data;
using Nutritionist.Models;
using Nutritionist.ViewModels;

namespace Nutritionist.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewslettersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _users;
        private readonly IEmailSender _emails;

        public NewslettersController(
            ApplicationDbContext db,
            UserManager<User> users,
            IEmailSender emails)
        {
            _db = db;
            _users = users;
            _emails = emails;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _db.Newsletters
                .Include(n => n.User)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            return View(list);
        }

        // GET: /Admin/Newsletters/Create
        public IActionResult Create()
        {
            return View(new NewsletterCreateViewModel());
        }

        // POST: /Admin/Newsletters/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsletterCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // map VM → entity
            var user = await _users.GetUserAsync(User);
            var newsletter = new Newsletter
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
                Title = vm.Title,
                Text = vm.Text
            };

            _db.Newsletters.Add(newsletter);
            await _db.SaveChangesAsync();

            var subscribers = await _db.Users
                .Where(u => u.IsSubscribedToNewsletter)
                .Select(u => u.Email)
                .ToListAsync();

            foreach (var email in subscribers)
                await _emails.SendEmailAsync(email, vm.Title, vm.Text);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var newsletter = await _db.Newsletters
                .Include(n => n.User)            
                .FirstOrDefaultAsync(n => n.Id == id);

            if (newsletter == null)
                return NotFound();

            return View(newsletter);
        }
    }
}
