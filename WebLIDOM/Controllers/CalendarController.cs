using Microsoft.AspNetCore.Mvc;

namespace WebLIDOM.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
