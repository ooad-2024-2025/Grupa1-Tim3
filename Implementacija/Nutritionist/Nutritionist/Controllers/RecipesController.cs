using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutritionist.Data;
using Nutritionist.Models;
using Nutritionist.ViewModels;

namespace Nutritionist.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RecipesController(ApplicationDbContext context)
            => _context = context;

        [HttpGet]
        public async Task<IActionResult> Index(RecipeType? type)
        {
            // Base query
            var query = _context.Recipes
                           .Include(r => r.User)
                           .AsQueryable();

            // Optional filter
            if (type.HasValue)
                query = query.Where(r => r.Type == type.Value);

            // Build VM
            var vm = new RecipeListViewModel
            {
                Recipes = await query.ToListAsync(),
                RecipeTypes = Enum.GetValues<RecipeType>().Cast<RecipeType>(),
                SelectedType = type
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Favorites)
                .Include(r => r.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null) return NotFound();

            return View(recipe);
        }
    }
}
