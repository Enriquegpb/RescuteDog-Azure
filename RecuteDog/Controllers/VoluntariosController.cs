using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;
using RecuteDog.Services;

namespace RecuteDog.Controllers
{
    public class VoluntariosController : Controller
    {
        private ServiceApiRescuteDog service;
        private ServiceBlobRescuteDog serviceblob;
        private string containerName;
        private ServiceCatastro serviceCatastro;
        public VoluntariosController(ServiceApiRescuteDog service, ServiceBlobRescuteDog serviceBlob, IConfiguration configuration, ServiceCatastro serviceCatastro)
        {
            this.service = service;
            this.serviceblob = serviceBlob;
            this.containerName =
                 configuration.GetValue<string>("BlobContainers:rescuteDogContainerName");
            this.serviceCatastro = serviceCatastro;
        }

        public async Task<IActionResult> Index()
        {
            List<Voluntario> voluntarios = await this.service.GetVoluntariosAsync();
            foreach (Voluntario voluntario in voluntarios)
            {
                string blobname = voluntario.Imagen;
                voluntario.Imagen = await this.serviceblob.GetBlobUriAsync(this.containerName, blobname);
            }
            return View(voluntarios);
        }

        public async Task<IActionResult> FormVoluntarios()
        {
            List<Provincia> provincias = await this.serviceCatastro.GetProvinciasAsync();
            ViewData["PROVINCIAS"] = provincias;
            return View();
        }
        //public async Task<ActionResult> ObtenerMunicipiosProvincia(string provincia)
        //{
        //    List<string> municipios =
        //        await this.serviceCatastro.GetMunicipiosAsync(provincia);
        //    return Json(municipios, JsonRequestBehavior.AllowGet);

        //}
        [HttpPost]
        public async Task<IActionResult> FormVoluntarios(Voluntario voluntario, IFormFile Imagen)
        {
            string blobName = Imagen.FileName;
            if (await this.serviceblob.BlobExistsAsync(this.containerName, blobName) == false)
            {
                using (Stream stream = Imagen.OpenReadStream())
                {
                    await this.serviceblob.UploadBlobAsync(this.containerName, blobName, stream);
                }
            }

            voluntario.Imagen = blobName;
            string token =
             HttpContext.Session.GetString("token");
            await this.service.NewVoluntarioAsync(voluntario, token);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ModificarVoluntarios(int idvoluntario)
        {
            Voluntario voluntario = await this.service.FindVoluntarioAsync(idvoluntario);
            return View(voluntario);
        }

        [HttpPost]
        public async Task<IActionResult> ModificarVoluntarios(Voluntario voluntario, IFormFile Imagen)
        {
            string blobName = Imagen.FileName;
            if (await this.serviceblob.BlobExistsAsync(this.containerName, blobName) == false)
            {
                using (Stream stream = Imagen.OpenReadStream())
                {
                    await this.serviceblob.UploadBlobAsync(this.containerName, blobName, stream);
                }
            }

            voluntario.Imagen = blobName;
            string token =
             HttpContext.Session.GetString("token");
            await this.service.UpdateVoluntarioAsync(voluntario, token);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BajaVolntuario(int idvoluntario)
        {
            string token =
             HttpContext.Session.GetString("token");
            await this.service.DeleteVoluntarioAsync(idvoluntario, token);
            return RedirectToAction("Index");
        }
    }
}
