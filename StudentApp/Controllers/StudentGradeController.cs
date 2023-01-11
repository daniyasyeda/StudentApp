using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repositories;
using System.Diagnostics;

namespace StudentApp.Controllers
{
    public class StudentGradeController : Controller
    {
        private readonly ILogger<StudentGradeController> _logger;
        private readonly IRepository<StudentGrade> StudentGradeRepository;
        private readonly IRepository<StudentProfile> StudentProfileRepository;
        private readonly IRepository<RollCall> RollCallRepository;


        public StudentGradeController(ILogger<StudentGradeController> logger, IRepository<StudentGrade> studentRepository, IRepository<StudentProfile> studentProfileRepository, IRepository<RollCall> rollCallRepository)
        {
            _logger = logger;
            StudentGradeRepository = studentRepository;
            StudentProfileRepository = studentProfileRepository;
            RollCallRepository = rollCallRepository;
        }
        public IActionResult Return()
        {
            return View("Index", "StudentProfile");
        }

        public StudentGrade TotalMarkCalculation(StudentGrade model)
        {
            model.TotalMarks = (model.English + model.Maths) / 2 ; 
            
            return model;
        }
        public StudentGrade ReportLetterCal(StudentGrade model)
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
        public StudentGrade OutOf(StudentGrade model)
        {
        
            model.TotalMarks = Convert.ToInt32(model.TotalMarks*1.00 / model.OutOf * 1.00 * 100);            
            return model;
        }


        public IActionResult Index()
        {
        
            return View(StudentGradeRepository.GetAll());
        }   
        public IActionResult Search(string searchTerm)
        {
            var items = StudentGradeRepository.GetAll();

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

       
    
        public bool NameComparison(StudentGrade model)
        {
            var Allstudents = StudentGradeRepository.GetAll();
            bool studentexists = false;

            foreach (StudentGrade student in Allstudents)
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
        public IActionResult Create(int id)
        {
            var studentProfile = StudentProfileRepository.Get(id);                       
            
            var studentgrade = StudentGradeRepository.Get(studentProfile.Name,studentProfile.Class);
            if(studentgrade==null)
            {
                var newstudentgrade = new StudentGrade
                {
                    Name = studentProfile.Name,
                    Class=studentProfile.Class
                };
                return View(newstudentgrade);
            }
            else
            {
                return View(studentgrade);
            }
            
        }
        [HttpPost]
        public IActionResult Create(StudentGrade model)
        {
            var StudentExists = NameComparison(model);
            if (StudentExists)
            {
                ViewBag.Message = "This Student Name Has already been added, please enter another";
                ModelState.Clear();
                return View();
            }
            else
            {
                TotalMarkCalculation(model);
                OutOf(model);
                ReportLetterCal(model);

                var item = new StudentGrade
                {                    
                    Name = model.Name,
                    English = model.English,
                    OutOf = model.OutOf,
                    Maths = model.Maths,
                    Class = model.Class,
                    MarkByLetter = model.MarkByLetter,
                    TotalMarks = model.TotalMarks,
                    CreatedAt = DateTimeOffset.UtcNow

                };
                StudentGradeRepository.Add(item);
                return RedirectToAction("Index");
            }
            
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = StudentGradeRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }
            //ViewBag.Students = StudentRepository.GetAll();

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(StudentGrade item)
        {
            var model = StudentGradeRepository.Get(item.Id);

            TotalMarkCalculation(model);
            OutOf(model);
            ReportLetterCal(model);
            StudentGradeRepository.Edit(model);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = StudentGradeRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }

            StudentGradeRepository.Delete(item);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteRange()
        {
            var items = StudentGradeRepository.GetAll();
            StudentGradeRepository.DeleteRange(items);

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