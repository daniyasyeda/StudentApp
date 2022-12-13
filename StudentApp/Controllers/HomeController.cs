using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repositories;

namespace StudentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<LogIn> LoginRepository;


        public HomeController(ILogger<HomeController> logger, IRepository<LogIn> loginRepository)
        {
            _logger = logger;
            LoginRepository = loginRepository;

        }
        public IActionResult Index()
        {
            return View(LoginRepository.GetAll());
        }
        public IActionResult Display()
        {
            var a = LoginRepository.GetAll();
            return View(LoginRepository.GetAll());
        }
        public LogIn UniqueCodeCal(LogIn item)
        {
            if (item.UniqueCode == "C3B2A1")
            {
                LoginRepository.Add(item);
            }
           else if (item.UniqueCode == "1A2B3C")
            {
                LoginRepository.Add(item);
            }
            else
            {
                item.UniqueCode = "Error";
                
            }
            return item;
        }
        public bool NameComparison(LogIn model)
        {
            var Allstudents = LoginRepository.GetAll();
            bool studentexists = false;

            foreach (LogIn student in Allstudents)
            {
                if (student.Name == model.Name)
                {
                    studentexists = true;
                    break;
                }

            }
            return studentexists;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LogIn model)
        {
            var login=UniqueCodeCal(model);
            if(login.UniqueCode!="Error")
            { 
            var StudentExists = NameComparison(model);
                if (StudentExists)
                {
                    ViewBag.Message = "Please provide a name that hasn't been added into the system";
                    ModelState.Clear();
                    return View();
                }
                else
                {

                    var item = new LogIn
                    {
                        Id = model.Id,
                        Name = model.Name,

                        Password = model.Password,
                        UniqueCode = model.UniqueCode,
                        CreatedDate = DateTime.Now



                    };
                    LoginRepository.Add(item);
                }
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View("UnsuccesfulLogin");
            }
         }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = LoginRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }

            LoginRepository.Delete(item);

            return RedirectToAction("Index", "LogIn");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = LoginRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }
            ViewBag.Students = LoginRepository.GetAll();

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(LogIn model)
        {
            LoginRepository.Edit(model);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteRange()
        {
            var items = LoginRepository.GetAll();
            LoginRepository.DeleteRange(items);

            return RedirectToAction("Index");
        }

    }
}
