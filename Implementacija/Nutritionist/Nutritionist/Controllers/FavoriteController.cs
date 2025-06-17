using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutritionist.Data;
using Nutritionist.Models;

namespace Nutritionist.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _users;

        public FavoriteController(ApplicationDbContext db, UserManager<User> users)
        {
            _db = db;
            _users = users;
        }

        public async Task<IActionResult> MyFavorites()
        {
            var user = await _users.GetUserAsync(User);
            if (user == null) return Challenge();

            // Fetch only the Recipes they favorited
            var recipes = await _db.Favorites
                .Where(f => f.UserId == user.Id)
                .Include(f => f.Recipe)
                .Select(f => f.Recipe)
                .ToListAsync();

            return View(recipes);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(Guid recipeId)
        {
            var user = await _users.GetUserAsync(User);
            if (user == null) return Challenge();

            var fav = await _db.Favorites
                 .FirstOrDefaultAsync(f => f.UserId == user.Id && f.RecipeId == recipeId);

            if (fav != null)
                _db.Favorites.Remove(fav);
            else
                _db.Favorites.Add(new Favorite
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RecipeId = recipeId,
                    AddedAt = DateTime.UtcNow
                });

            await _db.SaveChangesAsync();
            return RedirectToAction("Details", "Recipes", new { id = recipeId });
        }
    }
}
