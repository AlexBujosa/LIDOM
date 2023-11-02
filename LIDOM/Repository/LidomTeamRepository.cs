using LIDOM.Databases;
using LIDOM.Interface;
using LIDOM.Models;

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
                _context.LidomTeams.Remove(lidomTeam);
                return true;
            }
            
            return false;
        }

        public IEnumerable<LidomTeam> GetAll()
        {
            return _context.LidomTeams.ToList();

        }

        public LidomTeam GetById(int id)
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

        public void Update(LidomTeam lidomTeam)
        {
            LidomTeam? getLidomTeam = this.GetById((int)lidomTeam.Id!);
            if (getLidomTeam != null)
            {
                lidomTeam.CreatedDate = getLidomTeam.CreatedDate!;
                _context.Entry(getLidomTeam).State = EntityState.Detached;
                _context.Entry(lidomTeam).State = EntityState.Modified;
                _context.LidomTeams.Update(lidomTeam);
            }
        }
    }
}
