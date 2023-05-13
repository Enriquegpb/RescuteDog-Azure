using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RecuteDog.Extensions;
using RecuteDog.Helpers;
using NugetRescuteDog.Models;
using RecuteDog.Repositories;
using System.Security.Claims;
using RecuteDog.Services;

namespace RecuteDog.Controllers
{
    public class HomeController : Controller
    {
        private HelperMail helperMail;
        private HelperPathProvider helper;
        private ServiceApiRescuteDog service;
        public HomeController( HelperMail helperMail, HelperPathProvider helper, ServiceApiRescuteDog service)
        {
            
            this.helperMail = helperMail;
            this.helper = helper;
            this.service = service;
        }

        public async Task<IActionResult> Index(int idrefugio)
        {
            List<Mascota> mascotas = await this.service.GetMascotasAsync(idrefugio);
            ViewData["ESTEREFUGIO"] = idrefugio;
            return View(mascotas);
        }

        public async Task<IActionResult> FormularioAdopcion(int idmascota)
        {
            Mascota mascota = await this.service.FindMascotaAsync(idmascota);
            return View(mascota);
        }
        [HttpPost]
        public async Task <IActionResult> FormularioAdopcion(int idmascota, string para, string asunto, string mensaje)
        {

            Mascota mascota = await this.service.FindMascotaAsync(idmascota);

            /*
             * Creacion del correo
             */


            /**
             * Configuracion del correo
             */
            string peligroso = "";
            if(mascota.Peligrosidad == false)
            {
                peligroso = "<p style='color:green'>No Peligroso</p>";
            }
            else
            {
                peligroso = "<p style='color:red'>Peligroso</p>";
            }

            asunto = "Has adoptado a " + mascota.Nombre;
            para = "rdog49903@gmail.com";
            mensaje = "Gracias por la solicitud de adopcion de" + mascota.Nombre + " Estudiaremos el caso y procederemos lo antes posible al siguente paso del proceso de adopcion" +
                "recuerde que los datos de este perro son los siguientes" +
                "Edad: " + mascota.Edad + " Dimesiones" +
                " Alto: " + mascota.Alto + " Anchura:" + mascota.Ancho + " Peso:" + mascota.Peso + "" +
                "" + peligroso + "";


            await this.helperMail.SendMailAsync(para, asunto, mensaje);
            /**
             * Ahora el siguiente paso es crear el servicio de 
             * mensajería para utilizarlo en toda la app
             */
            string token =
               HttpContext.Session.GetString("token");
            await this.service.NewAdopcionAsync(idmascota, int.Parse( this.HttpContext.User.FindFirst(ClaimTypes.Role).Value), token);
            //NECESITO UN METODO EN MASCOTAS PARA ACTUALIZAR EL ESTADO DE LA MASCOTA A TRUE O FALSE            
            mascota.Adoptado = true;

            await this.service.UpdateEstadoAdopcionAsync(idmascota, mascota.Adoptado, token);/**El objetivo de buscar a la mascota es para asegurarse de pasar el estaod que corresponde a esa mascota en concreto, para modificar su estado de adopcion**/
            return RedirectToAction("Index", "Refugios");
        }

        public async Task<IActionResult> InformeAdopcion()
        {
            string token =
              HttpContext.Session.GetString("token");
            List<Mascota> mascotasinforme = await this.service.GenerarInformeAdopcionesAsync(token);
            return View(mascotasinforme);
        }
        [HttpPost]
        public async Task<IActionResult> InformeAdopcion(int idmascota)
        {
            string token =
             HttpContext.Session.GetString("token");
            await this.service.DevolverAnimalAlRefugioAsync(idmascota,token);
            Mascota mascota = await this.service.FindMascotaAsync(idmascota);
            mascota.Adoptado = false;

            await this.service.UpdateEstadoAdopcionAsync(idmascota, mascota.Adoptado, token);
            return RedirectToAction("Index","Refugios");

        }

        public IActionResult NuevaMascota(int idrefugio)
        {
            ViewData["IDREFUGIO"] = idrefugio;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevaMascota(Mascota mascota, IFormFile imagen)
        {           
            string filename = imagen.FileName;
            string path = this.helper.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }

            //string pathserver = "https://localhost:7057/images/" + imagen.FileName;
            mascota.Imagen = filename;
            string token =
               HttpContext.Session.GetString("token");
            await this.service.NewMascotaAsync(mascota, token);
            return RedirectToAction("Index", "Refugios");
        }
        public async Task<IActionResult> ModificarDatosMascota(int idmascota)
        {
            Mascota mascota = await this.service.FindMascotaAsync(idmascota);
            return View(mascota);
        }
        [HttpPost]
        public async Task<IActionResult> ModificarDatosMascota(Mascota mascota, IFormFile imagen)
        {
            string filename = imagen.FileName;
            string path = this.helper.MapPath(filename, Folders.Images);//¿AQUI DEBERIA PONER EL STRING DE IMAGEN???
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }
            
            //string pathserver = "https://localhost:7057/images/" + imagen.FileName;
            mascota.Imagen = filename;
            string token =
              HttpContext.Session.GetString("token");
            await this.service.UpdateMascotaAsync(mascota, token);
            return RedirectToAction("Index", "Refugios");
        }
        public async Task<IActionResult> ActionBajasAllMascotasRefugio(int idrefugio)
        {
            TempData["REFUGIO"] = idrefugio;
            await this.service.FullBajaMascotasRufugio(idrefugio);
            return RedirectToAction("DeleteRefugio", "Refugios");
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

        public async Task<string> GenerateAndDownLoadExcel()
        {
            string token =
              HttpContext.Session.GetString("token");
            List<Mascota> mascotas = await this.service.GenerarInformeAdopcionesAsync(token);
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

        
    }
}
