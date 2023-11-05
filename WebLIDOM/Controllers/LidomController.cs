using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;
using WebLIDOM.Services;
using WebLIDOM.utils;

namespace WebLIDOM.Controllers
{
    public class LidomController : Controller
    {
        private readonly ILogger<LidomController> _logger;
        private readonly LidomService _lidomService;
        private readonly IMemoryCache _memoryCache;
        private readonly DateTime _expirationDate = DateTime.Now.AddSeconds(5);
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private readonly ActionResponse _actionResponse;

        public LidomController(IMemoryCache cache, ILogger<LidomController> logger, LidomService lidomService)
        {
            _logger = logger;
            _lidomService = lidomService;   
            _memoryCache = cache;   

            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = _expirationDate,
            };

            _actionResponse = new ActionResponse();

        }

        public async Task<IActionResult> Index()
        {
            List<LidomTeam> teams = await _lidomService.GetAllTeams();
            ActionResponse message = (ActionResponse)_memoryCache.Get("message");
            ViewBag.lidomTeams = teams;
            ViewBag.message = message;
            _memoryCache.Set("lidomTeams", teams);

            return View();
        }

        public async Task<IActionResult> CreateLidomTeam(LidomTeam lidomTeam)
        {
            List<LidomTeam> lidomTeams = (List<LidomTeam>)_memoryCache.Get("lidomTeams");

            LidomTeam newLidomTeam= await _lidomService.CreateLidomTeam(lidomTeam);
            if (lidomTeam != null)
            {
                lidomTeams.Add(newLidomTeam);
                _actionResponse.status = ActionStatus.Success;
                _actionResponse.message = "Nuevo Equipo Creado!!!";
            }
            else
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Equipo no creado!!!";
            }
            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);

            ViewBag.lidomTeams = lidomTeams;
            _memoryCache.Set("lidomTeams", lidomTeams);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateLidomTeam(UpdateLidomTeam updateLidomTeam)
        {
            List<LidomTeam> lidomTeams = (List<LidomTeam>)_memoryCache.Get("lidomTeams");

            LidomTeam lidomTeamUpdated = await _lidomService.UpdateLidomTeam(updateLidomTeam);
            int index = lidomTeams.FindIndex(value => value.Id == lidomTeamUpdated.Id);

            if (index != -1) {
                _actionResponse.status = ActionStatus.Success;
                _actionResponse.message = "Equipo Actualizado";
                lidomTeams[index] = lidomTeamUpdated;
            }
            else
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Equipo No Actualizado";
            }
            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);

            ViewBag.lidomTeams = lidomTeams;
            _memoryCache.Set("lidomTeams", lidomTeams);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteLidomTeam(int lidomTeamId)
        {
            List<LidomTeam> lidomTeams = (List<LidomTeam>)_memoryCache.Get("lidomTeams");
            LidomTeam? lidomTeam = lidomTeams.Find(x => x.Id == lidomTeamId);

            if (lidomTeam == null) return RedirectToAction("Index");

            bool lidomTeamDeleted = await _lidomService.DeleteLidomTeam(lidomTeamId);


            if (lidomTeamDeleted)
            {
                lidomTeams = lidomTeams.Where(x => x.Id != lidomTeamId).ToList();

                _actionResponse.status = ActionStatus.Success;
                _actionResponse.message = "Equipo Eliminado";
            }else
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Equipo No Eliminado";
            }

            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);

            ViewBag.lidomTeams = lidomTeams;
            _memoryCache.Set("lidomTeams", lidomTeams);
            
            return RedirectToAction("Index");
        }
    }
}
