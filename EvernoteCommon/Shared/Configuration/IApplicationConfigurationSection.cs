
namespace EvernoteCommon.Shared.Configuration
{
    public interface IApplicationConfigurationSection
    {
        string EnvironmentType { get; }
        string Name { get; }
    }
}
