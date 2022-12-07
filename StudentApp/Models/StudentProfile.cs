namespace StudentApp.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Class { get; set; }
        public DateTime DOB { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public string Parent1 { get; set; }
        public string? Parent2 { get; set; }
        public int Parent1PN{ get; set; }
        public int? Parent2PN { get; set; }
        public string Address { get; set; }
    }
}
