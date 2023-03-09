using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoAutentication
    {
        User LogIn(string email, string password);
        Task NewUser(string username, string password, string email, string phone, string imagen, string cumple);
    }
}
