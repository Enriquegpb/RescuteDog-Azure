using Microsoft.AspNetCore.Mvc;
using RecuteDog.Helpers;
using NugetRescuteDog.Models;
using RecuteDog.Services;

namespace RecuteDog.Controllers
{
    public class VoluntariosController : Controller
    {
        private HelperPathProvider helperPathProvider;
        private ServiceApiRescuteDog service;
        public VoluntariosController(HelperPathProvider helperPathProvider, ServiceApiRescuteDog service)
        {
            this.helperPathProvider = helperPathProvider;
            this.service = service;
            
        }

        public async Task<IActionResult> Index()
        {
            List<Voluntario> voluntarios = await this.service.GetVoluntariosAsync();
            return View(voluntarios);
        }

        public IActionResult FormVoluntarios()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FormVoluntarios(Voluntario voluntario, IFormFile Imagen)
        {
            string filename = Imagen.FileName;
            string path = this.helperPathProvider.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await Imagen.CopyToAsync(stream);
            }
            //string pathserver = "https://localhost:7057/images/" + Imagen.FileName;
            voluntario.Imagen = filename;
            string token =
             HttpContext.Session.GetString("token");
            await this.service.NewVoluntarioAsync(voluntario, token);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ModificarVoluntarios(int idvoluntario)
        {
            Voluntario voluntario = await this.service.FindVoluntarioAsync(idvoluntario);
            return View(voluntario);
        }

        [HttpPost]
        public async Task<IActionResult> ModificarVoluntarios(Voluntario voluntario, IFormFile Imagen)
        {
            string filename = Imagen.FileName;
            string path = this.helperPathProvider.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await Imagen.CopyToAsync(stream);
            }
            //string pathserver = "https://localhost:7057/images/" + Imagen.FileName;
            voluntario.Imagen = filename;
            string token =
             HttpContext.Session.GetString("token");
            await this.service.UpdateVoluntarioAsync(voluntario, token);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BajaVolntuario(int idvoluntario)
        {
            string token =
             HttpContext.Session.GetString("token");
            await this.service.DeleteVoluntarioAsync(idvoluntario, token);
            return RedirectToAction("Index");
        }
    }
}
