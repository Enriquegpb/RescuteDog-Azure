using Microsoft.AspNetCore.Mvc;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class RefugiosController : Controller
    {
        private IRepoRefugios repo;
        public RefugiosController(IRepoRefugios repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Refugio> refugios = this.repo.GetRefugios();
            return View(refugios);
        }
        public IActionResult AltaRefugios()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AltaRefugios(Refugio refugio)
        {
            this.repo.AgregarRefugio(refugio);
            return RedirectToAction("Index");
        }
        public IActionResult ModificarRefugio(int idrefugio)
        {
            Refugio refugio = this.repo.DetailsRefugio(idrefugio);
            return View(refugio);
        }
        [HttpPost]
        public IActionResult ModificarRefugio(Refugio refugio)
        {
            this.repo.ModificarDatosRefugio(refugio);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteRefugios(int idrefugio)
        {
            this.repo.BajaRefugio(idrefugio);
            return RedirectToAction("Index");
        }
    }
}
