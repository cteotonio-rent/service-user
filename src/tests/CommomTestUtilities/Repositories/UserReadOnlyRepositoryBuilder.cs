﻿using Moq;
using rent.user.domain.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommomTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();

        public void ExistActiveUserWithEmail(string email)
        {
            _repository.Setup(repo => repo.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
        }
        public IUserReadOnlyRepository Build() => _repository.Object;
        
    }
}