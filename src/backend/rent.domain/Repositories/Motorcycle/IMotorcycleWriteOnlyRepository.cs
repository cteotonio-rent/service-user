namespace rent.domain.Repositories.Motorcycle
{
    public interface IMotorcycleWriteOnlyRepository
    {
        Task Add(Entities.Motorcycle motorcycle);
    }
}
