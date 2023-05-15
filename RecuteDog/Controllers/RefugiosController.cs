using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecuteDog.Extensions;
using NugetRescuteDog.Models;
using RecuteDog.Services;

namespace RecuteDog.Controllers
{
    public class RefugiosController : Controller
    {
        private IMemoryCache memoryCache;
        private ServiceApiRescuteDog service;
        private ServiceBlobRescuteDog serviceBlob;
        private ServiceCatastro serviceCatastro ;
        private string containerName;
        public RefugiosController(IMemoryCache memoryCache, ServiceApiRescuteDog service, IConfiguration configuration, ServiceBlobRescuteDog serviceBlob, ServiceCatastro serviceCatastro)
        {
            
            this.memoryCache = memoryCache;
            this.service = service;
            this.serviceBlob = serviceBlob;
            this.serviceCatastro= serviceCatastro;
            this.containerName =
                 configuration.GetValue<string>("BlobContainers:rescuteDogContainerName");
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
            foreach (Refugio refugio in refugios)
            {
                string blobname = refugio.Imagen;
                refugio.Imagen = await this.serviceBlob.GetBlobUriAsync(this.containerName, blobname);
            }
            HttpContext.Session.SetObject("REFUGIOS", refugios);
            return View(refugios);
        }
        public async Task<IActionResult> AltaRefugios()
        {
            List<Provincia> provincias = await this.serviceCatastro.GetProvinciasAsync();
            ViewData["PROVINCIAS"] = provincias;
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> AltaRefugios(Refugio refugio, IFormFile Imagen)
        {
            string blobName = Imagen.FileName;
            if (await this.serviceBlob.BlobExistsAsync(this.containerName, blobName) == false)
            {
                using (Stream stream = Imagen.OpenReadStream())
                {
                    await this.serviceBlob.UploadBlobAsync(this.containerName, blobName, stream);
                }
            }
              
            refugio.Imagen = blobName;
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
            string blobName = Imagen.FileName;
            if(await this.serviceBlob.BlobExistsAsync(this.containerName, blobName) == false)
            {
                using (Stream stream = Imagen.OpenReadStream())
                {
                    await this.serviceBlob.UploadBlobAsync(this.containerName, blobName, stream);
                }
            }
          
            refugio.Imagen = blobName;
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
