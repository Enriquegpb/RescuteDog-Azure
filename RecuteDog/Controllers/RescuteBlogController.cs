using Microsoft.AspNetCore.Mvc;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class RescuteBlogController : Controller
    {
        private IRepoBlog repoBlog;
        private IRepoComentarios repoComentarios;
        public RescuteBlogController(IRepoBlog repoBlog, IRepoComentarios repoComentarios)
        {
            this.repoBlog = repoBlog;
            this.repoComentarios = repoComentarios;
        }

        public IActionResult EditComentario(int idcomentario)
        {
            Comentario comentario = this.repoComentarios.FindComentario(idcomentario);
            return View(comentario);
        }

        public async Task<IActionResult> EditComentario(Comentario comentario)
        {
            await this.repoComentarios.EditComentario(comentario);
            return RedirectToAction("Publicaciones");
        }

        public async Task<IActionResult> EliminarComentario(int idcomentario)
        {
            await this.repoComentarios.DeleteComentario(idcomentario);
            return RedirectToAction("Publicaciones");
        }

        
        public IActionResult CreateComentario(int idpost)
        {
            
                BlogModel publicacion = this.repoBlog.FindPost(idpost);
                return View(publicacion);
        }
        [HttpPost]
        public async Task<IActionResult> Publicaciones(Comentario comentario)
        {
                await this.repoComentarios.NewComentario(comentario);
                return RedirectToAction("Publicaciones");
        }

        public async Task<IActionResult> DeletePublicacion(int idpost)
        {
            await this.repoBlog.DeletePost(idpost);
            return RedirectToAction("Publicaciones");
        }
        public IActionResult Publicaciones()
        {
            List<BlogModel> publicaciones = this.repoBlog.GetPost();
            List<Comentario> comentarios = this.repoComentarios.GetComentarios();
            ViewData["COMENTARIOS"] = comentarios;
            return View(publicaciones);
        }
        
        public IActionResult NewPost()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewPost(BlogModel model)
        {
            await this.repoBlog.NewPost(model);
            return RedirectToAction("Publicaciones");
        }
        public IActionResult EditPublicaciones(int idpost)
        {
            BlogModel publicacion = this.repoBlog.FindPost(idpost);
            return View(publicacion);
        }
        [HttpPost]
        public async Task<IActionResult> EditPublicaciones(BlogModel blog)
        {
            await this.repoBlog.EditPostAsync(blog);
            return RedirectToAction("Publicaciones");
        }


    }
}
