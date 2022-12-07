using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repositories;

namespace StudentApp.Controllers
{
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;
        private readonly IRepository<LogIn> _loginStore;


        public LogInController(ILogger<LogInController> logger, IRepository<LogIn> loginStore)
        {
            _logger = logger;
            _loginStore = loginStore;

        }
        public IActionResult Index()
        {
            return View();
        }
        public LogIn UniqueCodeCal(LogIn item)
        {
            if (item.UniqueCode == "C3B2A1")
            {
                _loginStore.Add(item);
            }
           else if (item.UniqueCode == "1A2B3C")
            {
                _loginStore.Add(item);
            }
            else
            {

            }
            return item;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LogIn model)
        {


            var item = new LogIn
            {
                Id = model.Id,
                Name = model.Name,

                Password = model.Password,
                UniqueCode = model.UniqueCode,
                CreatedDate = DateTime.Now



            };
            _loginStore.Add(item);

            return RedirectToAction("Index", "LogIn");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _loginStore.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }

            _loginStore.Delete(item);

            return RedirectToAction("Index", "LogIn");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _loginStore.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }
            ViewBag.Students = _loginStore.GetAll();

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(LogIn model)
        {
            _loginStore.Edit(model);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteRange()
        {
            var items = _loginStore.GetAll();
            _loginStore.DeleteRange(items);

            return RedirectToAction("Index");
        }

    }
}
