using Microsoft.AspNetCore.Mvc;
using RecuteDog.Helpers;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class RefugiosController : Controller
    {
        private IRepoRefugios repo;
        private HelperPathProvider helperPathProvider;
        public RefugiosController(IRepoRefugios repo, HelperPathProvider helperPathProvider)
        {
            this.repo = repo;
            this.helperPathProvider = helperPathProvider;
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
        public async Task <IActionResult> AltaRefugios(Refugio refugio, IFormFile Imagen)
        {
            string filename = Imagen.FileName;
            string path = this.helperPathProvider.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using(Stream stream = new FileStream(path, FileMode.Create))
            {
                await Imagen.CopyToAsync(stream);
            }
            string pathserver = "https://localhost:7057/images/" + Imagen.FileName;
            refugio.Imagen = pathserver;
            await this.repo.AgregarRefugio(refugio);
            return RedirectToAction("Index");
        }
        public IActionResult ModificarRefugio(int idrefugio)
        {
            Refugio refugio = this.repo.DetailsRefugio(idrefugio);
            return View(refugio);
        }
        [HttpPost]
        public async Task<IActionResult> ModificarRefugio(Refugio refugio, IFormFile Imagen)
        {
            string filename = Imagen.FileName;
            string path = this.helperPathProvider.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await Imagen.CopyToAsync(stream);
            }
            string pathserver = "https://localhost:7057/images/" + Imagen.FileName;
            refugio.Imagen = pathserver;
            await this.repo.ModificarDatosRefugio(refugio);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRefugios(int idrefugio)
        {
            await this.repo.BajaRefugio(idrefugio);
            return RedirectToAction("Index");
        }

    }
}
