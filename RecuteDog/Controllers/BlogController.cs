using Microsoft.AspNetCore.Mvc;

namespace RecuteDog.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
