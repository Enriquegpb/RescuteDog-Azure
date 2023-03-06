using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoAnimales
    {
        List<Mascota> GetMascotas(int idrefugio);
        List<Mascota> GenerarInformeAdopciones();
        Mascota DetailsMascota(int idmascota);
        void IngresoAnimal(Mascota mascota);

    }
}
