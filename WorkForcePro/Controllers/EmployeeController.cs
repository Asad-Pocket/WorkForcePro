using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System.Data;
using WorkForcePro.DtoModel;
using WorkForcePro.Models;
using WorkForcePro.Repositories;

namespace WorkForcePro.Controllers
{
    public class EmployeeController : Controller
    {
       
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDesignationRepository _designationRepository;
        private readonly IShiftRepository _shiftRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISalaryRepository _salaryRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,
            IDesignationRepository designationRepository, ICompanyRepository companyRepository,
            IShiftRepository shiftRepository, ISalaryRepository salaryRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _designationRepository = designationRepository;
            _shiftRepository = shiftRepository;
            _companyRepository = companyRepository;
            _salaryRepository = salaryRepository;
        }
        public IActionResult Index()
        {
            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int C_id = Convert.ToInt32(storedCompanyId);
            var designations = _designationRepository.GetAllItem().Where(x => x.ComId == C_id).ToList();
            var departments = _departmentRepository.GetAllItem().Where(x => x.ComId == C_id).ToList();
            var shifts = _shiftRepository.GetAllItem().Where(x => x.ComId == C_id).ToList();
            ViewData["depts"] = departments;
            ViewData["desigs"] = designations;
            ViewData["shift"] = shifts;
            return View();
        }
        [HttpPost]
        public IActionResult Index(Employee employee)
        {
            if(employee != null)
            {
                var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
                int C_id = Convert.ToInt32(storedCompanyId);
                employee.ComId = C_id;
                var company = _companyRepository.GetById(C_id);
                if (employee.DtJoin.HasValue && employee.DtJoin.Value.Kind == DateTimeKind.Unspecified)
                {
                    employee.DtJoin = DateTime.SpecifyKind(employee.DtJoin.Value, DateTimeKind.Utc);
                }
                // Calculate Basic based on Gross and division
                double division = ((double)company.Basic) / 100.00;
                employee.Basic = (int?)(employee.Gross * division);

                division = ((double)company.HRent) / 100.00;
                // Calculate HRent based on Gross and company.HRent
                employee.HRent = (int?)(employee.Gross * division);

                division = ((double)company.Medical) / 100.00;
                // Calculate Medical based on Gross and company.Medical
                employee.Medical = (int?)(employee.Gross * division);

                // Insert the employee into the repository
                _employeeRepository.Insert(employee);

            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _employeeRepository.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return View(employee);
        }
        public IActionResult Edit(int id)
        {
            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int C_id = Convert.ToInt32(storedCompanyId);
            var designations = _designationRepository.GetAllItem().Where(x => x.ComId == C_id).ToList();
            var departments = _departmentRepository.GetAllItem().Where(x => x.ComId == C_id).ToList();
            var shifts = _shiftRepository.GetAllItem().Where(x => x.ComId == C_id).ToList();
            ViewData["depts"] = departments;
            ViewData["desigs"] = designations;
            ViewData["shift"] = shifts;
            var employee = _employeeRepository.GetById(id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int ComId = Convert.ToInt32(storedCompanyId);
            employee.ComId = ComId;
            _employeeRepository.Update(employee);
            return RedirectToAction("Index");
        }
        public IActionResult ShowEmployee()
        {
            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int C_id = Convert.ToInt32(storedCompanyId);
            var empList = _employeeRepository.GetAllInItem(C_id);
            return Json(empList);
        }
        public IActionResult ProcessAll()
        {
            // Replace these values with your actual PostgreSQL connection string
            string connectionString = "Server=localhost;Port=5432;Username=postgres;Password=orpit23arfatur;Database=WorkForcePro;";

            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int comId = Convert.ToInt32(storedCompanyId);
            // Replace these values with the actual parameters for your function
            DateTime dateTime = DateTime.Now;
            int month = dateTime.Month;
            int year = dateTime.Year;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("public.attendanceandsummary", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_comid", comId);
                    cmd.Parameters.AddWithValue("in_month", month);
                    cmd.Parameters.AddWithValue("in_year", year);

                    List <SalaryViewModel> employeeSummaries = new List<SalaryViewModel>();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SalaryViewModel employeeSummary = new SalaryViewModel
                            {
                                EmpId = reader.GetInt32(0),
                                ComId = reader.GetInt32(1),
                                DtYear = reader.GetInt32(2),
                                DtMonth = reader.GetInt32(3),
                                Present = reader.GetInt32(4),
                                Late = reader.GetInt32(5),
                                Absent = reader.GetInt32(6),
                                Gross = reader.GetDecimal(7),
                                Basic = reader.GetDecimal(8),
                                HRent = reader.GetDecimal(9),
                                Medical = reader.GetDecimal(10),
                                AbsentAmount = reader.GetDecimal(11),
                                PayableAmount = reader.GetDecimal(12),
                                IsPaid = reader.GetBoolean(13)
                            };
                            employeeSummary.MonthName = new DateTime(1, employeeSummary.DtMonth, 1).ToString("MMMM");
                            Employee employee = _employeeRepository.GetById(employeeSummary.EmpId);
                            employeeSummary.EmpCode = employee.EmpCode;
                            employeeSummary.EmpName = employee.EmpName;
                            employeeSummary.DeptName = _departmentRepository.GetById(employee.DeptId).DeptName;
                            employeeSummary.DesigName =_designationRepository.GetById(employee.DesigId).DesigName;
                            employeeSummaries.Add(employeeSummary);
                        }
                    }
                    if(employeeSummaries != null)
                    {
                        return View(employeeSummaries);
                    }
                }
                return View();
            }
        }
        
        public IActionResult ShowEmployeeByDepartment()
        {
            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int C_id = Convert.ToInt32(storedCompanyId);
            var empList = _employeeRepository.GetAllInItem(C_id);
            var departments = _departmentRepository.GetAllItem().Where(x => x.ComId == C_id).ToList();
            ViewData["depts"] = departments;
            return View();
        }
        [HttpPost]
        public IActionResult EmployeeByDepartment(int DeptId)
        {
            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int C_id = Convert.ToInt32(storedCompanyId);
            var employeeList = _employeeRepository.GetAllByDept(C_id, DeptId).ToList();
            return Json(employeeList);
        }

    }
}