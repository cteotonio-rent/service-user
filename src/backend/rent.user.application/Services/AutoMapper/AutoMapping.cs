using AutoMapper;
using rent.user.communication.Requests;

namespace rent.user.application.Services.AutoMapper
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<RequestRegisterUserJson, rent.user.domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

        }

        private void ResquestToDomain()
        {
            
        }

    }
}
