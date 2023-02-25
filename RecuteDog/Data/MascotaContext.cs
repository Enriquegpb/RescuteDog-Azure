using Microsoft.EntityFrameworkCore;
using RecuteDog.Models;

namespace RecuteDog.Data
{
    public class MascotaContext: DbContext
    {
        public MascotaContext(DbContextOptions<MascotaContext> options)
            : base(options) { }
        DbSet<Mascota> Mascotas { get; set;}
        DbSet<User> Users { get; set;}
    }
}
