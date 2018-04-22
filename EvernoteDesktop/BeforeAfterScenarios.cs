using Autofac;
using System.Configuration;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using EvernoteDesktop.PageObjectModel;
using System;
using EvernoteCommon;
using System.Reflection;

namespace EvernoteDesktop
{
    [Binding]
    public sealed class BeforeAfterScenarios
    {
        private ILifetimeScope _testScope;
        private TestContext _context;
        public ITestFrameworkConfiguration _config;
        private LoginPage loginPage;
        private BaseDashboardPage dash;

        public BeforeAfterScenarios() {

        }

        public BeforeAfterScenarios(TestContext context, TestFrameworkConfiguration config) {
            _context = context;
            _config = config;
        }

        [BeforeScenario(Order = 1)]
        public void SetupScope() {
            if (!ScenarioContext.Current.TryGetValue("TestScope", out _testScope))
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<DesktopModule>();

                var container = builder.Build();
                _testScope = container.BeginLifetimeScope();

                //var browser = ConfigurationManager.AppSettings["driver"];
                var browser = NUnit.Framework.TestContext.Parameters.Get("driver", "Chrome");
                switch (browser)
                {
                    case "Chrome":
                        _testScope.ResolveNamed<IWebDriver>("Chrome").Navigate().Refresh();
                        break;
                    case "Firefox":
                        var driver = _testScope.ResolveNamed<IWebDriver>("Firefox");
                        driver.Manage().Window.Maximize();
                        driver.Navigate().Refresh();
                        break;
                    case "IE":
                        driver = _testScope.ResolveNamed<IWebDriver>("IE");
                        driver.Manage().Window.Maximize();
                        driver.Navigate().Refresh();
                        break;
                }
                ScenarioContext.Current["TestScope"] = _testScope;
            }
        }

        [BeforeScenario(Order = 2)]
        public void SetupPageObjects()
        {
            loginPage = _testScope.Resolve<Func<TestContext, LoginPage>>()(_context);
            //dash = _testScope.Resolve<Func<TestContext, BaseDashboardPage>>()(_context);
        }

        [AfterScenario]
        public void Teardown()
        {
            Common.FinalizeTest();
        }






    }
}
