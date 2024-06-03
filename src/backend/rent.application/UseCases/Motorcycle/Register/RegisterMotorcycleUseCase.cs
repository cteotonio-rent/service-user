using AutoMapper;
using FluentValidation.Results;
using rent.communication.Requests;
using rent.communication.Responses;
using rent.domain.Repositories;
using rent.domain.Repositories.Motorcycle;
using rent.domain.Repositories.User;
using rent.domain.Services.LoggedUser;
using rent.exceptions.ExceptionsBase;
using rent.exceptions;
using rent.domain.Enuns;

namespace rent.application.UseCases.Motorcycle.Register
{
    public class RegisterMotorcycleUseCase : IRegisterMotorcycleUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMotorcycleReadOnlyRepository _motorcycleReadOnlyRepository;
        private readonly IMotorcycleWriteOnlyRepository _motorcycleWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterMotorcycleUseCase(
            ILoggedUser loggedUser,
            IMotorcycleReadOnlyRepository motorcycleReadOnlyRepository,
            IMotorcycleWriteOnlyRepository motorcycleWriteOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _loggedUser = loggedUser;
            _motorcycleReadOnlyRepository = motorcycleReadOnlyRepository;
            _motorcycleWriteOnlyRepository = motorcycleWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredMotorcycleJson> Execute(RequestRegisterMotorcycleJson request)
        {
            if (!await _loggedUser.IsAuthorized(new List<UserType> { UserType.Admin }))
                throw new RentUnauthorizedAccessException();

            await Validate(request);

            var loggedUser = await _loggedUser.User();

            var motorcycle = _mapper.Map<rent.domain.Entities.Motorcycle>(request);

            motorcycle.UserUniqueIdentifier = loggedUser.UserUniqueIdentifier;

            await _motorcycleWriteOnlyRepository.Add(motorcycle);

            await _unitOfWork.Commit();

            return new ResponseRegisteredMotorcycleJson
            {
                LicensePlate = motorcycle.LicensePlate
            };
        }

        private async Task Validate(RequestRegisterMotorcycleJson request)
        {
            var validator = new RegisterMotorcycleValidator();

            var result = validator.Validate(request);

            var licensePlateExists = await _motorcycleReadOnlyRepository.ExistActiveMotorcycleWithLicensePlate(request.LicensePlate);
            if (licensePlateExists)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.LICENSE_PLATE_ALREADY_REGISTERED));

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }

        }
    }
}
