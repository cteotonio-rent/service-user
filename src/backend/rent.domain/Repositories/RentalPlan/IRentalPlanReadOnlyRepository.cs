namespace rent.domain.Repositories.RentalPlan
{
    public interface IRentalPlanReadOnlyRepository
    {
        Task<bool> ExistActivePlanWithPlanDays(int planDays);

        Task<Entities.RentalPlan?> GetActivePlanWithPlanDays(int planDays);
    }
}
