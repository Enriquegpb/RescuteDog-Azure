using Microsoft.AspNetCore.Authentication.Cookies;
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
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthorization(options =>
{
    /***
     * Aqu� van los coordinadores de la web
     * �Debe de haber un campo en la BBBDD para operar con este permiso
     * 
     */
    options.AddPolicy("AdminOnly",
       policy =>
       policy.RequireRole("Administrador"));
});
builder.Services.AddAuthentication(options =>
{

}).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme,
    config =>
    {
        config.AccessDeniedPath = "/Managed/ErrorAccesos";
    }
    );

builder.Services.AddSingleton<HelperMail>();
builder.Services.AddSingleton<HelperPathProvider>();
builder.Services.AddTransient<IRepoBlog,RepositoryBlog>();
builder.Services.AddTransient<IRepoComentarios,RepositoryComentarios>();
builder.Services.AddTransient<IRepoMascotas,RepositoryMascotas>();
builder.Services.AddTransient<IRepoAutentication,RepositoryAutentication>();
builder.Services.AddTransient<IRepoVoluntarios,RepositoryVoluntarios>();
builder.Services.AddTransient<IRepoRefugios, RepositoryRefugios>();
builder.Services.AddTransient<IRepoAdopciones, RepositoryAdopciones>();
builder.Services.AddDbContext<MascotaContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews(options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();
var app = builder.Build();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseMvc(route =>
{
    route.MapRoute(
        name: "default",
        template: "{controller=Refugios}/{action=Index}/{id?}");
});
//app.MapGet("/", () => "Hello World!");

app.Run();
