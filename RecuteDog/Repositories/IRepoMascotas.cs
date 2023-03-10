using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoMascotas
    {
        List<Mascota> GetMascotas(int idrefugio);
        List<Mascota> GenerarInformeAdopciones();
        Mascota DetailsMascota(int idmascota);
        Task IngresoAnimal(Mascota mascota);
        //void AdoptarMascota(int idmascota);
        Task UpdateEstadoAdopcion(int idmascota, bool estado);

    }
}
