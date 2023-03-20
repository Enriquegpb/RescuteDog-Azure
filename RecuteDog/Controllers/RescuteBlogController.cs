using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
