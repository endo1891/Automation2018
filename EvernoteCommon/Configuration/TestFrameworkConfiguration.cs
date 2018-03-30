using System;
using System.Configuration;

namespace EvernoteCommon
{

    public class TestFrameworkConfiguration : ITestFrameworkConfiguration
    {
        public Uri BaseUrl { get; set; }
        public string WebDriverName { get; set; }

        public TestFrameworkConfiguration()
        {
            BaseUrl = new Uri(ConfigurationManager.AppSettings["TestingApplicationUri"]);
            WebDriverName = ConfigurationManager.AppSettings["driver"];
        }
    }

}
