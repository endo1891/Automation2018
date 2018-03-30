using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Reflection;
using TechTalk.SpecFlow;
using System.IO;

namespace EvernoteCommon
{

    public static class WebdriverExtensions
    {
        public static IWebElement GetElement(this IWebDriver driver, String byXpath, TimeSpan? timeout = null)
        {
            var wait = new WebDriverWait(driver, timeout ?? new TimeSpan(0, 0, 15));
            return wait.Until(dr => dr.FindElement(By.XPath(byXpath)));
        }

        public static IWebElement ClickElem(this IWebDriver driver, String byXpath, TimeSpan? timeout = null)
        {
            var e = driver.GetElement(byXpath, timeout);
            e.Click();
            return e;
        }

        public static IWebElement SendKeys(this IWebDriver driver, String byXpath, string Keys, TimeSpan? timeout = null)
        {
            var e = driver.GetElement(byXpath);
            e.SendKeys(Keys);
            return e;
        }

        public static void TakeScreenshot(this IWebDriver driver)
        {
            try
            {
                var filenameBase = $"error - {FeatureContext.Current.FeatureInfo.Title}_{ScenarioContext.Current.ScenarioInfo.Title}_{DateTime.Now:yyyymmdd_hhmmss}";

                var artefactDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "testresults");

                if (!Directory.Exists(artefactDirectory))
                    Directory.CreateDirectory(artefactDirectory);

                driver.Manage().Window.Maximize();
                var takesScreenshot = driver as ITakesScreenshot;
                if (takesScreenshot != null)
                {

                    var screenshot = takesScreenshot.GetScreenshot();
                    var ssFilePath = Path.Combine(artefactDirectory, filenameBase + "Screenshot.jpg");
                    screenshot.SaveAsFile(ssFilePath, ScreenshotImageFormat.Jpeg);
                    Console.WriteLine("Screenshot:{0}", new Uri(ssFilePath));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: { 0}", ex);
            }



        }
    }
}
