namespace StudentApp.Models
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public DateTime DOB { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public string Parent1 { get; set; }
        public string? Parent2 { get; set; }
        public double? Parent1Phone{ get; set; }
        public double? Parent2Phone { get; set; }
        public string Address { get; set; }

        //Name,Class,DOB.EnrolmentDate,Parent1,Parent2, Parent1Phone, Parent2Phonny, Address
    
        
    }
}
