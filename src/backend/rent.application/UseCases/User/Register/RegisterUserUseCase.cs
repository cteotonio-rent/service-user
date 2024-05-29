using AutoMapper;
using FluentValidation.Results;
using rent.application.Services.Cryptography;
using rent.communication.Requests;
using rent.communication.Responses;
using rent.domain.Repositories;
using rent.domain.Repositories.User;
using rent.domain.Security.Tokens;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.User.Register
{
    public class RegisterUserUseCase: IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public RegisterUserUseCase(
            IUserWriteOnlyRepository userWriteOnlyRepository, 
            IUserReadOnlyRepository userReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            PasswordEncripter passwordEncripter,
            IAccessTokenGenerator accessTokenGenerator)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _unitOfWork = unitOfWork;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<rent.domain.Entities.User>(request);

            user.Password = _passwordEncripter.Encrypt(request.Password);
            user.UserUniqueIdentifier = Guid.NewGuid();
            user.UserType = domain.Enuns.UserType.DeliveryPerson;

            await _userWriteOnlyRepository.Add(user);
            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson { 
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserUniqueIdentifier)
                }
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);
            
            var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExists)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

            var nrleExists = await _userReadOnlyRepository.ExistActiveUserWithNRLE(request.NRLE);
            if (nrleExists)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.NRLE_ALREADY_REGISTERED));

            var driversLicenseExists = await _userReadOnlyRepository.ExistActiveUserWithDriversLicense(request.DriversLicense);
            if (driversLicenseExists)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.DRIVERS_LICENSE_ALREADY_REGISTERED));


            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }
    }
}
