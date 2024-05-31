using FluentValidation.Results;
using MongoDB.Bson;
using rent.communication.Requests;
using rent.domain.Repositories;
using rent.domain.Repositories.Motorcycle;
using rent.domain.Services.LoggedUser;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.Motorcycle.Update
{
    public class UpdateMotorcycleLicensePlateUseCase : IUpdateMotorcycleLicensePlateUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMotorcycleUpdateOnlyRepository _motorcycleUpdateOnlyRepository;
        private readonly IMotorcycleReadOnlyRepository _motorcycleReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMotorcycleLicensePlateUseCase(
            ILoggedUser loggedUser,
            IMotorcycleUpdateOnlyRepository motorcycleUpdateOnlyRepository,
            IMotorcycleReadOnlyRepository motorcycleReadOnlyRepository,
            IUnitOfWork unitOfWork
            )
        {
            _loggedUser = loggedUser;
            _motorcycleUpdateOnlyRepository = motorcycleUpdateOnlyRepository;
            _motorcycleReadOnlyRepository = motorcycleReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(string id, RequestUpdateMotorcycleLicensePlateJson request)
        {
            await Validate(ObjectId.Parse(id), request);
            var loggedUser = await _loggedUser.User();
            var motorcycle = await _motorcycleUpdateOnlyRepository.GetById(ObjectId.Parse(id));

            motorcycle.LicensePlate = request.LicensePlate;
            motorcycle.UserUniqueIdentifier = loggedUser.UserUniqueIdentifier;
            _motorcycleUpdateOnlyRepository.UpdateLicensePlate(motorcycle);

            await _unitOfWork.Commit();
        }

        private async Task Validate(ObjectId id, RequestUpdateMotorcycleLicensePlateJson request)
        {
            var validator = new UpdateMotorcycleLicensePlateValidator();

            var result = validator.Validate(request);

            var motorcycleCurrent = await _motorcycleReadOnlyRepository.GetMotorcycleByLicensePlate(request.LicensePlate);

            if (motorcycleCurrent is not null)
            {
                if (!(motorcycleCurrent._id == id))
                {
                    var licensePlateExists = await _motorcycleReadOnlyRepository.ExistActiveMotorcycleWithLicensePlate(request.LicensePlate);
                    if (licensePlateExists)
                        result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.LICENSE_PLATE_ALREADY_REGISTERED));
                }
            }

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }
    }
}
