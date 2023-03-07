using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoRefugios
    {
        List<Refugio> GetRefugios();
        Refugio DetailsRefugio(int idrefugio);
        void ModificarDatosRefugio(Refugio refugio);
        void AgregarRefugio(Refugio refugio);
        void BajaRefugio(int idrefugio);
        
    }
}
