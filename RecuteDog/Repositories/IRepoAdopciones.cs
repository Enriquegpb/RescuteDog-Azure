namespace RecuteDog.Repositories
{
    public interface IRepoAdopciones
    {
        void NuevaAdopcion(int idmascota, int iduser);
        void DevolverAnimalAlRefugio(int idmascota);
    }
}
