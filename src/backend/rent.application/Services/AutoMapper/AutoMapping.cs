using AutoMapper;
using rent.communication.Requests;
using rent.communication.Responses;

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
            CreateMap<rent.domain.Entities.User, ResponseUserProfileJson>();
            CreateMap<rent.domain.Entities.Motorcycle, ResponseGetMotocycle>()
                .ForMember(dest => dest._Id, opt => opt.MapFrom(src => src._id.ToString()));
        }

    }
}
