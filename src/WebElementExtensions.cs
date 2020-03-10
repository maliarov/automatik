using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Automatik
{

    public static class WebElementExtensions
    {

        public static void SendKeysForce(this IWebElement webElement, string text)
        {
            webElement.SendKeys(text);

            if (!new [] {"text", "password", "number", "hidden" }.Contains(webElement.GetAttribute("type")))
                return;

            var value = webElement.GetAttribute("value");
            if (value == text)
                return;

            var webDriver = webElement.GetWebDriver();
            var timeout = webDriver.Manage().Timeouts().ImplicitWait;

            var wait = new WebDriverWait(webDriver, timeout);

            wait.IgnoreExceptionTypes(typeof(ArgumentException));

            wait.Until((webDriver) =>
                {
                    webElement.Clear();
                    
                    foreach (var ch in text)
                       webElement.SendKeys(ch.ToString());

                    var value = webElement.GetAttribute("value");
                    if (value != text)
                        throw new ArgumentException();

                    return true;
                });
        }

        public static IWebDriver GetWebDriver(this IWebElement webElement)
        {
            var elementType = webElement.GetType();
            
            if (elementType == typeof(RemoteWebElement))
                return ((RemoteWebElement)webElement).WrappedDriver;
            
            if (elementType.BaseType == typeof(ResolverDecorator<IWebElement>))
                return ((RemoteWebElement)((ResolverDecorator<IWebElement>)(webElement)).Value).WrappedDriver;

            throw new Exception("Unsupported web element type.");
        }

        public static string[] GetCssClasses(this IWebElement webElement)
        {   

            var classAttrValue = webElement.GetAttribute("class");
            
            if (string.IsNullOrWhiteSpace(classAttrValue))
                return new string[0];

            return classAttrValue.Trim().Split(" ").ToArray();
        }

        public static string[] GetAttributesNames(this IWebElement webElement)
        {
            var driver = (RemoteWebDriver)webElement.GetWebDriver();

            if (webElement is ResolverDecorator<IWebElement> resolverDecorator)
                webElement = resolverDecorator.Value;

            var jsArray = (ReadOnlyCollection<object>)driver.ExecuteScript(@"
                var attributes = arguments[0].attributes;
                var attributesNames = [];

                for (var index = 0; index < attributes.length; index++) {
                    attributesNames.push(attributes[index].nodeName);
                }
                
                return attributesNames;
            ", webElement);

            return jsArray
                .Select(item => (string)item)
                .ToArray();
        }

        public static IDictionary<string, string> GetAttributes(this IWebElement webElement)
        {
            var driver = (RemoteWebDriver)webElement.GetWebDriver();

            if (webElement is ResolverDecorator<IWebElement> resolverDecorator)
                webElement = resolverDecorator.Value;

            var jsArray = (ReadOnlyCollection<object>)driver.ExecuteScript(@"
                var attributes = arguments[0].attributes;
                var attributesPairs = [];

                for (var index = 0; index < attributes.length; index++) {
                    attributesPairs.push([attributes[index].nodeName, attributes[index].value]);
                }
                
                return attributesPairs;
            ", webElement);

            return jsArray
                .Select(item => (ReadOnlyCollection<object>)item)
                .ToDictionary(item => (string)item.First(), item => (string)item.Last());
        }
    }


}