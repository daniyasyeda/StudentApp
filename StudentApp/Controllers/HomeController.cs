using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repositories;
using System.Diagnostics;

namespace StudentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }

        public Student TotalMarkCalculation(Student model)
        {
            model.TotalMarks = (model.English + model.Maths) / 2 ; 
            
            return model;
        }
        public Student ReportLetterCal(Student model)
        {
          if (model.TotalMarks >= 90)
            {
                model.MarkByLetter = "A (Outstanding)";
            }
            else if (model.TotalMarks >= 80)
            {
                model.MarkByLetter = "B (Good)";
            }
            else if (model.TotalMarks >= 70)
            {
                model.MarkByLetter = "C (Satisfactory)";
            }
            else if (model.TotalMarks >= 50)
            {
                model.MarkByLetter = "D (Limited)";
            }
            else if (model.TotalMarks <= 49)
            {
                model.MarkByLetter = "E (Not Passed)";
            }
       
            return model;
        }
        public Student ReportLetter(Student model)
        {
            if (model.OutOf == 10)
            {
                model.TotalMarks = model.TotalMarks * 10;
            }
           else if (model.OutOf == 20)
            {
                model.TotalMarks = model.TotalMarks * 5;
            }
            else if (model.OutOf == 25)
            {
                model.TotalMarks = model.TotalMarks * 4;
            }
            else if (model.OutOf == 50)
            {
                model.TotalMarks = model.TotalMarks * 2;
            }
            else if (model.OutOf == 100)
            {
                model.TotalMarks = model.TotalMarks * 5;
            }
            return model;
        }


        public IActionResult Index()
        {
            return View(_unitOfWork.Students.GetAll());
        }
        public IActionResult Search(string searchTerm)
        {
            var items = _unitOfWork.Students.GetAll();

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Sort(string sortBy, string orderBy)
        {
            var items = _unitOfWork.Students.GetAll();

            if (sortBy == "CreatedAt")
            {
                items = orderBy == "Asc" ? items.OrderBy(x => x.CreatedAt) : items.OrderByDescending(x => x.CreatedAt);

            }
            else if (sortBy == "TotalMarks")
            {
                items = orderBy == "Asc" ? items.OrderBy(x => x.TotalMarks) : items.OrderByDescending(x => x.TotalMarks);

            }
            else if (sortBy == "English")
            {
                items = orderBy == "Asc" ? items.OrderBy(x => x.English) : items.OrderByDescending(x => x.English);

            }
            else if (sortBy == "Maths")
            {
                items = orderBy == "Asc" ? items.OrderBy(x => x.Maths) : items.OrderByDescending(x => x.Maths);

            }
            else if (sortBy == "Grade")
            {
                items = orderBy == "Asc" ? items.OrderBy(x => x.Grade) : items.OrderByDescending(x => x.Grade);

            }

            return View("Index", items);
        }
        [HttpPost]
        public IActionResult Create(Student model)
        {
            TotalMarkCalculation(model);
            ReportLetterCal(model);
            ReportLetter(model);
            
            var item = new Student
            {
                Id = model.Id,
                Name = model.Name,
 
                English = model.English,

                Maths = model.Maths,
                Grade = model.Grade,
                MarkByLetter = model.MarkByLetter,
                TotalMarks= model.TotalMarks,
                CreatedAt = DateTimeOffset.UtcNow
                
                

            };
            _unitOfWork.Students.Add(item);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _unitOfWork.Students.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }
            ViewBag.Students = _unitOfWork.Students.GetAll();

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(Student model)
        {
            _unitOfWork.Students.Edit(model);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _unitOfWork.Students.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }

            _unitOfWork.Students.Delete(item);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteRange()
        {
            var items = _unitOfWork.Students.GetAll();
            _unitOfWork.Students.DeleteRange(items);

            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}