using WorkForcePro.Data;
using WorkForcePro.Models;

namespace WorkForcePro.Repositories
{
    public class CompanyRepository : Repository<Company> , ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
