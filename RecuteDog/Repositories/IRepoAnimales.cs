using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoAnimales
    {
        List<Mascota> GetMascotas(int idrefugio);
        List<Mascota> GenerarInformeAdopciones();
        Mascota DetailsMascota(int idmascota);
        void IngresoAnimal(Mascota mascota);
        //void AdoptarMascota(int idmascota);
        void UpdateEstadoAdopcion(int idmascota, bool estado);

    }
}
