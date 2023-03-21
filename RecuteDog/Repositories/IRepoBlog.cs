using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoBlog
    {
        List<BlogModel> GetPost();
        BlogModel FindPost(int idpost);
        Task NewPost(BlogModel post);
        Task EditPostAsync(BlogModel post);
        Task DeletePost(int idpost);
    }
}
