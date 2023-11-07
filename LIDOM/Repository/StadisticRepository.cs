using LIDOM.Databases;
using LIDOM.Interface;
using LIDOM.Models;
using LIDOM.utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebLIDOM.Models.DTO;
using Calendar = LIDOM.Models.Calendar;

namespace LIDOM.Repository
{
    public class StadisticRepository : IStadisticRepository<Stadistic>
    {
        private readonly LidomDBContext _context;
        private readonly ICalendarRepository<Calendar, UpdateCalendar> _calendarRepository;

        public StadisticRepository()
        {
            _context = new LidomDBContext();
            _calendarRepository = new CalendarRespository();
        }

        public bool Delete(int Id_Calendar, int Id_Team)
        {
            Stadistic? stadistic = this.GetById(Id_Calendar, Id_Team);
            if (stadistic != null)
            {
                _context.Stadistics.Remove(stadistic);
                return true;
            }

            return false;
        }

        public IEnumerable<Stadistic> GetAll()
        {
            return _context.Stadistics.ToList();
        }

        public Stadistic GetById(int Id_Calendar, int Id_Team)
        {
            Stadistic? existingStadistic = _context.Stadistics
                .Where(s => s.Id_Calendar == Id_Calendar && s.Id_Team == Id_Team).FirstOrDefault();
            return existingStadistic!;
        }

        public void Insert(Stadistic newStadistic)
        {
            _context.Entry(newStadistic).State = EntityState.Added;
            _context.Stadistics.Add(newStadistic);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Stadistic updateStadistic)
        {
            Stadistic? existingStadistic = this.GetById(updateStadistic.Id_Calendar, updateStadistic.Id_Team);
            if (existingStadistic != null)
            {
                updateStadistic.CreatedDate = existingStadistic.CreatedDate!;
                _context.Entry(existingStadistic).State = EntityState.Detached;
                _context.Entry(updateStadistic).State = EntityState.Modified;
                _context.Stadistics.Update(updateStadistic);
            }
        }

        public bool UpsertStadisticsWithTransaction(Stadistic[] stadistics)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Stadistic? existingStadistic = this.GetById(stadistics[0].Id_Calendar, stadistics[1].Id_Team);
                    if(existingStadistic != null)
                    {
                        this.Update(stadistics[0]);
                        this.Update(stadistics[1]);
                    }

                    Calendar? calendar = _calendarRepository.GetById(stadistics[0].Id_Calendar);
                    if (calendar == null) return false;
                    calendar.Status = GameStatus.Past;

                    _calendarRepository.Save();

                    if (existingStadistic == null)
                    {
                        this.Insert(stadistics[0]);
                        this.Insert(stadistics[1]);
                    }
                    

                    this.Save();

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public List<Standing> GetCurrentStadisticsProcedure(string? dateString)
        {
            using(var context = _context)
            {
                var results = context.Standings
                    .FromSqlRaw("EXEC [dbo].[GetStanding] @GameDate = {0}", dateString);
                return results.ToList();
            }
        }


    }
}
