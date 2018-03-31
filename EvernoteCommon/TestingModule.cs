using System;
using Autofac;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Configuration;

namespace EvernoteCommon
{
    public class TestingModule : Module
    {
        private string rootdir = AppDomain.CurrentDomain.BaseDirectory;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<SharedModule>();

            builder.Register(ctx =>
            {
                string driverdir = Path.Combine(rootdir, @"..\..\..\packages\Selenium.WebDriver.ChromeDriver.2.35.0\driver\win32");
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArgument("ignore-certificate-errors");
                options.AddArgument("--apps-keep-chrome-alive-in-tests");
                ChromeDriver driver = new ChromeDriver(driverdir, options);
                return driver;
            }).Named<IWebDriver>("Chrome")
              .InstancePerLifetimeScope();


            builder.Register(ctx =>
            {
                var BaseURL =  new Uri(ConfigurationManager.AppSettings["TestingApplicationUri"]);
                string driverdir = Path.Combine(rootdir, @"..\..\..\packages\Selenium.WebDriver.IEDriver.3.11.1\driver");
                InternetExplorerOptions options = new InternetExplorerOptions()
                {
                    InitialBrowserUrl = BaseURL.AbsoluteUri,
                    IgnoreZoomLevel = true,
                    EnsureCleanSession = true, 
                    EnablePersistentHover = true,
                    EnableNativeEvents = true,
                    RequireWindowFocus = true,
                    UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                    AcceptInsecureCertificates = true
                };
                InternetExplorerDriver driver = new InternetExplorerDriver(driverdir, options);
                return driver;
            }).Named<IWebDriver>("IE")
              .InstancePerLifetimeScope();




            builder.Register(ctx =>
            {
                string driverdir = Path.Combine(rootdir, @"..\..\..\packages\Selenium.WebDriver.GeckoDriver.0.19.0\driver\win32");
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(driverdir);
                service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

                var profile = new FirefoxProfile
                {
                    AcceptUntrustedCertificates = true,
                    AssumeUntrustedCertificateIssuer = true
                };

                var ffOptions = new FirefoxOptions
                {
                    LogLevel = FirefoxDriverLogLevel.Debug,
                    Profile = profile
                };

                ffOptions.BrowserExecutableLocation = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                ffOptions.SetPreference("browser.tabs.remote.autostart", false);
                ffOptions.SetPreference("browser.tabs.remote.autostart.1", false);
                ffOptions.SetPreference("browser.tabs.remote.autostart.2", false);

                var driver = new FirefoxDriver(service, ffOptions, TimeSpan.FromSeconds(5));
                return driver;
            }).Named<IWebDriver>("Firefox")
            .InstancePerLifetimeScope();


            builder.RegisterType<TestFrameworkConfiguration>()
            .As<ITestFrameworkConfiguration>()
            .SingleInstance();

            builder.RegisterType<TestingProfile>()
            .As<TestingProfile>()
            .InstancePerLifetimeScope();
        }
    }

}