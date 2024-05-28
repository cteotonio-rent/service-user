﻿namespace rent.domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);
        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
        public Task<bool?> ExistsActiveUserWithIdentifier(Guid userIdentifier);

        public Task<Entities.User?> GetByUserIdentifier(Guid userIdentifier);
    }
}