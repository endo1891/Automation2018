using AutoMapper;

namespace EvernoteCommon.Shared.Configuration
{
    public class ConfigurationMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<IApplicationConfigurationSection, ApplicationConfiguration>();
        }
    }
}
