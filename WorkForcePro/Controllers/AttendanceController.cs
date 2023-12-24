using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using WorkForcePro.Data;
using WorkForcePro.DtoModel;
using WorkForcePro.Models;
using WorkForcePro.Repositories;

namespace WorkForcePro.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IEmployeeAttendenceRepository _AttendenceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAttSummuryRepository _attSummuryRepository;
        private readonly ApplicationDbContext _context;
        public AttendanceController(IEmployeeAttendenceRepository employeeAttendenceRepository, IAttSummuryRepository summuryRepository, IEmployeeRepository employeeRepository)
        {
            _AttendenceRepository = employeeAttendenceRepository;
            _employeeRepository = employeeRepository;
            _attSummuryRepository = summuryRepository;
        }

        public IActionResult Entry(int EmpId)
        {
            ViewBag.EmpId = EmpId;
            return View();
        }

        [HttpPost]
        public IActionResult Entry(EmployeeAttendance attendance)
        {
            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int C_id = Convert.ToInt32(storedCompanyId);

            attendance.ComId = C_id;

            var employee = _employeeRepository.Get(attendance.EmpId);

            if (attendance.InTime > employee.ShiftLateTime)
            {
                attendance.AttStatus = "Late";
            }
            else
            {
                attendance.AttStatus = "Present";
            }
            if (attendance.InTime == null)
            {
                attendance.AttStatus = "Absent";
            }
            if (attendance.dtDate.DayOfWeek.ToString() == "Friday")
            {
                attendance.AttStatus = "Weekday";
                attendance.InTime = null;
                attendance.OutTime = null;
            }
            _AttendenceRepository.Insert(attendance);
            return RedirectToAction("Index", "Employee");
        }
        public IActionResult Delete(int Id)
        {
            _AttendenceRepository.Delete(Id);
            return RedirectToAction("Entry");
        }

        public IActionResult ShowReport(int EmpId)
        {
            var Allattendance = _AttendenceRepository.GetAllItem().ToList().Where(x => x.EmpId == EmpId).ToList();
            return View(Allattendance);
        }
        public IActionResult Edit(int Id)
        {
            var entry = _AttendenceRepository.GetById(Id);
            return View(entry);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeAttendance attendance)
        {
            _AttendenceRepository.Update(attendance);
            return RedirectToAction("Entry");
        }

        public IActionResult GenerateReport(int empId)
        {
            var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
            int companyId = Convert.ToInt32(storedCompanyId);

            List<AttendanceDetails> attendanceDetails = new List<AttendanceDetails>();

            string connectionString = "Server=ASAD-POCKET\\SQLEXPRESS;Database=GTRTrainingFinal; Integrated Security = true; TrustServerCertificate=True;MultipleActiveResultSets=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("GetEmployeeAttendanceDetails", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ComId", companyId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AttendanceDetails details = new AttendanceDetails
                            {
                                MonthName = reader["MonthName"].ToString(),
                                PresentDays = reader["PresentDays"].ToString(),
                                AbsentDays = Convert.ToInt32(reader["AbsentDays"]),
                                LateDays = Convert.ToInt32(reader["LateDays"])
                            };

                            attendanceDetails.Add(details);
                        }
                    }
                }
            }

            return View(attendanceDetails);
        }

        //public IActionResult AttReport()
        //{
        //    var storedCompanyId = Request.Cookies["SelectedCompanyId"].ToString();
        //    int companyId = Convert.ToInt32(storedCompanyId);
        //    var AttendenceReport = _context.AttendanceSummaries.Where(x => x.ComId == companyId)
        //        .Include(x => x.Employee)
        //        .Include(x => x.department)
        //        .Include(x => x.designation)
        //        .AsEnumerable()
        //        .Select(x => new AttendanceDetails
        //        {
        //            MonthName = x.dtMonth.ToString(),
        //            EmpName = x.employee.EmpName,
        //            DeptName = x.department.DeptName,
        //            DesigName = x.designation.DesigName,
        //            PresentDays = x.PresentCount,
        //            AbsentDays = x.AbsentCount,
        //            LateDays = x.LateCount
        //        });

        //    return View(AttendenceReport);
        //}
    }
}
