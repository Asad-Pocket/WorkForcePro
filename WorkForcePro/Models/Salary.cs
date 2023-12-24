using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
namespace WorkForcePro.Models
{
    public class Salary
    {
        [Key, Column(Order = 1)]
        public int ComId { get; set; }
        [Key, Column(Order = 2)]
        public int EmpId { get; set; }
        [Key, Column(Order = 3)]

        public int dtYear { get; set; }
        [Key, Column(Order = 4)]

        public int dtMonth { get; set; }


        public int Gross { get; set; }

        public int Basic { get; set; }
        public int Hrent { get; set; }

        public int Medical { get; set; }

        public int AbsentAmount { get; set; }
        public int PayableAmount { get; set; }

        public bool IsPaid { get; set; }

        public int PaidAmount { get; set; }

        public Employee? Employee { get; set; }
    }
}