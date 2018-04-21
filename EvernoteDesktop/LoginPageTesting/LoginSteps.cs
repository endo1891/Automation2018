using Autofac;
using EvernoteDesktop.PageObjectModel;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using EvernoteCommon;
using NUnit.Framework;

namespace EvernoteDesktop.LoginPageTesting
{
    [Binding]
    public class LoginSteps
    {
        private ILifetimeScope testScope;
        private readonly TestContext _context;

        private BaseDashboardPage dash;
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
            dash = testScope.Resolve<Func<TestContext, BaseDashboardPage>>()(_context);
            //do for rest of page objects
        }
        string BaseUrl = ConfigurationManager.AppSettings["TestingApplicationUri"];
        
        [Given(@"I enter login details")]
        public void GivenIEnterLoginDetails()
        {
            var username = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];
            login.Login(username, password);
            //login.ConfirmOnLoggedInScreen()
            //Assert?
            login.SetToLoggedIn(); //???
            Assert.IsTrue(_context.IsLoggedIn);
            ScenarioContext.Current["LoginPage"] = login; // SC.Current["DbPage"] = loggedInDash;
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

        [Then(@"I am not logged in")]
        public void ThenIAmNotLoggedIn()
        {
            Console.WriteLine("Not logged in");
        }
    }
}
