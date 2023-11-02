
namespace LIDOM.Interface
{
    public interface ICalendarRepository<T, U>
    {
        IEnumerable<T> GetAll();
        T GetById(int calendarId);
        void Insert(T newCalendar);
        T Update(U updateCalendar);
        bool Delete(int calendarId);
        void Save();
    }
}
