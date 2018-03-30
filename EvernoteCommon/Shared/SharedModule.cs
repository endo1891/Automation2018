using Autofac;
using AutoMapper;
using EvernoteCommon.Shared.Configuration;
using EvernoteCommon.Shared.Extensions;
//using EvernoteCommon.Shared.Extensions;
//using EvernoteCommon.Shared.Logging;
//using EvernoteCommon.Shared.Security;
//using EvernoteCommon.Shared.Serialization;
//using EvernoteCommon.Shared.Text;
//using EvernoteCommon.Shared.Time;
//using EvernoteCommon.Shared.Validation;


namespace EvernoteCommon.Shared
{
    public class SharedModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            {
                builder.RegisterAutoMapper();
                builder.RegisterConfiguration(ThisAssembly);
                //builder.RegisterModule<LoggingModule>();
                //builder.RegisterModule<SerializationModule>();
                //builder.RegisterModule<SecurityModule>();
                //builder.RegisterModule<TextModule>();
                //builder.RegisterModule<TimeModule>();
                //builder.RegisterModule<ValidationModule>();
                //builder.RegisterType<GuidRef>().InstancePerDependency();
                builder.RegisterType<ConfigurationMappingProfile>().As<Profile>();

            }

        }
    }
}
