using LIDOM.Databases;
using LIDOM.Interface;
using LIDOM.Models;
using Microsoft.EntityFrameworkCore;

namespace LIDOM.Repository
{
    public class CalendarRespository : ICalendarRepository<Calendar>
    {
        private readonly LidomDBContext _context;

        public CalendarRespository()
        {
            _context = new LidomDBContext();
        }

        public bool Delete(int calendarId)
        {
            Calendar? calendar = this.GetById(calendarId);
            if (calendar != null)
            {
                _context.Calendars.Remove(calendar);
                return true;
            }

            return false;
        }

        public IEnumerable<Calendar> GetAll()
        {
            return _context.Calendars.ToList();
        }

        public Calendar GetById(int calendarId)
        {
            Calendar? calendar = _context.Calendars.Find(calendarId);
            return calendar;
        }

        public void Insert(Calendar newCalendar)
        {
            _context.Entry(newCalendar).State = EntityState.Added;
            _context.Calendars.Add(newCalendar);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Calendar updateCalendar)
        {
            
            Calendar? calendar = this.GetById((int)updateCalendar.Id!);
            if (calendar != null)
            {
                updateCalendar.CreatedDate = calendar.CreatedDate!;
                _context.Entry(calendar).State = EntityState.Detached;
                _context.Entry(updateCalendar).State = EntityState.Modified;
                _context.Calendars.Update(updateCalendar);
            }
        }
    }
}
