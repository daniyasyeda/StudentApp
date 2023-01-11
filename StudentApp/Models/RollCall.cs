using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
    public class RollCall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsHere { get; set; }
        public string? PartialAttendance { get; set; }
        
        public DateTime? EarlyDepartTime { get; set; }
    
        public DateTime? LateArrivalTime { get; set; }
        public DateTime CreatedDate  { get; set; }
    }
}
