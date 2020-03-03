using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Automatik
{

    public static class WebElementExtensions
    {

        public static void SendKeysForce(this IWebElement webElement, string text)
        {
            webElement.Click();
            webElement.SendKeys(text);

            if (webElement.GetAttribute("value") == text)
                return;

            var webDriver = webElement.GetWebDriver();
            var timeout = webElement.GetWebDriver().Manage().Timeouts().ImplicitWait;

            var wait = new WebDriverWait(webDriver, timeout);

            wait.IgnoreExceptionTypes(typeof(ArgumentException));

            wait.Until((webDriver) =>
                {
                    webElement.Clear();
                    
                    foreach (var ch in text)
                       webElement.SendKeys(ch.ToString());

                    if (webElement.GetAttribute("value") != text)
                        throw new ArgumentException();

                    return true;
                });
        }

        public static IWebDriver GetWebDriver(this IWebElement webElement)
        {
            var element = (RemoteWebElement)webElement;

            return element.WrappedDriver;
        }

        public static object GetAttribute(this IWebElement webElement, string name)
        {
            var driver = (RemoteWebDriver)webElement.GetWebDriver();

            return driver.ExecuteScript("arguments[0].getAttribute(arguments[1])", webElement, name);
        }

        public static void SetAttribute(this IWebElement webElement, string name, string value)
        {
            var driver = (RemoteWebDriver)webElement.GetWebDriver();

            driver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", webElement, name, value);
        }


    }


}