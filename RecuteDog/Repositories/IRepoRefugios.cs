using NugetRescuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoRefugios
    {
        List<Refugio> GetRefugios();
        Refugio DetailsRefugio(int idrefugio);
        Task ModificarDatosRefugio(Refugio refugio);
        Task AgregarRefugio(Refugio refugio);
        Task BajaRefugio(int idrefugio);
        
    }
}
