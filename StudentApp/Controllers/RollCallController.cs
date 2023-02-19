using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Repositories;

namespace StudentApp.Controllers
{
    public class RollCallController : Controller
    {
        private readonly ILogger<RollCallController> _logger;
        private readonly IRepository<RollCall> RollCallRepository;
        private readonly IRepository<StudentProfile> StudentProfileRepository;


        public RollCallController(ILogger<RollCallController> logger, IRepository<RollCall> rollcallRepository, IRepository<StudentGrade> studentRepository)
        {
            _logger = logger;
            RollCallRepository = rollcallRepository;

        }
        [HttpPost]
        public IActionResult Edit(int id, IFormCollection model)
        {
            var PA = model["PartialAttendance"];
            bool ishere = false;
            ishere = model["IsHere"]=="on"?true:false;
            DateTime CreatedDate = new DateTime();
            DateTime.TryParse(model["CreatedDateTime"], out CreatedDate);
            DateTime earlydeparttimeonly = new DateTime() ;
            DateTime.TryParse(model["EarlyDepartureTime"], out earlydeparttimeonly);
            DateTime latearrivaltimeonly = new DateTime();
            DateTime.TryParse(model["LateArrivalTime"], out latearrivaltimeonly);            
            var item = new RollCall
            {
                Id = id,
                Name = model["Name"],
                PartialAttendance = PA,
                EarlyDepartTime = Convert.ToDateTime(CreatedDate.ToString("dd/MM/yyyy") + " " + earlydeparttimeonly.ToString("HH:mm:ss")),
                LateArrivalTime = Convert.ToDateTime(CreatedDate.ToString("dd/MM/yyyy") + " " + latearrivaltimeonly.ToString("HH:mm:ss")),
                IsHere = ishere,
                CreatedDate = CreatedDate

            };
            RollCallRepository.Edit(item);
            return RedirectToAction("Index");
        }
        public IActionResult PartialAttendance(RollCall model)
        {
            if (model.PartialAttendance == "LateArrival")
            {

            }
            return View();
        }
        public IActionResult EarlyDepartTime()
        {
            return View();
        }
        public IActionResult Fill(RollCall model)
        {
            ViewBag.Message = model.Name;
           
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            //var Allstudents = StudentProfileRepository.GetAll();
            //foreach (StudentProfile item in Allstudents)
            //{


            //    var items = new RollCall
            //    {
            //        Name = model.Name,

            //        CreatedDate = DateTime.Now

            //    };
            //    RollCallRepository.Add(items);
            //    return RedirectToAction("Index");
            //}

            return View("Index");
        }

        public IActionResult Index()
        {
            return View(RollCallRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = RollCallRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }

            RollCallRepository.Delete(item);

            return RedirectToAction("Success", "Home");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = RollCallRepository.Get(id);

            if (item == null)
            {
                ViewBag.ErrorMessage = $"An item with the id {id} was not found";
                return View("NotFound");
            }


            return View(item);
        }
        [HttpPost]
        //public IActionResult Edit(RollCall model)
        //{
        //    RollCallRepository.Edit(model);

        //    return RedirectToAction("Index", "Home");
        //}

        public IActionResult DeleteRange()
        {
            var items = RollCallRepository.GetAll();
            RollCallRepository.DeleteRange(items);

            return RedirectToAction("Index");
        }
    }
}
