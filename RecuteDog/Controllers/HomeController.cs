using Microsoft.AspNetCore.Mvc;
using RecuteDog.Models;
using RecuteDog.Repositories;
using System.Net;
using System.Net.Mail;

namespace RecuteDog.Controllers
{
    public class HomeController : Controller
    {
        private IRepoAnimales repo;
        private IRepoAdopciones repoAdopciones;
        private IConfiguration configuration;
        public HomeController(IRepoAnimales repo, IRepoAdopciones repoAdopciones, IConfiguration configuration)
        {
            this.repo = repo;
            this.repoAdopciones = repoAdopciones;
            this.configuration = configuration;
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
        public async Task <IActionResult> FormularioAdopcion(int idmascota, int iduser)
        {
            iduser = 2;
            Mascota mascota = this.repo.DetailsMascota(idmascota);
            /*
             * Creacion del correo
             */
            MailMessage mail =new MailMessage();
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            mail.From = new MailAddress(user);
            mail.To.Add(new MailAddress("enriquegpb5@gmail.com"));
            mail.Subject = "Has adoptado a "+ mascota.Nombre;
            mail.Body = "Has aadoptado a nuestra mascota preferiado, no!!!";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            /**
             * Configuracion del correo
             */

            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");  
            string hostName = this.configuration.GetValue<string>("MailSettings:Smtp:Host");  
            int port = this.configuration.GetValue<int>("MailSettings:Smtp:Port");  
            bool enableSsl = this.configuration.GetValue<bool>("MailSettings:Smtp:EnableSSL");  
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Smtp:DefaultCredentials");  

            SmtpClient client = new SmtpClient();
            client.Host = hostName;
            client.Port = port;
            client.EnableSsl = enableSsl;
            client.UseDefaultCredentials = defaultCredentials;
            NetworkCredential credentials  = new NetworkCredential(user, password);
            client.Credentials = credentials;
            await client.SendMailAsync(mail);

            this.repoAdopciones.NuevaAdopcion(idmascota, iduser);
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
            return RedirectToAction("Index","Refugios");
        }
       
    }
}
