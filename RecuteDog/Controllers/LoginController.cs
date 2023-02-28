using Microsoft.AspNetCore.Mvc;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class LoginController : Controller
    {
        private RepositoryAutentication repo;
        public LoginController(RepositoryAutentication repo)
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
        public async Task<IActionResult> SingUp(User user)
        {
            if (ModelState.IsValid)
            {
                var usercredentials = this.repo.FindUser(user);
                if (usercredentials == null)
                {
                    await this.repo.NewUser(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "El nombre de usuario ya está escogido");

                }
            }
            
            return View(user);
        }
        [HttpPost]
        public IActionResult SingIn(User user)
        {
            if(ModelState.IsValid)
            {
                var usercredentials = this.repo.FindUser(user);
               if(usercredentials != null && usercredentials.Password == user.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "El nombre o la contraseña son incorretcas, inténtelo de nuevo");

                }
            }
            return View(user);
            
        }
        public IActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}
