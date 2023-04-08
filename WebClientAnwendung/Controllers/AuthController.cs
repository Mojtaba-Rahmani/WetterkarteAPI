using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using WebClientAnwendung.Models;
using WetterKarte.DL.Services.Interfaces;

namespace WebClientAnwendung.Controllers
{
    public class AuthController : Controller
    {
        IHttpClientFactory _httpClientFactory;
        IUserService _IUserService;
        public AuthController(IHttpClientFactory httpClientFactory , IUserService IUserService)
        {
            _httpClientFactory = httpClientFactory;
            _IUserService= IUserService;
        }
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var _client = _httpClientFactory.CreateClient("EshopClienet");             
                                                                                       
            var jsonBody = JsonConvert.SerializeObject(login);                         
                                                                                       
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = _client.PostAsync("/Api/Auth", content).Result;

            if (response.IsSuccessStatusCode)
            {
                 string token = response.Content.ReadAsStringAsync().Result;
         
                var Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,login.Nutzername),

                    new Claim(ClaimTypes.Name, login.Nutzername),

                     new Claim("AccessToken", token)

                };

                var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true
                };
                
                HttpContext.SignInAsync(principal, properties);
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("Nutzername", "user Not Valid");
                return View(login);
            }


        }


        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Auth/Login");
        }

        #endregion

    }
}