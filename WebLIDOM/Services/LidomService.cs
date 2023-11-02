using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebLIDOM.Models;

namespace WebLIDOM.Services
{
    public class LidomService
    {
        public async Task<List<LidomTeam>> GetAllTeams()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders
                    .Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("LidomTeam");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var lidomTeams = JsonConvert.DeserializeObject<List<LidomTeam>>(data);
                    return lidomTeams!;
                }

                return null!;
            }
        }
    }
}
