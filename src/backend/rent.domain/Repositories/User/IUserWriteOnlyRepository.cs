namespace rent.domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(rent.domain.Entities.User user);
    }
}
