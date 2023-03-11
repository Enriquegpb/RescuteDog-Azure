using RecuteDog.Models;

namespace RecuteDog.Service
{
    public interface IService
    {
        List<Adopcion> GetAdopciones();
        List<Adopcion> SaveInformeAdopciones(List<Adopcion> adopciones);
    }
}
