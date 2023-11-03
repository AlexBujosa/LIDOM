using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;
using WebLIDOM.Services;

namespace WebLIDOM.Controllers
{
    public class LidomController : Controller
    {
        private readonly ILogger<LidomController> _logger;
        private readonly LidomService _lidomService;
        private readonly IMemoryCache _memoryCache;

        public LidomController(IMemoryCache cache, ILogger<LidomController> logger, LidomService lidomService)
        {
            _logger = logger;
            _lidomService = lidomService;   
            _memoryCache = cache;   
        }

        public async Task<IActionResult> Index()
        {
            List<LidomTeam> teams = await _lidomService.GetAllTeams();

            ViewBag.lidomTeams = teams;

            _memoryCache.Set("lidomTeams", teams);

            return View();
        }

        public async Task<IActionResult> UpdateLidomTeam(UpdateLidomTeam updateLidomTeam)
        {
            List<LidomTeam> lidomTeams = (List<LidomTeam>)_memoryCache.Get("lidomTeams");

            LidomTeam lidomTeamUpdated = await _lidomService.UpdateLidomTeam(updateLidomTeam);
            int index = lidomTeams.FindIndex(value => value.Id == lidomTeamUpdated.Id);;
            lidomTeams[index] = lidomTeamUpdated;

            ViewBag.lidomTeams = lidomTeams;
            _memoryCache.Set("lidomTeams", lidomTeams);

            return RedirectToAction("Index");
        }
    }
}
