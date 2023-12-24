using WorkForcePro.Data;
using WorkForcePro.Models;

namespace WorkForcePro.Repositories
{
    public class EmployeeAttendenceRepository : Repository<EmployeeAttendance>, IEmployeeAttendenceRepository
    {
        public EmployeeAttendenceRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }


    }
}