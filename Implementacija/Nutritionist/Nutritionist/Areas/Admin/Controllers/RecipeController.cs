using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nutritionist.Data;
using Nutritionist.Models;
using Nutritionist.ViewModels;

namespace Nutritionist.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly BlobContainerClient _blobContainer;

        public RecipesController(ApplicationDbContext context, UserManager<User> userManager, BlobContainerClient blobContainer)
        {
            _context = context;
            _userManager = userManager;
            _blobContainer = blobContainer;
        }

        // GET: Recipe
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recipes.Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipe/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeCreateViewModel vm)
        {

            if (!ModelState.IsValid)
                return View(vm);

            // 1) get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Title = vm.Title,
                Description = vm.Description,
                Type = vm.Type,
                TimeToMake = vm.TimeToMake,
                Ingredients = vm.Ingredients,
                UserId = user.Id   
            };

            if (vm.ImageFile != null)
            {
                var ct = vm.ImageFile.ContentType.ToLowerInvariant();
                if (!new[] { "image/jpeg", "image/png", "image/gif" }.Contains(ct))
                {
                    ModelState.AddModelError("ImageFile", "Samo JPEG/PNG/GIF su dozvoljeni.");
                    return View(vm);
                }

                if (vm.ImageFile.Length > 20 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "Maksimalna veličina je 20 MB.");
                    return View(vm);
                }

                // upload
                var ext = Path.GetExtension(vm.ImageFile.FileName);
                var blobName = $"{recipe.Id}{ext}";
                var blob = _blobContainer.GetBlobClient(blobName);
                await blob.UploadAsync(vm.ImageFile.OpenReadStream(), overwrite: true);
                recipe.ImageUrl = blob.Uri.ToString();
            }

            _context.Add(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Recipe/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", recipe.UserId);
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,CreatedAt,Type,TimeToMake,Ingredients,UserId")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", recipe.UserId);
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(Guid id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
