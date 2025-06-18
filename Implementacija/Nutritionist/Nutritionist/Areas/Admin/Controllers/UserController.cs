using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutritionist.Data;
using Nutritionist.Models;
using Nutritionist.ViewModels;

namespace Nutritionist.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Gender = vm.Gender,
                Birthday = vm.Birthday,
                Email = vm.Email,
                UserName = vm.UserName,
                NormalizedEmail = vm.Email.ToUpperInvariant(),
                NormalizedUserName = vm.UserName.ToUpperInvariant(),
                PhoneNumber = vm.PhoneNumber,
                IsSubscribedToNewsletter = vm.IsSubscribedToNewsletter,
                CreatedAt = DateTime.UtcNow
            };

            // 1) create the user (you can also pass a default password here)
            var result = await _userManager.CreateAsync(user, "Password123!");
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    ModelState.AddModelError("", e.Description);
                return View(vm);
            }

            // 2) assign them the Korisnik role
            await _userManager.AddToRoleAsync(user, "Korisnik");

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Users/Edit/{id}
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Users.FindAsync(id.Value);
            if (user == null) return NotFound();

            var vm = new UserEditViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Gender = user.Gender,
                Birthday = user.Birthday,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsSubscribedToNewsletter = user.IsSubscribedToNewsletter
            };
            return View(vm);
        }

        // POST: Admin/Users/Edit/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _context.Users.FindAsync(vm.Id);
            if (user == null) return NotFound();

            // map only the editable fields back
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.Gender = vm.Gender;
            user.Birthday = vm.Birthday;
            user.Email = vm.Email;
            user.UserName = vm.UserName;        // or keep email==username
            user.NormalizedEmail = vm.Email.ToUpperInvariant();
            user.NormalizedUserName = vm.Email.ToUpperInvariant();
            user.PhoneNumber = vm.PhoneNumber;
            user.IsSubscribedToNewsletter = vm.IsSubscribedToNewsletter;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
