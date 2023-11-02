using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;
using WebLIDOM.Services;

namespace WebLIDOM.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly CalendarService _calendarService;

        public CalendarController(IMemoryCache cache, ILogger<CalendarController> logger, CalendarService calendarService)
        {
            _logger = logger;
            _memoryCache = cache;
            _calendarService = calendarService; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<List<Calendar>> GetAllCalendar()
        {
           return await _calendarService.GetAllCalendar();
        }

        public async Task<IActionResult> AddNewCalendar(AddNewCalendar calendar)
        {
            List<Calendar> calendars = (List<Calendar>)_memoryCache.Get("calendar");

            Calendar newCalendar = await _calendarService.AddNewCalendar(calendar);
            
            calendars.Add(newCalendar);
            ViewBag.calendars = calendars;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateCalendar(UpdateCalendar updateCalendar)
        {
            Calendar newCalendar = await _calendarService.AddNewCalendar(calendar);

            calendars.Add(newCalendar);
            ViewBag.calendars = calendars;
            return RedirectToAction("Index", "Home");
        }
    }
}
