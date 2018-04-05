using System;
using Autofac;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace EvernoteCommon
{
    public class TestingModule : Module
    {
        private string rootdir = AppDomain.CurrentDomain.BaseDirectory;

        protected override void Load(ContainerBuilder builder /*string profile,string environment*/)
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


            builder.Register(ctx =>
            {   //profile and environment coming from variables passed in. this code is on browserstack site:
                //https://www.browserstack.com/automate/specflow

                NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" /* + profile*/) as NameValueCollection;
                NameValueCollection settings = ConfigurationManager.GetSection("environments/" /* + environment*/) as NameValueCollection;

                DesiredCapabilities capability = new DesiredCapabilities();

                foreach (string key in caps.AllKeys)
                {
                    capability.SetCapability(key, caps[key]);
                }

                foreach (string key in settings.AllKeys)
                {
                    capability.SetCapability(key, settings[key]);
                }

                String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
                if (username == null)
                {
                    username = ConfigurationManager.AppSettings.Get("user");
                }

                String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
                if (accesskey == null)
                {
                    accesskey = ConfigurationManager.AppSettings.Get("key");
                }

                capability.SetCapability("browserstack.user", username);
                capability.SetCapability("browserstack.key", accesskey);

                /*File.AppendAllText("C:\\Users\\Admin\\Desktop\\sf.log", "Starting local");

                if (capability.GetCapability("browserstack.local") != null && capability.GetCapability("browserstack.local").ToString() == "true")
                {
                    browserStackLocal = new Local();
                    List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
        new KeyValuePair<string, string>("key", accesskey)
      };
                    browserStackLocal.start(bsLocalArgs);
                }

                File.AppendAllText("C:\\Users\\Admin\\Desktop\\sf.log", "Starting driver");*/
                var driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), capability, TimeSpan.FromSeconds(600));
                return driver;

            }).Named<IWebDriver>("RemoteWebDriver")
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