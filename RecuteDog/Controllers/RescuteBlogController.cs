using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;
using RecuteDog.Services;
using System.Security.Claims;

namespace RecuteDog.Controllers
{
    public class RescuteBlogController : Controller
    {
        private ServiceApiRescuteDog service;
        private ServiceBlobRescuteDog serviceblob;
        private string containerName;
        public RescuteBlogController(ServiceApiRescuteDog service, ServiceBlobRescuteDog serviceblob, IConfiguration configuration)
        {

            this.service = service;
            this.serviceblob = serviceblob;
            this.containerName =
                 configuration.GetValue<string>("BlobContainers:rescuteDogContainerName");

        }

        public async Task<IActionResult> _EditComentariosPartial(int idcomentario)
        {
            Comentario comentario = await this.service.FindComentarioAsync(idcomentario);
            return PartialView("_EditComentariosPartial", comentario);
        }
        [HttpPost]
        public async Task<IActionResult> _EditComentariosPartial(Comentario comentario)
        {
            string token =
             HttpContext.Session.GetString("token");
            await this.service.EditComentarioAsync(comentario, token);
            return RedirectToAction("Publicaciones");
        }

        public async Task<IActionResult> EliminarComentario(int idcomentario)
        {
            string token =
             HttpContext.Session.GetString("token");
            await this.service.DeleteComentarioAsync(idcomentario, token);
            return RedirectToAction("Publicaciones");
        }

        
        public async Task<IActionResult> CreateComentario(int idpost)
        {
            
                BlogModel publicacion = await this.service.FindPublicacionAsync(idpost);
            string blobname = publicacion.Imagen;
            publicacion.Imagen = blobname + await this.serviceblob.GetBlobUriAsync(this.containerName, blobname);
            return View(publicacion);
        }
        [HttpPost]
        public async Task<IActionResult> Publicaciones(int idpost, string correo, string comentario)
        {
                DateTime fechacomentario = DateTime.UtcNow;
                int iduser = int.Parse( this.HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            string token =
            HttpContext.Session.GetString("token");
            await this.service.NewComentarioAsync(idpost, correo, comentario, fechacomentario, iduser, token);
                return RedirectToAction("Publicaciones");
        }

        public async Task<IActionResult> DeletePublicacion(int idpost)
        {
            string token =
           HttpContext.Session.GetString("token");
            await this.service.DeletePublicacionAsync(idpost, token);
            return RedirectToAction("Publicaciones");
        }
        public async Task<IActionResult> Publicaciones()
        {
            List<BlogModel> publicaciones = await this.service.GetPublicacionesAsync();
            foreach (BlogModel publicacion in publicaciones)
            {
                string blobname = publicacion.Imagen;
                publicacion.Imagen = await this.serviceblob.GetBlobUriAsync(this.containerName, blobname);
            }
            List<Comentario> comentarios = await this.service.GetComentariosAsync();
            ViewData["COMENTARIOS"] = comentarios;
            return View(publicaciones);
        }
        
        public IActionResult NewPost()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewPost(BlogModel model, IFormFile Imagen)
        {
            string blobName = Imagen.FileName;
            if (await this.serviceblob.BlobExistsAsync(this.containerName, blobName) == false)
            {
                using (Stream stream = Imagen.OpenReadStream())
                {
                    await this.serviceblob.UploadBlobAsync(this.containerName, blobName, stream);
                }
            }
            model.Imagen = blobName;
            string token =
                HttpContext.Session.GetString("token");
            await this.service.NewBlogAsync(model, token);

            return RedirectToAction("Publicaciones");
        }
        public async Task<IActionResult> EditPublicaciones(int idpost)
        {
            BlogModel publicacion = await this.service.FindPublicacionAsync(idpost);
            return View(publicacion);
        }
        [HttpPost]
        public async Task<IActionResult> EditPublicaciones(BlogModel blog)
        {
            string token =
           HttpContext.Session.GetString("token");

            await this.service.EditBlogAsync(blog, token);
            return RedirectToAction("Publicaciones");
        }
        /**
         * Que falta lo de las imagenes!!
         * 
         */
    }
}
