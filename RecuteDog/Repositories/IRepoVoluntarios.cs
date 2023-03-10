using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoVoluntarios
    {
        List<Voluntario> Getvoluntarios();
        Task NewVoluntario(Voluntario voluntario, string refugio);
        Task ModificarDatosRefugio(Voluntario voluntario);
        Task BajaVoluntario(int idvoluntario);
    }
}
