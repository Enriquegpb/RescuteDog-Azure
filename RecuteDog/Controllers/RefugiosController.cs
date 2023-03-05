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
    }
}
