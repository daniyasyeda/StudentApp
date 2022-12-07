using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repositories;

namespace StudentApp.Controllers
{
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public LogInController(ILogger<LogInController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            return View(_unitOfWork.Logins.GetAll());
        }
        public LogIn UniqueCodeCal(LogIn item)
        {
            if (item.UniqueCode == "C3B2A1")
            {
                _unitOfWork.Logins.Add(item);
            }
           else if (item.UniqueCode == "1A2B3C")
            {
                _unitOfWork.Logins.Add(item);
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
            _unitOfWork.Logins.Add(item);

            return RedirectToAction("Index","Login");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _unitOfWork.Logins.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }

            _unitOfWork.Logins.Delete(item);

            return RedirectToAction("Index", "LogIn");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _unitOfWork.Logins.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }
            ViewBag.Students = _unitOfWork.Logins.GetAll();

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(LogIn model)
        {
            _unitOfWork.Logins.Edit(model);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteRange()
        {
            var items = _unitOfWork.Logins.GetAll();
            _unitOfWork.Logins.DeleteRange(items);

            return RedirectToAction("Index");
        }

    }
}
