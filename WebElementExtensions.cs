using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Automatik
{

    public static class WebElementExtensions
    {

        public static void ForceSendKeys(this IWebElement webElement, string text) 
        {
            webElement.Click();
            webElement.SendKeys(text);

            //if (webElement.)

            
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