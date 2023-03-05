using Microsoft.EntityFrameworkCore;
using RecuteDog.Models;

namespace RecuteDog.Data
{
    public class MascotaContext: DbContext
    {
        public MascotaContext(DbContextOptions<MascotaContext> options)
            : base(options) { }
        public DbSet<Mascota> Mascotas { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<Adopcion> Adopciones { get; set;}
        public DbSet<Voluntario> Voluntarios { get; set;}
        public DbSet<Refugio> Refugios { get; set;}
    }
}
