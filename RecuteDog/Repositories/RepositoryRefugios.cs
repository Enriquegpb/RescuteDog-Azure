using RecuteDog.Data;
using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public class RepositoryRefugios : IRepoRefugios
    {
        private MascotaContext context;
        public RepositoryRefugios(MascotaContext context)
        {
            this.context = context;
        }

        public List<Refugio> GetRefugios()
        {
            var consulta = from datos in this.context.Refugios
                           select datos;
            return consulta.ToList();
        }

        public void ModificarDatosRefugio(Refugio refugio)
        {
            throw new NotImplementedException();
        }

        public void ModificarRefugio(Refugio refugio)
        {
            throw new NotImplementedException();
        }
    }
}
