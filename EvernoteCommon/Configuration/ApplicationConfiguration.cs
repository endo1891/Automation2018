using System;
using AutoMapper;

namespace EvernoteCommon.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public ApplicationConfiguration(IMappingEngine mappingEngine, IApplicationConfigurationSection config)
        {
            mappingEngine.Map(config, this);
            InstanceId = Guid.NewGuid();
            DateStarted = DateTimeOffset.UtcNow;
        }

        public string MachineName
        {
            get {
                return Environment.MachineName;
            }
        }

        public DateTimeOffset DateStarted
        {
            get;
            private set;
        }

        public string EnvironmentType
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public Guid InstanceId
        {
            get; private set;
        }
    }
}
