using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;

namespace WebLIDOM.Services
{
    public class StadisticService
    {
        public async Task<List<Stadistic>> GetAllStadistic()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders
                    .Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Stadistic");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var stadistics = JsonConvert.DeserializeObject<List<Stadistic>>(data);
                    return stadistics!;
                }
                return null!;
            }
        }

        public async Task<List<Stadistic>> UpsertStadistic(List<Stadistic> stadistics)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Serialize the 'calendar' object to JSON and create a StringContent
                var jsonContent = JsonConvert.SerializeObject(stadistics);

                // Send a POST request to the "Calendar" endpoint
                HttpResponseMessage response = await client.PostAsync("Stadistic/UpsertStadistic", new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode) return null;

                var data = await response.Content.ReadAsStringAsync();
                var newStadistics = JsonConvert.DeserializeObject<List<Stadistic>>(data)!;
                return newStadistics;
            }
        }

        public async Task<List<Standing>> GetCurrentStadistic(DateTime? date)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"Stadistic/GetStadistics?gameDate={date}");

                if (!response.IsSuccessStatusCode) return null;

                var data = await response.Content.ReadAsStringAsync();
                var standings = JsonConvert.DeserializeObject<List<Standing>>(data)!;
                return standings;
            }
        }
    }
}
