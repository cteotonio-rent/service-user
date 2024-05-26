using AutoMapper;
using rent.user.application.Services.AutoMapper;

namespace CommomTestUtilities.Mapper
{
    public class MapperBuilder
    {
        public static IMapper Build()
        {
            return new MapperConfiguration(option =>
            {
                option.AddProfile(new AutoMapping());
            }).CreateMapper();
        }
    }
}
