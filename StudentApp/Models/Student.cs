using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
    public class Student
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public int OutOf { get; set; }
        public int English { get; set; }
        public int Maths { get; set; }
        public int TotalMarks { get; set; }
        public string MarkByLetter { get; set; }
        public  DateTimeOffset CreatedAt { get; set; }


    }
}
