using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoVoluntarios
    {
        List<Voluntario> Getvoluntarios();
        Voluntario FindVoluntario(int idvoluntario);
        Task NewVoluntario(Voluntario voluntario, string refugio);
        Task ModificarDatosRefugio(Voluntario voluntario, string refugio);
        Task BajaVoluntario(int idvoluntario);
    }
}
