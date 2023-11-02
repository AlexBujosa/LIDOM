using Microsoft.AspNetCore.Mvc;

namespace WebLIDOM.Controllers
{
    public class LidomController : Controller
    {
        private readonly ILogger<LidomController> _logger;

        public LidomController(ILogger<LidomController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
