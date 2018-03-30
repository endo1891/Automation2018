using EvernoteCommon;
using NLog;
using System;

namespace EvernoteDesktop.PageObjectModel
{
    public class LoginPage
    {
        private readonly TestingProfile _testingProfile;
        private readonly TestContext _testContext;
        public ITestFrameworkConfiguration _config;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LoginPage(TestContext testContext, TestingProfile testProfile, ITestFrameworkConfiguration config)
        {
            _testingProfile = testProfile;
            _testContext = testContext;
            _config = config;

            Initialize();
        }

        public void Initialize()
        {
            Username = new WebElement(desktoplabels.Username, _testingProfile.WebDriver);
            LoginButton = new WebElement(desktoplabels.LoginButton, _testingProfile.WebDriver);
            Password = new WebElement(desktoplabels.Password, _testingProfile.WebDriver);
        }

        public WebElement Username { get; set; }
        public WebElement LoginButton { get; set; }
        public WebElement Password { get; set; }
    }
}
