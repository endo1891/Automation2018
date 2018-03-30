using System.Configuration;

namespace EvernoteCommon.Shared.Configuration
{
    internal class ApplicationConfigurationSection : ConfigurationSection, IApplicationConfigurationSection
    {
        
        [ConfigurationProperty("environmentType", IsKey =false, IsRequired = false,DefaultValue = "Debug")]

        public string EnvironmentType
        {
            get {
                return base["environmentType"] as string;
            }
        }

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]

        public string Name
        {
            get
            {
                return base["name"] as string;
            }
        }

    }
}
