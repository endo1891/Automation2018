using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvernoteCommon;

namespace EvernoteDesktop.PageObjectModel
{
    public abstract class BaseDashboardPage
    {
        private readonly TestingProfile _testingProfile;
        private readonly TestContext _testContext;
        public ITestFrameworkConfiguration _config;

        public BaseDashboardPage(TestContext testContext, TestingProfile testProfile, ITestFrameworkConfiguration testConfig)
        {
            _testingProfile = testProfile;
            _testContext = testContext;
            _config = testConfig;
        }

        public abstract void Navigate();

        public virtual void Initialize()
        {   
            SignInButton = new WebElement(desktoplabels.LoginButton, _testingProfile.WebDriver);
        }

        public WebElement SignInButton { get; set; }
    }
}
