using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoBlog
    {
        List<BlogModel> GetPost();
        BlogModel FindPost(int idpost);
        Task NewPost(BlogModel post);
        Task EditPost(BlogModel post);
        Task DeletePost(int idpost);
    }
}
