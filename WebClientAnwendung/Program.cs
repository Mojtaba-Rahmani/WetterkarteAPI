using Microsoft.AspNetCore.Authentication.Cookies;
using WetterKarte.DL.Services;
using WetterKarte.DL.Services.Interfaces;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
//using WetterKarte.DL.Context;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddHttpClient("EshopClienet", configureClient => {
    configureClient.BaseAddress = new Uri("http://localhost:5070");

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Option =>
    {
        Option.LoginPath = "/Auth/Login";
        Option.LogoutPath = "/Auth/Logout";
        Option.Cookie.Name = "Auth.Coo";
    });
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUserService, UserService>();

/// <summary>
///  Wenn möchten Sie von Databank Anwenden
/// </summary>
//#region DataBase Context

//builder.Services.AddDbContext<WetterAPIDbContext>(options =>
//{

//    options.UseSqlServer("Data Source=MOJIX-PARIX\\MOJIX_SERVER;Initial Catalog=EshopApiDB;Integrated Security=True");
//}
//);

//#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseCookiePolicy();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
  pattern: "{controller=Auth}/{action=Login}/{id?}");
app.Run();
