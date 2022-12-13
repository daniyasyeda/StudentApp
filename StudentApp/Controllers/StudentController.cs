using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repositories;
using System.Diagnostics;

namespace StudentApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IRepository<Student> StudentRepository;


        public StudentController(ILogger<StudentController> logger, IRepository<Student> studentRepository)
        {
            _logger = logger;
            StudentRepository = studentRepository;

        }
        public IActionResult Return()
        {
            return View("Index", "StudentProfile");
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
        public Student OutOf(Student model)
        {
        
            model.TotalMarks = Convert.ToInt32(model.TotalMarks*1.00 / model.OutOf * 1.00 * 100);            
            return model;
        }


        public IActionResult Index()
        {
            var a = StudentRepository.GetAll();
            return View(a);
        }   
        public IActionResult Search(string searchTerm)
        {
            var items = StudentRepository.GetAll();

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
            var items = StudentRepository.GetAll();

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
        public bool NameComparison(Student model)
        {
            var Allstudents = StudentRepository.GetAll();
            bool studentexists = false;

            foreach (Student student in Allstudents)
            {
                if (student.Name == model.Name)
                {
                    studentexists = true;
                    break;
                }

            }
            return studentexists;
        }
        [HttpPost]
        public IActionResult Create(Student model)
        {
            var StudentExists = NameComparison(model);
            if (StudentExists)
            {
                ViewBag.Message = "This Username Has already been added, please enter another";
                ModelState.Clear();
                return View();
            }
            else
            {

                TotalMarkCalculation(model);
                OutOf(model);
                ReportLetterCal(model);

                var item = new Student
                {
                    Id = model.Id,
                    Name = model.Name,

                    English = model.English,

                    Maths = model.Maths,
                    Grade = model.Grade,
                    MarkByLetter = model.MarkByLetter,
                    TotalMarks = model.TotalMarks,
                    CreatedAt = DateTimeOffset.UtcNow



                };
                StudentRepository.Add(item);
                return RedirectToAction("Index", "Student");
            }
            
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = StudentRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }
            ViewBag.Students = StudentRepository.GetAll();

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(Student model)
        {
            StudentRepository.Edit(model);

            return RedirectToAction("Index", "Student");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = StudentRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }

            StudentRepository.Delete(item);

            return RedirectToAction("Index", "Student");
        }

        public IActionResult DeleteRange()
        {
            var items = StudentRepository.GetAll();
            StudentRepository.DeleteRange(items);

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