using Microsoft.AspNetCore.Mvc;

namespace Nutritionist.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
