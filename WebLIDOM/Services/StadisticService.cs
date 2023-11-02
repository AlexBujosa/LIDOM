using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebLIDOM.Models;

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
    }
}
