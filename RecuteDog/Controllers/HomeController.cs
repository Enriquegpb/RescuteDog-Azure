using Microsoft.AspNetCore.Mvc;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class HomeController : Controller
    {
        private RepositoryRefugioAnimales repo;
        public HomeController(RepositoryRefugioAnimales repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            //List<Mascota> mascotas = this.repo.GetMascotas();
            List<Mascota> mascotas= new List<Mascota>();
            Mascota mascota = new Mascota();
            Mascota mascota2 = new Mascota();
            Mascota mascota3 = new Mascota();
            Mascota mascota4 = new Mascota();
            mascotas.Add(mascota);
            mascotas.Add(mascota2);
            mascotas.Add(mascota3);
            mascotas.Add(mascota4);
            return View(mascotas);
        }
    }
}
