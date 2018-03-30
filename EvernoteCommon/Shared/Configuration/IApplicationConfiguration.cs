using System;

namespace EvernoteCommon.Shared.Configuration
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
