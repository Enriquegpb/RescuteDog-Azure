using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoAutentication
    {
        User FindUser(User user);
        Task NewUser(User user);
    }
}
