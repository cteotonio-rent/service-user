using rent.application.Services.Cryptography;
using rent.communication.Requests;
using rent.communication.Responses;
using rent.domain.Repositories.User;
using rent.domain.Security.Tokens;
using rent.exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rent.application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        public DoLoginUseCase(
            IUserReadOnlyRepository repository, 
            IAccessTokenGenerator accessTokenGenerator,
            PasswordEncripter passwordEncripter)
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var user = await 
                _repository.GetByEmailAndPassword(request.Email, _passwordEncripter.Encrypt(request.Password)) 
                ?? throw new InvalidLoginException();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserUniqueIdentifier)
                }
            };
        }
    }
}
