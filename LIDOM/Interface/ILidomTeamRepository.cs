using LIDOM.Models;
using LIDOM.Models.DTO;

namespace LIDOM.Interface
{
    public interface ILidomTeamRepository
    {
        IEnumerable<LidomTeam> GetAll();
        LidomTeam GetById(int id);
        void Insert(LidomTeam lidomTeam);
        LidomTeam Update(UpdateLidomTeam updateLidomTeam);
        bool Delete(int lidomTeamId);
        void Save();
    }
}
