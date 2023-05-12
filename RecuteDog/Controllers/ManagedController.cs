using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecuteDog.Helpers;
using NugetRescuteDog.Models;
using RecuteDog.Repositories;
using System.Security.Claims;
using RecuteDog.Extensions;
using RecuteDog.Filters;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;
using System;
using System.Security.Principal;
using RecuteDog.Services;

namespace RecuteDog.Controllers
{
    public class ManagedController : Controller
    {
        private IRepoAutentication repo;
        private HelperPathProvider helper;
        private ServiceApiRescuteDog service;

        public ManagedController(IRepoAutentication repo, HelperPathProvider helper, ServiceApiRescuteDog service)
        {
            this.repo = repo;
            this.helper = helper;
            this.service = service;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string email
            , string password)
        {
            string token =
               await this.service.GetTokenAsync(email, password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }
            else
            {
                User usuario = await this.repo.ExisteEmpleado(email, password);
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
                identity.AddClaim
                    (new Claim("PHONE", usuario.Phone.ToString()));
                identity.AddClaim
                    (new Claim("USERNAME", usuario.Username.ToString()));
                identity.AddClaim
                    (new Claim("USERIMAGE", usuario.Imagen.ToString()));
                identity.AddClaim
                    (new Claim("BIRTHDAY", usuario.Birdthday.ToString()));

                if (usuario.Id == 1)
                {
                    identity.AddClaim(new Claim("ADMIN", "Soy el Admin"));
                }

                ClaimsPrincipal user = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , user);

                //string controller = TempData["controller"].ToString();
                //string action = TempData["action"].ToString();
                //string id = TempData["id"].ToString();
                return RedirectToAction("Index", "Refugios");

                //return RedirectToAction("DeleteEnfermo", "Doctores", new { id = 45678});
            }
            return View();
            //await this.repo.ExisteUsuario(username, int.Parse(password));

            /**
             * 
             * Falta este metodo
             */

        }

        public IActionResult SingUp()
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
            //string pathserver = "https://localhost:7057/images/" + imagen.FileName;

            //ViewData["mensaje"] = "Fichero subido a" + path;
            await this.repo.NewUser(username, password, email, phone, filename, birdthday);
            return RedirectToAction("Index", "Refugios");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Refugios");
        }
        [AuthorizeUsuarios]
        public IActionResult PerfilUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> PerfilUsuario(string username, string telefono, string email, IFormFile imagen, int iduser)
        {
            string filename = imagen.FileName;
            string path = this.helper.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }
            //string pathserver = "https://localhost:7057/images/" + imagen.FileName;
            await this.repo.UpdatePerfilusuario(username, telefono, email, filename, iduser);
            var currentPrincipal = HttpContext.User as ClaimsPrincipal;
            if(currentPrincipal == null)
            {
                return RedirectToAction("LogIn");
            }
            var identity = (ClaimsIdentity)currentPrincipal.Identity;
            if (identity == null)
            {
                return RedirectToAction("LogIn");
            }
            var claimnameidentifier = currentPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            var claimname = currentPrincipal.FindFirst(ClaimTypes.Name);
            var claimphone = currentPrincipal.Claims.FirstOrDefault(c => c.Type == "PHONE");
            var claimusername = currentPrincipal.Claims.FirstOrDefault(c => c.Type == "USERNAME");
            var claimimagen = currentPrincipal.Claims.FirstOrDefault(c => c.Type == "USERIMAGE");

            identity.RemoveClaim(claimnameidentifier);
            identity.RemoveClaim(claimname);
            identity.RemoveClaim(claimphone);
            identity.RemoveClaim(claimusername);
            identity.RemoveClaim(claimimagen);

            identity.AddClaim
                 (new Claim("PHONE", telefono.ToString()));
            identity.AddClaim
                (new Claim("USERNAME", username.ToString()));
            identity.AddClaim
                (new Claim("USERIMAGE", filename.ToString()));
            identity.AddClaim
                    (new Claim(ClaimTypes.NameIdentifier, email));
            identity.AddClaim
                   (new Claim(ClaimTypes.Name, email));
            return View();
        }

        public async Task<IActionResult>BajaUsuario(int iduser)
        {
            await this.repo.BajaUsuario(iduser);
            return RedirectToAction("Index", "Refugios");
        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
