
using rent.domain.Repositories.Motorcycle;
using rent.domain.Repositories;
using rent.domain.Services.LoggedUser;
using rent.domain.Repositories.Rental;
using rent.domain.Enuns;
using rent.exceptions.ExceptionsBase;
using MongoDB.Bson;
using rent.exceptions;

namespace rent.application.UseCases.Motorcycle.Delete
{
    public class DeleteMotorcycleUseCase : IDeleteMotorcycleUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMotorcycleUpdateOnlyRepository _motorcycleUpdateOnlyRepository;
        private readonly IRentalReadOnlyRepository _rentalReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMotorcycleUseCase(
            ILoggedUser loggedUser,
            IMotorcycleUpdateOnlyRepository motorcycleUpdateOnlyRepository,
            IRentalReadOnlyRepository rentalReadOnlyRepository,
            IUnitOfWork unitOfWork
            )
        {
            _loggedUser = loggedUser;
            _motorcycleUpdateOnlyRepository = motorcycleUpdateOnlyRepository;
            _rentalReadOnlyRepository = rentalReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(string id)
        {
            if (!await _loggedUser.IsAuthorized(new List<UserType> { UserType.Admin }))
                throw new RentUnauthorizedAccessException();

            var user = await _loggedUser.User();

            var motorcycle = await _motorcycleUpdateOnlyRepository.GetById(ObjectId.Parse(id));

            if (motorcycle == null) throw new NotFoundException();

            var existRental = await _rentalReadOnlyRepository.ExistsRentalWithMotorcycle(motorcycle._id);

            if (existRental) throw new UserException(ResourceMessagesException.MOTORCYCLE_RENTAL_HITORY);

            motorcycle.UserUniqueIdentifier = user.UserUniqueIdentifier;
            motorcycle.Active = false;

            _motorcycleUpdateOnlyRepository.Update(motorcycle);

            await _unitOfWork.Commit();

        }
    }
}
