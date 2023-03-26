using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecuteDog.Extensions;
using RecuteDog.Helpers;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class RefugiosController : Controller
    {
        private IRepoRefugios repo;
        private HelperPathProvider helperPathProvider;
        private IMemoryCache memoryCache;
        public RefugiosController(IRepoRefugios repo, HelperPathProvider helperPathProvider, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.helperPathProvider = helperPathProvider;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            List<Refugio> refugios;
            if (this.memoryCache.Get("REFUGIOS") == null)
            {
                refugios = new List<Refugio>();
            }
            else
            {
                refugios = this.memoryCache.Get<List<Refugio>>("REFUGIOS");
            }
            refugios = this.repo.GetRefugios();
            HttpContext.Session.SetObject("REFUGIOS", refugios);
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
            //string pathserver = "https://localhost:7057/images/" + Imagen.FileName;
            refugio.Imagen = filename;
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
            //string pathserver = "https://localhost:7057/images/" + Imagen.FileName;
            refugio.Imagen = filename;
            await this.repo.ModificarDatosRefugio(refugio);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRefugio()
        {
            int idrefugio = int.Parse( TempData["REFUGIO"].ToString());
            await this.repo.BajaRefugio(idrefugio);
            return RedirectToAction("Index");
        }

    }
}
