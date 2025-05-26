using Microsoft.AspNetCore.Mvc;

namespace Nutritionist.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
