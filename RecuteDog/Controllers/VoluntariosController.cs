using Microsoft.AspNetCore.Mvc;
using RecuteDog.Helpers;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class VoluntariosController : Controller
    {
        private IRepoVoluntarios repo;
        private HelperPathProvider helperPathProvider;
        public VoluntariosController(IRepoVoluntarios repo, HelperPathProvider helperPathProvider)
        {
            this.repo = repo;
            this.helperPathProvider = helperPathProvider;
            
        }

        public IActionResult Index()
        {
            List<Voluntario> voluntarios = this.repo.Getvoluntarios();
            return View(voluntarios);
        }

        public IActionResult FormVoluntarios()
        {
            List<Refugio> refugios = TempData["REFUGIOS"] as List<Refugio>;
            ViewData["REFUGIOSFORM"] = refugios;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FormVoluntarios(Voluntario voluntario, string refugio, IFormFile Imagen)
        {
            string filename = Imagen.FileName;
            string path = this.helperPathProvider.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await Imagen.CopyToAsync(stream);
            }
            string pathserver = "https://localhost:7057/images/" + Imagen.FileName;
            voluntario.Imagen = pathserver;
            await this.repo.NewVoluntario(voluntario, refugio);
            return RedirectToAction("Index");
        }

        public IActionResult ModificarVoluntarios(int idvoluntario)
        {
            Voluntario voluntario = this.repo.FindVoluntario(idvoluntario);
            return View(voluntario);
        }

        [HttpPost]
        public async Task<IActionResult> ModificarVoluntarios(Voluntario voluntario, string refugio)
        {
            await this.repo.ModificarDatosRefugio(voluntario, refugio);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BajaVolntuario(int idvolntuario)
        {
            await this.repo.BajaVoluntario(idvolntuario);
            return RedirectToAction("Index");
        }
    }
}
