using StudentApp.Contexts;
using StudentApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Repositories;
using System.Diagnostics;


namespace StudentApp.Repositories
{
    public class StudentGradeRepository : IRepository<StudentGrade>
    {
        private readonly ApplicationDbContext _db;

        public StudentGradeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(StudentGrade item)
        {
            _db.StudentGrades.Add(item);
            _db.SaveChanges();
        }

        public void Delete(StudentGrade item)
        {
            _db.StudentGrades.Remove(item);
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<StudentGrade> items)
        {
            _db.StudentGrades.RemoveRange(items);
            _db.SaveChanges();
        }

        public StudentGrade Get(int id)
        {
            return _db.StudentGrades.Find(id);

        }

        public StudentGrade Get(string name,string classname)
        {
            return _db.StudentGrades.Where(s=>s.Name==name && s.Class==classname).FirstOrDefault();

        }

        public IEnumerable<StudentGrade> GetAll()
        {

            return _db.StudentGrades;
        }

        public StudentGrade TotalMarkCalculation(StudentGrade model)
        {
            model.TotalMarks = (model.English + model.Maths) / 2;

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

        public void Edit(StudentGrade item)
        {
            TotalMarkCalculation(item);
            ReportLetterCal(item);

            var originalItem = Get(item.Id);
            originalItem.Class = item.Class;
            originalItem.Name = item.Name;
            originalItem.TotalMarks = item.TotalMarks;
            originalItem.MarkByLetter = item.MarkByLetter;
            originalItem.English = item.English;
            originalItem.Maths = item.Maths;
            _db.SaveChanges();
        }

    }
    }
    
