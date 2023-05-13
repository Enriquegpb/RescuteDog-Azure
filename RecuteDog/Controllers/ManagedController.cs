using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;
using System.Security.Claims;
using RecuteDog.Filters;
using RecuteDog.Services;

namespace RecuteDog.Controllers
{
    public class ManagedController : Controller
    {
        
        private ServiceApiRescuteDog service;
        private ServiceBlobRescuteDog serviceblob;
        private string containerName;

        public ManagedController(ServiceApiRescuteDog service, ServiceBlobRescuteDog serviceBlob, IConfiguration configuration)
        {
            this.service = service;
            this.containerName = configuration.GetValue<string>("BlobContainers:rescuteDogContainerName");
            this.serviceblob = serviceBlob;
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
                HttpContext.Session.SetString("token", token);
                User usuario = await this.service.GetPerfilUsuarioAsync(token);
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
                    (new Claim("USERIMAGE", await this.serviceblob.GetBlobUriAsync(this.containerName, usuario.Imagen)));
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
        public async Task<IActionResult> SingUp(string username, string password, string email, string phone, IFormFile Imagen, string birdthday)
        {
            string blobName = Imagen.FileName;
            if (await this.serviceblob.BlobExistsAsync(this.containerName, blobName) == false)
            {
                using (Stream stream = Imagen.OpenReadStream())
                {
                    await this.serviceblob.UploadBlobAsync(this.containerName, blobName, stream);
                }
            }

            await this.service.NewUsuarioAsync(username, password, email, phone, blobName, birdthday);
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
        public async Task <IActionResult> PerfilUsuario(string username, string telefono, string email, IFormFile Imagen, int iduser)
        {
            string token =
               HttpContext.Session.GetString("token");
            string blobName = Imagen.FileName;
            if (await this.serviceblob.BlobExistsAsync(this.containerName, blobName) == false)
            {
                using (Stream stream = Imagen.OpenReadStream())
                {
                    await this.serviceblob.UploadBlobAsync(this.containerName, blobName, stream);
                }
            }

            await this.service.UpdateUsuarioAsync(username, telefono, email, blobName, iduser, token);
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
                    (new Claim("USERIMAGE", await this.serviceblob.GetBlobUriAsync(this.containerName, blobName)));
            identity.AddClaim
                    (new Claim(ClaimTypes.NameIdentifier, email));
            identity.AddClaim
                   (new Claim(ClaimTypes.Name, email));
            return View();
        }

        public async Task<IActionResult>BajaUsuario(int iduser)
        {
            string token =
             HttpContext.Session.GetString("token");
            await this.service.BajaUsuarioAsync(iduser, token);
            return RedirectToAction("LogOut", "Managed");
        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
