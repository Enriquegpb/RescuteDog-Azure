using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.Cookies;
using RecuteDog.Helpers;
using RecuteDog.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});

builder.Services.AddAuthorization(options =>
{
    /***
     * Aquí van los coordinadores de la web
     * ¿Debe de haber un campo en la BBBDD para operar con este permiso
     * 
     */
    options.AddPolicy("AdminOnly",
       policy =>
       policy.RequireRole("Administrador"));
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme,
    config =>
    {
        config.AccessDeniedPath = "/Managed/ErrorAccesos";
    }
    );
string azureKeys = builder.Configuration.GetValue<string>("AzureKeys:StorageAccount");
BlobServiceClient blobServiceClient =
    new BlobServiceClient(azureKeys);
builder.Services.AddTransient<BlobServiceClient>(x => blobServiceClient);
builder.Services.AddSingleton<HelperMail>();
builder.Services.AddTransient<ServiceApiRescuteDog>();
builder.Services.AddTransient<ServiceBlobRescuteDog>();


builder.Services.AddControllersWithViews(options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();
var app = builder.Build();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.UseSession();

app.UseMvc(route =>
{
    route.MapRoute(
        name: "default",
        template: "{controller=Refugios}/{action=Index}/{id?}");
});
//app.MapGet("/", () => "Hello World!");

app.Run();
