using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Helpers;
using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public class RepositoryAutentication: IRepoAutentication
    {
        private MascotaContext context;
        public RepositoryAutentication(MascotaContext context)
        {
            this.context = context;
        }

        public User LogIn(string email, string password)
        {
            User user =
                this.context.Users.FirstOrDefault(z => z.Email == email);
            if (user == null)
            {
                return null;
            }
            else
            {
                byte[] passUsuario = user.Password;
                string salt = user.Salt;
                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temp);
                if(respuesta == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
        public int GetMaximoUser()
        {
            int maximo = 1 + (from datos in this.context.Users
                              select datos).Max(x => x.Id);
            if (maximo == 0)
            {
                maximo = 1;
            }
            return maximo;
        }
        public async Task NewUser(string username, string password, string email, string phone, string imagen,string birdthday)
        {
            User user = new User();
            user.Id = this.GetMaximoUser();
            user.Username = username;
            user.Email = email;
            user.Phone = phone;
            user.Imagen = imagen;
            user.Contrasena = password;
            user.Birdthday = birdthday;
            user.Salt = HelperCryptography.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
        }
    }
}
