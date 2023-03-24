using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoVoluntarios
    {
        List<Voluntario> Getvoluntarios();
        Voluntario FindVoluntario(int idvoluntario);
        Task NewVoluntario(Voluntario voluntario);
        Task ModificarDatosVoluntario(Voluntario voluntario);
        Task BajaVoluntario(int idvoluntario);
    }
}
