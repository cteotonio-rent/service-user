using AutoMapper;
using FluentValidation.Results;
using rent.communication.Requests;
using rent.communication.Responses;
using rent.domain.Entities;
using rent.domain.Enuns;
using rent.domain.Repositories;
using rent.domain.Repositories.Motorcycle;
using rent.domain.Repositories.Rental;
using rent.domain.Repositories.RentalPlan;
using rent.domain.Services.LoggedUser;
using rent.exceptions;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.Rental.Register
{
    public class RegisterRentalUseCase : IRegisterRentalUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentalWriteOnlyRepository _rentalWriteOnlyRepository;
        private readonly IRentalReadOnlyRepository _rentalReadOnlyRepository;
        private readonly IRentalPlanReadOnlyRepository _rentalPlanReadOnlyRepository;
        private readonly IMotorcycleReadOnlyRepository _motorcycleReadOnlyRepository;
        private readonly IMotorcycleUpdateOnlyRepository _motorcycleUpdateOnlyRepository;
        private readonly IMapper _mapper;

        public RegisterRentalUseCase(
            ILoggedUser loggedUser,
            IUnitOfWork unitOfWork,
            IRentalWriteOnlyRepository rentalWriteOnlyRepository,
            IRentalReadOnlyRepository rentalReadOnlyRepository,
            IRentalPlanReadOnlyRepository rentalPlanReadOnlyRepository,
            IMotorcycleReadOnlyRepository motorcycleReadOnlyRepository,
            IMotorcycleUpdateOnlyRepository motorcycleUpdateOnlyRepository,
            IMapper mapper)
        {
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _rentalWriteOnlyRepository = rentalWriteOnlyRepository;
            _rentalReadOnlyRepository = rentalReadOnlyRepository;
            _rentalPlanReadOnlyRepository = rentalPlanReadOnlyRepository;
            _motorcycleReadOnlyRepository = motorcycleReadOnlyRepository;
            _motorcycleUpdateOnlyRepository = motorcycleUpdateOnlyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredRentalJson> Execute(RequestRegisterRentalJson request)
        {
            if (!await _loggedUser.IsAuthorized(new List<UserType> { UserType.DeliveryPerson }))
                throw new RentUnauthorizedAccessException();

            await Validate(request);

            var rent = await CalculateRentalValue(request);

            await CalculateAdditionalFine(rent);

            var motorcycle = await _motorcycleUpdateOnlyRepository.GetById(rent.MotorcycleId);

            motorcycle.MotorcycleStatus = (int)domain.Enuns.MotorcycleStatus.Rented;

            _motorcycleUpdateOnlyRepository.UpdateLicensePlate(motorcycle);

            await _rentalWriteOnlyRepository.Add(rent);

            await _unitOfWork.Commit();

            return new ResponseRegisteredRentalJson
            {
                RentalId = rent._id.ToString()
            };
        }

        private async Task Validate(RequestRegisterRentalJson request)
        {
            var user = await _loggedUser.User();

            // Validar o request
            var validator = new RegisterRentalValidator();
            var result = await validator.ValidateAsync(request);

            // Validar se o plano existe
            var reantalPlanExists = await _rentalPlanReadOnlyRepository.ExistActivePlanWithPlanDays(request.RentalPlanDays);
            if (!reantalPlanExists)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.RENTAL_PLAN_NOT_EXIST));

            // Verificar se existe uma moto disponível
            var motorcycleAvailable = await _motorcycleReadOnlyRepository.ExistActiveMotorcycleWithStatus((int)domain.Enuns.MotorcycleStatus.Available);
            if (!motorcycleAvailable)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.MOTORCYCLE_NOT_AVAILABLE));

            if (user.DriversLicenseCategory != "A" || user.DriversLicenseCategory != "AB")
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.DRIVERS_LICENSE_CATEGORY_INVALID));

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }

        private async Task<domain.Entities.Rental> CalculateRentalValue(RequestRegisterRentalJson request)
        {
            var rental = new domain.Entities.Rental();
            var rentalPlan = await _rentalPlanReadOnlyRepository.GetActivePlanWithPlanDays(request.RentalPlanDays);
            var motorcycle = await _motorcycleReadOnlyRepository.GetFirstActiveMotorcycleByStatus((int)domain.Enuns.MotorcycleStatus.Available);

            rental.StartDate = DateTime.Now.AddDays(1);
            rental.RealEndDate = request.RealEndDate;

            // Recupera Plano de Aluguel
            rental.RentalPlanId = rentalPlan!._id;
            rental.EndDate = rental.StartDate.AddDays(request.RentalPlanDays);
            rental.EstimatedPrice = rentalPlan.Price * rentalPlan.DurationInDays;
            rental.UserId = MongoDB.Bson.ObjectId.GenerateNewId();
            rental.DailyFineNotApplied = rentalPlan.DailyFineNotApplied;
            rental.AdditionalDailyFine = rentalPlan.AdditionalDailyFine;
            rental.DurationInDays = rentalPlan.DurationInDays;

            rental.MotorcycleId = motorcycle!._id;
            rental.TotalPrice = rental.EstimatedPrice;

            return rental;
        }

        private async Task CalculateAdditionalFine(domain.Entities.Rental rental)
        {
            if (rental.RealEndDate > rental.EndDate)
            {
                var daysOverdue = (rental.RealEndDate.Value - rental.EndDate).Days;
                var additionalFine = rental.AdditionalDailyFine * daysOverdue;
                rental.TotalPrice += additionalFine;
            }
            else if (rental.RealEndDate < rental.EndDate)
            {
                var daysInAdvance = (rental.EndDate - rental.RealEndDate.Value).Days;
                var discount = ((rental.TotalPrice / rental.DurationInDays) * rental.DailyFineNotApplied) * daysInAdvance;
                rental.TotalPrice -= discount;
            }
        }
    }
}
