using System.Xml.Linq;
using System.Xml;
using NugetRescuteDog.Models;
using ServiceReference1;

namespace RecuteDog.Services
{
    public class ServiceCatastro
    {
        CallejerodelasedeelectrónicadelcatastroSoapClient client;

        public ServiceCatastro(CallejerodelasedeelectrónicadelcatastroSoapClient client)
        {
            this.client = client;
        }

        public async Task<List<Provincia>> GetProvinciasAsync()
        {
            ConsultaProvincia1 response = await
                this.client.ObtenerProvinciasAsync();
            XmlNode nodo = response.Provincias;
            
            string dataXml = nodo.OuterXml;
            
            XDocument document = XDocument.Parse(dataXml);
            List<Provincia> provinciasList = new List<Provincia>();
            XNamespace ns = "http://www.catastro.meh.es/";
            var consulta = from datos in document.Descendants(ns + "prov")
                           select datos;
            foreach (XElement tag in consulta)
            {
                string cp = tag.Element(ns + "cpine").Value;
                string nombre = tag.Element(ns + "np").Value;
                Provincia p = new Provincia
                {
                    IdProvincia = int.Parse(cp),
                    Nombre = nombre
                };
                provinciasList.Add(p);
            }
            return provinciasList;
        }
      



    }

}
