namespace rent.domain.Repositories.Motorcycle
{
    public interface IMotorcycleReadOnlyRepository
    {
        Task<bool> ExistActiveMotorcycleWithLicensePlate(string licensePlate);

        Task<bool> ExistActiveMotorcycleWithStatus(int statusId);

        Task<Entities.Motorcycle?> GetMotorcycleByLicensePlate(string licensePlate);

        Task<Entities.Motorcycle?> GetFirstActiveMotorcycleByStatus(int statusId);
    }
}
