using System;
using System.Net.Http;
using System.Threading.Tasks;
using Azure;
using Newtonsoft.Json;

namespace RentStudio.Services.WeatherService
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
/*            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
*/        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            string _apiKey = "4b54f90b765c99948613bdf2383724a8";

            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(jsonResponse);
                    return weather;
                }
                else
                {
                    throw new HttpRequestException($"Failed to get weather data: {response.StatusCode}");
                }

            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Failed to get weather data: {ex.Message}");
            } 
            finally
            {
                
                _httpClient.Dispose();
            }
        }
    }

    public class WeatherResponse
    {
        public MainData Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
    }

    public class WeatherInfo
    {
        public string Description { get; set; }
    }
}
