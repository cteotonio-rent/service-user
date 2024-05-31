namespace rent.domain.Repositories.Order
{
    public interface IOrderWriteOnlyRepository
    {
        Task Add(Entities.Order order);
    }
}
