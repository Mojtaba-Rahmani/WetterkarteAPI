using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Net;
using static WetterKarte.DL.DTO.Class;

namespace WetterkarteAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WetterController : ControllerBase
    {
        public string url { get; set; }

        [HttpGet]
        public IActionResult GetCity()
        {
            return Ok();
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetCity(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    string appId = "6bb7a98d4c1fe93212efc2cad5ca0525";
                    url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", city, appId);

                    var response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var result = WeatherDetail(city);
                    return Ok(result);

                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        public string WeatherDetail(string city)
        {

            using (WebClient _client = new WebClient())
            {
                string json = _client.DownloadString(url);


                RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);
                ResultViewModel rslt = new ResultViewModel();
                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                //rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                //rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = weatherInfo.weather[0].icon;
                rslt.Wind = Convert.ToString(weatherInfo.wind.speed);
                rslt.Winddeg = Convert.ToString(weatherInfo.wind.deg);
                rslt.Pressure = Convert.ToString(weatherInfo.main.pressure);
                var jsonstring = new JavaScriptSerializer().Serialize(rslt);

                return jsonstring;

            }
        }



    }
}
