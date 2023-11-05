using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;

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

        public async Task<LidomTeam> CreateLidomTeam(LidomTeam lidomTeam)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders
                    .Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                // Serialize the 'calendar' object to JSON and create a StringContent
                var jsonContent = JsonConvert.SerializeObject(lidomTeam);

                // Send a POST request to the "Calendar" endpoint
                HttpResponseMessage response = await client.PostAsync("LidomTeam", new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var newLidomTeam = JsonConvert.DeserializeObject<LidomTeam>(data);
                    return newLidomTeam;
                }

                return null;
            }
        }

        public async Task<LidomTeam> UpdateLidomTeam(UpdateLidomTeam updateLidomTeam)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders
                    .Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            
                // Serialize the 'calendar' object to JSON and create a StringContent
                var jsonContent = JsonConvert.SerializeObject(updateLidomTeam);

                // Send a POST request to the "Calendar" endpoint
                HttpResponseMessage response = await client.PostAsync("LidomTeam/UpdateTeam", new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var lidomTeam = JsonConvert.DeserializeObject<LidomTeam>(data);
                    return lidomTeam!;
                }

                return null!;
            }
        }

        public async Task<bool> DeleteLidomTeam(int lidomTeamId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Make a POST request to the "LidomTeam/UpdateTeam" endpoint
                HttpResponseMessage response = await client.PostAsync($"LidomTeam/DeleteTeam?lidomTeamId={lidomTeamId}", null);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Dictionary<string, bool>>(data);
                    if(result == null) return false;
                   
                    if (result.TryGetValue("message", out bool message)) return message;
                }

                return false;
            }
        }
    }
}
