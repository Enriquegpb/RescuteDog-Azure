using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoComentarios
    {
        List<Comentario> GetComentarios();
        Comentario FindComentario(int idcomentario);
        Task NewComentario(Comentario comentario);
        Task EditComentario(Comentario comentario);
        Task DeleteComentario(int idcomentario);
    }
}
