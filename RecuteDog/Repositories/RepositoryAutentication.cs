using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Models;

namespace RecuteDog.Repositories
{
    public class RepositoryAutentication
    {
        private MascotaContext context;
        public RepositoryAutentication(MascotaContext context)
        {
            this.context = context;
        }

        public User FindUser(User user)
        {
            return this.context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
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
        public async Task NewUser(User user)
        {
            
            

            User newUser = new User
            {
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Birdthday = user.Birdthday,
                Id = this.GetMaximoUser(),
                Password = user.Password,
                Phone = user.Phone,
            };
            this.context.Users.Add(newUser);
            await this.context.SaveChangesAsync();
        }
    }
}
