namespace rent.user.domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(rent.user.domain.Entities.User user);
    }
}
