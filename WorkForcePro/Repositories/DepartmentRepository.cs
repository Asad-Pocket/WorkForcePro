using WorkForcePro.Data;
using WorkForcePro.Models;

namespace WorkForcePro.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
