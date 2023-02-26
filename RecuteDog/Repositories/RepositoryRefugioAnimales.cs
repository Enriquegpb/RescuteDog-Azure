using RecuteDog.Data;
using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public class RepositoryRefugioAnimales
    {
        private MascotaContext context;
        public RepositoryRefugioAnimales(MascotaContext context)
        {
            this.context = context;
        }
        public List<Mascota> GetMascotas()
        {
            var consulta = from datos in this.context.Mascotas
                           select datos;
            return consulta.ToList();
        }
    }
}
