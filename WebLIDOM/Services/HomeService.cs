using Microsoft.Extensions.Caching.Memory;
using WebLIDOM.Controllers;
using WebLIDOM.Models;

namespace WebLIDOM.Services
{
    public class HomeService
    {
        private readonly CalendarService _calendarService;
        private readonly StadisticService _statisticService;
        private readonly LidomService _lidomService;
        public HomeService(CalendarService calendarService, StadisticService statisticService, LidomService lidomService)
        {
            _calendarService = calendarService;
            _statisticService = statisticService;   
            _lidomService = lidomService;
        }

        public async Task<LidomInfo> GetAllServiceInfo()
        {
            List<LidomTeam> teams = await _lidomService.GetAllTeams();
            List<Calendar> calendars = await _calendarService.GetAllCalendar();
            List<Stadistic> stadistics = await _statisticService.GetAllStadistic();

            LidomInfo info = new LidomInfo() {
                Teams = teams,
                Calendars = calendars,
                Stadistics = stadistics
            };

            return info;

        }
    }
}
