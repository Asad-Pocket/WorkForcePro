
using WorkForcePro.Models;

namespace WorkForcePro.DtoModel
{
    public class SearchViewItem 
    {
        public List<Department> Departments { get; set; }
       
        public int SelectedDepartment { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
