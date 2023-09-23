using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WetterKarte.DL.Services;
using WetterKarte.DL.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();

builder.Services.AddResponseCaching();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(Options =>
              {
                    Options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,   
                        ValidateAudience = false, 
                        ValidateLifetime = true,
                      ValidateIssuerSigningKey = true, 
                        ValidIssuer = "http://localhost:5070", 
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("6bb7a98d4c1fe93212efc2cad5ca0525s"))

                  };
              });


builder.Services.AddCors(Options =>
{
    Options.AddPolicy("EnableCors", builder =>
    {

        builder
        //.AllowAnyOrigin()
            .SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .Build();
    });
});
/// <summary>
/// Wenn möchten Sie von Databank Anwenden
/// </summary>
//builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
           ForwardedHeaders.XForwardedProto
});
app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();
app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("EnableCors");

app.UseAuthentication();
app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Wetter}/{action=GetCity}");


app.Run();
