using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoRefugios
    {
        List<Refugio> GetRefugios();
        void ModificarRefugio(Refugio refugio);
        void ModificarDatosRefugio(Refugio refugio);
        
    }
}
