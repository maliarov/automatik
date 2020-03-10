using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Automatik
{
    public static class WaitExtensions
    {
       public static WaitForElementFunction WaitAll(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IWebElement>, bool>> conditions
        ) =>
            (resolve, timeout) =>
            {
                var wait = new WebDriverWait(webDriver, timeout ?? webDriver.Manage().Timeouts().ImplicitWait);
                wait.Until((_) =>
                    conditions.All(condition => condition(resolve))
                );
            };

        public static WaitForElementFunction WaitAll(
            this IWebDriver webDriver,
            params Func<Func<IWebElement>, bool>[] conditions
        ) => webDriver.WaitAll((IEnumerable<Func<Func<IWebElement>, bool>>)conditions);



        public static WaitForElementsFunction WaitAll(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>> conditions
        ) =>
            (resolve, timeout) =>
            {
                var wait = new WebDriverWait(webDriver, timeout ?? webDriver.Manage().Timeouts().ImplicitWait);
                wait.Until((_) =>
                    conditions.All(condition => condition(resolve))
                );
            };

        public static WaitForElementsFunction WaitAll(
            this IWebDriver webDriver,
            params Func<Func<IEnumerable<IWebElement>>, bool>[] conditions
        ) => webDriver.WaitAll((IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>>)conditions);



        public static WaitForElementsFunction WaitAll(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>> elementsConditions,
            IEnumerable<Func<Func<IWebElement>, bool>> elementConditions
        ) =>
            (resolve, timeout) =>
            {
                var wait = new WebDriverWait(webDriver, timeout ?? webDriver.Manage().Timeouts().ImplicitWait);
                wait.Until((_) =>
                    elementsConditions.All(condition => condition(resolve)) &&
                    resolve().All(webElement => elementConditions.All(condition => condition(() => webElement)))
                );
            };





        public static WaitForElementFunction WaitAny(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IWebElement>, bool>> conditions
        ) =>
            (resolve, timeout) =>
            {
                var wait = new WebDriverWait(webDriver, timeout ?? webDriver.Manage().Timeouts().ImplicitWait);
                wait.Until((_) =>
                    conditions.Any(condition => condition(resolve))
                );
            };

        public static WaitForElementFunction WaitAny(
            this IWebDriver webDriver,
            params Func<Func<IWebElement>, bool>[] conditions
        ) => webDriver.WaitAny((IEnumerable<Func<Func<IWebElement>, bool>>)conditions);



        public static WaitForElementsFunction WaitAny(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>> conditions
        ) =>
            (resolve, timeout) =>
            {
                var wait = new WebDriverWait(webDriver, timeout ?? webDriver.Manage().Timeouts().ImplicitWait);
                wait.Until((_) =>
                    conditions.Any(condition => condition(resolve))
                );
            };

        public static WaitForElementsFunction WaitAny(
            this IWebDriver webDriver,
            params Func<Func<IEnumerable<IWebElement>>, bool>[] conditions
        ) => webDriver.WaitAny((IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>>)conditions);



        public static WaitForElementsFunction WaitAny(
            this IWebDriver webDriver,
            IEnumerable<Func<Func<IEnumerable<IWebElement>>, bool>> elementsConditions,
            IEnumerable<Func<Func<IWebElement>, bool>> elementConditions
        ) =>
            (resolve, timeout) =>
            {
                var wait = new WebDriverWait(webDriver, timeout ?? webDriver.Manage().Timeouts().ImplicitWait);
                wait.Until((_) =>
                    elementsConditions.Any(condition => condition(resolve)) ||
                    resolve().Any(webElement => elementConditions.Any(condition => condition(() => webElement)))
                );
            };
    }
}