using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NLog;
using OpenQA.Selenium.Remote;

namespace EvernoteCommon
{
    public class WebElement
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public WebElement(string xPath, IWebDriver webDriver)
        {
            XPath = xPath;
            WebDriver = webDriver;
        }

        public WebElement(string xPath, Func<WebElementContext, bool> predicate, IWebDriver webDriver)
        {
            XPath = xPath;
            WebDriver = webDriver;
            Predicate = predicate;
        }

        public IWebDriver WebDriver
        {
            get; set;
        }

        public string XPath
        {
            get; set;
        }

        public Func<WebElementContext, bool> Predicate
        {
            get; private set;
        }

        private IWebElement GetElement(TimeSpan? timeout = null)
        {
            IWebElement elem = null;
            var WebDriverWait = new WebDriverWait(WebDriver, timeout ?? TimeSpan.FromSeconds(5));

            try
            {
                if (Predicate == null)
                {
                    elem = WebDriverWait.Until(ExpectedConditions.ElementExists(By.XPath(XPath)));
                }
                else
                {
                    var elements = WebDriverWait.Until(dr => dr.FindElements(By.XPath(XPath)));
                    foreach (var el in elements)
                    {
                        var elementContext = new WebElementContext { Element = el };
                        if (Predicate(elementContext))
                        {
                            elem = elementContext.Element;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Could not get element");
            }
            return elem;
        }

        public string GetAttribute(string attribute, TimeSpan? timeout = null)
        {
            var e = GetElement(timeout);
            return e.GetAttribute(attribute);
        }

        public string GetImageSource(TimeSpan? timeout = null)
        {
            var e = GetElement(timeout);
            return e.GetAttribute("src");
        }

        public bool PickDropdownItem(string item, TimeSpan? timeout = null)

        {
            IWebElement e = null;

            var wait = new WebDriverWait(WebDriver, timeout ?? new TimeSpan(0, 0, 5));
            e = wait.Until(dr => dr.FindElement(By.XPath(XPath + @"/option[text()='" + item + "']")));
            e.Click();
            return true;
        }

        public bool Click(TimeSpan? timeout = null)
        {
            WaitForVisibility();
            //Sleep.HalfSecond();
            IWebElement elem = null;

            var wait = new WebDriverWait(WebDriver, timeout ?? new TimeSpan(0, 0, 10));

            if (Predicate != null)
            {
                var elements = wait.Until(dr => dr.FindElements(By.XPath(XPath)));
                foreach (var el in elements)

                {
                    var cl = new WebElementContext { Element = el };

                    if (Predicate(cl))
                    {
                        elem = cl.Element;
                        break;
                    }

                }

            }
            else
            {
                elem = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPath)));
            }
            if (elem != null && elem.Enabled)

            {
                
                try
                {
                    elem.Click();
                }
                catch (Exception)
                {
                    ((IJavaScriptExecutor)WebDriver).ExecuteScript("window.ScrollTo(0" + elem.Location.Y + ")");
                    //Sleep.HalfSecond();

                }
                return true;
            }
            return false;
        }

        public void TriggerMouseDownEvent()
        {
            //CreateFireMouseEventJSMethod();
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("window.FireMouseEvents('" + XPath + "',['mousedown']);");
        }

        public bool SendKeys(string keys, TimeSpan? timeout = null)
        {
            _logger.Info($"Sending Keys:{keys}");

            var elem = GetElement(timeout);
            var currentRetryIndex = 0;
            const int maxRetryCount = 3;

            while (currentRetryIndex <= maxRetryCount)
            {
                elem.Clear();
                foreach (var key in keys)
                {
                    elem.SendKeys(key.ToString());
                }

                var enteredText = elem.Text;

                if (keys.Length != 0 && string.IsNullOrEmpty(enteredText))


                {
                    enteredText = elem.GetAttribute("value");
                }
                var enteredTextSuccessfully = enteredText.Equals(keys);
                if (enteredTextSuccessfully) break;
                currentRetryIndex++;
            }
            return true;
        }

        public bool IsDisplayed(TimeSpan? timeout = null)
        {
            Sleep.HalfSecond();
            var e = GetElement(timeout);
            return e != null && e.Displayed;
        }

        public bool IsEnabled(TimeSpan? timeout = null)
        {
            Sleep.HalfSecond();
            var e = GetElement(timeout);
            return e != null && e.Enabled;
        }

        public string GetInnerText(TimeSpan? timeout = null)
        {
            var e = GetElement(timeout);
            return e.Text;
        }

        public void ClearField(TimeSpan? timeout = null)
        {
            var e = GetElement(timeout);
            e.Clear();
        }

        public bool Exists(TimeSpan? timeout = null)
        {
            var wait = new WebDriverWait(WebDriver, timeout ?? new TimeSpan(0, 0, 15));

            if (Predicate == null)
            {
                try
                {
                    var foundElement = wait.Until(dr =>
                    {

                        try
                        {
                            return dr.FindElement(By.XPath(XPath));
                        }
                        catch
                        {
                            return null;
                        }

                    });

                    var tagName = foundElement?.TagName;
                    Console.WriteLine(tagName);
                    return !string.IsNullOrWhiteSpace(tagName);
                }
                catch
                {
                    return false;
                }

            }

            var elements = wait.Until(dr => dr.FindElements(By.XPath(XPath)));
            foreach (var el in elements)
            {
                var elementContext = new WebElementContext { Element = el };
                if (Predicate(elementContext))
                {
                    return true;

                }
            }
            return false;
        }

        public void WaitForVisibility(TimeSpan? timeout = null)
        {
            var wait = new WebDriverWait(WebDriver, timeout ?? new TimeSpan(0, 0, 15));
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(XPath)));
            }
            catch (ElementNotVisibleException envex)
            {
                _logger.Error(envex, "Element not visible");
            }

        }

        public void WaitForInvisibility(TimeSpan? timeout = null)
        {
            var wait = new WebDriverWait(WebDriver, timeout ?? new TimeSpan(0, 0, 15));
            try
            {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(XPath)));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Element is still visible");
            }

        }

        public void SelectCheckbox(TimeSpan? timeout = null)
        {
            IWebElement e = null;
            var wait = new WebDriverWait(WebDriver, timeout ?? new TimeSpan(0, 0, 15));
            e = wait.Until(dr => dr.FindElement(By.XPath(XPath)));

            if (((RemoteWebDriver)WebDriver).Capabilities.BrowserName == "Firefox")
            {
                //firefox
                e.Click();
            }

            else
            {
                //chrome and IE
                e.SendKeys(Keys.Space);
            }


        }
    }
}
