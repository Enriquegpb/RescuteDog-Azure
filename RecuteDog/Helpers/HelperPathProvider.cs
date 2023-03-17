using Microsoft.VisualStudio.Web.CodeGeneration;
using static System.Net.WebRequestMethods;

namespace RecuteDog.Helpers
{
    public enum Folders { Images = 0, Uploads = 1, Temporal = 2 };  
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        /**
         * Será el envirioiment donde se guardarán nuestros ficheros(Imagenes)
         */
        public HelperPathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string filename, Folders folder)
        {
            /**
             * Se mapean las carpetas
             */
            string carpeta = "";
            if(folder == Folders.Images)
            {
                carpeta = "images";
            }else if(folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }else if(folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            /**
             * Añadimos la ruta raiz y la juntamos con la rutas del la carpeta
             * y el fichero
             */
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, filename); 
            return path;
        }
    }
}
