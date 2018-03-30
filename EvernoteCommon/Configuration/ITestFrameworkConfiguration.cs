using System;

namespace EvernoteCommon
{

    public interface ITestFrameworkConfiguration
    {
        Uri BaseUrl { get; set; }
        string WebDriverName { get; set; }

    }

}
