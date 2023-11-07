using Microsoft.AspNetCore.Mvc;
using WebLIDOM.Models.DTO;
using WebLIDOM.Models;
using WebLIDOM.Services;
using WebLIDOM.utils;
using Microsoft.Extensions.Caching.Memory;

namespace WebLIDOM.Controllers
{
    public class StadisticController : Controller
    {
        private readonly ILogger<StadisticController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly StadisticService _stadisticService;
        private readonly DateTime _expirationDate = DateTime.Now.AddSeconds(5);
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private readonly ActionResponse _actionResponse;

        public StadisticController(IMemoryCache cache, ILogger<StadisticController> logger, StadisticService stadisticService)
        {
            _logger = logger;
            _memoryCache = cache;
            _stadisticService = stadisticService;

            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = _expirationDate,
            };

            _actionResponse = new ActionResponse();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddStadistic(AddStadistic addStadistic)
        {
            List<Stadistic> stadisticList = new List<Stadistic>();

            if (addStadistic != null && addStadistic.Winner != null)
            {
                stadisticList.Add(new Stadistic()
                {
                    Id_Calendar = addStadistic.Id_Calendar,
                    Id_Team = addStadistic.Id_FirstTeam,
                    Win = addStadistic.Winner == addStadistic.Id_FirstTeam,
                });

                stadisticList.Add(new Stadistic()
                {
                    Id_Calendar = addStadistic.Id_Calendar,
                    Id_Team = addStadistic.Id_SecondTeam,
                    Win = addStadistic.Winner == addStadistic.Id_SecondTeam,
                });
                
               var newStadisticList = await  _stadisticService.UpsertStadistic(stadisticList);
               
                if(newStadisticList != null)
                {
                    _actionResponse.status = ActionStatus.Success;
                    _actionResponse.message = "Se ha guardado el ganador!!!";
                }
                else
                {
                    _actionResponse.status = ActionStatus.Fail;
                    _actionResponse.message = "Upss algo ha fallado!!!";
                }
            }
            else
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Upss algo ha fallado!!!";
            }

            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);

            return RedirectToAction("Index", "Home");
        }
    }
}
