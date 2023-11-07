using LIDOM.Interface;
using LIDOM.Models;
using LIDOM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LIDOM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StadisticController : Controller
    {
        private readonly IStadisticRepository<Stadistic> _stadisticRepository;
        public StadisticController()
        {
            _stadisticRepository = new StadisticRepository();
        }

        [HttpGet]
        public IEnumerable<Stadistic> Index()
        {
            IEnumerable<Stadistic> stadistics = _stadisticRepository.GetAll();
            return stadistics;
        }

        [HttpPost("UpsertStadistic")]
        public IActionResult UpdateStadistic(Stadistic[] stadistics)
        {
            if (!ModelState.IsValid) return Ok(null);
            if (stadistics.Length != 2) return Ok(null);

            bool transactionUpdated = _stadisticRepository.UpsertStadisticsWithTransaction(stadistics);
            if(!transactionUpdated) return Ok(null);

            return Ok(stadistics);
        }

        [HttpGet("GetStadistics")]
        public IActionResult GetCurrentStadistic(DateTime? gameDate)
        {
            if (!ModelState.IsValid) return Ok(null);
            string? formattedDate = gameDate != null ? gameDate.Value.ToString("MM-dd-yyyy") : null;
            var standings =_stadisticRepository.GetCurrentStadisticsProcedure(formattedDate);

            return Ok(standings);
        }

        [HttpPost("DeleteStadistic")]
        public IActionResult DeleteStadistic(int Id_Calendar, int Id_Team)
        {

            if (!ModelState.IsValid) return Ok(null);

            bool deleteStadistic = _stadisticRepository.Delete(Id_Calendar, Id_Team);
            if (!deleteStadistic) return Ok(new { message = "No Existe esa estadistica!" });

            _stadisticRepository.Save();

            return Ok(new { message = "Estadistica eliminado!" });
        }
    }
}
