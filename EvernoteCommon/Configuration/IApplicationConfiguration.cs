using System;

namespace EvernoteCommon.Configuration
{
    public interface IApplicationConfiguration
    {
        string MachineName { get; }
        string EnvironmentType { get; }
        string Name { get; }
        Guid InstanceId { get; }
        DateTimeOffset DateStarted { get; }
    }
}
