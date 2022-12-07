using Microsoft.AspNetCore.Mvc;

namespace StudentApp.Controllers
{
    public class StudentProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
