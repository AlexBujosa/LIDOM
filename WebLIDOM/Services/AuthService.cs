using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebLIDOM.Models.DTO;

namespace WebLIDOM.Services
{
    public class AuthService
    {
        public async Task<bool> Register(AuthDto authDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders
                    .Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Serialize the 'calendar' object to JSON and create a StringContent
                var jsonContent = JsonConvert.SerializeObject(authDto);

                // Send a POST request to the "Calendar" endpoint
                HttpResponseMessage response = await client.PostAsync("Authentication", new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var userCreated = JsonConvert.DeserializeObject<bool>(data);
                    return userCreated;
                }
                return false;
            }
        }

        public async Task<string?> Login(AuthDto authDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders
                    .Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Authentication");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    string? token = JsonConvert.DeserializeObject<string>(data);
                    return token;
                }
                return null;
            
            }
        }
    }
}
