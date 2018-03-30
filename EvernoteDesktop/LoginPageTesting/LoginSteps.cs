using Autofac;
using EvernoteDesktop.PageObjectModel;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using EvernoteCommon;

namespace EvernoteDesktop.LoginPageTesting
{
    [Binding]
    public class LoginSteps
    {
        private ILifetimeScope testScope;
        private readonly TestContext _context;

        private LoginPage login;

        public LoginSteps()
        {

        }

        public LoginSteps(TestContext context)
        {
            _context = context;
        }

        [BeforeScenario]
        public void SetupScenario()
        {
            if (!ScenarioContext.Current.TryGetValue("TestScope", out testScope))
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<DesktopModule>();
                var container = builder.Build();
                testScope = container.BeginLifetimeScope();
                ScenarioContext.Current["TestScope"] = testScope;
            }
        }

        [BeforeStep]
        public void BeforeStep()
        {
            login = testScope.Resolve<Func<TestContext, LoginPage>>()(_context);
            //do for rest of page objects
        }
        string BaseUrl = ConfigurationManager.AppSettings["TestingApplicationUri"];

        [Given(@"I am on login page")]
        public void GivenIAmOnLoginPage()
        {
            Common.WebDriver.Navigate().GoToUrl(BaseUrl);
        }

        [Given(@"I enter login details")]
        public void GivenIEnterLoginDetails()
        {
            login.Username.SendKeys("endacarroll02@yahoo.ie");
            login.LoginButton.Click();
            login.Password.WaitForVisibility();
            login.Password.SendKeys("dutchgold1");
        }

        [When(@"I click submit")]
        public void WhenIClickSubmit()
        {
            login.LoginButton.Click();
        }

        [Then(@"I am logged in")]
        public void ThenIAmLoggedIn()
        {
            Console.WriteLine("logged in");
        }
    }
}
