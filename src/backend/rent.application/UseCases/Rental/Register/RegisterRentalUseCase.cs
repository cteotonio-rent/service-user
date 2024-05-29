using AutoMapper;
using rent.communication.Requests;
using rent.communication.Responses;
using rent.domain.Repositories;
using rent.domain.Repositories.Rental;
using rent.domain.Repositories.RentalPlan;
using rent.domain.Services.LoggedUser;

namespace rent.application.UseCases.Rental.Register
{
    public class RegisterRentalUseCase : IRegisterRentalUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentalWriteOnlyRepository _rentalWriteOnlyRepository;
        private readonly IRentalReadOnlyRepository _rentalReadOnlyRepository;
        private readonly IRentalPlanReadOnlyRepository _rentalPlanReadOnlyRepository;
        private readonly IMapper _mapper;

        public RegisterRentalUseCase(
            ILoggedUser loggedUser,
            IUnitOfWork unitOfWork,
            IRentalWriteOnlyRepository rentalWriteOnlyRepository,
            IRentalReadOnlyRepository rentalReadOnlyRepository,
            IRentalPlanReadOnlyRepository rentalPlanReadOnlyRepository,
            IMapper mapper)
        {
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _rentalWriteOnlyRepository = rentalWriteOnlyRepository;
            _rentalReadOnlyRepository = rentalReadOnlyRepository;
            _rentalPlanReadOnlyRepository = rentalPlanReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredRentalJson> Execute(RequestRegisterRentalJson request)
        {
            await Validate(request);

            throw new NotImplementedException();
        }

        private async Task Validate(RequestRegisterRentalJson request)
        {
            // Validar o request

            // Validar se o plano existe

            // Verificar se existe uma moto disponível

            // Calcular o valor do aluguel

            // Calcular o valor multa adicional



            throw new NotImplementedException();
        }
    }
}
