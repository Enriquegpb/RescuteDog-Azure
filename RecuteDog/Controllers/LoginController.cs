using Microsoft.AspNetCore.Mvc;
using RecuteDog.Extensions;
using RecuteDog.Helpers;
using RecuteDog.Models;
using RecuteDog.Repositories;
using System.Net.Mail;

namespace RecuteDog.Controllers
{
    public class LoginController : Controller
    {
        private IRepoAutentication repo;
        private HelperPathProvider helperPath;
        public LoginController(IRepoAutentication repo,HelperPathProvider helperPath)
        {
            this.repo = repo;
            this.helperPath = helperPath;
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
        public async Task<IActionResult> SingUp(string username, string password, string email, string phone, IFormFile imagen, string birdthday)
        {
            string filename = imagen.FileName;
            string path = this.helperPath.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using(Stream stream = new FileStream(path, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }
            string pathserver = "https://localhost:7057/images/" + imagen.FileName;
           
            //ViewData["mensaje"] = "Fichero subido a" + path;
             await this.repo.NewUser(username, password, email, phone, pathserver, birdthday);
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
