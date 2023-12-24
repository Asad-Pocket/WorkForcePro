using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorkForcePro.Models;

namespace WorkForcePro.DtoModel
{
    public class SalaryViewModel
    {
        public int EmpId { get; set; }
        public int? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? DeptName { get; set; }  
        public string? DesigName { get; set; }
        public int ComId { get; set; }
        public int DtYear { get; set; }
        public int DtMonth { get; set; }
        public string MonthName { get; set; }
        public int Present { get; set; }
        public int Late { get; set; }
        public int Absent { get; set; }
        public decimal Gross { get; set; }
        public decimal Basic { get; set; }
        public decimal HRent { get; set; }
        public decimal Medical { get; set; }
        public decimal AbsentAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}
