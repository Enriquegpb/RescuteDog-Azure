using Microsoft.AspNetCore.Mvc;
using RecuteDog.Helpers;
using RecuteDog.Models;
using RecuteDog.Repositories;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Mail;

namespace RecuteDog.Controllers
{
    public class HomeController : Controller
    {
        private IRepoAnimales repo;
        private IRepoAdopciones repoAdopciones;
        private HelperMail helperMail;
        public HomeController(IRepoAnimales repo, IRepoAdopciones repoAdopciones, HelperMail helperMail)
        {
            this.repo = repo;
            this.repoAdopciones = repoAdopciones;
            this.helperMail = helperMail;
        }

        public IActionResult Index(int idrefugio)
        {
            List<Mascota> mascotas = this.repo.GetMascotas(idrefugio);
            return View(mascotas);
        }
        public IActionResult FormularioAdopcion(int idmascota)
        {
            Mascota mascota = this.repo.DetailsMascota(idmascota);
            return View(mascota);
        }
        [HttpPost]
        public async Task <IActionResult> FormularioAdopcion(int idmascota, int iduser, string para, string asunto, string mensaje)
        {
            iduser = 2;
            //Mascota mascota = this.repo.DetailsMascota(idmascota);
            ///*
            // * Creacion del correo
            // */


            ///**
            // * Configuracion del correo
            // */
            

            //asunto = "Has adoptado a " + mascota.Nombre;
            //para = "rescutedogkw@gmail.com";
            //mensaje = "Gracias por la solicitud de adopcion de"+ mascota.Nombre +" Estudiaremos el caso y procederemos lo antes posible al siguente paso del proceso de adopcion";

            //await this.helperMail.SendMailAsync(para, asunto, mensaje);
            ///**
            // * Ahora el siguiente paso es crear el servicio de 
            // * mensajería para utilizarlo en toda la app
            // */

            this.repoAdopciones.NuevaAdopcion(idmascota, iduser);
            //NECESITO UN METODO EN MASCOTAS PARA ACTUALIZAR EL ESTADO DE LA MASCOTA A TRUE O FALSE
            Mascota mascota = this.repo.DetailsMascota(idmascota);
            mascota.Adopdatado = true;
            this.repo.UpdateEstadoAdopcion(idmascota, mascota.Adopdatado);/**El objetivo de buscar a la mascota es para asegurarse de pasar el estaod que corresponde a esa mascota en concreto, para modificar su estado de adopcion**/
            return RedirectToAction("Index", "Refugios");
        }

        public IActionResult InformeAdopcion()
        {
            List<Mascota> mascotasinforme = this.repo.GenerarInformeAdopciones();
            return View(mascotasinforme);
        }
        [HttpPost]
        public IActionResult InformeAdopcion(int idmascota)
        {
            this.repoAdopciones.DevolverAnimalAlRefugio(idmascota);
            Mascota mascota = this.repo.DetailsMascota(idmascota);
            mascota.Adopdatado = false;
            this.repo.UpdateEstadoAdopcion(idmascota, mascota.Adopdatado);
            return RedirectToAction("Index","Refugios");
        }

        public IActionResult NuevaMascota()
        {
            /**
             * Aqui necesitaría un metodo para recuperar el id del refugio de la mascota
             */
            return View();
        }
        [HttpPost]
        public IActionResult NuevaMascota(Mascota mascota)
        {
            this.repo.IngresoAnimal(mascota);
            return RedirectToAction("Index", "Refugios");
        }
    }
}
