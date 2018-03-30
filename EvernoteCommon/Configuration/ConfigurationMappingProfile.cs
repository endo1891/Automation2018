using AutoMapper;

namespace EvernoteCommon.Configuration
{
    public class ConfigurationMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<IApplicationConfigurationSection, ApplicationConfiguration>();
        }
    }
}
