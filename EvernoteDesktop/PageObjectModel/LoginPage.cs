using EvernoteCommon;
using NLog;
using NUnit.Framework;
using System;

namespace EvernoteDesktop.PageObjectModel
{
    public class LoginPage : BaseDashboardPage
    {
        private readonly TestingProfile _testingProfile;
        private readonly TestContext _testContext;
        public ITestFrameworkConfiguration _config;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LoginPage(TestContext testContext, TestingProfile testProfile, ITestFrameworkConfiguration config) : base(testContext, testProfile, config)
        {
            _testingProfile = testProfile;
            _testContext = testContext;
            _config = config;

            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            Username = new WebElement(desktoplabels.Username, _testingProfile.WebDriver);
            LoginButton = new WebElement(desktoplabels.LoginButton, _testingProfile.WebDriver);
            Password = new WebElement(desktoplabels.Password, _testingProfile.WebDriver);
            LoginErrorMessage = new WebElement(desktoplabels.LoginErrorMessage, _testingProfile.WebDriver);
            Logo = new WebElement(desktoplabels.Logo, _testingProfile.WebDriver);
        }

        public WebElement Username { get; set; }
        public WebElement LoginButton { get; set; }
        public WebElement Password { get; set; }
        public WebElement LoginErrorMessage { get; set; }
        public WebElement Logo { get; set; }

        public override void Navigate()
        {
            _testingProfile.WebDriver.Navigate().GoToUrl(_config.BaseUrl);
        }

        public void Login(string username, string password)
        {
            Username.WaitForVisibility();
            Username.SendKeys(username);
            LoginButton.Click();
            Password.WaitForVisibility();
            Password.SendKeys(password);

            var errorMsgExists = LoginErrorMessage.Exists();
            if (errorMsgExists)
            {
                _testContext.CurrentUsername = null;
                _testContext.CurrentPassword = null;
                _testContext.IsLoggedIn = false;
            }
        }

        public void SetToLoggedIn()
        {
            _testContext.SetLoggedIn();
        }

        public void AutoLoginFirstStep()
        {
            _testingProfile.WebDriver.Navigate().GoToUrl(_config.BaseUrl);
            ConfirmRedirectToLoginPage();
        }

        private void ConfirmRedirectToLoginPage()
        {
            //Logo.WaitForVisibility();
            if (!Logo.IsDisplayed(new TimeSpan(0, 0, 30)))
            {
                throw new InvalidOperationException("navigating to login page failed");
            }
            var currentUrl = new Common().WebDriver.Url;
            var url = _config.BaseUrl.ToString();
            _logger.Info("****************LOGGING INFO***************");
            Assert.IsTrue(currentUrl.Contains(url));
        }
    }
}
