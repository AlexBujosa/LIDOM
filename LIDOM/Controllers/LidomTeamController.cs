using LIDOM.Interface;
using LIDOM.Models;
using LIDOM.Models.DTO;
using LIDOM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LidomTeamController : Controller
    {
        private readonly ILidomTeamRepository _lidomTeamRepository;
        public LidomTeamController()
        {
            _lidomTeamRepository = new LidomTeamRepository();
        }

        [HttpGet]
        public IEnumerable<LidomTeam> Index()
        {
            IEnumerable<LidomTeam> lidomTeams = _lidomTeamRepository.GetAll();
            return lidomTeams;
        }

        [HttpPost]
        public IActionResult AddTeam(LidomTeam lidomTeam)
        {
            if (!ModelState.IsValid) return Ok(null);
            _lidomTeamRepository.Insert(lidomTeam);
            _lidomTeamRepository.Save();
            return Ok(lidomTeam);
        }

        [HttpPost("UpdateTeam")]
        public IActionResult UpdateTeam(UpdateLidomTeam updateLidomTeam)
        {
            if (!ModelState.IsValid) return Ok(null);
            var lidomTeam = _lidomTeamRepository.Update(updateLidomTeam);
            _lidomTeamRepository.Save();
            return Ok(lidomTeam);
        }

        [HttpPost("DeleteTeam")]
        public IActionResult DeleteTeam(int lidomTeamId)
        {
           
            if (!ModelState.IsValid) return Ok(null);

            bool deleteLidomTeam = _lidomTeamRepository.Delete(lidomTeamId);
            if(!deleteLidomTeam) return Ok(new { message = "No Existe el equipo!" });

            _lidomTeamRepository.Save();

            return Ok(new { message = "Equipo Eliminado!" });
        }
    }
}
