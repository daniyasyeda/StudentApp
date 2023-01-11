using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
    public class StudentGrade
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int OutOf { get; set; }
        public int English { get; set; }
        public int Maths { get; set; }
        public int TotalMarks { get; set; }
        public string MarkByLetter { get; set; }
        public  DateTimeOffset CreatedAt { get; set; }


    }
}
