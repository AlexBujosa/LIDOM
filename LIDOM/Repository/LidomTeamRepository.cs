using LIDOM.Databases;
using LIDOM.Interface;
using LIDOM.Models;
using LIDOM.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace LIDOM.Repository
{
    public class LidomTeamRepository : ILidomTeamRepository
    {
        private readonly LidomDBContext _context;

        public LidomTeamRepository()
        {
            _context = new LidomDBContext();
        }

        public bool Delete(int lidomTeamId)
        {
            LidomTeam? lidomTeam = this.GetById(lidomTeamId);
            if(lidomTeam != null)
            {
                var relatedCalendars = _context.Calendars.Where(c => c.Id_FirstTeam == lidomTeamId || c.Id_SecondTeam == lidomTeamId);
                _context.Calendars.RemoveRange(relatedCalendars);
                _context.LidomTeams.Remove(lidomTeam);
                return true;
            }
            
            return false;
        }

        public IEnumerable<LidomTeam> GetAll()
        {
            return _context.LidomTeams.ToList();

        }

        public LidomTeam? GetById(int id)
        {
            LidomTeam? lidomTeam = _context.LidomTeams.Find(id);
            return lidomTeam;
        }

        public void Insert(LidomTeam lidomTeam)
        {
            _context.Entry(lidomTeam).State = EntityState.Added;
            _context.LidomTeams.Add(lidomTeam);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public LidomTeam? Update(UpdateLidomTeam updateLidomTeam)
        {
            LidomTeam? lidomTeam = this.GetById((int)updateLidomTeam.Id!);
            if (lidomTeam != null)
            {
                if (updateLidomTeam.Name != null)  lidomTeam.Name = updateLidomTeam.Name;

                if (updateLidomTeam.Home != null) lidomTeam.Home = updateLidomTeam.Home;

                _context.Entry(lidomTeam).State = EntityState.Detached;
                _context.Entry(lidomTeam).State = EntityState.Modified;
                _context.LidomTeams.Update(lidomTeam);
                return lidomTeam;
            }
            return null;
        }
    }
}
