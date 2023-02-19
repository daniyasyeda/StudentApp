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
           return View();
           
        }
        public IActionResult Logins()
        {
            return View(LoginRepository.GetAll());

        }

        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Done()
        {
            return View("Index");
        }
        public IActionResult ViewProfile()
        {
            return View(LoginRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        public IActionResult ConstructLoad()
        {
            return View(); 

        }
        public IActionResult Success() 
        {
            return View();

        }

        public IActionResult Privacy()
        {
            return View();

        }
     



        [HttpPost]
        public IActionResult Login(LogIn model)
        {
            var Allstudents = LoginRepository.GetAll();
            bool studentExists = false;
            string StudentName = "";
            foreach (LogIn student in Allstudents)
            {
                if (student.Name == model.Name)
                {
                    if (student.Password == model.Password)
                    {
                        studentExists = true;
                        StudentName = student.Name;
                        break;
                    }
                }            
            }

            if(studentExists)
            { 
                ViewBag.Message = StudentName;
                return View("Success");
            }
            else
            {
                ViewBag.Message = "This Name doesn't exist, Please register it";
                return View("UnsuccesfulLogIn");
            }
            

        }

        public IActionResult Search(string searchTerm)
        {
            var items = LoginRepository.GetAll();

            if (searchTerm == null)
            {
                return View("Index", items);
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var filteredItems = items.Where(x =>
                x.Name.ToLower().Contains(lowerCaseSearchTerm)
                || x.Name.ToLower().Contains(lowerCaseSearchTerm));

            return View("Index", filteredItems);
        }

        public LogIn UniqueCodeCal(LogIn item)
        {
            if(! (item.UniqueCode == "C3B2A1" || item.UniqueCode == "1A2B3C" || item.UniqueCode == "A1B2C3" || item.UniqueCode == "3C2B1A"))
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
            //RedirectToAction("Index");
            return studentexists;
        }
        public bool ClassComparison(LogIn model)
        {
            var Allstudents = LoginRepository.GetAll();
            bool studentexists = false;

            foreach (LogIn student in Allstudents)
            {
                if (student.Class == model.Class)
                {
                    studentexists = true;
                    break;
                }

            }
            //RedirectToAction("Index");
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
            if (login.UniqueCode == "Error")
            {
                ViewBag.Message = "Use Another Code, this one isn't right";
                ModelState.Clear();
                return View();
            }
            else
            {
                var StudentExists = NameComparison(model);
                var StudentExist = ClassComparison(model);
                if (StudentExists)
                {
                    ViewBag.Message = "Please provide a name that hasn't been added into the system";
                    ModelState.Clear();
                    return View();
                }


                   else if (StudentExist)
                    {
                        ViewBag.Message = "A teacher has already taken this class";
                        ModelState.Clear();
                        return View();
                    }

                else
                {
                    var item = new LogIn
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Class = model.Class,
                        Password = model.Password,
                        UniqueCode = model.UniqueCode,
                        CreatedDate = DateTime.Now

                    };
                    LoginRepository.Add(item);
                    return RedirectToAction("Index");
                }
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

            return RedirectToAction("Logins", "Home");
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
