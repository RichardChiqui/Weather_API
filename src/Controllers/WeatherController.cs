using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace ACHC_WEATHER_2._0.Controllers
{

    public class WeatherController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public WeatherController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
          
        }

        [HttpGet("/weather")]
        public async Task<IActionResult> GetWeather(double latitude, double longitude)
        {
            
            var client = _clientFactory.CreateClient();
            var apiUrl = "https://api.open-meteo.com/v1/forecast?latitude=" + latitude + "&longitude="+longitude + "&current_weather=true&temperature_unit=fahrenheit";
            Debug.WriteLine("Request received for latitude {0} and longitude {1}", apiUrl, longitude);
           

            // var response = await client.SendAsync(request);
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
