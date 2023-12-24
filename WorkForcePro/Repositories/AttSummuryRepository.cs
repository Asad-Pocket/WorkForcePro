using WorkForcePro.Data;
using WorkForcePro.Models;

namespace WorkForcePro.Repositories
{
    public class AttSummuryRepository :Repository<AttendanceSummary> , IAttSummuryRepository
    {
        public AttSummuryRepository(ApplicationDbContext  ctx) : base(ctx)
        {
            
        }
    }
}
