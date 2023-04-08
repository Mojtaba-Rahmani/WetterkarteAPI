using Newtonsoft.Json;
using WetterKarte.DL.Services.Interfaces;
using static WetterKarte.DL.DTO.Class;

namespace WetterKarte.DL.Services
{
    public class UserService : IUserService
    {
        private HttpClient _client;
        /// <summary>
        ///  Wenn möchten Sie von Databank Anwenden
        /// </summary>
        /// 
        //public WetterAPIDbContext _WetterAPIDbContext;
        //public UserService(WetterAPIDbContext WetterAPIDbContext, HttpClient client)
        //{
        //    _WetterAPIDbContext = WetterAPIDbContext;
        //    _client = client;
        //}

        public UserService(HttpClient client)
        {
            _client = client;
        }
        public ResultViewModel GetCity(string City)
        {
            //var token = HttpContext.

            var userApiUrl = "http://localhost:5070/api/Wetter/GetCity/" + City;


            var Result = _client.GetStringAsync(userApiUrl).Result;

            ResultViewModel List = JsonConvert.DeserializeObject<ResultViewModel>(Result);

            return List;
        }

        /// <summary>
        /// Wenn möchten Sie von Databank Anwenden
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        //public User LoginUser(LoginViewModel login)
        //{
        //    return _WetterAPIDbContext.Users.SingleOrDefault(u => u.Nutzername.Nutzername && u.Passwort == login.Passwort);
        //}

    }
}
