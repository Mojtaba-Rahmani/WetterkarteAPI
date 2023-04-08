using Microsoft.AspNetCore.Mvc;
using WetterKarte.DL.Services.Interfaces;
using Newtonsoft.Json;

namespace WebClientAnwendung.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {
        private IUserService _IUserService;
        public HomeController(IUserService IUserService)
        {
            _IUserService = IUserService;
        }

   

        [HttpGet]
        public IActionResult Index()
        {
            var user = User.Identity.Name;
            if (user != null)
            {
                string token = User.FindFirst("AccessToken").Value;
                return View();
            }
            else
            {
                ModelState.AddModelError("Nutzername", "Sie Müssen Erst Authorize Werden ");
                return Redirect("../Auth/Login");
            }
        }


        public string WeatherDetail(string city)
        {
            var result = _IUserService.GetCity(city);
            return JsonConvert.SerializeObject(result);
        }


    }
}
