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
            List<Mascota> mascotas = this.repo.GetMascotas();
            return View(mascotas);
        }
    }
}
