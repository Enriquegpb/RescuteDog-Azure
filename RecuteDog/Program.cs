using Microsoft.EntityFrameworkCore;
using RecuteDog.Data;
using RecuteDog.Repositories;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("SqlPanimales");
builder.Services.AddTransient<IRepoAnimales,RepositoryAnimales>();
builder.Services.AddTransient<IRepoAutentication,RepositoryAutentication>();
builder.Services.AddTransient<IRepoVoluntarios,RepositoryVoluntarios>();
builder.Services.AddDbContext<MascotaContext>
    (options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
    );
//app.MapGet("/", () => "Hello World!");

app.Run();
