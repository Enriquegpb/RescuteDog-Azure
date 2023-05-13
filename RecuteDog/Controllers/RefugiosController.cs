using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecuteDog.Extensions;
using RecuteDog.Helpers;
using NugetRescuteDog.Models;
using RecuteDog.Repositories;
using RecuteDog.Services;

namespace RecuteDog.Controllers
{
    public class RefugiosController : Controller
    {
        private HelperPathProvider helperPathProvider;
        private IMemoryCache memoryCache;
        private ServiceApiRescuteDog service;
        public RefugiosController(HelperPathProvider helperPathProvider, IMemoryCache memoryCache, ServiceApiRescuteDog service)
        {
            
            this.helperPathProvider = helperPathProvider;
            this.memoryCache = memoryCache;
            this.service = service;
        }

        public async Task<IActionResult> Index()
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
            refugios = await this.service.GetRefugiosAsync();
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
            string token =
                HttpContext.Session.GetString("token");
            await this.service.NewRefugioAsync(refugio, token);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ModificarRefugio(int idrefugio)
        {
            Refugio refugio =  await this.service.FindRefugioAsync(idrefugio);
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
            string token =
               HttpContext.Session.GetString("token");
            await this.service.UpdateRefugioAsync(refugio, token);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRefugio()
        {
            int idrefugio = int.Parse(TempData["REFUGIO"].ToString());
            string token =
              HttpContext.Session.GetString("token");
            await this.service.DeleteRefugiosAsync(idrefugio, token);
            return RedirectToAction("Index");
        }

    }
}
