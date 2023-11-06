using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;
using WebLIDOM.Services;
using WebLIDOM.utils;

namespace WebLIDOM.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly CalendarService _calendarService;
        private readonly DateTime _expirationDate = DateTime.Now.AddSeconds(5);
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private readonly ActionResponse _actionResponse;

        public CalendarController(IMemoryCache cache, ILogger<CalendarController> logger, CalendarService calendarService)
        {
            _logger = logger;
            _memoryCache = cache;
            _calendarService = calendarService;

            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = _expirationDate,
            };

            _actionResponse = new ActionResponse();
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
            if (newCalendar != null)
            {
                calendars.Add(newCalendar);
                _actionResponse.status = ActionStatus.Success;
                _actionResponse.message = "Nuevo Calendario Creado!!!";
            }
            else
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Upss algo ha fallado!!!";
            }

            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);

            ViewBag.calendars = calendars;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateCalendar(UpdateCalendar updateCalendar)
        {
            List<Calendar> calendars = (List<Calendar>)_memoryCache.Get("calendars");

            Calendar calendarUpdated = await _calendarService.UpdateCalendar(updateCalendar);
            int index = calendars.FindIndex(value => value.Id == calendarUpdated.Id);
           
            if (index != -1)
            {
                _actionResponse.status = ActionStatus.Success;
                _actionResponse.message = "Calendario Actualizado";
                calendars[index] = calendarUpdated;
            }
            else
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Calendario No Actualizado";
            }

            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);

            ViewBag.calendars = calendars;
            _memoryCache.Set("calendars", calendars);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteCalendar(int calendarId)
        {
            List<Calendar> calendars = (List<Calendar>)_memoryCache.Get("calendars");
            Calendar? calendar = calendars.Find(value => value.Id == calendarId);    

            if (calendar == null) return RedirectToAction("Index", "Home");

            bool calendarDeleted = await _calendarService.DeleteCalendar(calendarId);
            if (calendarDeleted)
            {
                calendars = calendars.Where(x => x.Id != calendarId).ToList();
                _actionResponse.status = ActionStatus.Success;
                _actionResponse.message = "Fecha de juego Eliminada";
            }
            else
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Upsss algo fallo...";
            }

            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);
            ViewBag.calendars = calendars;
            _memoryCache.Set("calendars", calendars);

            return RedirectToAction("Index", "Home");
        }
    }
}
