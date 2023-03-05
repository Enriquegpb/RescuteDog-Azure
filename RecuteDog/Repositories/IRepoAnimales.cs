using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoAnimales
    {
        List<Mascota> GetMascotas();
        Mascota DetailsMascota(int idmascota);
        void IngresoAnimal(Mascota mascota);

    }
}
