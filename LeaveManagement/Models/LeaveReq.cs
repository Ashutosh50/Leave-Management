namespace LeaveManagement.Models
{
    public class LeaveReq
    {
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set;}
        public string Staus { get; set; }
    }
}
