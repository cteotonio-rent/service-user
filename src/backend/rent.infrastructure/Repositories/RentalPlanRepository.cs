using Microsoft.EntityFrameworkCore;
using rent.domain.Entities;
using rent.domain.Repositories.RentalPlan;
using rent.infrastructure.DataAccess;

namespace rent.infrastructure.Repositories
{
    public class RentalPlanRepository: IRentalPlanReadOnlyRepository
    {
        private readonly UserDbContext _dbContext;
        public RentalPlanRepository(UserDbContext context) => _dbContext = context;
        public async Task<bool> ExistActivePlanWithPlanDays(int planDays) => await _dbContext.RentalPlans.AnyAsync(x => x.DurationInDays == planDays && x.Active);

        public Task<RentalPlan?> GetActivePlanWithPlanDays(int planDays) => _dbContext.RentalPlans.AsNoTracking().FirstOrDefaultAsync(x => x.DurationInDays == planDays && x.Active);
    }
}
