using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public interface IRepoAutentication
    {
        User LogIn(string email, string password);
        Task NewUser(string username, string password, string email, string phone, string imagen, string cumple);
        User FindUser(string email);
        Task<User> ExisteEmpleado(string email, string password);
        Task UpdatePerfilusuario(string username, string telefono, string email, string imagen, int iduser);

        Task BajaUsuario(int iduser);
    }
}
