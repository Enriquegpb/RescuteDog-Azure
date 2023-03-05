using Microsoft.AspNetCore.Mvc;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class VoluntariosController : Controller
    {
        private IRepoVoluntarios repo;
        public VoluntariosController(IRepoVoluntarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Voluntario> voluntarios = this.repo.Getvoluntarios();
            return View(voluntarios);
        }

        public IActionResult FormVoluntarios()
        {
            List<string> refugios = new List<string>();
            Refugio refugioname = new Refugio();
            refugioname.Nombre = "REFU1";
            Refugio refugioname2 = new Refugio();
            refugioname2.Nombre = "REFU2";
            Refugio refugioname3 = new Refugio();
            refugioname3.Nombre = "REFU3";
            Refugio refugioname4 = new Refugio();
            refugioname4.Nombre = "REFU4";
            refugios.Add(refugioname.Nombre);
            refugios.Add(refugioname2.Nombre);
            refugios.Add(refugioname3.Nombre);
            refugios.Add(refugioname4.Nombre);
            ViewData["REFUGIOS"] = refugios;
            return View();
        }
        [HttpPost]
        public IActionResult FormVoluntarios(Voluntario voluntario, string refugio)
        {
            this.repo.NewVoluntario(voluntario, refugio);
            return RedirectToAction("Index");
        }
    }
}
