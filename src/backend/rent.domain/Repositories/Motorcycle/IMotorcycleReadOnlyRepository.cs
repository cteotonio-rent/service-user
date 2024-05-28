namespace rent.domain.Repositories.Motorcycle
{
    public interface IMotorcycleReadOnlyRepository
    {
        Task<bool> ExistActiveMotorcycleWithLicensePlate(string licensePlate);
        Task<rent.domain.Entities.Motorcycle?> GetMotorcycleByLicensePlate(string licensePlate);
    }
}
