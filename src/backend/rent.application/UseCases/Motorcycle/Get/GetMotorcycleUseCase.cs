using AutoMapper;
using rent.communication.Responses;
using rent.domain.Repositories.Motorcycle;

namespace rent.application.UseCases.Motorcycle.Get
{
    public class GetMotorcycleUseCase : IGetMotorcycleUseCase
    {
        private readonly IMotorcycleReadOnlyRepository _motorcycleReadOnlyRepository;
        private readonly IMapper _mapper;
        public GetMotorcycleUseCase(
            IMotorcycleReadOnlyRepository motorcycleReadOnlyRepository,
            IMapper mapper)
            
        {
            _motorcycleReadOnlyRepository = motorcycleReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseGetMotocycle> Execute(string licensePlate)
        {
            var motorcycle = await _motorcycleReadOnlyRepository.GetMotorcycleByLicensePlate(licensePlate);
            var response = _mapper.Map<ResponseGetMotocycle>(motorcycle);
            return response;
        }
    }
}
