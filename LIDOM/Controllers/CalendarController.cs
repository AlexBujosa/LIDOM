using LIDOM.Interface;
using LIDOM.Models;
using LIDOM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : Controller
    {
        private readonly ICalendarRepository<Calendar> _calendarRepository;
        public CalendarController()
        {
            _calendarRepository = new CalendarRespository();
        }

        [HttpGet]
        public IEnumerable<Calendar> Index()
        {
            IEnumerable<Calendar> calendars = _calendarRepository.GetAll();
            return calendars;
        }

        [HttpPost]
        public IActionResult AddNewCalendar(Calendar calendar)
        {
            if (!ModelState.IsValid) return Ok(null);
            _calendarRepository.Insert(calendar);
            _calendarRepository.Save();
            return Ok(calendar);
        }

        [HttpPost("UpdateCalendar")]
        public IActionResult UpdateTeam(Calendar calendar)
        {
            if (!ModelState.IsValid) return Ok(null);
            _calendarRepository.Update(calendar);
            _calendarRepository.Save();
            return Ok(calendar);
        }

        [HttpPost("DeleteCalendar")]
        public IActionResult DeleteTeam(int calendarId)
        {
           
            if (!ModelState.IsValid) return Ok(null);

            bool deleteLidomTeam = _calendarRepository.Delete(calendarId);
            if(!deleteLidomTeam) return Ok(new { message = "No Existe esa juego agendado!" });

            _calendarRepository.Save();

            return Ok(new { message = "Juego agendado eliminado!" });
        }
    }
}
