using WorkForcePro.Data;
using WorkForcePro.DtoModel;
using WorkForcePro.Models;
using Microsoft.EntityFrameworkCore;
using WorkForcePro.Repositories;

namespace WorkForcePro.Repositories
{
    public class SalaryRepository : Repository<Salary> ,ISalaryRepository
    {
        public SalaryRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
