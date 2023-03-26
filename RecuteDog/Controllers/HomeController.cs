using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RecuteDog.Extensions;
using RecuteDog.Helpers;
using RecuteDog.Models;
using RecuteDog.Repositories;

namespace RecuteDog.Controllers
{
    public class HomeController : Controller
    {
        private IRepoMascotas repo;
        private IRepoAdopciones repoAdopciones;
        private HelperMail helperMail;
        public HomeController(IRepoMascotas repo, IRepoAdopciones repoAdopciones, HelperMail helperMail)
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
        public async Task <IActionResult> FormularioAdopcion(int idmascota, string para, string asunto, string mensaje)
        {
            User user = HttpContext.Session.GetObject<User>("LOGSESSION");
            Mascota mascota = this.repo.DetailsMascota(idmascota);

            /*
             * Creacion del correo
             */


            /**
             * Configuracion del correo
             */


            asunto = "Has adoptado a " + mascota.Nombre;
            para = "rescutedogkw@gmail.com";
            mensaje = "Gracias por la solicitud de adopcion de" + mascota.Nombre + " Estudiaremos el caso y procederemos lo antes posible al siguente paso del proceso de adopcion";

            await this.helperMail.SendMailAsync(para, asunto, mensaje);
            /**
             * Ahora el siguiente paso es crear el servicio de 
             * mensajería para utilizarlo en toda la app
             */

            await this.repoAdopciones.NuevaAdopcion(idmascota, user.Id);
            //NECESITO UN METODO EN MASCOTAS PARA ACTUALIZAR EL ESTADO DE LA MASCOTA A TRUE O FALSE            
            mascota.Adoptado = true;
            await this.repo.UpdateEstadoAdopcion(idmascota, mascota.Adoptado);/**El objetivo de buscar a la mascota es para asegurarse de pasar el estaod que corresponde a esa mascota en concreto, para modificar su estado de adopcion**/
            return RedirectToAction("Index", "Refugios");
        }

        public IActionResult InformeAdopcion()
        {
            List<Mascota> mascotasinforme = this.repo.GenerarInformeAdopciones();
            return View(mascotasinforme);
        }
        [HttpPost]
        public async Task<IActionResult> InformeAdopcion(int idmascota)
        {
            await this.repoAdopciones.DevolverAnimalAlRefugio(idmascota);
            Mascota mascota = this.repo.DetailsMascota(idmascota);
            mascota.Adoptado = false;
            await this.repo.UpdateEstadoAdopcion(idmascota, mascota.Adoptado);
            return RedirectToAction("Index","Refugios");
        }

        public IActionResult NuevaMascota(int idrefugio)
        {
            ViewData["REFUGIO"] = idrefugio;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevaMascota(Mascota mascota)
        {
            await this.repo.IngresoAnimal(mascota);
            return RedirectToAction("Index", "Refugios");
        }
        public IActionResult ModificarDatosMascota(int idmascota)
        {
            this.repo.DetailsMascota
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevaMascota(Mascota mascota)
        {
            await this.repo.IngresoAnimal(mascota);
            return RedirectToAction("Index", "Refugios");
        }
        /**
         * Metodos Para las vistas
         * 
         */

        //public async Task<JsonResult> SaveInformeMascotasAsync(List<Mascota> mascotas)
        //{
        //    mascotas = await this.repo.SaveInformeAsync(mascotas);
        //    return Json(mascotas);
        //}

        public string GenerateAndDownLoadExcel()
        {
            List<Mascota> mascotas = this.repo.GenerarInformeAdopciones();
            var dataTable = DataTableExtensions.GetDataTable(mascotas);
            //dataTable.Columns.Remove("IDMASCOTA");

            byte[] fileContents = null;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets.Add("Mascotas");
                ws.Cells["A1"].Value = "Informe De Adopciones";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Size = 18;
                ws.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                ws.Cells["A2"].Value = "Listado de Adopciones";
                ws.Cells["A2"].Style.Font.Bold = true;
                ws.Cells["A2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                /**
                 * Cargar los datos en el informe
                 */

                ws.Cells["A3"].LoadFromDataTable(dataTable, true);
                ws.Cells["A3:L3"].Style.Font.Bold = true;
                ws.Cells["A3:L3"].Style.Font.Size = 14;
                ws.Cells["A3:L3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["A3:L3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
                ws.Cells["A3:L3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A3:L3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                excelPackage.Save();
                fileContents = excelPackage.GetAsByteArray();
            }
            return Convert.ToBase64String(fileContents);

        }

        public async Task<IActionResult> ActionBajasAllMascotasRefugio(int idrefugio)
        {
            TempData["REFUGIO"] = idrefugio;
            await this.repo.BajasAllMascotasPorRefugio(idrefugio);
            return RedirectToAction("DeleteRefugio", "Refugios");
        }
    }
}
