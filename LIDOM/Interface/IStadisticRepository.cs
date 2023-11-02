using LIDOM.Models;

namespace LIDOM.Interface
{
    public interface IStadisticRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int Id_Calendar, int Id_Team);
        void Insert(T newStadistic);
        void Update(T updateStadistic);
        bool Delete(int Id_Calendar, int Id_Team);
        public bool UpsertStadisticsWithTransaction(Stadistic[] stadistics);
        public void GetCurrentStadisticsProcedure();
        void Save();
    }
}
