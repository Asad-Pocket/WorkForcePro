using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorkForcePro.Models;

namespace WorkForcePro.Models
{
    public class AttendanceSummary
    {
        [Key, Column(Order = 1)]
        public int ComId { get; set; }

        [Key, Column(Order = 2)]
        public int EmpId { get; set; }

        [Key, Column(Order = 3)]
        public int DtYear { get; set; }

        [Key, Column(Order = 4)]
        public int DtMonth { get; set; }

        // Other properties
        public int Present { get; set; }
        public int Late { get; set; }
        public int Absent { get; set; }

        // Navigation properties
        public Company Company { get; set; }
        public Employee Employee { get; set; }
    }
}