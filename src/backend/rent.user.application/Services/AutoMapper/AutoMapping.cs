using AutoMapper;
using rent.user.communication.Requests;
using rent.user.communication.Responses;

namespace rent.user.application.Services.AutoMapper
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            ResquestToDomain();
            DomainToResponse();
        }

        private void ResquestToDomain()
        {
            CreateMap<RequestRegisterUserJson, rent.user.domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }

        private void DomainToResponse()
        {
            CreateMap<rent.user.domain.Entities.User, ResponseUserProfileJson>();
        }

    }
}
