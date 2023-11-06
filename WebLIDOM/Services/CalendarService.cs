using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;

namespace WebLIDOM.Services
{
    public class CalendarService
    {
        public async Task<List<Calendar>> GetAllCalendar()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders
                    .Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("Calendar");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var calendars = JsonConvert.DeserializeObject<List<Calendar>>(data);
                    return calendars!;
                }
                return null!;
            }
        }

        public async Task<Calendar> AddNewCalendar(AddNewCalendar calendar)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Serialize the 'calendar' object to JSON and create a StringContent
                var jsonContent = JsonConvert.SerializeObject(calendar);

                // Send a POST request to the "Calendar" endpoint
                HttpResponseMessage response = await client.PostAsync("Calendar", new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode) return null;

                var data = await response.Content.ReadAsStringAsync();
                var newCalendar = JsonConvert.DeserializeObject<Calendar>(data)!;
                return newCalendar;
            }
        }

        public async Task<Calendar> UpdateCalendar(UpdateCalendar updateCalendar)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Serialize the 'calendar' object to JSON and create a StringContent
                var jsonContent = JsonConvert.SerializeObject(updateCalendar);

                // Send a POST request to the "Calendar" endpoint
                HttpResponseMessage response = await client.PostAsync("Calendar/UpdateCalendar", new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode) return null;

                var data = await response.Content.ReadAsStringAsync();
                var calendarUpdated = JsonConvert.DeserializeObject<Calendar>(data)!;
                return calendarUpdated;
            }
        }

        public async Task<bool> DeleteCalendar(int calendarId)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7022/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Send a POST request to the "Calendar" endpoint
                HttpResponseMessage response = await client.PostAsync($"Calendar/DeleteCalendar?calendarId={calendarId}", null);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Dictionary<string, bool>>(data);
                    if (result == null) return false;

                    if (result.TryGetValue("message", out bool message)) return message;
                }

                return false;
            }
        }
    }
}
