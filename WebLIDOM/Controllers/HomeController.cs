using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;
using System.Web;
using Microsoft.Extensions.Caching.Memory;

namespace WebLIDOM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;

        public HomeController(IMemoryCache cache, ILogger<HomeController> logger)
        {
            _logger = logger;
            _memoryCache = cache;   
        }
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
                    return lidomTeams;
                }

                return null;
            }
        }

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
                    return calendars;
                }
                return null;
            }
        }

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
                    return stadistics;
                }
                return null;
            }
        }


        public async Task<IActionResult> AddNewCalendar(AddNewCalendar calendar)
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

                if(response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var newCalendar = JsonConvert.DeserializeObject<Calendar>(data);
                    List<Calendar> calendars = (List<Calendar>)_memoryCache.Get("calendars");
                    calendars.Add(newCalendar);
                    ViewBag.calendars = calendars;
                }

            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Index()
        {
            List<LidomTeam> teams = await this.GetAllTeams();
            List<Calendar> calendars = await this.GetAllCalendar();
            List<Stadistic> stadistics = await this.GetAllStadistic();   

            ViewBag.lidomTeams = teams;
            ViewBag.calendars = calendars;
            ViewBag.stadistics = stadistics;

            _memoryCache.Set("lidomTeams", teams);
            _memoryCache.Set("calendars", calendars);
            _memoryCache.Set("stadistics", stadistics);


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}