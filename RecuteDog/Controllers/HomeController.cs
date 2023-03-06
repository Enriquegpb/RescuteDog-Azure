using Microsoft.AspNetCore.Mvc;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class HomeController : Controller
    {
        private IRepoAnimales repo;
        private IRepoAdopciones repoAdopciones;
        public HomeController(IRepoAnimales repo, IRepoAdopciones repoAdopciones)
        {
            this.repo = repo;
            this.repoAdopciones = repoAdopciones;
        }

        public IActionResult Index(int idrefugio)
        {
            List<Mascota> mascotas = this.repo.GetMascotas(idrefugio);
            return View(mascotas);
        }
        public IActionResult FormularioAdopcion(int idmascota)
        {
            Mascota mascota = this.repo.DetailsMascota(idmascota);
            return View(mascota);
        }
        [HttpPost]
        public IActionResult FormularioAdopcion(int idmascota, int iduser)
        {
            iduser = 2;
            this.repoAdopciones.NuevaAdopcion(idmascota, iduser);
            return RedirectToAction("Index", "Refugios");
        }

        public IActionResult InformeAdopcion()
        {
            List<Mascota> mascotasinforme = this.repo.GenerarInformeAdopciones();
            return View(mascotasinforme);
        }
        [HttpPost]
        public IActionResult InformeAdopcion(int idmascota)
        {
            this.repoAdopciones.DevolverAnimalAlRefugio(idmascota);
            return RedirectToAction("Index","Refugios");
        }
       
    }
}
