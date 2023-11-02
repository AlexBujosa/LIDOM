
namespace LIDOM.Interface
{
    public interface ICalendarRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int calendarId);
        void Insert(T newCalendar);
        void Update(T updateCalendar);
        bool Delete(int calendarId);
        void Save();
    }
}
