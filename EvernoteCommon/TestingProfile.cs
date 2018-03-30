using System;
using Autofac;
using OpenQA.Selenium;
using EvernoteCommon.Configuration;

namespace EvernoteCommon
{

    public interface ITestingProfile
    {
        ITestFrameworkConfiguration TestFrameworkConfiguration { get; }
        IApplicationConfiguration ApplicationConfiguration { get; }
        ILifetimeScope LifetimeScope { get; }
        IWebDriver WebDriver { get; }
    }

    public class TestingProfile : ITestingProfile, IDisposable
    {
        public TestingProfile(ILifetimeScope lifetimeScope)
        {
            ApplicationConfiguration = lifetimeScope.Resolve<IApplicationConfiguration>();
            TestFrameworkConfiguration = lifetimeScope.Resolve<ITestFrameworkConfiguration>();
            LifetimeScope = lifetimeScope;
            WebDriver = lifetimeScope.ResolveNamed<IWebDriver>(TestFrameworkConfiguration.WebDriverName);
        }

        public ITestFrameworkConfiguration TestFrameworkConfiguration { get; private set; }
        public IApplicationConfiguration ApplicationConfiguration { get; private set; }
        public ILifetimeScope LifetimeScope { get; private set; }
        public IWebDriver WebDriver { get; private set; }

        

        public void Dispose()
        {
            if (WebDriver != null)
            {
                WebDriver.Close();
                WebDriver.Quit();
            }
        }

    }
}

