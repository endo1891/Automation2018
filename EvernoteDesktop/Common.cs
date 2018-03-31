using Autofac;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace EvernoteDesktop
{
    public class Common
    {
        private string rootdir = AppDomain.CurrentDomain.BaseDirectory;

        public static void FinalizeTest()
        {
            //var browser = NUnit.Framework.TestContext.Parameters.Get("driver", "Chrome");
            var browser = ConfigurationManager.AppSettings["driver"];
            ILifetimeScope lifetimeScope;

            if (ScenarioContext.Current.TryGetValue("TestScope", out lifetimeScope) && lifetimeScope != null)
            {
                object driver;
                if (lifetimeScope.TryResolveNamed(browser, typeof(IWebDriver), out driver))
                    ((IWebDriver)driver).TakeScreenshot();

                lifetimeScope.Dispose();
                ScenarioContext.Current["TestScope"] = null;
            }
        }

        public static IWebDriver WebDriver
        {
            get
            {
                //var browser = NUnit.Framework.TestContext.Parameters.Get("driver", "Chrome");
                var browser = ConfigurationManager.AppSettings["driver"];
                ILifetimeScope lifetimeScope;

                if (ScenarioContext.Current.TryGetValue("TestScope", out lifetimeScope) && lifetimeScope != null)
                {
                    object driver;
                    if (lifetimeScope.TryResolveNamed(browser, typeof(IWebDriver), out driver))
                        return (IWebDriver)driver;
                }
                return null;
            }
        }

        public static void ClearBrowserCache()
        {
            WebDriver.Manage().Cookies.DeleteAllCookies();
        }

        public void KillProcesses()
        {
            string projectdir = Path.GetFullPath(Path.Combine(rootdir, @"..\..\..\"));
            string processdir = Path.GetFullPath(Path.Combine(projectdir, @"EvernoteCommon"));
            string batchfile = "TERMINATEPROCESSES.BAT";
            string filepath = Path.GetFullPath(Path.Combine(processdir, batchfile));
            var processInfo = new ProcessStartInfo("cmd.exe", "/c" + filepath);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            var process = Process.Start(processInfo);
            process.WaitForExit();
            process.Close(); 
        }

        public static void SwitchWindow(IList<string> windows) {
            IList<string> currentWindows = WebDriver.WindowHandles;
            List<string> newWindows = (from o in currentWindows
                                        join p in windows on o equals p into t
                                        from od in t.DefaultIfEmpty()
                                        where od == null
                                        select o).ToList<string>();

            var newWindow = newWindows[0];
            if(string.IsNullOrEmpty(newWindow))
            {
                throw new Exception("Didnt find popup window within timeout");
            }

            WebDriver.SwitchTo().Window(newWindow);
            WebDriver.SwitchTo().Window(currentWindows[0]);

        }

    }
}
