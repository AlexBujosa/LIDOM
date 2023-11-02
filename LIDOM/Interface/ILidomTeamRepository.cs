using LIDOM.Models;

namespace LIDOM.Interface
{
    public interface ILidomTeamRepository
    {
        IEnumerable<LidomTeam> GetAll();
        LidomTeam GetById(int id);
        void Insert(LidomTeam lidomTeam);
        void Update(LidomTeam lidomTeam);
        bool Delete(int lidomTeamId);
        void Save();
    }
}
