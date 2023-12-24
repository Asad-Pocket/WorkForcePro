using WorkForcePro.Models;

namespace WorkForcePro.DtoModel
{
    public class AttendanceDetails
    {
        public string EmpName { get; set; }
        public string MonthName { get; set; }
        public string DeptName { get; set; }
        public string DesigName { get; set; }
        public string PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LateDays { get; set; }
    }
}
