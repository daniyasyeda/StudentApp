using Microsoft.AspNetCore.Mvc;
using StudentApp.Contexts;
using StudentApp.Models;
using StudentApp.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace StudentApp.Repositories
{
    public class RollCallRepository : IRepository<RollCall>
    {

        private readonly ApplicationDbContext _db;

        public RollCallRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(RollCall model)
        {
            var item = new RollCall
            {                
                Name = model.Name,
                IsHere = model.IsHere,
                EarlyDepartTime= DateTime.Now,
                LateArrivalTime= DateTime.Now,
                PartialAttendance= model.PartialAttendance,
                //Time = TimeOnly.FromDateTime(DateTime.Now),
                CreatedDate = DateTime.Now

            };
            _db.RollCalls.Add(item);
            _db.SaveChanges();
        }

        private string ToShortTimeString(TimeOnly timeOnly)
        {
            throw new NotImplementedException();
        }

        public void Delete(RollCall item)
        {
            _db.RollCalls.Remove(item);
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<RollCall> items)
        {
            _db.RollCalls.RemoveRange(items);
            _db.SaveChanges();
        }

        public RollCall Get(int id)
        {
            return _db.RollCalls.Find(id);
        }

        public IEnumerable<RollCall> GetAll()
        {
            return _db.RollCalls;
        }
        public void Edit(RollCall item)
        {
            var originalItem = Get(item.Id);
            originalItem.Name = item.Name;
            originalItem.IsHere = item.IsHere;
            originalItem.PartialAttendance = item.PartialAttendance;
            originalItem.EarlyDepartTime = item.EarlyDepartTime;
            originalItem.LateArrivalTime = item.LateArrivalTime;
            originalItem.CreatedDate = item.CreatedDate;
            _db.SaveChanges();
        }

        public RollCall Get(string value1, string value2)
        {
            throw new NotImplementedException();
        }
    }
}
