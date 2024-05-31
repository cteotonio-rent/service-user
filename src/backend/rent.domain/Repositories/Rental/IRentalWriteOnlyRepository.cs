namespace rent.domain.Repositories.Rental
{
    public interface IRentalWriteOnlyRepository
    {
        Task Add(Entities.Rental rental);
    }
}
