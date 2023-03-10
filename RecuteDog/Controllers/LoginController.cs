using Microsoft.AspNetCore.Mvc;
using RecuteDog.Extensions;
using RecuteDog.Models;
using RecuteDog.Repositories;
using System.Net.Mail;

namespace RecuteDog.Controllers
{
    public class LoginController : Controller
    {
        private IRepoAutentication repo;
        public LoginController(IRepoAutentication repo)
        {
            this.repo = repo;
        }
        public IActionResult SingUp()
        {
            return View();
        }
        public IActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SingUp(string username, string password, string email, string phone, string imagen, string birdthday)
        {
            await this.repo.NewUser(username, password, email, phone, imagen, birdthday);

            return RedirectToAction("Index","Refugios");
        }
        [HttpPost]
        public IActionResult SingIn(string email, string password)
        {
            
            User user = this.repo.LogIn(email, password);
            if(user == null)
            {
                return View();
            }
            else
            {
                HttpContext.Session.SetObject("LOGSESSION", user);
                return RedirectToAction("Index","Refugios");
            }
        }
        public IActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}
