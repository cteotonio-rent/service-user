using AutoMapper;
using rent.communication.Requests;
using rent.communication.Responses;
using rent.domain;

namespace rent.application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            ResquestToDomain();
            DomainToResponse();
        }

        private void ResquestToDomain()
        {
            CreateMap<RequestRegisterUserJson, rent.domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RequestRegisterMotorcycleJson, rent.domain.Entities.Motorcycle>();
        }

        private void DomainToResponse()
        {
            CreateMap<domain.Entities.User, ResponseUserProfileJson>();
            CreateMap<rent.domain.Entities.Motorcycle, ResponseGetMotocycle>()
                .ForMember(dest => dest._Id, opt => opt.MapFrom(src => src._id.ToString()));
            CreateMap<domain.Entities.DeliveryPerson, DeliveryPersonJson>();
            CreateMap<domain.Entities.DeliveryPersonNotification, ResponseGetOrderDeliveryPersonJson>()
                .ForMember(dest => dest.DeliveryPerson, opt => opt.MapFrom(src => src.User));
        }

    }
}
