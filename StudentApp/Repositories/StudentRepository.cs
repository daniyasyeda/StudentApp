using StudentApp.Contexts;
using StudentApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Repositories;
using System.Diagnostics;

namespace StudentApp.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Student item)
        {
            _db.Students.Add(item);
            _db.SaveChanges();
        }

        public void Delete(Student item)
        {
            _db.Students.Remove(item);
            _db.SaveChanges();
        }       

        public void DeleteRange(IEnumerable<Student> items)
        {
            _db.Students.RemoveRange(items);
            _db.SaveChanges();
        }

        public Student Get(int id)
        {
            return _db.Students.Find(id);
           
        }

        public IEnumerable<Student> GetAll()
        {

            return _db.Students;
        }
        public Student TotalMarkCalculation(Student model)
        {
            model.TotalMarks = (model.English + model.Maths) / 2;

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

        public void Edit(Student item)
        {
            TotalMarkCalculation(item);
            ReportLetterCal(item);
            var originalItem = Get(item.Id);
            originalItem.Grade = item.Grade;
            originalItem.Name = item.Name;
            originalItem.TotalMarks = item.TotalMarks;
            originalItem.MarkByLetter = item.MarkByLetter;
            originalItem.English = item.English;
            originalItem.Maths = item.Maths;
            _db.SaveChanges();
        }
    }
}
