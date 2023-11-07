using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using WebLIDOM.Models;
using WebLIDOM.Models.DTO;
using WebLIDOM.Services;
using WebLIDOM.utils;

namespace WebLIDOM.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly DateTime _expirationDate = DateTime.Now.AddSeconds(5);
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private readonly ActionResponse _actionResponse;

        public AuthenticationController(IMemoryCache cache, ILogger<AuthenticationController> logger, AuthService authService)
        {
            _logger = logger;
            _memoryCache = cache;
            _authService = authService;
            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = _expirationDate,
            };

            _actionResponse = new ActionResponse();
        }

        public IActionResult Index()
        {

            ActionResponse message = (ActionResponse)_memoryCache.Get("message");
            ViewBag.message = message;
            return View();
        }

        public async Task<IActionResult> Login(AuthDto authDto)
        {
            if (IsAnyFieldNull(authDto))
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Upps error campos incompletos!";
                _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);
                return RedirectToAction("Index");
            }

            var token = await _authService.Login(authDto);
            if(token == null)
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Upps algo ha sucedido, intente de nuevo!";
                _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);
                return RedirectToAction("Index");
            }

            _actionResponse.status = ActionStatus.Success;
            _actionResponse.message = "Se ha logeado correctamente!";
            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);
            _memoryCache.Set("authToken", token);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register(AuthDto authDto)
        {
            if (IsAnyFieldNull(authDto))
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Upps error campos incompletos!";
                _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);
                return RedirectToAction("Index");
            }

            bool action = await _authService.Register(authDto);
            if (!action)
            {
                _actionResponse.status = ActionStatus.Fail;
                _actionResponse.message = "Upps algo ha sucedido!";
                _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);
            }

            _actionResponse.status = ActionStatus.Success;
            _actionResponse.message = "Se ha registrado correctamente!";
            _memoryCache.Set("message", _actionResponse, _cacheEntryOptions);

            return RedirectToAction("Index", "Home");
        }

        public bool IsAnyFieldNull(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            return properties.Any(property => property.GetValue(obj) == null);
        }
    }
}
