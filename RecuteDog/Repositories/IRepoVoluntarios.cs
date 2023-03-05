using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoVoluntarios
    {
        List<Voluntario> Getvoluntarios();
        void NewVoluntario(Voluntario voluntario, string refugio);
    }
}
