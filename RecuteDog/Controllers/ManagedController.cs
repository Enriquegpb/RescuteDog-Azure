using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RecuteDog.Helpers;
using RecuteDog.Models;
using RecuteDog.Repositories;
using System.Security.Claims;

namespace MvcSeguridadDoctores.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryAutentication repo;
        private HelperPathProvider helper;

        public ManagedController(RepositoryAutentication repo, HelperPathProvider helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string username
            , string password)
        {
            User usuario = null;
            //await this.repo.ExisteUsuario(username, int.Parse(password));

            /**
             * 
             * Falta este metodo
             */

            if (usuario != null)
            {
                ClaimsIdentity identity =
               new ClaimsIdentity
               (CookieAuthenticationDefaults.AuthenticationScheme
               , ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim
                    (new Claim(ClaimTypes.Name, usuario.Email));
                identity.AddClaim
                    (new Claim(ClaimTypes.NameIdentifier, usuario.Email));
                identity.AddClaim
                    (new Claim(ClaimTypes.Role, usuario.Id.ToString()));
                if(usuario.Id == 1)
                {
                    identity.AddClaim(new Claim("Administrador", "Soy el Admin"));
                }

                ClaimsPrincipal user = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , user);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString(); 
                string id = TempData["id"].ToString();
                return RedirectToAction(action, controller, new {id = id});
                
                //return RedirectToAction("DeleteEnfermo", "Doctores", new { id = 45678});
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public IActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SingUp(string username, string password, string email, string phone, IFormFile imagen, string birdthday)
        {
            string filename = imagen.FileName;
            string path = this.helper.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }
            string pathserver = "https://localhost:7057/images/" + imagen.FileName;

            //ViewData["mensaje"] = "Fichero subido a" + path;
            await this.repo.NewUser(username, password, email, phone, pathserver, birdthday);
            return RedirectToAction("Index", "Refugios");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Enfermos", "Doctores");
        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
