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
            List<Calendar> calendars = (List<Calendar>)_memoryCache.Get("calendars");

            Calendar newCalendar = await _calendarService.AddNewCalendar(calendar);
            
            calendars.Add(newCalendar);
            ViewBag.calendars = calendars;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateCalendar(UpdateCalendar updateCalendar)
        {
            List<Calendar> calendars = (List<Calendar>)_memoryCache.Get("calendars");

            Calendar calendarUpdated = await _calendarService.UpdateCalendar(updateCalendar);
            int index = calendars.FindIndex(value => value.Id == calendarUpdated.Id);
            calendars[index] = calendarUpdated;

            ViewBag.calendars = calendars;
            _memoryCache.Set("calendars", calendars);

            return RedirectToAction("Index", "Home");
        }
    }
}
