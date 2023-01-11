namespace StudentApp.Models
{
    public class LogIn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Password { get; set; }
        public string UniqueCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
