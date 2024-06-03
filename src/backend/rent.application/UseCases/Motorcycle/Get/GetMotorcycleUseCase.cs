using AutoMapper;
using rent.communication.Responses;
using rent.domain.Enuns;
using rent.domain.Repositories.Motorcycle;
using rent.domain.Services.LoggedUser;
using rent.exceptions.ExceptionsBase;

namespace rent.application.UseCases.Motorcycle.Get
{
    public class GetMotorcycleUseCase : IGetMotorcycleUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMotorcycleReadOnlyRepository _motorcycleReadOnlyRepository;
        private readonly IMapper _mapper;
        public GetMotorcycleUseCase(
            IMotorcycleReadOnlyRepository motorcycleReadOnlyRepository,
            ILoggedUser loggedUser,
            IMapper mapper)
            
        {
            _motorcycleReadOnlyRepository = motorcycleReadOnlyRepository;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseGetMotocycle> Execute(string licensePlate)
        {
            if (!await _loggedUser.IsAuthorized(new List<UserType> { UserType.Admin }))
                throw new RentUnauthorizedAccessException(); 

            var motorcycle = await _motorcycleReadOnlyRepository.GetMotorcycleByLicensePlate(licensePlate);
            var response = _mapper.Map<ResponseGetMotocycle>(motorcycle);
            return response;
        }
    }
}
