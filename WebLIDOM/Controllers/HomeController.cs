using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;
using System.Web;
using Microsoft.Extensions.Caching.Memory;
using WebLIDOM.Services;

namespace WebLIDOM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly HomeService _homeService;

        public HomeController(IMemoryCache cache, ILogger<HomeController> logger, HomeService homeService)
        {
            _logger = logger;
            _memoryCache = cache;   
            _homeService = homeService; 

        }

        public async Task<IActionResult> Index()
        {
            LidomInfo info = await _homeService.GetAllServiceInfo();

            ViewBag.lidomTeams = info.Teams;
            ViewBag.calendars = info.Calendars;
            ViewBag.stadistics = info.Stadistics;

            ActionResponse message = (ActionResponse)_memoryCache.Get("message");
            ViewBag.message = message;

            _memoryCache.Set("lidomTeams", info.Teams);
            _memoryCache.Set("calendars", info.Calendars);
            _memoryCache.Set("stadistics", info.Stadistics);


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