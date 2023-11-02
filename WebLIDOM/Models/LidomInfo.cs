using WebLIDOM.Services;

namespace WebLIDOM.Models
{
    public class LidomInfo
    {
        public List<LidomTeam> Teams { get; set; }
        public List<Calendar> Calendars { get;set; }
        public List<Stadistic> Stadistics { get; set; }
    }
}
