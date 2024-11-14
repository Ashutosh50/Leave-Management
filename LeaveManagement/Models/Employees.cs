using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Models
{
    public class Employees
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Email { get; set; }
        public bool Manager { get; set; }
    }
}
