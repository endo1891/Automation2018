
namespace EvernoteCommon.Configuration
{
    public interface IApplicationConfigurationSection
    {
        string EnvironmentType { get; }
        string Name { get; }
    }
}
