using Microsoft.AspNetCore.Mvc;
using RecuteDog.Models;

namespace RecuteDog.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult SingUp()
        {
            return View();
        }
        public IActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SingUp(User user)
        {
            
            return RedirectToAction("Home", "Index");
        }
        [HttpPost]
        public IActionResult SingIn(User user)
        {
            return RedirectToAction("Home", "Index");
        }

    }
}
