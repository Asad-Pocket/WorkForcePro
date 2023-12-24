using WorkForcePro.Data;
using WorkForcePro.Models;

namespace WorkForcePro.Repositories
{
    public class ShiftRepository : Repository<Shift>,IShiftRepository
    {
        public ShiftRepository(ApplicationDbContext ctx) : base(ctx)
        {
            
        }
    }
}
