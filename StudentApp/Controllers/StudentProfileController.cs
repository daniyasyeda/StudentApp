using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repositories;

namespace StudentApp.Controllers
{

    public class StudentProfileController : Controller
    {
        private readonly ILogger<StudentProfileController> _logger;
        private readonly IRepository<StudentProfile> StudentProfileRepository;
        private readonly IRepository<RollCall> RollCallRepository;


        public StudentProfileController(ILogger<StudentProfileController> logger, IRepository<StudentProfile> studentProfileRepository, IRepository<RollCall> rollCallRepository)
        {
            _logger = logger;            
            StudentProfileRepository = studentProfileRepository;
            RollCallRepository = rollCallRepository;
        }
        public IActionResult Index()
        {
            return View(StudentProfileRepository.GetAll());
        }
        public IActionResult Search(string searchTerm)
        {
            var items = StudentProfileRepository.GetAll();

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

        public IActionResult DeleteRange()
        {
            var items = StudentProfileRepository.GetAll();
            StudentProfileRepository.DeleteRange(items);

            return RedirectToAction("Index");
        }
        public bool NameComparison(StudentProfile model)
        {
            var Allstudents = StudentProfileRepository.GetAll();
            bool studentexists = false;

            foreach (StudentProfile student in Allstudents)
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
        public IActionResult Delete(int id)
        {
            var item = StudentProfileRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }

            StudentProfileRepository.Delete(item);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = StudentProfileRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }
            //ViewBag.Students = StudentRepository.GetAll();

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(StudentProfile item)
        {
            var model = StudentProfileRepository.Get(item.Id);

    
            StudentProfileRepository.Edit(model);

            return RedirectToAction("Index");
        }     

        [HttpPost]
        public IActionResult Create(StudentProfile model, RollCall items)
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
                var item = new StudentProfile
                {
                    Id = model.Id,
                    Name = model.Name,
                    Class = model.Class,
                    DOB = model.DOB.ToUniversalTime(),                    
                    EnrolmentDate = model.EnrolmentDate.ToUniversalTime(),
                    Parent1 = model.Parent1,
                    Parent2 = model.Parent2,
                    Parent1Phone = model.Parent1Phone,
                    Parent2Phone = model.Parent2Phone,
                    Address = model.Address

                      
            };
                StudentProfileRepository.Add(item);
                var rollcall = new RollCall
                {
                    Name =items.Name, 
                    CreatedDate = DateTime.UtcNow,
                    PartialAttendance="false"

                };
                RollCallRepository.Add(rollcall); 
                return RedirectToAction("Index");
            }

        }
    }
}
