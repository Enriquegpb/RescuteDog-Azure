using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Helpers;
using RecuteDog.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});
string connectionString = builder.Configuration.GetConnectionString("SqlPanimales");
builder.Services.AddSingleton<HelperMail>();
builder.Services.AddTransient<IRepoMascotas,RepositoryMascotas>();
builder.Services.AddTransient<IRepoAutentication,RepositoryAutentication>();
builder.Services.AddTransient<IRepoVoluntarios,RepositoryVoluntarios>();
builder.Services.AddTransient<IRepoRefugios, RepositoryRefugios>();
builder.Services.AddTransient<IRepoAdopciones, RepositoryAdopciones>();
builder.Services.AddDbContext<MascotaContext>
    (options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Refugios}/{action=Index}"
    );
//app.MapGet("/", () => "Hello World!");

app.Run();
