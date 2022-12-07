using Microsoft.AspNetCore.Mvc;

namespace StudentApp.Controllers
{
    public class SubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
